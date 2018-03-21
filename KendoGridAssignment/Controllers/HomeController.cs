using BLL;
using DTO;
using KendoGridAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoGridAssignment.Controllers
{
    public class HomeController : Controller
    {
        public static bool isRefresh;
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGridData()
        {
            try
            {
                isRefresh = false;
                DataModel model = new DataModel();
                DataDTO dto = EmployeeBLL.GetEmployeeDetails(isRefresh);
                MapEmployeesSupervisors(model, dto);

                model.departments = dto.Departments.Select(item => new Lookup
                {
                    ID = item.ID,
                    NAME = item.NAME
                }).ToList();

                model.designations = dto.Designations.Select(item => new Lookup
                {
                    ID = item.ID,
                    NAME = item.NAME
                }).ToList();


                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Message = "There was a problem fetching the records from data source";
                ViewBag.Error = e;
                return View("Index");
            }
        }

        private static void MapEmployeesSupervisors(DataModel model, DataDTO dto)
        {
            model.employees = dto.Employees.Select(item => new Employee
            {
                ID = item.ID,
                NAME = item.NAME,
                DESIGNATION_ID = item.DESIGNATION_ID,
                DESIGNATION = item.DESIGNATION,
                GENDER = item.GENDER,
                DEPARTMENT_ID = item.DEPARTMENT_ID,
                DEPARTMENT = item.DEPARTMENT,
                EXPERIENCE = item.EXPERIENCE,
                SKILLS = item.SKILLS,
                EMAIL_ID = item.EMAIL_ID,
                CONTACT_NUMBER = item.CONTACT_NUMBER,
                SUPERVISOR_ID = item.SUPERVISOR_ID,
                SUPERVISOR = item.SUPERVISOR,
                DATE_OF_BIRTH = item.DATE_OF_BIRTH,
                ADDRESS = item.ADDRESS,
                INSERTED_BY = item.INSERTED_BY,
                INSERTED_ON = item.INSERTED_ON,
                UPDATED_BY = item.UPDATED_BY,
                UPDATED_ON = item.UPDATED_ON
            }).ToList();

            model.supervisors = dto.Supervisors.Select(item => new Lookup
            {
                ID = item.ID,
                NAME = item.NAME
            }).ToList();
        }

        [HttpGet]
        public JsonResult GetUpdatedData()
        {
            try
            {
                DataModel model = new DataModel();
                DataDTO dto = EmployeeBLL.GetEmployeeDetails(true);
                MapEmployeesSupervisors(model, dto);
                

                return Json(new { isSuccess = true, data = model.employees, svsource=model.supervisors },JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, data = e },JsonRequestBehavior.AllowGet);
            }

        }
    
        //Note: If you find internal server error on browser console, It may be because parameters passed to procedure are incorrect, 
        //not matching, illegal values, permission issues, wrong way of passing the value to addWithValue method on parameters
        public JsonResult AddNewEmployee(EntityDTO obj, string operationType)
        {
            try
            {
                EmployeeBLL.AddEmployee(obj, operationType);
                return Json(new { isSuccess = true, message="Employee details are saved successfully" });

            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, message="There was a problem saving the record. Additional Info: " +e });
            }
            
        }

        public JsonResult GetEmployee(int id)
        {
            try
            {
                #region Mapping
                DataDTO dto = EmployeeBLL.GetEmployee(id);
                Employee result = new Employee();
                result.NAME = dto.Employee.NAME;
                result.ADDRESS = dto.Employee.ADDRESS;
                result.CONTACT_NUMBER = dto.Employee.CONTACT_NUMBER;
                result.DATE_OF_BIRTH = dto.Employee.DATE_OF_BIRTH;
                result.DEPARTMENT = dto.Employee.DEPARTMENT;
                result.DEPARTMENT_ID = dto.Employee.DEPARTMENT_ID;
                result.DESIGNATION = dto.Employee.DESIGNATION;
                result.DESIGNATION_ID = dto.Employee.DESIGNATION_ID;
                result.EMAIL_ID = dto.Employee.EMAIL_ID;
                result.EXPERIENCE = dto.Employee.EXPERIENCE;
                result.GENDER = dto.Employee.GENDER;
                result.ID = dto.Employee.ID;
                result.INSERTED_BY = dto.Employee.INSERTED_BY;
                result.INSERTED_ON = dto.Employee.INSERTED_ON;
                result.SKILLS = dto.Employee.SKILLS;
                result.SUPERVISOR = dto.Employee.SUPERVISOR;
                result.SUPERVISOR_ID = dto.Employee.SUPERVISOR_ID;
                result.UPDATED_BY = dto.Employee.UPDATED_BY;
                result.UPDATED_ON = dto.Employee.UPDATED_ON;

                List<Lookup> supervisors = new List<Lookup>();
                supervisors = dto.Supervisors.Select(item => new Lookup
                {
                    ID=item.ID,
                    NAME=item.NAME
                }).ToList();
                #endregion
                return Json(new { isSuccess = true, data = result, svsource=supervisors});
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, data = e },JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteEmployee(int id)
        {
            try
            {
                EmployeeBLL.DeleteEmployee(id);
                return Json(new {isSuccess=true, message="One record deleted successfully" });
            }
            catch(Exception e)
            { 
                return Json(new { isSuccess = false, message ="There was a problem deleting the record. Information is: " +e });
            }
        }


        #region Old Code
    
        //[HttpGet]
        //public JsonResult GetAllEmp()
        //{
        //    #region Stupid Code
        //    /*
        //    model.employees = EmployeeBLL.GetEmployeeDetails().Employees.Select(item => new Employee
        //    {
        //        ID = item.ID,
        //        NAME = item.NAME,
        //        DESIGNATION_ID = item.DESIGNATION_ID,
        //        DESIGNATION = item.DESIGNATION,
        //        GENDER = item.GENDER,
        //        DEPARTMENT_ID = item.DEPARTMENT_ID,
        //        DEPARTMENT = item.DEPARTMENT,
        //        EXPERIENCE = item.EXPERIENCE,
        //        SKILLS = item.SKILLS,
        //        EMAIL_ID = item.EMAIL_ID,
        //        CONTACT_NUMBER = item.CONTACT_NUMBER,
        //        SUPERVISOR_ID = item.SUPERVISOR_ID,
        //        SUPERVISOR = item.SUPERVISOR,
        //        DATE_OF_BIRTH = item.DATE_OF_BIRTH,
        //        ADDRESS = item.ADDRESS,
        //        INSERTED_BY = item.INSERTED_BY,
        //        INSERTED_ON = item.INSERTED_ON,
        //        UPDATED_BY = item.UPDATED_BY,
        //        UPDATED_ON = item.UPDATED_ON
        //    }).ToList();

        //    model.departments = EmployeeBLL.GetEmployeeDetails().Departments.Select(item => new Lookup
        //    {
        //        ID = item.ID,
        //        NAME = item.NAME
        //    }).ToList();

        //    model.designations = EmployeeBLL.GetEmployeeDetails().Designations.Select(item => new Lookup
        //    {
        //        ID = item.ID,
        //        NAME = item.NAME
        //    }).ToList();

        //    model.supervisors = EmployeeBLL.GetEmployeeDetails().Supervisors.Select(item => new Lookup
        //    {
        //        ID = item.ID,
        //        NAME = item.NAME
        //    }).ToList();*/
        //    #endregion

        //    var json = new { isSuccess = true, data = employees };
        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}

        #endregion
    }
}