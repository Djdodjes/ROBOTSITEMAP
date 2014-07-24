namespace DemoRobotTXTsitemapXML.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using System.Xml.Linq;


    public class SitemapHelper
    {

        public interface ISitemapGenerator
        {
            XDocument GenerateSiteMap(IEnumerable<ISitemapItem> items);
        }

        public interface ISitemapItem
        {

            string Url { get; }

            DateTime? LastModified { get; }

            SitemapChangeFrequency? ChangeFrequency { get; }


            double? Priority { get; }
        }


        public class SitemapItem : ISitemapItem
        {
            public SitemapItem(string url, DateTime? lastModified = null, SitemapChangeFrequency? changeFrequency = null, double? priority = null)
            {
                Url = url;
                LastModified = lastModified;
                ChangeFrequency = changeFrequency;
                Priority = priority;
            }

            public string Url { get; protected set; }

            public DateTime? LastModified { get; protected set; }

            public SitemapChangeFrequency? ChangeFrequency { get; protected set; }

            public double? Priority { get; protected set; }
        }

        public enum SitemapChangeFrequency
        {
            Always,
            Hourly,
            Daily,
            Weekly,
            Monthly,
            Yearly,
            Never
        }

        public class SitemapGenerator : ISitemapGenerator
        {
            private static readonly XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            private static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

            public virtual XDocument GenerateSiteMap(IEnumerable<ISitemapItem> items)
            {
                var sitemap = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement(xmlns + "urlset",
                          new XAttribute("xmlns", xmlns),
                          new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                          new XAttribute(xsi + "schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
                          from item in items
                          select CreateItemElement(item)
                          )
                     );

                return sitemap;
            }

            private XElement CreateItemElement(ISitemapItem item)
            {
                var itemElement = new XElement(xmlns + "url", new XElement(xmlns + "loc", item.Url.ToLowerInvariant()));
                // optional
                if (item.LastModified.HasValue)
                    itemElement.Add(new XElement(xmlns + "lastmod", item.LastModified.Value.ToString("yyyy-MM-dd")));

                if (item.ChangeFrequency.HasValue)
                    itemElement.Add(new XElement(xmlns + "changefreq", item.ChangeFrequency.Value.ToString().ToLower()));

                if (item.Priority.HasValue)
                    itemElement.Add(new XElement(xmlns + "priority", item.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));

                return itemElement;
            }

        }


    }
}