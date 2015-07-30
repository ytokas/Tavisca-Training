CREATE PROCEDURE [dbo].[spAddRemark]
	@RemarkText NVARCHAR(MAX),  
    @CreateTimestamp DATETIME2
AS
BEGIN
      INSERT INTO Remark(RemarkText, CreateTimestamp)
      VALUES (@RemarkText, @CreateTimestamp)
END

