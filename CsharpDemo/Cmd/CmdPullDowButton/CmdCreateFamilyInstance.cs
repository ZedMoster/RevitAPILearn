using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using CsharpDemo.Extension;
using RevitAPI.Toolkit.Attributes;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建族实例","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateFamilyInstance : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var type = doc.OfClass<FamilySymbol>()
                .FirstOrDefault(o => o.FamilyName == "建筑拖车" && o.Name == "12000 x 2400mm");
            if (type == null)
            {
                message = "获取族类型失败";
                return Result.Failed;
            }

            doc.Transaction(t =>
            {
                if (!type.IsActive)
                {
                    type.Activate();
                }
                var ins = doc.Create.NewFamilyInstance(
                    new XYZ(1000 / 304.8, 1000 / 304.8, 4000.0 / 304.8),
                    type,
                    Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
            });

            return Result.Succeeded;
        }
    }
}
