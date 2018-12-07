USE ElectronicCardDB;
GO

CREATE TABLE Patients
(
    UserId INT PRIMARY KEY NOT NULL
	FOREIGN KEY REFERENCES Users(Id) ON DELETE CASCADE,
	FirstName NVARCHAR(20) NOT NULL,
	MiddleName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	DateBirth DATE NOT NULL,
	PlaceWork NVARCHAR(40) NOT NULL,
	PhotoPath NVARCHAR(50) NULL,
)