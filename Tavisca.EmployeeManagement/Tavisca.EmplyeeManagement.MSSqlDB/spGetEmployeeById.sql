CREATE PROCEDURE [dbo].[spGetEmployeeById]
	@EmployeeId int
AS
BEGIN
	SELECT Id, Title, FirstName, LastName, Email, Phone, JoiningDate, Roles FROM Employee 
	WHERE Id = @EmployeeId
END
