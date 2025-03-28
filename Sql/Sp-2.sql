CREATE PROCEDURE InsertUser
    @UserName NVARCHAR(50),
	@Password NVARCHAR(50)
AS
BEGIN
    INSERT INTO Users (UserName, Password)
    VALUES (@UserName, @Password);
END;