using System.ComponentModel.DataAnnotations;

namespace EmpManagement.Models
{
    public class Employee
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter First Name")]
        public string FirstName { get; set; }   
        public string? MiddleName { get; set; }
        [Required(ErrorMessage ="Enter Last Name")]
        public string LastName { get; set; }   

        public string? Address1 { get; set; }
        public string? Address2 { get; set;}
        public string? City { get; set; }
        
        public int? StateId { get; set; }
        public string? Zip { get; set;}

        [Required(ErrorMessage ="Select Joining Date")]
        public DateTime JoiningDate { get;set ; }

        [Required(ErrorMessage ="Select Department")]
        public int DepartmentId { get;set; }
        public int? Salary { get; set; }    
        public bool? hasLeft { get; set; }
        public string? LeavingDate { get; set; }
      
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; } 
        public DateTime? UpdateDate { get; set; }   
        public int? UpdatedBy { get;set; }
      
        public string? State { get; set; }  
        public string? DepartmentName { get; set; }

    }


    public class empList
    {
        public int RowNo { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }        

        public string JoiningDate { get; set; }
        public string DepartmentName { get; set; }
        public int Salary { get; set; }
        public string LeavingDate { get; set; }
        public string? StateName { get; set; }
   
        public string Zip { get; set; }
      
    }

    public class EmployeeUpdate
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        public string LastName { get; set; }

        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }

        public int? StateId { get; set; }
        public string? Zip { get; set; }

        [Required(ErrorMessage = "Enter Department Id")]
        public int DepartmentId { get; set; }
        public int? Salary { get; set; }
        public bool? hasLeft { get; set; }
        
        public string LeavingDate { get; set; }

     
        public DateTime? UpdateDate { get; set; }
        public int? UpdatedBy { get; set; }


    }


}
