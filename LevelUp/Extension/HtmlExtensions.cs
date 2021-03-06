﻿using System;
using System.Web.Mvc;
using Present.WebMvc.Configuration;

namespace Present.WebMvc.Extension
{
    public static class HtmlExtensions
    {
        [ThreadStatic]
        private static readonly Random rnd = new Random();

        private static int randomNumber = rnd.Next();

        public static MvcHtmlString RenderScript(this UrlHelper helper, string path)
        {
            var debug = helper.RequestContext.HttpContext.IsDebuggingEnabled;
            int version = debug ? randomNumber : typeof(BootStrapper).Assembly.GetName().Version.Minor;
            string url = string.Format("{0}{1}?v={2}", helper.Content(path), "", +version);
            return MvcHtmlString.Create(string.Format("<script src='{0}' type='text/javascript'></script>\r\n", url));
        }


        public static MvcHtmlString RenderCssLink(this UrlHelper helper, string path)
        {
            var debug = helper.RequestContext.HttpContext.IsDebuggingEnabled;
            int version = debug ? randomNumber : typeof(BootStrapper).Assembly.GetName().Version.Minor;


            string url = string.Format("{0}{1}?v={2}", helper.Content(path), "", +version);
            return MvcHtmlString.Create(string.Format(" <link href='{0}' rel='stylesheet' type='text/css' />\r\n", url));
        }
    }
}