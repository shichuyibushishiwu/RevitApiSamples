///************************************************************************************
///   Author:十五
///   CretaeTime:2022/8/3 21:07:16
///   Mail:1012201478@qq.com
///   Github:https://github.com/shichuyibushishiwu
///
///   Description:
///
///************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace DynamicModelUpdater
{
    public class ViewTitleUpdater : IUpdater
    {
        private readonly AddInId _addInId;

        public ViewTitleUpdater(AddInId addInId)
        {
            _addInId = addInId;
        }

        public void Execute(UpdaterData data)
        {
            var ids = data.GetModifiedElementIds();
            var document = data.GetDocument();


            if (ids.Any())
            {
                foreach (var item in ids)
                {
                    var element = document.GetElement(item);
                    if (element is Viewport viewport)
                    {
                        using (SubTransaction transaction = new SubTransaction(document))
                        {
                            transaction.Start();
                            FilteredElementCollector elements = new FilteredElementCollector(document, document.ActiveView.Id);
                            var instance = elements.OfClass(typeof(FamilyInstance))
                                   .Cast<FamilyInstance>()
                                   .Where(f => f.Symbol.FamilyName == "Title" && f.LookupParameter("Id").AsInteger() == viewport.Id.IntegerValue)
                                   .FirstOrDefault();

                            if (instance != null)
                            {
                                instance.LookupParameter("Title").Set(viewport.get_Parameter(BuiltInParameter.VIEWPORT_VIEW_NAME).AsString());
                            }

                            transaction.Commit();
                        };
                    }
                }
                return;
            }

            var addedIds = data.GetAddedElementIds();

            if (addedIds.Any())
            {
                foreach (var item in addedIds)
                {
                    var element = document.GetElement(item);
                    if (element is Viewport viewport)
                    {
                        FilteredElementCollector elements = new FilteredElementCollector(document);
                        var family = elements.OfClass(typeof(Family)).Cast<Family>().Where(f => f.Name == "Title").FirstOrDefault();
                        if (family != null)
                        {
                            using (SubTransaction transaction = new SubTransaction(document))
                            {

                                var outLine = viewport.GetBoxOutline();



                                transaction.Start();
                                var typeIds = family.GetFamilySymbolIds();
                                var symbol = document.GetElement(typeIds.First()) as FamilySymbol;
                                var instance = document.Create.NewFamilyInstance(new XYZ(viewport.GetBoxCenter().X, outLine.MinimumPoint.Y, 0), symbol, document.ActiveView);

                                instance.LookupParameter("Title").Set(viewport.get_Parameter(BuiltInParameter.VIEWPORT_VIEW_NAME).AsString());
                                instance.LookupParameter("Id").Set(viewport.Id.IntegerValue);

                                transaction.Commit();
                            };
                        }
                    }
                }
            }

            //do something
        }

        public string GetAdditionalInformation() => "Shiwu View Title Updater";

        public ChangePriority GetChangePriority() => ChangePriority.Annotations;

        public UpdaterId GetUpdaterId() => new UpdaterId(_addInId, new Guid("5C1478AA-0106-4A6C-911C-54E8423A28BB"));

        public string GetUpdaterName() => "Shiwu View Title Updater";
    }
}
