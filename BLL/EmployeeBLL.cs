using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data;
using DAL;
using ENTITY;

namespace BLL
{
    public class EmployeeBLL
    {
        public static DataDTO GetEmployeeDetails(bool isRefresh)
        {
            try
            {
                EmployeeData obj = EmployeeDAL.GetDetails(isRefresh);
                DataDTO data = new DataDTO();
                data.Employees = obj.Employees.Select(item => new EntityDTO
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

                data.Supervisors = obj.Supervisors.Select(item => new LookupDTO
                {
                    ID = item.ID,
                    NAME = item.NAME
                }).ToList();

                if (isRefresh)
                {
                    return data;
                }

                data.Designations = obj.Designations.Select(item => new LookupDTO
                {
                    ID = item.ID,
                    NAME = item.NAME
                }).ToList();

                data.Departments = obj.Departments.Select(item => new LookupDTO
                {
                    ID = item.ID,
                    NAME = item.NAME
                }).ToList();

                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool AddEmployee(EntityDTO employee, string operationType)
        {
            try
            {
                EmployeeEntity entity = new EmployeeEntity();
                entity.ID = employee.ID;
                entity.NAME = employee.NAME;
                entity.DESIGNATION_ID = employee.DESIGNATION_ID;
                entity.GENDER = employee.GENDER;
                entity.DEPARTMENT_ID = employee.DEPARTMENT_ID;
                entity.EXPERIENCE = employee.EXPERIENCE;
                entity.SKILLS = employee.SKILLS;
                entity.EMAIL_ID = employee.EMAIL_ID;
                entity.CONTACT_NUMBER = employee.CONTACT_NUMBER;
                entity.SUPERVISOR_ID = employee.SUPERVISOR_ID;
                entity.DATE_OF_BIRTH = employee.DATE_OF_BIRTH;
                entity.ADDRESS = employee.ADDRESS;
                entity.INSERTED_BY = employee.INSERTED_BY;
                entity.INSERTED_ON = employee.INSERTED_ON;
                entity.UPDATED_BY = employee.UPDATED_BY;
                entity.UPDATED_ON = employee.UPDATED_ON;
                EmployeeDAL.Put(entity, "EmployeeOps", operationType);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataDTO GetEmployee(int id)
        {
            try
            {
                EmployeeData employee = EmployeeDAL.Get(id, "EmployeeOps");
                DataDTO dto = new DataDTO();
                dto.Employee = new EntityDTO();
                dto.Employee.ID = employee.Entity.ID;
                dto.Employee.NAME = employee.Entity.NAME; ;
                dto.Employee.DESIGNATION_ID = employee.Entity.DESIGNATION_ID; ;
                dto.Employee.DESIGNATION = employee.Entity.DESIGNATION;
                dto.Employee.GENDER = employee.Entity.GENDER;
                dto.Employee.DEPARTMENT_ID = employee.Entity.DEPARTMENT_ID;
                dto.Employee.DEPARTMENT = employee.Entity.DEPARTMENT;
                dto.Employee.EXPERIENCE = employee.Entity.EXPERIENCE;
                dto.Employee.SKILLS = employee.Entity.SKILLS;
                dto.Employee.EMAIL_ID = employee.Entity.EMAIL_ID;
                dto.Employee.CONTACT_NUMBER = employee.Entity.CONTACT_NUMBER;
                dto.Employee.SUPERVISOR_ID = employee.Entity.SUPERVISOR_ID;
                dto.Employee.SUPERVISOR = employee.Entity.SUPERVISOR;
                dto.Employee.DATE_OF_BIRTH = employee.Entity.DATE_OF_BIRTH;
                dto.Employee.ADDRESS = employee.Entity.ADDRESS;

                dto.Supervisors = employee.Supervisors.Select(item => new LookupDTO
                {
                    ID = item.ID,
                    NAME = item.NAME
                }).ToList();

                #region NotRequired
                //dto.Designations = employee.Designations.Select(item => new LookupDTO
                //{
                //    ID = item.ID,
                //    NAME = item.NAME
                //}).ToList();

                //dto.Departments = employee.Departments.Select(item => new LookupDTO
                //{
                //    ID = item.ID,
                //    NAME = item.NAME
                //}).ToList();

                //dto.Supervisors = employee.Supervisors.Select(item => new LookupDTO
                //{
                //    ID = item.ID,
                //    NAME = item.NAME
                //}).ToList();
                #endregion
                return dto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static bool DeleteEmployee(int id)
        {
            try
            {
                EmployeeDAL.Remove(id, "EmployeeOps");
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
