IF DB_ID('Besuchermanagment') IS NULL
    CREATE DATABASE Besuchermanagment;
GO

USE Besuchermanagment;
GO

IF OBJECT_ID('Besucher') IS NOT NULL
    DROP TABLE Besucher;
GO

IF OBJECT_ID('Status') IS NOT NULL
    DROP TABLE Status;
GO

IF OBJECT_ID('StatusBesucher') IS NOT NULL
    DROP TABLE StatusBesucher;
GO

IF OBJECT_ID('Karten') IS NOT NULL
    DROP TABLE Karten;
GO

IF OBJECT_ID('Schwimmbad') IS NOT NULL
    DROP TABLE Schwimmbad;
GO

IF OBJECT_ID('SchwimmbadBesucher') IS NOT NULL
    DROP TABLE SchwimmbadBesucher;
GO

IF OBJECT_ID('Reservierung') IS NOT NULL
    DROP TABLE Reservierung;
GO

create table Besucher
(
	ID_Besucher int primary key IDENTITY(1,1),
	Vorname varchar(100),
	Nachname varchar(100),
	AlterKunde int,
	Status varchar(100)
);

create table Status
(
	ID_Status int primary key IDENTITY(1,1),
	Bezeichnung varchar(100),
	Rabatt decimal,
);

create table StatusBesucher
(
	ID_Besucher INT,
    ID_Status INT,
    PRIMARY KEY (ID_Besucher, ID_Status),
    FOREIGN KEY (ID_Besucher) REFERENCES Besucher(ID_Besucher),
    FOREIGN KEY (ID_Status) REFERENCES Status(ID_Status)
);

create table Schwimmbad
(
	ID_Schwimmbad int primary key identity(1,1),
	Name varchar(100),
	Straﬂe varchar(100),
	Ort varchar(100),
	PLZ int,
	Auslastung int,
);

create table SchwimmbadBesucher
(
	ID_Besucher INT,
    ID_Schwimmbad INT,
    PRIMARY KEY (ID_Besucher, ID_Schwimmbad),
    FOREIGN KEY (ID_Besucher) REFERENCES Besucher(ID_Besucher),
    FOREIGN KEY (ID_Schwimmbad) REFERENCES Schwimmbad(ID_Schwimmbad)
);

create table Karten
(
	ID_Karten int primary key IDENTITY(1,1),
	Bezeichnung varchar(100),
	Preis decimal,
	Anzahl int,
);

create table Reservierung
(
	ID_Besucher INT,
    ID_Karten INT,
    PRIMARY KEY (ID_Besucher, ID_Karten),
    FOREIGN KEY (ID_Besucher) REFERENCES Besucher(ID_Besucher),
    FOREIGN KEY (ID_Karten) REFERENCES Karten(ID_Karten) 
);

SET IDENTITY_INSERT Besucher ON;  
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde) VALUES(1,'Lukas', 'Schneider', 17)
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde) VALUES(2,'Emma', 'Weber', 23)
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde) VALUES(3,'Noah', 'Fischer', 20)
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde) VALUES(4,'Mia', 'M¸ller', 41)
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde) VALUES(5,'Ben', 'Schmidt', 16)
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde) VALUES(6,'Sophie', 'Braun', 22)
SET IDENTITY_INSERT Besucher OFF; 

SET IDENTITY_INSERT Status ON;
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(1,'Sch¸ler', 0.90)
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(2,'Student', 0.95)
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(3,'Kind', 0.75)
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(4,'Erwachsen', 1)
SET IDENTITY_INSERT Status OFF;

INSERT INTO StatusBesucher VALUES
(1, 2),
(2, 3),
(3, 3),
(4, 3),
(5, 1),
(6, 3)

SET IDENTITY_INSERT Schwimmbad ON;
INSERT INTO Schwimmbad(ID_Schwimmbad, Name, Straﬂe, Ort, PLZ, Auslastung) VALUES(1,'Freibad Sonnensee', 'Seestraﬂe 4', 'M¸nchen', 80331, 113)
INSERT INTO Schwimmbad(ID_Schwimmbad, Name, Straﬂe, Ort, PLZ, Auslastung) VALUES(2,'Hallenschwimmbad Westend', 'Westendstraﬂe 56', 'Frankfurt', 60322, 82)
INSERT INTO Schwimmbad(ID_Schwimmbad, Name, Straﬂe, Ort, PLZ, Auslastung) VALUES(3,'Erlebnisbad Blaues Wasser', 'Wasserstraﬂe 7', 'Hamburg', 20095, 136)
INSERT INTO Schwimmbad(ID_Schwimmbad, Name, Straﬂe, Ort, PLZ, Auslastung) VALUES(4,'Sportbad Nordpark', 'Nordparkstraﬂe 25', 'Kˆln', 50667, 78)
SET IDENTITY_INSERT Schwimmbad OFF;

INSERT INTO SchwimmbadBesucher VALUES
(1, 1),
(2, 1),
(3, 4),
(4, 2),
(5, 3),
(6, 4)

SET IDENTITY_INSERT Karten ON;
INSERT INTO Karten(ID_Karten, Bezeichnung, Preis, Anzahl) VALUES(1,'Standartticket', 4, 154)
INSERT INTO Karten(ID_Karten, Bezeichnung, Preis, Anzahl) VALUES(2,'Tagesticket', 5, 123)
INSERT INTO Karten(ID_Karten, Bezeichnung, Preis, Anzahl) VALUES(3,'Kinderticket', 3, 96)
INSERT INTO Karten(ID_Karten, Bezeichnung, Preis, Anzahl) VALUES(4,'Familienticket', 7.5, 109)
INSERT INTO Karten(ID_Karten, Bezeichnung, Preis, Anzahl) VALUES(5,'Gruppenticket', 10, 75)
SET IDENTITY_INSERT Karten OFF;

INSERT INTO Reservierung VALUES
(1, 5),
(2, 2),
(3, 4),
(4, 1),
(5, 1),
(6, 1)




