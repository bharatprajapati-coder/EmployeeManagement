using EmpManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Threading.Tasks.Dataflow;

namespace EmpManagement.Data
{
    public  class EmployeeRepostiory
    {
        private readonly Database _db;
        public  EmployeeRepostiory(IConfiguration configuration)
        {
            _db = EmployeeHelper.ConfigureDb(configuration);    
        }

        public DataTable  listEmployee(string? FirstName  ,string? LastName, string? City, int? StateId, int? DepartmentId, string? JoiningDate, string? LeavingDate, int? hasLeft)
        {
            try
            {
                List<empList> employeeList = new List<empList>();
                
                DbCommand command = _db.GetStoredProcCommand("List_Employee");
                _db.AddInParameter(command, "FirstName", DbType.String, FirstName);
                _db.AddInParameter(command, "LastName", DbType.String, LastName);
                _db.AddInParameter(command, "City", DbType.String, City);
                _db.AddInParameter(command, "StateId", DbType.Int32, StateId);
                _db.AddInParameter(command, "DepartmentId", DbType.Int32, DepartmentId);
                _db.AddInParameter(command, "JoiningDate", DbType.DateTime, JoiningDate);
                _db.AddInParameter(command, "LeavingDate", DbType.DateTime, LeavingDate);
                _db.AddInParameter(command, "HasLeft", DbType.Int32, hasLeft);
                DataTable dt = _db.ExecuteDataSet(command).Tables[0];

                return dt;
                
            }
            catch (Exception ex) {
                throw;
            }
        }

        public int AddEmployee(Employee obj)
        {
            var command = _db.GetStoredProcCommand("AddEmployee");
            _db.AddInParameter(command, "FirstName", DbType.String, obj.FirstName);
            _db.AddInParameter(command, "MiddleName", DbType.String, obj.MiddleName);
            _db.AddInParameter(command, "LastName", DbType.String, obj.LastName);
            _db.AddInParameter(command, "Address1", DbType.String, obj.Address1);
            _db.AddInParameter(command, "Address2", DbType.String, obj.Address2);
            _db.AddInParameter(command, "City", DbType.String, obj.City);
            _db.AddInParameter(command, "StateId", DbType.Int32, obj.StateId);
            _db.AddInParameter(command, "Zip", DbType.String, obj.Zip);
            _db.AddInParameter(command, "JoiningDate", DbType.DateTime, obj.JoiningDate);
            _db.AddInParameter(command, "DepartmentId", DbType.Int32, obj.DepartmentId);
            _db.AddInParameter(command, "Salary", DbType.Int32, obj.Salary);
            _db.AddInParameter(command, "CreatedDate", DbType.DateTime, DateTime.Now);
            _db.AddInParameter(command, "HasLeft", DbType.Boolean, false);
            //_db.AddInParameter(command, "CreatedBy", DbType.Int32, obj);

            int i = _db.ExecuteNonQuery(command);

            return i;       
        }


        public List<Department> getAllDepartments()
        {
            var command = _db.GetStoredProcCommand("GetDepartments");
            IDataReader reader = _db.ExecuteReader(command);
            List<Department> list = new List<Department>();

            while (reader.Read())
            {
                list.Add(new Department
                {
                    DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                    DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName"))
                });
            }

            return list;
        }

        public List<State> getAllStates()
        {
            var command = _db.GetStoredProcCommand("GetAllStates");
            IDataReader reader = _db.ExecuteReader(command);
            List<State> list = new List<State>();

            while (reader.Read())
            {
                list.Add(new State
                {
                    StateId = reader.GetInt32(reader.GetOrdinal("StateId")),
                    StateName= reader.GetString(reader.GetOrdinal("StateName"))
                });
            }

            return list;
        }


        public int DeleteEmployee(int Id)
        {
            var command = _db.GetStoredProcCommand("DeleteEmployeeById");
            _db.AddInParameter(command, "Id", DbType.Int32, Id);
            int i = _db.ExecuteNonQuery(command);
            return i;

        }


        public Employee GetOneEmployee(int Id)
        {
            var command = _db.GetStoredProcCommand("GetEmployeeById");
            _db.AddInParameter(command , "Id" , DbType.Int32, Id);
            Employee employee = new Employee();
            var reader = _db.ExecuteReader(command);
            while (reader.Read())
            {
                employee = new Employee {
                    FirstName = !reader.IsDBNull(reader.GetOrdinal("FirstName")) ? reader.GetString(reader.GetOrdinal("FirstName")) : null,
                MiddleName = !reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? reader.GetString(reader.GetOrdinal("MiddleName")) : null,
                    LastName = !reader.IsDBNull(reader.GetOrdinal("LastName")) ? reader.GetString(reader.GetOrdinal("LastName")) : null,
                    Address1 = !reader.IsDBNull(reader.GetOrdinal("Address1")) ? reader.GetString(reader.GetOrdinal("Address1")) : null,
                    Address2 = !reader.IsDBNull(reader.GetOrdinal("Address2")) ?  reader.GetString(reader.GetOrdinal("Address2")) : null,
                    City = !reader.IsDBNull(reader.GetOrdinal("City")) ?  reader.GetString(reader.GetOrdinal("City")) : null, 
                    StateId = !reader.IsDBNull(reader.GetOrdinal("StateId")) ?  reader.GetInt32(reader.GetOrdinal("StateId")) : 0,
                    DepartmentId = !reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? reader.GetInt32(reader.GetOrdinal("DepartmentId")) : 0,
                    LeavingDate = !reader.IsDBNull(reader.GetOrdinal("LeavingDate")) ? reader.GetString(reader.GetOrdinal("LeavingDate")) : null,
                    hasLeft = reader.GetBoolean(reader.GetOrdinal("HasLeft"))
                };

               
            }
            return employee;
        }



        public int UpdateEmployee(EmployeeUpdate obj)
        {
            var command = _db.GetStoredProcCommand("UpdateEmployee");
            _db.AddInParameter(command, "Id", DbType.Int32, obj.Id);
            _db.AddInParameter(command, "FirstName", DbType.String, obj.FirstName);
            _db.AddInParameter(command, "MiddleName", DbType.String, obj.MiddleName);
            _db.AddInParameter(command, "LastName", DbType.String, obj.LastName);
            _db.AddInParameter(command, "Address1", DbType.String, obj.Address1);
            _db.AddInParameter(command, "Address2", DbType.String, obj.Address2);
            _db.AddInParameter(command, "City", DbType.String, obj.City);
            _db.AddInParameter(command, "StateId", DbType.Int32, obj.StateId);
            _db.AddInParameter(command, "Zip", DbType.String, obj.Zip);
            _db.AddInParameter(command, "DepartmentId", DbType.Int32, obj.DepartmentId);
            _db.AddInParameter(command, "Salary", DbType.Int32, obj.Salary);
            _db.AddInParameter(command, "UpdatedDate", DbType.DateTime, DateTime.Now);
            DateTime? Leavingdate = null;
            if (!string.IsNullOrEmpty(obj.LeavingDate))
            {
                 Leavingdate = DateTime.ParseExact(obj.LeavingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            }
            _db.AddInParameter(command, "LeavingDate", DbType.DateTime, Leavingdate);
            _db.AddInParameter(command, "UpdatedBy", DbType.Int32, obj.UpdatedBy);
            _db.AddInParameter(command, "HasLeft", DbType.Boolean, obj.hasLeft);


            int i = _db.ExecuteNonQuery(command);
            return i;
        }



        public int ValidateUser(string UserName , string Password)
        {
            try
            {
                var command = _db.GetStoredProcCommand("ValidateUser");
                _db.AddInParameter(command, "UserName", DbType.String, UserName);
                _db.AddInParameter(command, "Password", DbType.String, Password);
                int i = Convert.ToInt32(_db.ExecuteScalar(command));
                return i;
            }
            catch
            {
                throw;
            }
        }
    }
}
