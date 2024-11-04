/*
	EXEC ValidateUser 'Bharat' , '123';

*/
CREATE PROCEDURE ValidateUser @UserName VARCHAR(50)
	,@Password VARCHAR(50)
AS
BEGIN
	DECLARE @Status INT;

	IF EXISTS (
			SELECT Id
			FROM APPUSER Ap
			WHERE Ap.Username = @UserName
				AND Ap.Password = @Password
			)
		SET @Status = 1;
	ELSE
		SET @Status = 0;

	SELECT @Status;
END