CREATE PROCEDURE [dbo].[spSaveEmployee]
	@Title NVARCHAR(10), 
    @FirstName NVARCHAR(50), 
    @LastName NVARCHAR(50) , 
    @Email NVARCHAR(200) , 
	@Password NVARCHAR(50) , 
    @Phone NVARCHAR(50) , 
    @JoiningDate DATETIME2,
	@Roles NVARCHAR(200),
	@Id INT OUT
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Employee (Title, FirstName, LastName, Email, Password, Phone, JoiningDate, Roles)
      VALUES (@Title, @FirstName, @LastName, @Email, @Password, @Phone, @JoiningDate, @Roles);
      SELECT @Id = SCOPE_IDENTITY();
END

