using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建标高","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateLevels : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            // 层高
            var h = 3600.0;
            doc.Transaction(t =>
            {
                for (int i = 0; i < 10; i++)
                {
                    var e = i * h;
                    var level = Level.Create(doc, e / 304.8);
                    level.Name = "标高_" + (i + 1).ToString();
                }
            }, "Csharp level");
            return Result.Succeeded;
        }
    }
}
