namespace DemoRobotTXTsitemapXML.Controllers
{
    using DemoRobotTXTsitemapXML.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml;

    public class SitemapController : Controller
    {
        //
        // GET: /Sitemap/
        public ActionResult Index()
        {
            var sitemapItems = new List<SitemapHelper.SitemapItem>
        {
            new SitemapHelper.SitemapItem(Url.FullActionLink ("index", "home"), changeFrequency: SitemapHelper.SitemapChangeFrequency.Always, priority: 1.0),
            new SitemapHelper.SitemapItem(Url.FullActionLink("about", "home"), lastModified: DateTime.Now, changeFrequency: SitemapHelper.SitemapChangeFrequency.Always, priority:0.7),
            new SitemapHelper.SitemapItem(Url.FullActionLink("Contact", "home"), lastModified: DateTime.Now,changeFrequency: SitemapHelper.SitemapChangeFrequency.Monthly, priority:0.2),
       };

            return new SitemapResult(sitemapItems);
        }

        public class SitemapResult : ActionResult
        {
            private readonly IEnumerable<SitemapHelper.ISitemapItem> siteMapItems;
            private readonly SitemapHelper.ISitemapGenerator siteMapGenerator;

            public SitemapResult(IEnumerable<SitemapHelper.ISitemapItem> items)
                : this(items, new SitemapHelper.SitemapGenerator())
            {
            }

            public SitemapResult(IEnumerable<SitemapHelper.ISitemapItem> items, SitemapHelper.ISitemapGenerator generator)
            {
                this.siteMapItems = items;
                this.siteMapGenerator = generator;
            }
            public override void ExecuteResult(ControllerContext context)
            {
                var response = context.HttpContext.Response;

                response.ContentType = "text/xml";
                response.ContentEncoding = Encoding.UTF8;

                using (var writer = new XmlTextWriter(response.Output))
                {
                    writer.Formatting = Formatting.Indented;
                    var sitemap = siteMapGenerator.GenerateSiteMap(siteMapItems);

                    sitemap.WriteTo(writer);
                }
            }
        }

    }
}
