namespace RobotSiteMap.Helpers
{
    using System.Text;
    using System.Web;

    public class RobotsHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { { return false; } }
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
            //contentBuilder.AppendLine("Sitemap: http://www.beableto.fr/sitemap");

            //contentBuilder.AppendLine("Disallow: /");
            //return File(Encoding.UTF8.GetBytes(contentBuilder.ToString()), "text/plain");
            //var robotsFile = "~/robots.txt";
            //return File(robotsFile, "text/plain");

            // return the robots content
            context.Response.Write(contentBuilder);
        }
    }
}