using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CsharpDemo.Extension;
using System.Windows;

namespace CsharpDemo.Views
{
    /// <summary>
    /// ColumnCreateBeamWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ColumnCreateBeamWindow : Window
    {
        public ColumnCreateBeamWindow(UIDocument uidoc)
        {
            InitializeComponent();
            BeamTypeList.ItemsSource = uidoc.Document.OfClass<FamilySymbol>(BuiltInCategory.OST_StructuralFraming);
        }

        private void SelectElement_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
