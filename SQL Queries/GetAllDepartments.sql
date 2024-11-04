Create Procedure GetDepartments
As
Begin
Select 
[ID] AS DepartmentId,
[Name] as DepartmentName
From Department
END