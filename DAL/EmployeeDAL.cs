
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;

namespace DAL
{
    public class EmployeeDAL
    {
        public static string ConString = string.Empty;
        public static SqlConnection con = null;
        static EmployeeDAL()
        {
            ConString = ConfigurationManager.ConnectionStrings["ConnectTrainees"].ConnectionString;
        }

        public static EmployeeData GetDetails(bool isRefresh)
        {
            try
            {
                string ProcName = "EmployeeOps";
                con= new SqlConnection(ConString);                            
                SqlCommand cmd = new SqlCommand(ProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TYPE", "SELECT");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();                                                             //open database connection
                }
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataSet tab = new DataSet();
                sd.Fill(tab);
                con.Close();                                                                //Close database connection

                EmployeeData data = new EmployeeData();
                data.Employees = new List<EmployeeEntity>();

                if (tab.Tables.Count > 0)
                {
                    #region GetEmployees
                    foreach (DataRow row in tab.Tables[0].Rows)
                    {
                        data.Employees.Add(new EmployeeEntity
                        {
                            ID = Convert.ToInt32(row["ID"]),
                            NAME = row["NAME"].ToString(),
                            DESIGNATION_ID = Convert.ToInt32(row["DESIGNATION_ID"]),
                            DESIGNATION = row["DESIGNATION"].ToString(),
                            GENDER = row["GENDER"].ToString(),
                            DEPARTMENT_ID = Convert.ToInt32(row["DEPARTMENT_ID"]),
                            DEPARTMENT = row["DEPARTMENT"].ToString(),
                            EXPERIENCE = Convert.ToInt32(row["EXPERIENCE"]),
                            SKILLS = row["SKILLS"].ToString(),
                            EMAIL_ID = row["EMAIL_ID"].ToString(),
                            CONTACT_NUMBER = row["CONTACT_NUMBER"].ToString(),
                            SUPERVISOR_ID = row["SUPERVISOR_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SUPERVISOR_ID"]),
                            SUPERVISOR = row["SUPERVISOR"].ToString(),
                            DATE_OF_BIRTH = Convert.ToDateTime(row["DATE_OF_BIRTH"].ToString()),
                            ADDRESS = row["ADDRESS"].ToString(),
                            INSERTED_BY = row["INSERTED_BY"] == DBNull.Value ? null : row["INSERTED_BY"].ToString(),
                            INSERTED_ON = row["INSERTED_ON"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["INSERTED_ON"].ToString()),
                            UPDATED_BY = row["UPDATED_BY"] == DBNull.Value ? null : row["UPDATED_BY"].ToString(),
                            UPDATED_ON = row["UPDATED_ON"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["UPDATED_ON"].ToString())
                        });


                    }

                    data.Supervisors = new List<EmployeeLookup>();
                    foreach (DataRow row in tab.Tables[3].Rows)
                    {
                        data.Supervisors.Add(new EmployeeLookup
                        {
                            ID = Convert.ToInt32(row["ID"]),
                            NAME = row["NAME"].ToString()
                        });
                    }
                    #endregion

                    if (isRefresh)                                                   //check if request for data to refresh grid
                    {
                        return data;
                    }

                    data.Designations = new List<EmployeeLookup>();

                        foreach (DataRow row in tab.Tables[1].Rows)
                        {
                            data.Designations.Add(new EmployeeLookup
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                NAME = row["NAME"].ToString()
                            });
                        }


                        data.Departments = new List<EmployeeLookup>();
                        foreach (DataRow row in tab.Tables[2].Rows)
                        {
                            data.Departments.Add(new EmployeeLookup
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                NAME = row["NAME"].ToString()
                            });
                        }

                        
                }
                return data;
            }                                                                      //Try block ends
            catch (Exception e)
            {
                throw e;
            }                                                                      //catch block ends
           
        }                                                                          //Method GetDetails ends


        public static bool Put(EmployeeEntity employee, string ProcName, string operationType)
        {
            try
            {
                con = new SqlConnection(ConString);
                //con = new SqlConnection(ConString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(ProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (operationType == "Save")
                {
                    cmd.Parameters.AddWithValue("@TYPE", "INSERT");
                    cmd.Parameters.AddWithValue("@NAME", employee.NAME);
                    cmd.Parameters.AddWithValue("@DESIGNATION_ID", employee.DESIGNATION_ID);
                    cmd.Parameters.AddWithValue("@GENDER", employee.GENDER);
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", employee.DEPARTMENT_ID);
                    cmd.Parameters.AddWithValue("@EXPERIENCE", employee.EXPERIENCE);
                    cmd.Parameters.AddWithValue("@SKILLS", employee.SKILLS);
                    cmd.Parameters.AddWithValue("@EMAIL_ID", employee.EMAIL_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NUMBER", employee.CONTACT_NUMBER);
                    cmd.Parameters.AddWithValue("@SUPERVISOR_ID", employee.SUPERVISOR_ID);
                    cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", employee.DATE_OF_BIRTH);
                    cmd.Parameters.AddWithValue("@ADDRESS", employee.ADDRESS);
                    cmd.Parameters.AddWithValue("@INSERTED_BY", employee.INSERTED_BY);
                    cmd.Parameters.AddWithValue("@INSERTED_ON", employee.INSERTED_ON);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", "");
                    cmd.Parameters.AddWithValue("@UPDATED_ON", employee.UPDATED_ON);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Type", operationType);
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@NAME", employee.NAME);
                    cmd.Parameters.AddWithValue("@DESIGNATION_ID", employee.DESIGNATION_ID);
                    cmd.Parameters.AddWithValue("@GENDER", employee.GENDER);
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", employee.DEPARTMENT_ID);
                    cmd.Parameters.AddWithValue("@EXPERIENCE", employee.EXPERIENCE);
                    cmd.Parameters.AddWithValue("@SKILLS", employee.SKILLS);
                    cmd.Parameters.AddWithValue("@EMAIL_ID", employee.EMAIL_ID);
                    cmd.Parameters.AddWithValue("@CONTACT_NUMBER", employee.CONTACT_NUMBER);
                    cmd.Parameters.AddWithValue("@SUPERVISOR_ID", employee.SUPERVISOR_ID);
                    cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", employee.DATE_OF_BIRTH);
                    cmd.Parameters.AddWithValue("@ADDRESS", employee.ADDRESS);
                    cmd.Parameters.AddWithValue("@UPDATED_BY", employee.UPDATED_BY);
                    cmd.Parameters.AddWithValue("@UPDATED_ON", employee.UPDATED_ON);
                }

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        public static EmployeeData Get(int id, string ProcName)
        {
            try
            {
                con = new SqlConnection(ConString);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(ProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", "GET");
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                con.Close();

                EmployeeData data = new EmployeeData();
                data.Entity = new EmployeeEntity();
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        data.Entity.ID = Convert.ToInt32(row["ID"]);
                        data.Entity.NAME = row["NAME"].ToString();
                        data.Entity.DESIGNATION_ID = Convert.ToInt32(row["DESIGNATION_ID"]);
                        data.Entity.DESIGNATION = row["DESIGNATION"].ToString();
                        data.Entity.GENDER = row["GENDER"].ToString();
                        data.Entity.DEPARTMENT_ID = Convert.ToInt32(row["DEPARTMENT_ID"]);
                        data.Entity.DEPARTMENT = row["DEPARTMENT"].ToString();
                        data.Entity.EXPERIENCE = Convert.ToInt32(row["EXPERIENCE"]);
                        data.Entity.SKILLS = row["SKILLS"].ToString();
                        data.Entity.EMAIL_ID = row["EMAIL_ID"].ToString();
                        data.Entity.CONTACT_NUMBER = row["CONTACT_NUMBER"].ToString();
                        data.Entity.SUPERVISOR_ID = row["SUPERVISOR_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SUPERVISOR_ID"]);
                        data.Entity.SUPERVISOR = row["SUPERVISOR"].ToString();
                        data.Entity.DATE_OF_BIRTH = Convert.ToDateTime(row["DATE_OF_BIRTH"].ToString());
                        data.Entity.ADDRESS = row["ADDRESS"].ToString();
                    }

                    data.Supervisors = new List<EmployeeLookup>();
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            data.Supervisors.Add(new EmployeeLookup
                            {
                                ID = Convert.ToInt32(row["ID"]),
                                NAME = row["NAME"].ToString()
                            });
                        }

                    #region NotRequired
                    //    data.Designations = new List<EmployeeLookup>();

                    //    foreach (DataRow row in ds.Tables[1].Rows)
                    //    {
                    //        data.Designations.Add(new EmployeeLookup
                    //        {
                    //            ID = Convert.ToInt32(row["ID"]),
                    //            NAME = row["NAME"].ToString()
                    //        });
                    //    }


                    //    data.Departments = new List<EmployeeLookup>();
                    //    foreach (DataRow row in ds.Tables[2].Rows)
                    //    {
                    //        data.Departments.Add(new EmployeeLookup
                    //        {
                    //            ID = Convert.ToInt32(row["ID"]),
                    //            NAME = row["NAME"].ToString()
                    //        });
                    //    }

                    //    data.Supervisors = new List<EmployeeLookup>();
                    //    foreach (DataRow row in ds.Tables[3].Rows)
                    //    {
                    //        data.Supervisors.Add(new EmployeeLookup
                    //        {
                    //            ID = Convert.ToInt32(row["ID"]),
                    //            NAME = row["NAME"].ToString()
                    //        });
                    //    }
                    //}
                    #endregion

                }
                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        public static bool Remove(Int32 id, string ProcName)
        {
            try
            {
                con = new SqlConnection(ConString);
                SqlCommand cmd = new SqlCommand(ProcName, con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", "DELETE");
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            
            
        }
    }
}