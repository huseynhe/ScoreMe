﻿using ScoreMe.DAL;
using ScoreMe.UTILITY;
using ScoreMe.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ScoreMe.UI.Services
{
    public class EnumService
    {

        public static IEnumerable<SelectListItem> GetControllers()
        {
            List<MyTypes.ControllersList> controllers = new List<MyTypes.ControllersList>();
            MyTypes.ControllersList MyController = new MyTypes.ControllersList();

            foreach (Type t in GetSubClasses<Controller>())
            {
                MyController.ControllerName = t.Name;
                MyController.Description = Extensions.GetDisplayName(t);
                if (MyController.Description != MyController.ControllerName)
                {
                    controllers.Add(MyController);
                }

            }

            controllers = controllers.OrderBy(c => c.Description).ToList();
            List<SelectListItem> items = controllers.Select(
                x =>
                new SelectListItem
                {
                    Value = x.ControllerName,
                    Text = x.Description
                }).ToList();

            var itemtip = new SelectListItem()
            {
                Value = null,
                Text = "--- Seçin ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetActions(string ControllerName)
        {
            List<MyTypes.ActionsList> actions = new List<MyTypes.ActionsList>();
            MyTypes.ActionsList MyAction = new MyTypes.ActionsList();

            var asm = Assembly.GetExecutingAssembly();
            var methods = asm.GetTypes()
                .Where(type => typeof(Controller)
                .IsAssignableFrom(type) && type.Name == ControllerName)
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic
                       && !method.IsDefined(typeof(NonActionAttribute))
                       && (method.ReturnType == typeof(ActionResult) ||
                           method.ReturnType == typeof(Task<ActionResult>))
                      );
            foreach (MethodInfo m in methods)
            {
                MyAction.ActionName = m.Name;
                string desc = null;
                if ((DescriptionAttribute)m.GetCustomAttribute(typeof(DescriptionAttribute)) != null)
                {
                    desc = ((DescriptionAttribute)m.GetCustomAttribute(typeof(DescriptionAttribute))).Title;
                }
                if (desc != null)
                {
                    MyAction.Description = desc;
                    actions.Add(MyAction);
                }

            }
            actions = actions.OrderBy(a => a.Description).ToList();
            List<SelectListItem> items = actions.Select(
                a =>
                new SelectListItem
                {
                    Value = a.ActionName,
                    Text = a.Description
                }).ToList();

            var itemtip = new SelectListItem()
            {
                Value = null,
                Text = "--- Seçin ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }

        public static string GetControllerDescription(string ControllerName)
        {
            string result = Extensions.GetDisplayName(GetSubClasses<Controller>().Where(c => c.Name == ControllerName).FirstOrDefault());
            return result;
        }

        public static string GetActionDescription(string ControllerName, string ActionName)
        {
            var asm = Assembly.GetExecutingAssembly();
            string result = asm.GetTypes()
                .Where(type => typeof(Controller)
                .IsAssignableFrom(type) && type.Name == ControllerName)
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic
                       && !method.IsDefined(typeof(NonActionAttribute))
                       && method.Name == ActionName
                       && (method.ReturnType == typeof(ActionResult) ||
                           method.ReturnType == typeof(Task<ActionResult>))
                      )
                .Select(method => ((DescriptionAttribute)method.GetCustomAttribute(typeof(DescriptionAttribute)))?.Title)
                .FirstOrDefault();
            return result;
        }

        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        public static IEnumerable<SelectListItem> GetLockEnumTypes()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            var itemtip = new SelectListItem()
            {
                Value = null,
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            var itemtFemale = new SelectListItem()
            {
                Value = "0",
                Text = "Xeyr"
            };
            items.Insert(1, itemtFemale);
            var itemtipMale = new SelectListItem()
            {
                Value = "1",
                Text = "Bəli"
            };
            items.Insert(2, itemtipMale);

            return new SelectList(items, "Value", "Text");
        }



        public static IEnumerable<SelectListItem> GetAccessEnumTypes()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            var itemtip = new SelectListItem()
            {
                Value = null,
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            var itemtFemale = new SelectListItem()
            {
                Value = "0",
                Text = "Xeyr"
            };
            items.Insert(1, itemtFemale);
            var itemtipMale = new SelectListItem()
            {
                Value = "1",
                Text = "Bəli"
            };
            items.Insert(2, itemtipMale);

            return new SelectList(items, "Value", "Text");
        }
        public static IEnumerable<SelectListItem> GetGenderEnumTypes()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            var itemtip = new SelectListItem()
            {
                Value = null,
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            var itemtipMale = new SelectListItem()
            {
                Value = "1",
                Text = "Kişi"
            };
            items.Insert(1, itemtipMale);
            var itemtFemale = new SelectListItem()
            {
                Value = "2",
                Text = "Qadın"
            };
            items.Insert(2, itemtFemale);
            return new SelectList(items, "Value", "Text");
        }
        public static IEnumerable<SelectListItem> GetLocationEnumTypes()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            var itemtip = new SelectListItem()
            {
                Value = null,
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            var itemtipMale = new SelectListItem()
            {
                Value = "1",
                Text = "Ölkədə"
            };
            items.Insert(1, itemtipMale);
            var itemtFemale = new SelectListItem()
            {
                Value = "2",
                Text = "Səfərdə"
            };
            items.Insert(2, itemtFemale);
            return new SelectList(items, "Value", "Text");
        }
        public static IEnumerable<SelectListItem> GetUserListByEvID(int evID)
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetUsersByTypeEVID(evID)
                .OrderBy(n => n.UserName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ID.ToString(),
                        Text = n.UserName,
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetMessajeUsersByTypeEVID(int evID)
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetMessajeUsersByTypeEVID(evID)
                .OrderBy(n => n.UserName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ID.ToString()+ "~"+ n.UserName,
                        Text = n.UserName,
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }
        public static IEnumerable<SelectListItem> GetNetConsumeUsersByTypeEVID(int evID)
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetNetConsumeUsersByTypeEVID(evID)
                .OrderBy(n => n.UserName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ID.ToString() + "~" + n.UserName,
                        Text = n.UserName,
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }
        public static IEnumerable<SelectListItem> GetEmployeeList()
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetEmployees()
                .OrderBy(n => n.FirstName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ID.ToString(),
                        Text = n.FirstName + " " + n.LastName + " " + n.FatherName,
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetEnumCategoryList()
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetEnumCategorys()
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ID.ToString(),
                        Text = n.Name,
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetEnumValueListByEcID(Int64 ecategoryId)
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetEnumValuesByEnumCategoryID(ecategoryId)
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ID.ToString(),
                        Text = n.Name,
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }
        public static IEnumerable<SelectListItem> GetEnumValueListByEcIDForINOUT(Int64 ecategoryId)
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetEnumValuesByEnumCategoryID(ecategoryId)
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ID.ToString(),
                        Text = n.Name+"("+n.Description+")",
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }
        public static IEnumerable<SelectListItem> GetEnumValueListByEcIDForPrefix(Int64 ecategoryId)
        {

            CRUDOperation dataOperations = new CRUDOperation();
            List<SelectListItem> items = dataOperations.GetEnumValuesByEnumCategoryID(ecategoryId)
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.Name.ToString(),
                        Text = n.Name,
                        //Selected = n.ID == parentID ? true : false
                    }).ToList();
            var itemtip = new SelectListItem()
            {
                Value = "0",
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            return new SelectList(items, "Value", "Text");
        }

        public static IEnumerable<SelectListItem> GetBoleanEnumTypes()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            var itemtip = new SelectListItem()
            {
                Value = null,
                Text = "---  Seçiniz ---"
            };
            items.Insert(0, itemtip);
            var itemtipMale = new SelectListItem()
            {
                Value = "0",
                Text = "Xeyir"
            };
            items.Insert(1, itemtipMale);
            var itemtFemale = new SelectListItem()
            {
                Value = "1",
                Text = "Bəli"
            };
            items.Insert(2, itemtFemale);
            return new SelectList(items, "Value", "Text");
        }
    }
}