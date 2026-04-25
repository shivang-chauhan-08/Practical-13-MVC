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
    public class EmployeeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult View()
        {
            var data = context.Employees2.Include("Designation").Select(e => new EmployeeVM
            {
                Id = e.Id,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                LastName = e.LastName,
                DOB = e.DOB,
                MobileNumber = e.MobileNumber,
                Address = e.Address,
                Salary = e.Salary,
                DesignationName = e.Designation.Name
            }).ToList();

            return View(data);
        }
        public ActionResult Index()
        {
            List<Employee2> employees = new List<Employee2>();
            employees = context.Employees2.ToList();
            return View(employees);
        }

        public ActionResult Create()
        {
            ViewBag.DesignationList = new SelectList(context.Designations, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee2 emp)
        {
            context.Employees2.Add(emp);
            context.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Update(int id)
        {
            var employee = context.Employees2.Where(e => e.Id == id).FirstOrDefault();
            ViewBag.DesignationList = new SelectList(context.Designations, "Id", "Name", employee.DesignationId);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Update(Employee2 updateEmp)
        {
            using (var context = new ApplicationDbContext())
            {
                var employee = context.Employees2.Where(e => e.Id == updateEmp.Id).FirstOrDefault();
                if (employee != null)
                {
                    employee.FirstName = updateEmp.FirstName;
                    employee.MiddleName = updateEmp.MiddleName;
                    employee.LastName = updateEmp.LastName;
                    employee.DOB = updateEmp.DOB;
                    employee.MobileNumber = updateEmp.MobileNumber;
                    employee.Address = updateEmp.Address;
                    employee.Salary = updateEmp.Salary;
                    employee.DesignationId = updateEmp.DesignationId;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Delete(int id)
        {
            var employee = context.Employees2.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(Employee2 deleteEmp)
        {
            var employee = context.Employees2.Where(e => e.Id == deleteEmp.Id).FirstOrDefault();
            if (employee != null)
            {
                context.Employees2.Remove(employee);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Employee");
        }
    }
}