IF OBJECT_ID('UpdateStudent', 'P') IS NOT NULL
DROP PROCEDURE UpdateStudent;
GO

CREATE PROCEDURE UpdateStudent
@Id INT,
@Name NVARCHAR(100),
@RollNumber NVARCHAR(50),
@Course NVARCHAR(100)
AS
BEGIN
UPDATE Students
SET Name = @Name,
RollNumber = @RollNumber,
Course = @Course
WHERE Id = @Id
END
GO
