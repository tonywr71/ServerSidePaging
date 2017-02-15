using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServerSidePaging;
using Syncfusion.JavaScript;
using System.Collections;
using ServerSidePaging.Models;
using Syncfusion.JavaScript.DataSources;
namespace ServerSidePaging.Controllers
{
    public class GridController : Controller
    {
        //
        // GET: /Grid/
        public ActionResult GridFeatures()
        {            
            return View();
        }

        public ActionResult DataSource(DataManager dm)
        {
            IEnumerable DataSource = OrderRepository.GetAllRecords().ToList();
            DataResult result = new DataResult();
            DataOperations obj = new DataOperations();
            if (dm.Skip != 0)
            {
                DataSource = obj.PerformSkip(DataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                DataSource = obj.PerformTake(DataSource, dm.Take);
            }
            result.result = DataSource;
            result.count = OrderRepository.GetAllRecords().Count();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    public ActionResult DataSource2(DataManager dm)
    {
      IQueryable<EditableOrder> ordersQueryable = (from ord in new NorthwindDataContext().Orders.Take(200)
                             select new EditableOrder
                             {
                               OrderID = ord.OrderID,
                               OrderDate = ord.OrderDate,
                               CustomerID = ord.CustomerID,
                               EmployeeID = ord.EmployeeID,
                               Freight = ord.Freight,
                               ShipAddress = ord.ShipAddress,
                               ShipCity = ord.ShipCity,
                               ShipName = ord.ShipName,
                               ShipPostalCode = ord.ShipPostalCode,
                               ShipRegion = ord.ShipRegion,
                               ShipCountry = ord.ShipCountry
                             });

      //apply filter
     DataOperations dataOperations = new DataOperations();

      if (dm.Search != null)
      {
        ordersQueryable = (IQueryable<EditableOrder>)dataOperations.PerformSearching(ordersQueryable, dm.Search);
      }
      var recordCountQueryable = ordersQueryable;
      
      if (dm.Sorted!=null)
      {
        ordersQueryable = (IQueryable<EditableOrder>)dataOperations.PerformSorting(ordersQueryable, dm.Sorted);
      }

      if (dm.Skip != 0)
      {
        ordersQueryable = (IQueryable<EditableOrder>)dataOperations.PerformSkip(ordersQueryable, dm.Skip);
      }
      if (dm.Take != 0)
      {
        ordersQueryable = (IQueryable<EditableOrder>)dataOperations.PerformTake(ordersQueryable, dm.Take);
      }
      var data = ordersQueryable.ToList();
      var recordCount = recordCountQueryable.Count();
      var dataResult = new DataResult { count = recordCount, result = data };
      return Json(dataResult, JsonRequestBehavior.AllowGet);
    }

    public class DataResult
        {
            public IEnumerable result { get; set; }
            public int count { get; set; }
        }
    }
}
