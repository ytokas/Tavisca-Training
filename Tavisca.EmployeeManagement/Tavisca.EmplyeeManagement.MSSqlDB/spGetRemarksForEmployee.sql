CREATE PROCEDURE [dbo].[spGetRemarksForEmployee]
	@EmployeeId int,
	@PageNumber int,
	@PageSize int,
	@OrderBy nvarchar(50),
	@SortingOrder nvarchar(50)
AS
BEGIN

;WITH Remark_cte AS (
	 SELECT ROW_NUMBER() over (
								ORDER BY
									CASE WHEN @SortingOrder = 'ASC' THEN
										CASE
											WHEN @OrderBy = 'CreateTimestamp' THEN CreateTimestamp
										END
									END ASC,
									CASE WHEN @SortingOrder = 'ASC' THEN
										CASE
											WHEN @OrderBy = 'Id' THEN Id
										END
									END ASC,
									CASE WHEN @SortingOrder = 'DESC' THEN
										CASE
											WHEN @OrderBy = 'Id' THEN Id
										END
									END DESC,
									CASE WHEN @SortingOrder = 'DESC' THEN
										CASE
											WHEN @OrderBy = 'CreateTimestamp' THEN CreateTimestamp
										END
									END DESC
							 ) as RowNo, RemarkText, CreateTimestamp FROM Remark
	WHERE Id = @EmployeeId
)

	SELECT RemarkText, CreateTimestamp , (select COUNT(*) from Remark_cte) as TotalResults FROM Remark_cte
	WHERE RowNo between (@PageNumber - 1 )* @PageSize + 1 and @PageNumber * @PageSize;
END



