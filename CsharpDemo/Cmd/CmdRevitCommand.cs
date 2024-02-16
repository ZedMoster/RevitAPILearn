using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using CsharpDemo.Utils;
using RevitAPI.Toolkit.Attributes;

namespace CsharpDemo.Cmd
{
    [Xml("API教程", "")]
    [Transaction(TransactionMode.Manual)]
    public class CmdRevitCommand : RevitCommand
    {
        public override void Action()
        {
            System.Diagnostics.Process.Start("https://space.bilibili.com/81888284/channel/collectiondetail?sid=805053");
        }
    }
}
