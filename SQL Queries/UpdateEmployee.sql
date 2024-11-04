CREATE Procedure UpdateEmployee
@Id Int,
@FirstName varchar(100),
@MiddleName varchar(100)  = null,
@LastName varchar(100),
@Address1 varchar(200) = null,
@Address2 varchar(200) = null,
@City varchar(200) = null,
@StateId int =null, 
@Zip varchar(10) = null,
@DepartmentId int,
@salary int = null,
@LeavingDate DateTime = null,
@UpdatedDate DateTime = null,
@UpdatedBy Int = null,
@HasLeft Bit = null
AS
BEGIN
Update Employee 
SET 
FirstName= @FirstName,
MiddleName = @MiddleName,
Lastname = @LastName ,
Address1 = @Address1 ,
Address2 = @Address2 ,
City = @City,
StateId = @StateId,
Zip = @zip,
DepartmentId = @DepartmentID,
salary  = @Salary,
LeavingDate= @LeavingDate,
UpdatedDate= @UpdatedDate,
UpdatedBy = @UpdatedBy
HasLeft = @HasLeft
where Id = @Id
End;






