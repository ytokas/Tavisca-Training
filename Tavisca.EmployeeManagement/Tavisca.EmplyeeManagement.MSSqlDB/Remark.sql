CREATE TABLE [dbo].[Remark]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RemarkText] NVARCHAR(MAX) NOT NULL, 
    [CreateTimestamp] DATETIME2 NOT NULL, 
    [EmployeeId] INT NOT NULL, 
    CONSTRAINT [FK_Remark_Employee] FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
)
