using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using todo.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace todo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services�д����һ����󣬰���DbContext����Controller�����,��������ע��

            services.AddDbContextPool<OrderContext>(options => options
               // �����ַ���"todoDatabase" ������appsetting.json������
               .UseMySql(Configuration.GetConnectionString("OrderDatabase"),
                   mySqlOptions => mySqlOptions
                   .ServerVersion(new Version(5, 7, 30), ServerType.MySql)
           ));
            services.AddControllers(); //�������������󣬴���ʱ��������ע��
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { //�����Ļ�������ΪDevelopment
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles(); //����ȱʡ��̬�ļ���index.html��index.htm��
            app.UseStaticFiles(); //������̬�ļ���ҳ�桢js��ͼƬ�ȸ���ǰ���ļ���

            app.UseHttpsRedirection(); //����http��https���ض���
            app.UseRouting();  //��·�������ӵ�app��
            app.UseAuthorization(); //����Ȩ�����ӵ�app��
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers(); //����ӳ�䣬��url·�ɵ�������
            });
        }
    }
}
