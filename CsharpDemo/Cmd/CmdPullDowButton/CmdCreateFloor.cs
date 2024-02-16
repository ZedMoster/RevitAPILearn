﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using CsharpDemo.Extension;
using RevitAPI.Toolkit.Attributes;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建楼板","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateFloor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            var level = doc.OfClass<Level>().FirstOrDefault(o => o.Name == "标高 1");
            var typeId = doc.GetDefaultElementTypeId(ElementTypeGroup.FloorType);
            var floortype = doc.GetElement(typeId) as FloorType;

            CurveArray curveArray = new CurveArray();
            var p0 = new XYZ(0, 0, 0);
            var p1 = new XYZ(5000 / 304.8, 0, 0);
            var p2 = new XYZ(5000 / 304.8, 5000 / 304.8, 0);
            var p3 = new XYZ(0, 5000 / 304.8, 0);
            var line1 = Line.CreateBound(p0, p1);
            var line2 = Line.CreateBound(p1, p2);
            var line3 = Line.CreateBound(p2, p3);
            var line4 = Line.CreateBound(p3, p0);
            curveArray.Append(line1);
            curveArray.Append(line2);
            curveArray.Append(line3);
            curveArray.Append(line4);

            doc.Transaction(t =>
            {
                doc.Create.NewFloor(curveArray, floortype, level, true);
            }, "c# floor");
            return Result.Succeeded;
        }
    }
}
