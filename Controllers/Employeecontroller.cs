using EmpManagement.Data;
using EmpManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace EmpManagement.Controllers
{
    public class Employeecontroller : Controller
    {
        private readonly EmployeeRepostiory empRepository;
        public Employeecontroller(IConfiguration configuration)
        {
            empRepository = new EmployeeRepostiory(configuration);
        }

        #region Employee CRUD
        public ViewResult Index()
        {
            ViewBag.StateList = new SelectList(empRepository.getAllStates(), "StateId", "StateName");
            ViewBag.DepartmentList = new SelectList(empRepository.getAllDepartments(), "DepartmentId", "DepartmentName");
            if (TempData.Count > 0)
            {
                ViewBag.Status = TempData["Status"].ToString();
                ViewBag.UserName = TempData["UserName"];
                ViewBag.Password = TempData["Password"];
            }
            return View();

        }

        #region List Of Employee Records

        [HttpPost]
        public JsonResult GetEmpList(string? FirstName, string? LastName, string? City, int? StateId, int? DepartmentId, string? JoiningDate, string? LeavingDate, bool? hasLeft)
        {
            int hasleftInInt = hasLeft == true ? 1 : 0;
            var data = empRepository.listEmployee(FirstName, LastName, City, StateId, DepartmentId, JoiningDate, LeavingDate, hasleftInInt);
            List<empList> employeeList = new List<empList>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                empList obj = new empList
                {
                    RowNo = Convert.ToInt32(data.Rows[i]["RowNo"]),
                    Id = Convert.ToInt32(data.Rows[i]["EmployeeId"]),
                    FirstName = data.Rows[i]["FirstName"] != DBNull.Value ? Convert.ToString(data.Rows[i]["FirstName"]) : string.Empty,
                    LastName = data.Rows[i]["LastName"] != DBNull.Value ? Convert.ToString(data.Rows[i]["LastName"]) : string.Empty,

                    Address1 = data.Rows[i]["Address1"] != DBNull.Value ? Convert.ToString(data.Rows[i]["Address1"]) : string.Empty,
                    Address2 = data.Rows[i]["Address2"] != DBNull.Value ? Convert.ToString(data.Rows[i]["Address2"]) : string.Empty,
                    City = data.Rows[i]["City"] != DBNull.Value ? Convert.ToString(data.Rows[i]["City"]) : string.Empty,
                    StateName = data.Rows[i]["StateName"] != DBNull.Value ? Convert.ToString(data.Rows[i]["StateName"]) : string.Empty,
                    DepartmentName = data.Rows[i]["DepartmentName"] != DBNull.Value ? Convert.ToString(data.Rows[i]["DepartmentName"]) : string.Empty,
                    Zip = data.Rows[i]["Zip"] != DBNull.Value ? Convert.ToString(data.Rows[i]["Zip"]) : string.Empty,
                    Salary = data.Rows[i]["Salary"] != DBNull.Value ? Convert.ToInt32(data.Rows[i]["Salary"]) : 0,
                    JoiningDate = data.Rows[i]["JoiningDate"] != DBNull.Value ? Convert.ToString(data.Rows[i]["JoiningDate"]) : string.Empty,
                    LeavingDate = data.Rows[i]["LeavingDate"] != DBNull.Value ? Convert.ToString(data.Rows[i]["LeavingDate"]) : string.Empty
                };
                employeeList.Add(obj);

            }

            return Json(new
            {
                data = employeeList.ToList()
            });

        }

        #endregion

        #region Inserting an Employee
        [HttpGet]
        public ViewResult AddEmployee()
        {
            ViewBag.DepartmentList = new SelectList(empRepository.getAllDepartments(), "DepartmentId", "DepartmentName");
            ViewBag.StateList = new SelectList(empRepository.getAllStates(), "StateId", "StateName");
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee obj)
        {
            if (obj == null)
            {
                TempData["ErrorMessage"] = "Employee data cannot be null.";
                return View();
            }

            try
            {

                int result = empRepository.AddEmployee(obj);
                if (result == 1)
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to add employee. Please try again.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                AppConstant.writeLog(ex.ToString());

                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";

                return View();
            }
        }

        #endregion


        #region  Edit

        #region Edit Employee PAge
        [HttpGet]
        public IActionResult GetOneEmployee(int Id)
        {
            try
            {
                Employee obj = empRepository.GetOneEmployee(Id);


                SelectList stateList = new SelectList(empRepository.getAllStates(), "StateId", "StateName");
                SelectList departmentList = new SelectList(empRepository.getAllDepartments(), "DepartmentId", "DepartmentName");
                return Json(new
                {
                    obj,
                    stateList,
                    departmentList

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Updating Employee
        [HttpPost]
        public JsonResult UpdateEmployee(string Obj)
        {
            EmployeeUpdate updateObj = new EmployeeUpdate();
            try
            {
                if (!string.IsNullOrEmpty(Obj))
                {

                    updateObj = JsonSerializer.Deserialize<EmployeeUpdate>(Obj);

                }

                if (updateObj != null)
                {

                    int i = empRepository.UpdateEmployee(updateObj);
                    if (i > 0)
                    {
                        return Json(new { Message = "Record Updated Successfully." });
                    }
                }

                return Json(new { Message = "Something Went Wrong!." });

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region Delete Employee
        [HttpDelete]
        public JsonResult DeleteEmployee(int Id)
        {
            if (Id > 0 && Id != null)
            {
                int i = empRepository.DeleteEmployee(Id);
                if (i == 1)
                {
                    return Json(new { Message = "Data deleted Successfully" });
                }
                else
                {
                    return Json(new { Message = "Something went wrong" });
                }
            }
            else
            {
                return Json(new { Message = "Please Select Valid Record" });
            }
        }
        #endregion

        #endregion

        #region Log Out 
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }
        #endregion
    }
}
