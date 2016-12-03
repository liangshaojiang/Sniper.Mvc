using System.Web;
using System.Web.Optimization;

namespace Sniper.Admin
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                     "~/Scripts/respond.js"));
            //
            bundles.Add(new ScriptBundle("~/bundles/cookie").Include(
                      "~/Content/js/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/form").Include(
                     "~/Content/js/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/md5").Include(
                     "~/Content/js/jquery.md5.js"));

            bundles.Add(new ScriptBundle("~/bundles/function").Include(
                     "~/Content/js/function.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxSetup").Include(
                    "~/Content/js/jquery.ajax.setup.js"));

            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                     "~/Content/js/jquery.modal.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/elements").Include(
                      "~/Content/ace/assets/js/ace-elements.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace").Include(
                     "~/Content/ace/assets/js/ace.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/extra").Include(
                     "~/Content/ace/assets/js/ace-extra.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/select2").Include(
                      "~/Content/ace/assets/select2/js/select2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/select2/zhCn").Include(
                      "~/Content/ace/assets/select2/js/i18n/zh-CN.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqtable/bootstrap").Include(
                     "~/Content/ace/assets/js/jquery.dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqtable").Include(
                      "~/Content/ace/assets/js/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/gritter").Include(
                     "~/Content/ace/assets/js/jquery.gritter.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/bootbox").Include(
               "~/Content/ace/assets/js/bootbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/custom").Include(
              "~/Content/ace/assets/js/jquery-ui.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/editable").Include(
             "~/Content/ace/assets/js/x-editable/bootstrap-editable.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/ace-editable").Include(
             "~/Content/ace/assets/js/x-editable/ace-editable.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/wysiwyg").Include(
             "~/Content/ace/assets/js/bootstrap-wysiwyg.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace/maskedinput").Include(
          "~/Content/ace/assets/js/bootstrap.maskedinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/avatarUpdate").Include(
          "~/Content/js/profile-avatar-update.js"));

            //CSS 
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Style.css")); 
             
                    
        }
    }
}