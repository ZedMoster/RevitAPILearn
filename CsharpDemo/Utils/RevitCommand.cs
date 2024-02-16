using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CsharpDemo.Extension;
using System.Windows;

namespace CsharpDemo.Utils
{
    public abstract class RevitCommand : IExternalCommand
    {
        /// <summary>
        /// 具体功能逻辑实现
        /// </summary>
        public abstract void Action();

        /// <summary>
        /// uidoc
        /// </summary>
        public UIDocument Uidoc { get; private set; }

        /// <summary>
        /// IExternalCommand
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Uidoc = commandData.Init();
                Action();
                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (System.Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
