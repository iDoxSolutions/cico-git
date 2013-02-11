
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/27/2013 12:10:55
-- Generated from EDMX file: C:\Users\Ken\Documents\iDoxSolutions\Mexico\CICO\C#\ContosoUniversity\CICO.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CICO];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EmployeesDependents]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dependents] DROP CONSTRAINT [FK_EmployeesDependents];
GO
IF OBJECT_ID(N'[dbo].[FK_CaseEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cases] DROP CONSTRAINT [FK_CaseEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_CaseStep]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Steps] DROP CONSTRAINT [FK_CaseStep];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentStaff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Staffs] DROP CONSTRAINT [FK_DepartmentStaff];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckListDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Departments] DROP CONSTRAINT [FK_CheckListDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckListStep]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Steps] DROP CONSTRAINT [FK_CheckListStep];
GO
IF OBJECT_ID(N'[dbo].[FK_CaseDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_CaseDocument];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Dependents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dependents];
GO
IF OBJECT_ID(N'[dbo].[Cases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cases];
GO
IF OBJECT_ID(N'[dbo].[Steps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Steps];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Staffs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staffs];
GO
IF OBJECT_ID(N'[dbo].[CheckListItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckListItems];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [PreferredName] nvarchar(max)  NOT NULL,
    [DateOfBirth] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Nationality] nvarchar(max)  NOT NULL,
    [HomePhone] nvarchar(max)  NOT NULL,
    [CellPhone] nvarchar(max)  NOT NULL,
    [TourStartDate] nvarchar(max)  NOT NULL,
    [TourEndDate] nvarchar(max)  NOT NULL,
    [AgencyOrSection] nvarchar(max)  NOT NULL,
    [PositionTitle] nvarchar(max)  NOT NULL,
    [HomeStreetAddress] nvarchar(max)  NOT NULL,
    [HomeStreetAddress2] nvarchar(max)  NOT NULL,
    [HomeState] nvarchar(max)  NOT NULL,
    [HomeCity] nvarchar(max)  NOT NULL,
    [HomeZip] nvarchar(max)  NOT NULL,
    [AgencyEmail] nvarchar(max)  NOT NULL,
    [PersonalEmail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Dependents'
CREATE TABLE [dbo].[Dependents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmployeesEmployeeId] int  NOT NULL,
    [DependentType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Cases'
CREATE TABLE [dbo].[Cases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DueDate] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Employee_EmployeeId] int  NOT NULL
);
GO

-- Creating table 'Steps'
CREATE TABLE [dbo].[Steps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [CaseId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Department] nvarchar(max)  NOT NULL,
    [Priority] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Viewable] nvarchar(max)  NOT NULL,
    [Subscriber] nvarchar(max)  NOT NULL,
    [CheckListId] int  NOT NULL,
    [DueDate] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Contact] nvarchar(max)  NOT NULL,
    [CheckListId] int  NOT NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [dbo].[Staffs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StaffId] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'CheckListItems'
CREATE TABLE [dbo].[CheckListItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DueDate] datetime  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [DocumentId] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(255)  NULL,
    [DocId] nvarchar(255)  NULL,
    [ContentType] nvarchar(255)  NULL,
    [CaseId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [Id] in table 'Dependents'
ALTER TABLE [dbo].[Dependents]
ADD CONSTRAINT [PK_Dependents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cases'
ALTER TABLE [dbo].[Cases]
ADD CONSTRAINT [PK_Cases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Steps'
ALTER TABLE [dbo].[Steps]
ADD CONSTRAINT [PK_Steps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CheckListItems'
ALTER TABLE [dbo].[CheckListItems]
ADD CONSTRAINT [PK_CheckListItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [DocumentId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([DocumentId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EmployeesEmployeeId] in table 'Dependents'
ALTER TABLE [dbo].[Dependents]
ADD CONSTRAINT [FK_EmployeesDependents]
    FOREIGN KEY ([EmployeesEmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeesDependents'
CREATE INDEX [IX_FK_EmployeesDependents]
ON [dbo].[Dependents]
    ([EmployeesEmployeeId]);
GO

-- Creating foreign key on [Employee_EmployeeId] in table 'Cases'
ALTER TABLE [dbo].[Cases]
ADD CONSTRAINT [FK_CaseEmployee]
    FOREIGN KEY ([Employee_EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CaseEmployee'
CREATE INDEX [IX_FK_CaseEmployee]
ON [dbo].[Cases]
    ([Employee_EmployeeId]);
GO

-- Creating foreign key on [CaseId] in table 'Steps'
ALTER TABLE [dbo].[Steps]
ADD CONSTRAINT [FK_CaseStep]
    FOREIGN KEY ([CaseId])
    REFERENCES [dbo].[Cases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CaseStep'
CREATE INDEX [IX_FK_CaseStep]
ON [dbo].[Steps]
    ([CaseId]);
GO

-- Creating foreign key on [DepartmentId] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [FK_DepartmentStaff]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentStaff'
CREATE INDEX [IX_FK_DepartmentStaff]
ON [dbo].[Staffs]
    ([DepartmentId]);
GO

-- Creating foreign key on [CheckListId] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [FK_CheckListDepartment]
    FOREIGN KEY ([CheckListId])
    REFERENCES [dbo].[CheckListItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckListDepartment'
CREATE INDEX [IX_FK_CheckListDepartment]
ON [dbo].[Departments]
    ([CheckListId]);
GO

-- Creating foreign key on [CheckListId] in table 'Steps'
ALTER TABLE [dbo].[Steps]
ADD CONSTRAINT [FK_CheckListStep]
    FOREIGN KEY ([CheckListId])
    REFERENCES [dbo].[CheckListItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckListStep'
CREATE INDEX [IX_FK_CheckListStep]
ON [dbo].[Steps]
    ([CheckListId]);
GO

-- Creating foreign key on [CaseId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_CaseDocument]
    FOREIGN KEY ([CaseId])
    REFERENCES [dbo].[Cases]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CaseDocument'
CREATE INDEX [IX_FK_CaseDocument]
ON [dbo].[Documents]
    ([CaseId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------