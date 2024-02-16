using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using CsharpDemo.Views;

namespace CsharpDemo.Cmd.CmdSplitButton
{
    [Xml("柱顶成梁3", "选择两个结构柱,柱子顶端自动创建梁(非模态窗口,MVVM)")]
    [Transaction(TransactionMode.Manual)]
    public class CmdColumnCreateBeam3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            commandData.Init();
            var window = new ColumnCreateBeam3Window();
            window.MainWindowHandle();
            window.Show();
            return Result.Succeeded;
        }
    }
}
