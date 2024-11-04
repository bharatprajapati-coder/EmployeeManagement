ALTER PROCEDURE List_Employee
    @FirstName NVARCHAR(255) = NULL,
    @LastName NVARCHAR(255) = NULL,
    @City NVARCHAR(255) = NULL,
    @StateId INT = NULL,
    @DepartmentId INT = NULL,
    @JoiningDate DATETIME = NULL,
    @LeavingDate DATETIME = NULL,
    @HasLeft BIT = 0
AS
BEGIN
    SELECT 
        ROW_NUMBER() OVER (ORDER BY Employee.Id DESC) AS RowNo,      
        Employee.Id AS EmployeeId,      
        FirstName,      
        MiddleName,      
        LastName,      
        Address1,      
        Address2,      
        City,      
        Employee.StateId,      
        S.StateName AS StateName,    
        D.Name AS DepartmentName,    
        Zip,      
        FORMAT(JoiningDate, 'dd/MM/yyyy') AS JoiningDate,      
        DepartmentId,      
        Salary      
    FROM 
        Employee      
    LEFT JOIN 
        State S ON S.StateId = Employee.StateId    
    INNER JOIN 
        Department D ON D.Id = Employee.DepartmentID    
    WHERE 
        Employee.HasLeft = @HasLeft AND
        (@FirstName IS NULL OR FirstName LIKE '%' + @FirstName + '%') AND
        (@LastName IS NULL OR LastName LIKE '%' + @LastName + '%') AND
        (@City IS NULL OR City LIKE '%' + @City + '%') AND
        (@StateId IS NULL OR Employee.StateId = @StateId) AND
        (@DepartmentId IS NULL OR Employee.DepartmentID = @DepartmentId) AND
        (@JoiningDate IS NULL OR JoiningDate = @JoiningDate) AND
        (@LeavingDate IS NULL OR LeavingDate = @LeavingDate)
END
