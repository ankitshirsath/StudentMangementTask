using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskStudentManagement.Data_Context;
using TaskStudentManagement.Models;

namespace TaskStudentManagement.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.Teacher = Teacher_Dropdawon();

            return View();
        }
        public ActionResult PartialView()
        {
            ViewBag.Teacher = Teacher_Dropdawon();

            return View();
        }
        public ActionResult SaveOrUpdate(Student_model model)
        {


            using (Task_Student_ManagementEntities Db = new Task_Student_ManagementEntities())
            {
                try
                {
                    if (model.id == 0)
                    {
                        Student_Table stu = new Student_Table()
                        {
                            FristName = model.FristName,
                            LastName = model.LastName,
                            DateOfBirth = model.DateOfBirth,
                            Gender = model.Gender,
                            Teacher_id = model.Teacher_id,

                        };
                        Db.Entry(stu).State = EntityState.Added;
                        TempData["message"] = "Your Data Save Successfuly..";
                    }
                    else
                    {
                        Student_Table stu = new Student_Table()
                        {
                            id = model.id,
                            FristName = model.FristName,
                            LastName = model.LastName,
                            DateOfBirth = model.DateOfBirth,
                            Gender = model.Gender,
                            Teacher_id = model.Teacher_id,

                        };
                        Db.Entry(stu).State = EntityState.Modified;
                        TempData["message"] = "Your Data Update Successfuly";
                    }
                    Db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction("Index");
            }


        }
        public List<SelectListItem> Teacher_Dropdawon()
        {
            var itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem { Value = "0", Text = "--select--" });
            try
            {
                using (Task_Student_ManagementEntities db = new Task_Student_ManagementEntities())
                {
                    var items = (from s in db.Teacher_Table

                                 select new { s.id, s.FristName, s.LastName }).ToList();

                    foreach (var data in items)
                    {
                        itemList.Add(new SelectListItem { Value = data.id.ToString(), Text = data.FristName + " " + data.LastName.ToString() });


                    }
                    return itemList;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public ActionResult EditData(int id)
        {
            ViewBag.Teacher = Teacher_Dropdawon();
            using (Task_Student_ManagementEntities Db = new Task_Student_ManagementEntities())
            {
                var data = new Student_Table();
                data = Db.Student_Table.Where(x => x.id == id).FirstOrDefault();
                Student_model model = new Student_model()
                {
                    id = data.id,
                    FristName = data.FristName,
                    LastName = data.LastName,
                    DateOfBirth = data.DateOfBirth,
                    Gender = data.Gender,
                    Teacher_id = data.Teacher_id,

                };
                return View("index", model);
            }
        }
        public ActionResult DeleteData(int id)
        {
            using (Task_Student_ManagementEntities Db = new Task_Student_ManagementEntities())
            {
                try
                {
                    Student_Table obj = new Student_Table()
                    {
                        id = id
                    };

                    Db.Entry(obj).State = EntityState.Deleted;
                    Db.SaveChanges();
                    TempData["Delete"] = "Data Delete Sucessfully!";
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
            return RedirectToAction("index");
        }

        public ActionResult ReportView()
        {
            using (Task_Student_ManagementEntities Db = new Task_Student_ManagementEntities())
            {
                return View(Db.sp_student().ToList());
            }

        }

    }
}