using Autodesk.Revit.UI;
using CsharpDemo.Extension;
using CsharpDemo.Updater;
using System.Linq;

namespace CsharpDemo
{
    public class App : IExternalApplication
    {
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            #region Updater
            UpdaterParamterByTypeName.Instance.OnShutdown();
            #endregion

            return Result.Succeeded;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            #region Updater
            UpdaterParamterByTypeName.Init(application.ActiveAddInId);
            UpdaterParamterByTypeName.Instance.OnStartup();
            #endregion

            application.CreateRibbonTab("17075104");
            var panel = application.CreateRibbonPanel("17075104", "api学习功能集");

            #region CsharpDemo.Cmd.CmdPullDowButton
            var pullDownButtonData = new PulldownButtonData("API学习", "API学习")
            {
                Image = Properties.Resources.API_16.GetImageBitmapSource(),
                LargeImage = Properties.Resources.API.GetImageBitmapSource(),
            };
            var pulldownButton = panel.AddItem(pullDownButtonData) as PulldownButton;
            var typesPullDowButton = typeof(App).Assembly.GetTypes().OrderBy(o => o.Name)
                .Where(o => o.IsClass && o.IsPublic && o.Namespace == "CsharpDemo.Cmd.CmdPullDowButton");
            foreach (var type in typesPullDowButton)
            {
                pulldownButton.AddPushButton(type.GetPushButtonData());
            }
            panel.AddSeparator();
            #endregion

            #region CsharpDemo.Cmd.CmdSplitButton
            var splitButtonData = new SplitButtonData("下拉功能", "下拉功能")
            {
                Image = Properties.Resources.API_16.GetImageBitmapSource(),
                LargeImage = Properties.Resources.API.GetImageBitmapSource()
            };
            var splitButton = panel.AddItem(splitButtonData) as SplitButton;
            var typesSplitButton = typeof(App).Assembly.GetTypes().OrderBy(o => o.Name)
                .Where(o => o.IsClass && o.IsPublic && o.Namespace == "CsharpDemo.Cmd.CmdSplitButton");
            foreach (var type in typesSplitButton)
            {
                splitButton.AddPushButton(type.GetPushButtonData());
            }
            panel.AddSeparator();
            #endregion

            #region CsharpDemo.Cmd
            var types = typeof(App).Assembly.GetTypes().Where(o => o.IsClass && o.IsPublic && o.Namespace == "CsharpDemo.Cmd");
            foreach (var type in types)
            {
                panel.AddItem(type.GetPushButtonData());
            }
            #endregion

            return Result.Succeeded;
        }
    }
}
