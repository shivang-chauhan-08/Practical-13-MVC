using EFCodeFirst.Models.Context;
using EFCodeFirst.Models.Entities;
using EFCodeFirst.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFCodeFirst.Controllers
{
    public class DesignationController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult CountByDesignation()
        {
            var data = context.Employees2.Include("Designation").GroupBy(x => x.Designation.Name).Select(g => new CountVM
            {
                Designation = g.Key,
                Count = g.Count()
            });
            return View(data);
        }
        public ActionResult Index()
        {
            List<Designation> designations = new List<Designation>();
            designations = context.Designations.ToList();
            return View(designations);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Designation des)
        {
            context.Designations.Add(des);
            context.SaveChanges();
            return RedirectToAction("Index", "Designation");
        }

        public ActionResult Update(int id)
        {
            var designation = context.Designations.Where(e => e.Id == id).FirstOrDefault();
            return View(designation);
        }

        [HttpPost]
        public ActionResult Update(Designation updateDes)
        {
            var designation = context.Designations.Where(e => e.Id == updateDes.Id).FirstOrDefault();
            if (designation != null)
            {
                designation.Name = updateDes.Name;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Designation");
        }

        public ActionResult Delete(int id)
        {
            var designation = context.Designations.Where(e => e.Id == id).FirstOrDefault();
            return View(designation);
        }

        [HttpPost]
        public ActionResult Delete(Designation delDes)
        {
            using (var context = new ApplicationDbContext())
            {
                var designation = context.Designations.Where(e => e.Id == delDes.Id).FirstOrDefault();
                if (designation != null)
                {
                    context.Designations.Remove(designation);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Designation");
        }
    }
}