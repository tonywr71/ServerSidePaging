#region Copyright Syncfusion Inc. 2001 - 2015
// Copyright Syncfusion Inc. 2001 - 2015. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace ServerSidePaging.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ServerSidePaging.Models;


    public static class OrderRepository
    {
        public static IList<EditableOrder> GetAllRecords()
        {
            IList<EditableOrder> orders = (IList<EditableOrder>)HttpContext.Current.Session["Orders"];

            if (orders == null)
            {
                HttpContext.Current.Session["Orders"] = orders = (from ord in new NorthwindDataContext().Orders.Take(200)
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
                                                                  }).ToList();
                foreach (var order in orders)
                {
                    if (order.Freight > 30)
                        order.Verified = true;
                    else
                        order.Verified = false;
                }
            }
            return orders;
        }

        public static void Add(EditableOrder order)
        {
            GetAllRecords().Insert(0, order);
        }

        public static void Add(List<EditableOrder> order)
        {
            foreach (var temp in order)
                GetAllRecords().Insert(0, temp);
        }

        public static void Delete(int OrderID)
        {
            EditableOrder result = GetAllRecords().Where(o => o.OrderID == OrderID).FirstOrDefault();
            GetAllRecords().Remove(result);
        }

        public static void Delete(List<EditableOrder> order)
        {
            foreach (var temp in order)
            {
                EditableOrder result = GetAllRecords().Where(o => o.OrderID == temp.OrderID).FirstOrDefault();
                GetAllRecords().Remove(result);
            }
        }

        public static void Update(EditableOrder order)
        {
            EditableOrder result = GetAllRecords().Where(o => o.OrderID == order.OrderID).FirstOrDefault();
            if (result != null)
            {
                result.OrderID = order.OrderID;
                result.OrderDate = order.OrderDate;
                result.CustomerID = order.CustomerID;
                result.EmployeeID = order.EmployeeID;
                result.Freight = order.Freight;
                result.ShipAddress = order.ShipAddress;
                result.ShipCity = order.ShipCity;               
                result.ShipName = order.ShipName;                
                result.ShipPostalCode = order.ShipPostalCode;
                result.ShipRegion = order.ShipRegion;
                result.ShipCountry = order.ShipCountry;
                result.Verified = order.Verified;
            }
        }              

        public static void Update(List<EditableOrder> order)
        {
            foreach (var temp in order)
            {
                EditableOrder result = GetAllRecords().Where(o => o.OrderID == temp.OrderID).FirstOrDefault();
                if (result != null)
                {
                    result.OrderID = temp.OrderID;
                    result.OrderDate = temp.OrderDate;
                    result.CustomerID = temp.CustomerID;
                    result.EmployeeID = temp.EmployeeID;
                    result.Freight = temp.Freight;
                    result.ShipAddress = temp.ShipAddress;
                    result.ShipCity = temp.ShipCity;
                    result.ShipName = temp.ShipName;
                    result.ShipPostalCode = temp.ShipPostalCode;
                    result.ShipRegion = temp.ShipRegion;
                    result.ShipCountry = temp.ShipCountry;
                    result.Verified = temp.Verified;
                }
            }
        }
    }
}
