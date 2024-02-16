using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using CsharpDemo.Utils;
using System;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("修改图元","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdElementTransformUtils : RevitCommand
    {
        public override void Action()
        {
            var uidoc = Uidoc;
            var doc = Uidoc.Document;

            //var r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            //var el = doc.GetElement(r);
            //uidoc.ShowElements(el);

            //var r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            //var el = doc.GetElement(r);
            //doc.Transaction(t =>
            //{
            //    doc.Delete(el.Id);
            //}, "删除");

            //var r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            //var el = doc.GetElement(r);
            //doc.Transaction(t =>
            //{
            //    ElementTransformUtils.MoveElement(doc, el.Id, doc.ActiveView.RightDirection.Multiply(1000 / 304.8));
            //}, "移动");

            //var r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            //var el = doc.GetElement(r);
            //doc.Transaction(t =>
            //{
            //    var center = (el.Location as LocationCurve).Curve.Evaluate(0.5, true);
            //    var axis = Line.CreateUnbound(center, doc.ActiveView.ViewDirection);
            //    ElementTransformUtils.RotateElement(doc, el.Id, axis, Math.PI * 0.25);
            //}, "旋转");

            //var r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            //var el = doc.GetElement(r);
            //doc.Transaction(t =>
            //{
            //    XYZ normal = XYZ.BasisX;    // use basis of the z-axis (0,0,1) for normal vector 
            //    XYZ origin = XYZ.Zero;  // origin is (0,0,0)  
            //    Plane geomPlane = Plane.CreateByNormalAndOrigin(normal, origin);
            //    ElementTransformUtils.MirrorElement(doc, el.Id, geomPlane);
            //}, "镜像");

            var r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            var el = doc.GetElement(r);
            doc.Transaction(t =>
            {
                ElementTransformUtils.CopyElement(doc, el.Id, XYZ.BasisY.Multiply(500 / 304.8));
            }, "复制");

        }
    }
}
