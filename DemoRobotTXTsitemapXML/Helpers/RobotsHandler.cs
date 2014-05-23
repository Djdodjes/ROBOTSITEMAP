namespace DemoRobotTXTsitemapXML.Helpers
{
    using System;
    using System.Text;
    using System.Web;

    public class RobotsHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        
        {
            // Récupération du domaine(nom) de notre site 
            string domain = context.Request.Url.Host;
            // Initialisation du code du statut de la page
            context.Response.StatusCode = 200;
            // spécification du type de la ressource
            context.Response.ContentType = "text/plain";

            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine("User-agent: *");
            contentBuilder.AppendLine("Disallow: /Home/About");
            contentBuilder.AppendLine("Disallow: /Home/Contact");
            contentBuilder.AppendLine("Sitemap: http://www.mon-site.fr/sitemap");

            //contentBuilder.AppendLine("Disallow: /");
            //<meta name="robots" content="noindex" />
         
            // return the robots content
            context.Response.Write(contentBuilder);
        }

        #endregion
    }
}
