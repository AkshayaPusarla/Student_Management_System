IF OBJECT_ID('AddStudent', 'P') IS NOT NULL
DROP PROCEDURE AddStudent;
GO

CREATE PROCEDURE AddStudent
@Name NVARCHAR(100),
@RollNumber NVARCHAR(50),
@Course NVARCHAR(100)
AS
BEGIN
INSERT INTO Students (Name, RollNumber, Course)
VALUES (@Name, @RollNumber, @Course)
END
GO
