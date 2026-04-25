using EFCodeFirst.Models.Context;
using EFCodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFCodeFirst.Controllers
{
    public class Task_1Controller : Controller
    {
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();

            using (var context = new ApplicationDbContext())
            {
                employees = context.Employees.ToList();
            }

            return View(employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            using(var context = new ApplicationDbContext())
            {
                context.Employees.Add(emp);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Task_1");
        }

        public ActionResult Update(int id)
        {
            var context = new ApplicationDbContext();
            var employee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Update(Employee updateEmp)
        {
            using (var context = new ApplicationDbContext()) 
            {
                var employee = context.Employees.Where(e => e.Id == updateEmp.Id).FirstOrDefault();
                if (employee != null) 
                {
                    employee.Name = updateEmp.Name;
                    employee.DOB = updateEmp.DOB;
                    employee.Age = updateEmp.Age;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Task_1");
        }

        public ActionResult Delete(int id)
        {
            var context = new ApplicationDbContext();
            var employee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(Employee updateEmp)
        {
            using (var context = new ApplicationDbContext())
            {
                var employee = context.Employees.Where(e => e.Id == updateEmp.Id).FirstOrDefault();
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Task_1");
        }
    }
}