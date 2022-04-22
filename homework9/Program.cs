using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCrawler
{
    class SimpleCrawler
    {
        private Hashtable urls = new Hashtable();
        private string urltype;
        private int count = 0;
        static void Main(string[] args)
        {
            SimpleCrawler myCrawler = new SimpleCrawler();
            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];
            myCrawler.urls.Add(startUrl, false);//加入初始页面
            myCrawler.urltype = "http://www.cnblogs.com/";
            new Thread(myCrawler.Crawl).Start();
        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }

                if (current == null || count > 10) break;
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                urls[current] = true;
                count++;
                Parse(html, current);//解析,并加入新的链接
                Console.WriteLine("爬行结束");
            }
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
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        // Push to urls
        //
        // 1.specify url type √
        // 2.undirect to direct √
        // 3.parse html/htm/aspx/php/jsp only
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
                if (urls[strRef] == null) urls[strRef] = false; //加入新的链接

                //爬取新页面 ->DFS
                string next = DownLoad(strRef);
                Parse(next, strRef);
                
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
            
            return  currurl + refurl;
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
