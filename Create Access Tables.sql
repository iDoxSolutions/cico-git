CREATE TABLE [dbo].[AccessFieldRights](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessField_Id] [int] NOT NULL,
	[Office_OfficeId] [int] NOT NULL,
	[Access] [char](1) NULL,
	[EmpDep] [nvarchar](50) NULL,
 CONSTRAINT [PK_AccessFieldsRights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[AccessFields](
	[Id] [int] NOT NULL,
	[FieldName] [nvarchar](255) NULL,
	[FieldDescription] [nvarchar](255) NULL
) ON [PRIMARY]