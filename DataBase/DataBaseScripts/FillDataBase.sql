USE ElectronicCardDB;
GO

EXEC dbo.CreateUser @login = 'Admin', @password = 'Admin', @email = 'Admin@gmail.com', @isDoctor = 1, @firstName = 'Admin', @middleName = 'Admin', @lastName = 'Admin', @work = 'Admin'