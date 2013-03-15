﻿CREATE TABLE [dbo].[Employees] (
    [EmployeeId]         INT            IDENTITY (1, 1) NOT NULL,
    [LastName]           NVARCHAR (MAX) NULL,
    [MiddleName]         NVARCHAR (MAX) NULL,
    [FirstName]          NVARCHAR (MAX) NULL,
    [PreferredName]      NVARCHAR (MAX) NULL,
    [DateOfBirth]        Datetime NULL,
    [Title]              NVARCHAR (MAX) NULL,
    [Nationality]        NVARCHAR (MAX) NULL,
    [HomePhone]          NVARCHAR (MAX) NULL,
    [CellPhone]          NVARCHAR (MAX) NULL,
    [TourStartDate]      NVARCHAR (MAX) NULL,
    [TourEndDate]        NVARCHAR (MAX) NULL,
    [AgencyOrSection]    NVARCHAR (MAX) NULL,
    [PositionTitle]      NVARCHAR (MAX) NULL,
    [HomeStreetAddress]  NVARCHAR (MAX) NULL,
    [HomeStreetAddress2] NVARCHAR (MAX) NULL,
    [HomeState]          NVARCHAR (MAX) NULL,
    [HomeCity]           NVARCHAR (MAX) NULL,
    [HomeZip]            NVARCHAR (MAX) NULL,
    [AgencyEmail]        NVARCHAR (MAX) NULL,
    [PersonalEmail]      NVARCHAR (MAX) NULL,
    [UserId]             NVARCHAR (MAX) NULL,
    [LastLogin]          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);
