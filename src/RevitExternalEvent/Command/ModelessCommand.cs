///************************************************************************************
///   Author:十五
///   CretaeTime:2022/7/12 20:59:40
///   Mail:1012201478@qq.com
///   Github:https://github.com/shichuyibushishiwu
///
///   Description:
///
///************************************************************************************

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitExternalEvent.Command
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class ModelessCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Views.Modeless testWindow = Views.Modeless.GetInstance();
            testWindow.Initial(commandData.Application.ActiveUIDocument.Document);
            testWindow.Show();

            return Result.Succeeded;
        }
    }
}
