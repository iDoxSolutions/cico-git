
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/12/2013 13:54:20
-- Generated from EDMX file: C:\Users\Ken\Documents\wingithub\cico\CICO\Models\CICO.edmx
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

IF OBJECT_ID(N'[dbo].[FK_CheckListDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Departments] DROP CONSTRAINT [FK_CheckListDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckListDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_CheckListDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckListStep]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CheckListItems] DROP CONSTRAINT [FK_CheckListStep];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeCheckList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CheckLists] DROP CONSTRAINT [FK_EmployeeCheckList];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentStaff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Staffs] DROP CONSTRAINT [FK_DepartmentStaff];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeesDependents]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dependents] DROP CONSTRAINT [FK_EmployeesDependents];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CheckLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckLists];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Dependents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dependents];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[CheckListItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckListItems];
GO
IF OBJECT_ID(N'[dbo].[Staffs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staffs];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CheckLists'
CREATE TABLE [dbo].[CheckLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [EmployeeEmployeeId] int  NOT NULL,
    [StartDate] nvarchar(max)  NOT NULL,
    [EndDate] nvarchar(max)  NOT NULL,
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

-- Creating table 'Dependents'
CREATE TABLE [dbo].[Dependents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmployeesEmployeeId] int  NOT NULL,
    [DependentType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [DocumentId] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(255)  NULL,
    [DocId] nvarchar(255)  NULL,
    [ContentType] nvarchar(255)  NULL,
    [CheckListId] int  NOT NULL
);
GO

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
    [PersonalEmail] nvarchar(max)  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [LastLogin] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CheckListItems'
CREATE TABLE [dbo].[CheckListItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Department] nvarchar(max)  NOT NULL,
    [Priority] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Viewable] nvarchar(max)  NOT NULL,
    [Subscriber] nvarchar(max)  NOT NULL,
    [CheckListId] int  NOT NULL,
    [DueDate] nvarchar(max)  NOT NULL,
    [DueDate1] datetime  NOT NULL,
    [InstructionDocument] nvarchar(max)  NOT NULL,
    [EmployeeCompleted] nvarchar(max)  NOT NULL,
    [Provisional] nvarchar(max)  NOT NULL,
    [OfficeComplete] nvarchar(max)  NOT NULL,
    [ItemDocument] nvarchar(max)  NOT NULL,
    [BlankForm] nvarchar(max)  NOT NULL,
    [InstructionText] nvarchar(max)  NOT NULL,
    [Group] nvarchar(max)  NOT NULL,
    [AlertDays] nvarchar(max)  NOT NULL,
    [AlertFrequency] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [dbo].[Staffs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CheckLists'
ALTER TABLE [dbo].[CheckLists]
ADD CONSTRAINT [PK_CheckLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Dependents'
ALTER TABLE [dbo].[Dependents]
ADD CONSTRAINT [PK_Dependents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [DocumentId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([DocumentId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [Id] in table 'CheckListItems'
ALTER TABLE [dbo].[CheckListItems]
ADD CONSTRAINT [PK_CheckListItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CheckListId] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [FK_CheckListDepartment]
    FOREIGN KEY ([CheckListId])
    REFERENCES [dbo].[CheckLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckListDepartment'
CREATE INDEX [IX_FK_CheckListDepartment]
ON [dbo].[Departments]
    ([CheckListId]);
GO

-- Creating foreign key on [CheckListId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_CheckListDocument]
    FOREIGN KEY ([CheckListId])
    REFERENCES [dbo].[CheckLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckListDocument'
CREATE INDEX [IX_FK_CheckListDocument]
ON [dbo].[Documents]
    ([CheckListId]);
GO

-- Creating foreign key on [CheckListId] in table 'CheckListItems'
ALTER TABLE [dbo].[CheckListItems]
ADD CONSTRAINT [FK_CheckListStep]
    FOREIGN KEY ([CheckListId])
    REFERENCES [dbo].[CheckLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckListStep'
CREATE INDEX [IX_FK_CheckListStep]
ON [dbo].[CheckListItems]
    ([CheckListId]);
GO

-- Creating foreign key on [EmployeeEmployeeId] in table 'CheckLists'
ALTER TABLE [dbo].[CheckLists]
ADD CONSTRAINT [FK_EmployeeCheckList]
    FOREIGN KEY ([EmployeeEmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeCheckList'
CREATE INDEX [IX_FK_EmployeeCheckList]
ON [dbo].[CheckLists]
    ([EmployeeEmployeeId]);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------