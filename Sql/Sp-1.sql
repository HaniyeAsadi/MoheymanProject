CREATE PROCEDURE GetUsers
AS
BEGIN
    SELECT ID, UserName FROM Users ORDER BY ID DESC;
END