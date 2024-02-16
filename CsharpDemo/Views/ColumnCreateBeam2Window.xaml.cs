using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.Exceptions;
using CsharpDemo.Extension;
using CsharpDemo.Utils;
using System.Windows;

namespace CsharpDemo.Views
{
    /// <summary>
    /// ColumnCreateBeam2Window.xaml 的交互逻辑
    /// </summary>
    public partial class ColumnCreateBeam2Window : Window
    {
        public ColumnCreateBeam2Window()
        {
            InitializeComponent();
            BeamTypeList.ItemsSource = XmlDoc.Instance.UIdoc.Document
                .OfClass<FamilySymbol>(BuiltInCategory.OST_StructuralFraming);
        }

        private void SelectElement_Click(object sender, RoutedEventArgs e)
        {
            XmlDoc.Instance.Task.Run(app =>
            {
                var uidoc = app.ActiveUIDocument;
                var symbol = BeamTypeList.SelectedItem as FamilySymbol;
                var filter = uidoc.Document.FilterElement(el => el.Category.Name == "结构柱", null);
                try
                {
                    while (true)
                    {
                        var ref1 = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, filter, "请选择第一个结构柱,Esc退出创建");
                        var ref2 = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, filter, "请选择第二个结构柱,Esc退出创建");
                        var doc = uidoc.Document;
                        var col1 = doc.GetElement(ref1) as FamilyInstance;
                        var col2 = doc.GetElement(ref2) as FamilyInstance;
                        var p1 = col1.GetTopPoint();
                        var p2 = col2.GetTopPoint();
                        var level = doc.GetElement(col1.LevelId) as Level;
                        doc.Transaction(t =>
                        {
                            if (!symbol.IsActive) symbol.Activate();
                            var beam = doc.Create.NewFamilyInstance(Line.CreateBound(p1, p2), symbol, level, StructuralType.Beam);
                            beam.LookupParameter("Z 轴偏移值").SetValueString(Offset.Text);
                        }, "柱顶成梁");

                    }
                }
                catch (OperationCanceledException) { }
            });
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
