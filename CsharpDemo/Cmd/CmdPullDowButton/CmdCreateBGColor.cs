using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("反转背景", "反转Revit窗口背景颜色")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateBGColor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var c = app.BackgroundColor;
            app.BackgroundColor = new Color((byte)(255 - c.Red), (byte)(255 - c.Green), (byte)(255 - c.Blue));
            return Result.Succeeded;
        }
    }
}
