CREATE PROCEDURE [dbo].[spSaveEmployee]
	@Title NVARCHAR(10), 
    @FirstName NVARCHAR(50), 
    @LastName NVARCHAR(50) , 
    @Email NVARCHAR(50) , 
    @Phone NVARCHAR(50) , 
    @JoiningDate DATETIME2
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Employee (Title, FirstName, LastName, Email, Phone, JoiningDate)
      VALUES (@Title, @FirstName, @LastName, @Email, @Phone, @JoiningDate)
      SELECT SCOPE_IDENTITY()
END

