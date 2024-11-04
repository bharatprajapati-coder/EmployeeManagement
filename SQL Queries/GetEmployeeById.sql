/*
	EXEC GetEmployeeById 20;

*/

ALTER Procedure GetEmployeeById    
@Id Int    
AS    
BEGIN    
Select      
FirstName,    
MiddleName,    
LastName,    
Address1,    
Address2,    
City,    
S.StateId,    
S.StateName,  
D.Id as DepartmentId,  
D.Name as DepartmentName,  
Format(LeavingDate ,'dd/MM/yyyy')AS LeavingDate,    
HasLeft     
from Employee    
  LEFt Join State S on S.StateId = Employee.StateId  
  Inner Join Department D on D.Id = Employee.DepartmentId  
where Employee.Id = @Id    
End;    
    
    
    
    
    