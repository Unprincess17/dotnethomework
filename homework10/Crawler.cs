using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCrawler
{
    class Crawler
    {
        public event Action<Crawler> CrawlerStopped;
        public event Action<Crawler, string, string> PageDownloaded;
        public string urltype { get; set; }
        public int count { get; set; }
        private int times = 0;

        private ConcurrentDictionary<string, bool> UrlDict = new ConcurrentDictionary<string, bool>();
        private ConcurrentQueue<string> urls = new ConcurrentQueue<string>();
        

        //加入初始页面
        public Crawler(string starturl, string urltype, int count)
        {
            this.urltype = urltype;
            this.UrlDict.GetOrAdd(starturl, false);
            urls.Enqueue(starturl);
            this.count = count;
            new Thread(this.Crawl).Start();
            //Task task = Task.Run(Crawl);
        }

        //
        private void Crawl()
        {
            while (true)
            {
                string current = urls.First<string>();
                if (current == null || times > count) break;//当urls队列为空
                Console.WriteLine("爬行" + current + "页面!");
                //一个新的任务
                string html = null;
                var taskp = new Task(() => {
                    html = DownLoad(current);
                    UrlDict[current] = true;
                    times++;
                });
                Task taskc = taskp.ContinueWith((T) =>
                {
                    Parse(html, current);
                });//解析,并加入新的链接
                taskp.Start();
            }
            Console.WriteLine("{current}结束完成");
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                PageDownloaded(this,url, $"Error:{ex.Message}");
                return "";
            }
        }


        private void Parse(string html, string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (!IsWantedType(strRef)) continue;//处理尾部
                strRef = Ref2Direct(strRef, current);//处理头部
                if (!IsWantedRoot(strRef)) continue;//处理头部
                if (!UrlDict.ContainsKey(strRef))
                {
                    UrlDict.GetOrAdd(strRef, false);
                    urls.Enqueue(strRef);
                }//加入新的链接
                ////爬取新页面 ->DFS
                //string next = DownLoad(strRef);
                //Parse(next, strRef);
                return;
            }
        }

        //判断结尾是否为.jsp/或.jsp<-html/htm/aspx/php/jsp
        private bool IsWantedType(string strRef)
        {
            string strFormat = @"(\.jsp/$|\.jsp$|\.php/$|\.php$|\.aspx/$|\.aspx$|\.htm/$|\.htm$|\.html/$|\.html$)";
            return Regex.IsMatch(strRef, strFormat);

        }


        //相对路径->绝对路径
        private string Ref2Direct(string refurl, string currurl)
        {
            //if start with [a-z] | ./[a-z] | ../[a-z] | /[a-z] => "currurl" +":" + refurl" 
            string strref = @"(^[a-z]|^\./[a-z]|^\.\./[a-z]|^/[a-z])";
            if (!Regex.IsMatch(refurl, strref) || Regex.IsMatch(refurl, "^https?://"))
            {
                //不符合相对路径条件，返回原路径
                return refurl;
            }

            return currurl + refurl;
        }

        //是否为同一个主页下的页面
        private bool IsWantedRoot(string url)
        {
            string SpecType = "^" + urltype;//parse pages from special domain
            if (Regex.IsMatch(url, urltype))
            {
                return true;
            }
            return false;
        }
    }
}
