CREATE TABLE APPUSER(
	Id Int primary key identity(1,1),
	Username varchar(50),
	Password varchar(50),
	EmployeeId int ,
	EmailId varchar(100),
	IsActive bit,
	CreatedDate  datetime,
	CreatedBy int,
	UpdatedDate DateTime,
	UpdatedBy int,
	Foreign key (EmployeeId) references Employee(Id)
);