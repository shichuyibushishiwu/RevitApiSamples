///************************************************************************************
///   Author:十五
///   CretaeTime:2022/8/3 21:06:29
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


namespace DynamicModelUpdater
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class ViewTitleCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var addin = commandData.Application.ActiveAddInId;
            ViewTitleUpdater updater = new ViewTitleUpdater(addin);

            //UpdaterRegistry.RemoveAllTriggers(updater.GetUpdaterId());


            if (UpdaterRegistry.IsUpdaterRegistered(updater.GetUpdaterId()))
            {
                UpdaterRegistry.UnregisterUpdater(updater.GetUpdaterId());
            }


            UpdaterRegistry.RegisterUpdater(updater,true);


            ElementFilter elementFilter = new ElementCategoryFilter(BuiltInCategory.OST_Viewports);
            UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), elementFilter, Element.GetChangeTypeAny());
            UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), elementFilter, Element.GetChangeTypeElementAddition());
            return Result.Succeeded;
        }
    }
}
