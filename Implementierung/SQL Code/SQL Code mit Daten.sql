IF DB_ID('BesuchermanagmentDatenbank') IS NULL
    CREATE DATABASE BesuchermanagmentDatenbank;
GO

USE BesuchermanagmentDatenbank;
GO

IF OBJECT_ID('Besucher') IS NOT NULL
    DROP TABLE Besucher;
GO

IF OBJECT_ID('Ticket') IS NOT NULL
    DROP TABLE Ticket;
GO

IF OBJECT_ID('Status') IS NOT NULL
    DROP TABLE Status;
GO

IF OBJECT_ID('Benutzer') IS NOT NULL
    DROP TABLE Benutzer;
GO

IF OBJECT_ID('Reservierung') IS NOT NULL
    DROP TABLE Reservierung;
GO

create table Benutzer
(
	ID_Benutzer int primary key IDENTITY(1,1),
	Benutzername varchar(100),
	Passwort int,
	EMail varchar(100),
	Telefon varchar(100)
);

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
	Rabatt int
);

create table Ticket
(
	ID_Ticket int primary key IDENTITY(1,1),
	Bezeichnung varchar(100),
	Preis decimal,
	Anzahl int
);

create table Reservierung
(
	ID_Reservierung int primary key IDENTITY(1,1),
	Vorname varchar(100),
	Nachname varchar(100),
	Status varchar(100),
	Ticket varchar(100),
	Rabatt int,
	Anwesend int
);

SET IDENTITY_INSERT Benutzer ON;
INSERT INTO Benutzer(ID_Benutzer, Benutzername, Passwort, EMail, Telefon) VALUES(1,'Admin', 12345, '', '')
INSERT INTO Benutzer(ID_Benutzer, Benutzername, Passwort, EMail, Telefon) VALUES(2,'Gast', 1234, '', '')
SET IDENTITY_INSERT Benutzer OFF;

SET IDENTITY_INSERT Besucher ON;
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde, Status) VALUES(1,'Lukas', 'Schneider', 17, 'Schüler')
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde, Status) VALUES(2,'Emma', 'Weber', 23, '')
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde, Status) VALUES(3,'Noah', 'Fischer', 20, 'Stundent')
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde, Status) VALUES(4,'Mia', 'Müller', 41, '')
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde, Status) VALUES(5,'Ben', 'Schmidt', 16, 'Schüler')
INSERT INTO Besucher(ID_Besucher, Vorname, Nachname, AlterKunde, Status) VALUES(6,'Sophie', 'Braun', 22, '')
SET IDENTITY_INSERT Besucher OFF;

SET IDENTITY_INSERT Ticket ON;
INSERT INTO Ticket(ID_Ticket, Bezeichnung, Preis, Anzahl) VALUES(1, 'Standartticket', 4, 154)
INSERT INTO Ticket(ID_Ticket, Bezeichnung, Preis, Anzahl) VALUES(2, 'Tagesticket', 5, 123)
INSERT INTO Ticket(ID_Ticket, Bezeichnung, Preis, Anzahl) VALUES(3, 'Kinderticket', 3, 96)
INSERT INTO Ticket(ID_Ticket, Bezeichnung, Preis, Anzahl) VALUES(4, 'Familienticket', 7.5, 109)
INSERT INTO Ticket(ID_Ticket, Bezeichnung, Preis, Anzahl) VALUES(5, 'Gruppenticket', 10, 75)
SET IDENTITY_INSERT Ticket OFF;

SET IDENTITY_INSERT Status ON;
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(1,'Schüler', 0.90)
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(2,'Student', 0.95)
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(3,'Kind', 0.75)
INSERT INTO Status(ID_Status, Bezeichnung, Rabatt) VALUES(4,'Erwachsen', 1)
SET IDENTITY_INSERT Status OFF;

SET IDENTITY_INSERT Reservierung ON;
INSERT INTO Reservierung(ID_Reservierung, Vorname, Nachname, Status, Ticket, Rabatt, Anwesend) VALUES(1,'Lukas', 'Schneider', 'Schüler', 'Tagesticket', 10, 1)
INSERT INTO Reservierung(ID_Reservierung, Vorname, Nachname, Status, Ticket, Rabatt, Anwesend) VALUES(2,'Noah', 'Fischer', 'Student', 'Standartticket', 5, 0)
INSERT INTO Reservierung(ID_Reservierung, Vorname, Nachname, Status, Ticket, Rabatt, Anwesend) VALUES(3,'Mia', 'Müller', '', 'Familienticket', 10, 0)
INSERT INTO Reservierung(ID_Reservierung, Vorname, Nachname, Status, Ticket, Rabatt, Anwesend) VALUES(4,'Sophie', 'Braun', '', 'Standartticket', 10, 1)
SET IDENTITY_INSERT Reservierung OFF;
