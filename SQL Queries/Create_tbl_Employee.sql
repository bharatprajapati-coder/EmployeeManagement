USE EmployeeManagement


CREATE TABLE Employee
(
Id INT  PRIMARY KEY  IDENTITY (1,1) ,
Firstname varchar(100) NOT NULL,
MiddleName varchar(100) ,
Lastname varchar(100)  NOT NULL,
Address1 varchar(200) ,
Address2  varchar(200) ,
City varchar(200) ,
StateId varchar(50) ,
Zip varchar(10) ,
JoiningDate DateTime NOT NULL,
DepartmentID INt  NOT NULL,
Salary int ,
HasLeft bit,
LeavingDate DateTime,
CreatedDate DateTime,
CreatedBy int,
UpdatedDate DateTime , 
UpdatedBy int

);
