using System.ComponentModel.DataAnnotations;

namespace EmpManagement.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Enter User Name")]
        public string userName { get;set; }

        [Required(ErrorMessage ="Enter Password")]
        public string password { get;set; }     
    }

}
