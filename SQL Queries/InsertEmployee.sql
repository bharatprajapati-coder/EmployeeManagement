Create Procedure AddEmployee
@FirstName varchar(100) ,
@MiddleName varchar(100) = null,
@LastName varchar(100) ,
@Address1 Varchar(200) =null,
@Address2 Varchar(200) =null,
@City varchar(200) = null,
@StateId Int = null,
@Zip varchar(10) = null,
@JoiningDate DateTime,
@DepartmentId int ,
@salary int = 0,
@CreatedDate DateTime = null,
@CreatedBy int =null
AS 
Begin
Insert Into Employee
Values(
	@FirstName,
	@MiddleName,
	@LastName,
	@Address1,
	@Address2,
	@City,
	@StateId,
	@Zip,
	@JoiningDate,
	@DepartmentId,
	@salary,
	@CreatedDate,
	@CreatedBy
);
END
