Create Procedure DeleteEmployeeById
@Id Int
As
Begin
Delete from Employee Where Id = @Id
End
