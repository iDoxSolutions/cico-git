CREATE TABLE [dbo].[AccessFields] (
    [Id] [int] NOT NULL IDENTITY,
    [FieldName] [nvarchar](max) NOT NULL,
    [FieldDescription] [nvarchar](max) NOT NULL,
    [EmpDep] [nvarchar](max),
    CONSTRAINT [PK_dbo.AccessFields] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[AccessFieldRights] (
    [Id] [int] NOT NULL IDENTITY,
    [Access] [nvarchar](max) NOT NULL,
    [EmpDep] [nvarchar](max),
    [AccessField_Id] [int] NOT NULL,
    [Office_OfficeId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AccessFieldRights] PRIMARY KEY ([Id])
	)
ALTER TABLE [dbo].[AccessFieldRights] ADD CONSTRAINT [FK_dbo.AccessFieldRights_dbo.AccessFields_AccessField_Id] FOREIGN KEY ([AccessField_Id]) REFERENCES [dbo].[AccessFields] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AccessFieldRights] ADD CONSTRAINT [FK_dbo.AccessFieldRights_dbo.Offices_Office_OfficeId] FOREIGN KEY ([Office_OfficeId]) REFERENCES [dbo].[Offices] ([OfficeId]) ON DELETE CASCADE
