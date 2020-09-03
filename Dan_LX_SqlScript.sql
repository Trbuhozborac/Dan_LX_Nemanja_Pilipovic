IF DB_ID('WorkersDb') IS NULL
CREATE DATABASE WorkersDb
GO

USE WorkersDb
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'tblWorkers')
DROP TABLE tblWorkers
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'tblLocations')
DROP TABLE tblLocations
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'tblSectors')
DROP TABLE tblSectors
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'vwWorker')
DROP VIEW vwWorker


CREATE TABLE tblWorkers (
Id int PRIMARY KEY IDENTITY(1,1),
Name varchar(20), 
Surname varchar(50),
DateOfBirth datetime,
IDCardNumber int,
JMBG char(13),
Gender varchar(10),
PhoneNumber int,
FKSector int,
FKLocation int,
FKManager int
);

CREATE TABLE tblSectors(
Id int PRIMARY KEY IDENTITY(1,1),
Sector nvarchar(30)
);

CREATE TABLE tblLocations(
Id int PRIMARY KEY IDENTITY(1,1),
Address varchar(100),
City varchar(30),
Country varchar(30)
);

ALTER TABLE tblWorkers ADD FOREIGN KEY (FKSector) REFERENCES tblSectors(Id);
ALTER TABLE tblWorkers ADD FOREIGN KEY (FKLocation) REFERENCES tblLocations(Id);
ALTER TABLE tblWorkers ADD FOREIGN KEY (FKManager) REFERENCES tblWorkers(Id);


USE WorkersDb
GO
CREATE VIEW vwWorker
AS
SELECT tblWorkers.Id, tblWorkers.Name, tblWorkers.Surname, tblWorkers.DateOfBirth, tblWorkers.IDCardNumber,
tblWorkers.JMBG,tblWorkers.Gender, tblWorkers.PhoneNumber, tblSectors.Sector,tblLocations.Address,
tblLocations.City, tblLocations.Country, tblWorkers.FKManager
FROM tblWorkers, tblSectors, tblLocations
WHERE tblWorkers.FKLocation = tblLocations.Id AND
tblWorkers.FKSector = tblSectors.Id;


