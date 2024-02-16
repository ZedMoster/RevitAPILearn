using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System.Linq;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建风管","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateMEPCurve : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            var ductSystemType = doc.OfClass<MechanicalSystemType>().FirstOrDefault();
            var ductType = doc.OfClass<DuctType>().FirstOrDefault();

            var level = doc.ActiveView.GenLevel;
            if (level == null)
            {
                message = "请到楼层平面运行";
                return Result.Failed;
            }

            #region 测试创建
            //var p0 = new XYZ(0, 1000 / 304.8, 1000 / 304.8);
            //var p1 = new XYZ(1000 / 304.8, 1000 / 304.8, 1000 / 304.8);
            //doc.Transaction(t =>
            //{
            //    var duct = Duct.Create(doc, ductSystemType.Id, ductType.Id, level.Id, p0, p1);

            //}, "c# duct");
            #endregion

            #region 模型线转换风管

            var lines = doc.OfClass<CurveElement>();
            doc.Transaction(t =>
            {
                foreach (var curveElement in lines)
                {
                    var line = curveElement.GeometryCurve as Line;
                    if (line == null) continue;
                    var p0 = line.GetEndPoint(0);
                    var p1 = line.GetEndPoint(1);
                    var duct = Duct.Create(doc, ductSystemType.Id, ductType.Id, level.Id, p0, p1);
                }
            }, "c# ducts");

            #endregion

            return Result.Succeeded;
        }
    }
}
