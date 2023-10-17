
--Task 1: creating database completed

--Task 2:
-- creating courses table
 create table Courses(
  CourseID int PRIMARY KEY,
  CourseName varchar(50)
  );
-- creating students table
create table Students (
  StudentID int PRIMARY KEY,
  FirstName varchar(50),
  LastName varchar(50),
  Age int,
  CourseID int references Courses(CourseID) 
  );
-- printing the created table
 select * from Students;
 --Task 3:
 -- inserting records in courses table 
 insert into Courses (CourseID,CourseName) values 
		(1, 'Data structures'),
		(2, 'Object oriented programming'),
		(3, 'Microprocessor systems'),
		(4, 'fundamentals of computing'),
		(5, 'introduction to computing');
-- printing the courses table
select * from Courses;
-- inserting records in students table
 insert into Students (StudentID,FirstName,LastName,Age,CourseID) values 
		(1001, 'Ali','Ahmed',18,1),
		(1002, 'Maria','Sultan',19,4),
		(1003, 'Zia','Rahman',21,5),
		(1004, 'Anum','Batool',19,4),
		(1005, 'Shaheen','Nawaz',18,3),
		(1006, 'Taha','Nazeer',23,4),
		(1007, 'Maira','Ali',19,4),
		(1008, 'Muhammad','Abuzar',17,1),
		(1009, 'Raiqa','Tanveer',22,3),
		(1010, 'Hina','Yasir',18,5);
-- printing the students table
select * from Students;
--Reading student
CREATE PROCEDURE GetAllStudents
AS
BEGIN
    SELECT * FROM Students;
END;
GO
Select *from Students;



--Adding students
CREATE PROCEDURE StudentsAdd
    @StudentID INT,
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
    @Age INT,
    @CourseID INT
AS
BEGIN
    INSERT INTO Students (StudentID, FirstName, LastName, Age, CourseID)
    VALUES (@StudentID, @FirstName, @LastName,@Age,@CourseID);
END;
GO

--updating student info
CREATE PROCEDURE UpdateStudentinfo
    @StudentID INT,
    @FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@Age INT,
	@CourseID INT
AS
BEGIN
    UPDATE Students
    SET FirstName=@FirstName, LastName=@LastName,  Age = @Age,CourseID=@CourseID
    WHERE StudentID = @StudentID;
END;
GO

--Deleting student
CREATE PROCEDURE DeleteStudent
    @StudentID INT
AS
BEGIN
    DELETE FROM Students
    WHERE StudentID = @StudentID;
END;
GO

EXEC DeleteStudent 1008
Select *from Students;

--Reading Courses
CREATE PROCEDURE GetAllCourses
AS
BEGIN
    SELECT * FROM Courses;
END;
GO
Select *from Courses;

--Adding Courses
CREATE PROCEDURE AddCourses
    @CourseID INT,
	@CourseName VARCHAR(50)
AS
BEGIN
    INSERT INTO Courses (CourseID, CourseName)
    VALUES (@CourseID, @CourseName);
END;
GO

--updating student info
CREATE PROCEDURE UpdateCoursesinfo
	@CourseID INT,
	@CourseName VARCHAR(50)
AS
BEGIN
    UPDATE Courses
    SET CourseName=@CourseName
    WHERE CourseID = @CourseID;
END;
GO

--Deleting student
CREATE PROCEDURE DeleteCourses
    @CourseID INT
AS
BEGIN
    DELETE FROM Courses
    WHERE CourseID = @CourseID;
END;
GO

--Task9 1. Listing all students older than 20
CREATE PROCEDURE Older_than_20
AS
BEGIN

SELECT FirstName,LastName FROM Students WHERE Age > (SELECT 20 FROM Students);

END;
GO
--2. Find the total number of students for each course.
CREATE PROCEDURE ListAllStudents
AS
BEGIN

SELECT Courses.CourseName, COUNT(*) AS TotalStudents
FROM Courses
LEFT JOIN Students ON Courses.CourseID = Students.CourseID
GROUP BY Courses.CourseName;

END;
GO
--3.	Find the most popular course (the course with the most students enrolled).
CREATE PROCEDURE Most_popular
AS
BEGIN
	SELECT TOP 1 Courses.CourseName, COUNT(*) AS EnrolledStudents
FROM Courses
JOIN Students ON Courses.CourseID = Students.CourseID
GROUP BY Courses.CourseName
ORDER BY EnrolledStudents DESC;

END;
GO