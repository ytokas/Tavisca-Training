CREATE PROCEDURE [dbo].[spGetEmployees]
	@PageNumber int,
	@PageSize int,
	@OrderBy nvarchar(50),
	@SortingOrder nvarchar(50)
AS
BEGIN

WITH Employee_CTE AS (
	 SELECT ROW_NUMBER() over (
								ORDER BY
									CASE WHEN @SortingOrder = 'ASC' THEN
										CASE
											WHEN @OrderBy = 'JoiningDate' THEN JoiningDate
										END
									END ASC,
									CASE WHEN @SortingOrder = 'ASC' THEN
										CASE
											WHEN @OrderBy = 'Id' THEN Id
										END
									END ASC,
									CASE WHEN @SortingOrder = 'DESC' THEN
										CASE
											WHEN @OrderBy = 'JoiningDate' THEN JoiningDate
										END
									END DESC,
									CASE WHEN @SortingOrder = 'DESC' THEN
										CASE
											WHEN @OrderBy = 'Id' THEN Id
										END
									END DESC
							 ) as RowNo, Id, Title, FirstName, LastName, Email, Phone, JoiningDate FROM Employee
)

	SELECT Id, Title, FirstName, LastName, Email, Phone, JoiningDate, (select COUNT(*) from Employee_CTE) as TotalResults FROM Employee_CTE
	WHERE RowNo between (@PageNumber - 1 )* @PageSize + 1 and @PageNumber * @PageSize;
END



