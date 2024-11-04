using EmpManagement.Data;
using EmpManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpManagement.Controllers
{
    public class LoginController : Controller
    {

        private readonly EmployeeRepostiory _empRepository;

        public LoginController(IConfiguration configuration)
        {
            _empRepository = new EmployeeRepostiory(configuration);

        }


        #region Login Functionality

        #region Get Login Page
        public IActionResult Login()
        {
            return View();

        }
        #endregion


        #region Submit Login Page
        [HttpPost]
        public IActionResult ValidateLogin(Login obj)
        {
            string UserName = obj.userName;
            string Password = obj.password;
            int i = _empRepository.ValidateUser(UserName, Password);
            if(i == 1)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewBag.Message = "Please Enter Valid Credentials";
                return View("Login");
            }

            
        }
        #endregion

        #endregion
    }
}
