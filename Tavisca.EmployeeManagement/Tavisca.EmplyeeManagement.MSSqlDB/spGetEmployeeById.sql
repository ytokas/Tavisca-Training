CREATE PROCEDURE [dbo].[spGetEmployeeById]
	@EmployeeId int
AS
BEGIN
	SELECT Title, FirstName, LastName, Email, Phone, JoiningDate FROM Employee 
	WHERE Id = @EmployeeId
END
