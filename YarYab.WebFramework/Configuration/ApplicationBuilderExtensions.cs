//using ElmahCore.Mvc;
using YarYab.Common;
using YarYab.Common.Utilities;
using YarYab.Data;
using YarYab.Service.DataInitializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.WebFramework.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHsts(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            Assert.NotNull(app, nameof(app));
            Assert.NotNull(env, nameof(env));

            if (!env.IsDevelopment())
                app.UseHsts();

            return app;
        }

        public static IApplicationBuilder IntializeDatabase(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));

            //Use C# 8 using variables
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<YarYabContext>(); //Service locator

            //Dose not use Migrations, just Create Database with latest changes
            //dbContext.Database.EnsureCreated();
            //Applies any pending migrations for the context to the database like (Update-Database)
            dbContext.Database.Migrate();

            var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
            foreach (var dataInitializer in dataInitializers)
                dataInitializer.InitializeData();

            return app;
        }

        //public static IApplicationBuilder UseElmahCore(this IApplicationBuilder app, SiteSettings siteSettings)
        //{
        //    Assert.NotNull(app, nameof(app));
        //    Assert.NotNull(siteSettings, nameof(siteSettings));

        //    app.UseWhen(context => context.Request.Path.StartsWithSegments(siteSettings.ElmahPath, StringComparison.OrdinalIgnoreCase), appBuilder =>
        //    {
        //        appBuilder.Use((ctx, next) =>
        //        {
        //            ctx.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO = true;
        //            return next();
        //        });
        //    });
        //    app.UseElmah();

        //    return app;
        //}

    }
}
