﻿namespace Sitecore.Feature.News.Controllers
{
    using System.Web.Mvc;
    using Sitecore.Feature.News.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Mvc.Presentation;
    using Models;
    using Glass.Mapper.Sc.Web.Mvc;
    using Glass.Mapper.Sc;

    public class NewsController : GlassController
    {
        private readonly INewsRepositoryFactory newsRepositoryFactory;

        public NewsController() : this(new NewsRepositoryFactory())
        {
        }

        public NewsController(INewsRepositoryFactory newsRepositoryFactory)
        {
            this.newsRepositoryFactory = newsRepositoryFactory;
        }

        public ActionResult NewsList()
        {
            ISitecoreContext context = new SitecoreContext();
            //var repo = newsRepositoryFactory.Create(RenderingContext.Current.ContextItem);

            var model = context.QueryRelative<INewsArticle>(".//*[@@templatename='News Article']");

            return View("NewsList", model);
        }

        public ActionResult LatestNews()
        {
            //TODO: change to parameter template
            var count = RenderingContext.Current.Rendering.GetIntegerParameter("count", 5);
            var items = this.newsRepositoryFactory.Create(RenderingContext.Current.Rendering.Item).GetLatestNews(count);
            return this.View("LatestNews", items);
        }
    }
}