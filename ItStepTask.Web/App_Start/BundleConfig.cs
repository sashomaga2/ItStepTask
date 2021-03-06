﻿using System.Web;
using System.Web.Optimization;

namespace ItStepTask.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        )); 

            bundles.Add(new ScriptBundle("~/bundles/shoppingCart").Include(
                        "~/Scripts/shoppingCart.js"));

            bundles.Add(new ScriptBundle("~/bundles/orders").Include(
                        "~/Scripts/orders.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                        "~/Scripts/kendo.all.min.js",
                        "~/Scripts/kendo-init.js",
                        "~/Scripts/category.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/kendo.common.min.css",
                      "~/Content/kendo.default.min.css",
                      "~/Content/site.css"));
        }
    }
}
