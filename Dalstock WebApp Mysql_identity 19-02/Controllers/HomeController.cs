﻿using BL;
using BL.Managers;
using DAL;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;

        public HomeController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            itemManager = new ItemManager(uow);
            workplaceManager = new WorkplaceManager(uow);
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public ActionResult Index()
        {
            DashboardViewModel dvm = new DashboardViewModel();
            var bobbins = itemManager.GetBobbins().Last();
            var items = itemManager.GetItems("Insufficient");
            var workplace = workplaceManager.GetWorkplaces().Last();
            decimal length = bobbins.CableLength;
            decimal rem = bobbins.AmountRemains;
            decimal perc = rem / length;
            dvm.LatestBobbin = bobbins;
            dvm.TotalAmountStock = items.Sum(x => x.Amount);
            dvm.LatestWorkplace = workplace;
            dvm.CablePerc = perc * 100;
            dvm.InsufficientItems = itemManager.GetItems().ToList();
            return View(dvm);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var user = UserManager.FindById(User.Identity.GetUserId());
            string database = user.Database;
            string username = user.Database;
            var ctx = new ItemDbContext(database, username);
            var list = ctx.Workplaces.Include("Debits").ToList();
            var list2 = ctx.Staff.Include("Debited_debits").Include("Approved_debits").ToList();
            var list3 = ctx.Debits.Include("Workplace").Include("Item").Include("Debited_By_Staff").ToList();
            var list4 = ctx.Debits.Include("Workplace").Include("Item").Include("Debited_By_Staff").Where(a => a.DebitState == Domain.DebitState.Approved).ToList();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}