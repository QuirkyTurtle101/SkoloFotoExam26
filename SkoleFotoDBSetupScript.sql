CREATE DATABASE SkoleFotoDB;
GO

USE SkoleFotoDB;

CREATE TABLE LoginInfo (
	[Email] NVARCHAR(300) PRIMARY KEY,
	[PasswordHash] BINARY(32) NOT NULL,
	[UserType] INT NOT NULL
);

CREATE TABLE ZipCodeLookup (
	[ZipCode] INT PRIMARY KEY,
	[City] NVARCHAR(100) NOT NULL,
);

CREATE TABLE StudentInPicture (
	[StudentID] INT NOT NULL,
	[PhotoID] INT NOT NULL,
	PRIMARY KEY (StudentID, PhotoID)
);

CREATE TABLE Photo (
	[PhotoID] INT IDENTITY (1,1) PRIMARY KEY,
	[FileName] NVARCHAR(100) NOT NULL,
	[FilePath] NVARCHAR(500) NOT NULL,
	[Price] DECIMAL(10,2) NOT NULL,
	[Date] DATETIME NOT NULL,
	[Height] INT NOT NULL,
	[Width] INT NOT NULL,
	[PhotoType] INT NOT NULL
);

CREATE TABLE School (
	[SchoolID] INT IDENTITY (1,1) PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[StreetName] NVARCHAR(100) NOT NULL,
	[ZipCode] INT NOT NULL, CONSTRAINT fk_School_ZipCode FOREIGN KEY (ZipCode) REFERENCES ZipCodeLookup(ZipCode),
	[SchoolType] INT NOT NULL
);

CREATE TABLE SchoolClass(
	[SchoolClassID] INT IDENTITY(1,1) PRIMARY KEY,
	[ClassName] NVARCHAR(50) NOT NULL,
	[SchoolID] INT NOT NULL, CONSTRAINT fk_SchoolClass_School FOREIGN KEY (SchoolID) REFERENCES School(SchoolID)
);

CREATE TABLE Photographer (
	[PhotographerID] INT IDENTITY(1,1) PRIMARY KEY, 
	[FirstName] NVARCHAR(300) NOT NULL,
	[LastName] NVARCHAR(300) NOT NULL,
	[Email] NVARCHAR(300) NOT NULL, CONSTRAINT fk_Photographer_LoginInfo FOREIGN KEY (Email) REFERENCES LoginInfo(Email),
	[PhoneNumber] NVARCHAR(11) NOT NULL,
	[Website] NVARCHAR(300) NOT NULL,
	[CVRNumber] NVARCHAR(300) NOT NULL,
	[StreetName] NVARCHAR(300) NOT NULL,
	[ExperienceInYears] INT,
	[MaxTravelRadiusInKm] INT,
	[Instagram] NVARCHAR(300),
	[Facebook] NVARCHAR(300)
);
CREATE TABLE Parent (
	[ParentID] INT IDENTITY(1,1) PRIMARY KEY,
	[FirstName] NVARCHAR(300) NOT NULL,
	[LastName] NVARCHAR(300) NOT NULL,
	[Email] NVARCHAR(300) NOT NULL, CONSTRAINT fk_Parent_LoginInfo FOREIGN KEY (Email) REFERENCES LoginInfo(Email),
	[PhoneNumber] NVARCHAR(11) NOT NULL,
	[StreetName] NVARCHAR(300) NOT NULL,
	[ZipCode] INT NOT NULL, CONSTRAINT fk_Parent_ZipCode FOREIGN KEY (ZipCode) REFERENCES ZipCodeLookup(ZipCode)
);

CREATE TABLE Teacher (
	[TeacherID] INT IDENTITY(1,1) PRIMARY KEY,
	[FirstName] NVARCHAR(300) NOT NULL,
	[LastName] NVARCHAR(300) NOT NULL,
	[Email] NVARCHAR(300) NOT NULL, CONSTRAINT fk_Teacher_LoginInfo FOREIGN KEY (Email) REFERENCES LoginInfo(Email),
	[PhoneNumber] NVARCHAR(11) NOT NULL,
	[Initials] NVARCHAR(20) NOT NULL,
	[SchoolID] INT NOT NULL, CONSTRAINT fk_Teacher_School FOREIGN KEY (SchoolID) REFERENCES School(SchoolID)
);

CREATE TABLE Administrator (
	[AdministratorID] INT IDENTITY(1,1) PRIMARY KEY,
	[FirstName] NVARCHAR(300) NOT NULL,
	[LastName] NVARCHAR(300) NOT NULL,
	[Email] NVARCHAR(300) NOT NULL, CONSTRAINT fk_Administrator_LoginInfo FOREIGN KEY (Email) REFERENCES LoginInfo(Email),
	[PhoneNumber] NVARCHAR(11) NOT NULL
);

CREATE TABLE SchoolSecretary (
	[SchoolSecretaryID] INT IDENTITY(1,1) PRIMARY KEY,
	[FirstName] NVARCHAR(300) NOT NULL,
	[LastName] NVARCHAR(300) NOT NULL,
	[Email] NVARCHAR(300) NOT NULL, CONSTRAINT fk_SchoolSecretary_LoginInfo FOREIGN KEY (Email) REFERENCES LoginInfo(Email),
	[PhoneNumber] NVARCHAR(11) NOT NULL,
	[Initials] NVARCHAR(20) NOT NULL,
	[SchoolID] INT NOT NULL, CONSTRAINT fk_SchoolSecretary_School FOREIGN KEY (SchoolID) REFERENCES School(SchoolID)
);

CREATE TABLE Student (
	[StudentID] INT IDENTITY(1,1) PRIMARY KEY,
	[FirstName] NVARCHAR(300) NOT NULL,
	[MiddleName] NVARCHAR(300),
	[LastName] NVARCHAR(300) NOT NULL,
	[ParentID] INT NOT NULL, CONSTRAINT fk_Student_Parent FOREIGN KEY (ParentID) REFERENCES Parent(ParentID),
	[SchoolClassID] INT NOT NULL, CONSTRAINT fk_Student_SchoolClass FOREIGN KEY (SchoolClassID) REFERENCES SchoolClass(SchoolClassID)
);

CREATE TABLE [Order] (
	[OrderID] INT IDENTITY (1,1) PRIMARY KEY,
	[TotalPrice] INT NOT NULL,
	[Date] DATETIME,
	[ParentID] INT NOT NULL, CONSTRAINT fk_Order_Parent FOREIGN KEY (ParentID) REFERENCES Parent(ParentID)
);

CREATE TABLE OrderLine (
	[OrderID] INT NOT NULL, CONSTRAINT fk_OrderLine_Order FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
	[PhotoID] INT NOT NULL, CONSTRAINT fk_OrderLine_Photo FOREIGN KEY (PhotoID) REFERENCES Photo(PhotoID),
	[Quantity] INT NOT NULL,
	PRIMARY KEY (OrderID, PhotoID)
);

CREATE TABLE PhotographingEvent (
	[PhotographingEventID] INT IDENTITY(1,1) PRIMARY KEY,
	[Start] DATETIME NOT NULL,
	[End] DATETIME NOT NULL,
	[SchoolSecretaryID] INT NOT NULL, CONSTRAINT fk_PhotographingEvent_SchoolSecretary FOREIGN KEY (SchoolSecretaryID) REFERENCES SchoolSecretary(SchoolSecretaryID),
	[PhotographerID] INT NOT NULL, CONSTRAINT fk_PhotographingEvent_Photographer FOREIGN KEY (PhotographerID) REFERENCES Photographer(PhotographerID)
);

CREATE TABLE Booking (
	[BookingID] INT IDENTITY(1,1) PRIMARY KEY,
	[Start] DATETIME NOT NULL,
	[End] DATETIME NOT NULL,
	[SchoolClassID] INT NOT NULL, CONSTRAINT fk_Booking_SchoolClass FOREIGN KEY (SchoolClassID) REFERENCES SchoolClass(SchoolClassID),
	[PhotographingEventID] INT NOT NULL, CONSTRAINT fk_Booking_PhotographingEvent FOREIGN KEY (PhotographingEventID) REFERENCES PhotographingEvent(PhotographingEventID),
	[TeacherID] INT NOT NULL, CONSTRAINT fk_Booking_Teacher FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID)
);
