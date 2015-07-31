CREATE PROCEDURE [dbo].[spAddRemark]
	@EmployeeId int,
	@RemarkText NVARCHAR(MAX),  
    @CreateTimestamp DATETIME2
AS
BEGIN
      INSERT INTO Remark(EmployeeId, RemarkText, CreateTimestamp)
      VALUES (@EmployeeId, @RemarkText, @CreateTimestamp)
END

