namespace DemoRobotTXTsitemapXML.Helpers
{
    using System.Web.Mvc;

    public static class UrlExtensionsHelper
    {
        public static string FullActionLink(this UrlHelper url, string actionName, string controllerName, object routeValues = null)
        {
            return url.Action(actionName, controllerName, routeValues, url.RequestContext.HttpContext.Request.Url.Scheme);
        }
    }
}