using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsharpDemo.Extension;
using CsharpDemo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CsharpDemo.ViewModels
{
    internal class ColumnCreateBeam3ViewModel : ObservableObject
    {

        #region 属性

        private double _Offset = 0;
        public double Offset
        {
            get { return _Offset; }
            set { SetProperty(ref _Offset, value); }
        }

        /// <summary>
        /// 梁类型
        /// </summary>
        public List<FamilySymbol> BeamTypes { get; set; }

        private FamilySymbol _Symbol;
        /// <summary>
        /// 当前选择梁类型
        /// </summary>
        public FamilySymbol Symbol
        {
            get { return _Symbol; }
            set { SetProperty(ref _Symbol, value); }
        }

        #endregion

        #region 构造



        /// <summary>
        /// 构造
        /// </summary>
        public ColumnCreateBeam3ViewModel()
        {
            BeamTypes = XmlDoc.Instance.UIdoc.Document.OfClass<FamilySymbol>(BuiltInCategory.OST_StructuralFraming);
            Symbol = BeamTypes.FirstOrDefault();
        }

        #endregion

        #region 命令

        private RelayCommand _CreateCommand;
        public RelayCommand CreateCommand => _CreateCommand ?? (_CreateCommand = new RelayCommand(Create));
        private void Create()
        {
            XmlDoc.Instance.Task.Run(app =>
            {
                var uidoc = app.ActiveUIDocument;
                var filter = uidoc.Document.FilterElement(el => el.Category.Name == "结构柱", null);
                while (true)
                {
                    try
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
                            if (!Symbol.IsActive) Symbol.Activate();
                            var beam = doc.Create.NewFamilyInstance(Line.CreateBound(p1, p2), Symbol, level, StructuralType.Beam);
                            beam.LookupParameter("Z 轴偏移值").SetValueString(Offset.ToString());
                        }, "柱顶成梁");
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            });
        }

        #endregion

    }
}
