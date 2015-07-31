CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(10) NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(200) NOT NULL, 
	[Password] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [JoiningDate] DATETIME2 NOT NULL, 
    [Roles] NVARCHAR(200) NULL, 
    CONSTRAINT [AK_Employee_Email] UNIQUE ([Email]) 
)
