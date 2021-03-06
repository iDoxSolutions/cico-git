

CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO



GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorXml]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application




GO
/****** Object:  StoredProcedure [dbo].[ELMAH_LogError]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NVARCHAR(MAX),
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )


		
GO
/****** Object:  Table [dbo].[AppFeatures]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppFeatures](
	[Name] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CheckListItemSubmitionTracks]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckListItemSubmitionTracks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Checked] [bit] NOT NULL,
	[Provisioned] [bit] NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CheckListSession_Id] [int] NULL,
	[CheckListItemTemplate_CheckListItemTemplateId] [int] NULL,
	[SubmittedFile_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CheckListItemTemplates]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckListItemTemplates](
	[CheckListItemTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Item] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Priority] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Viewable] [nvarchar](max) NULL,
	[Subscriber] [nvarchar](max) NULL,
	[CheckListId] [nvarchar](max) NULL,
	[DueDate] [datetime] NULL,
	[Instructions] [nvarchar](max) NULL,
	[EmployeeComplete] [nvarchar](max) NULL,
	[OfficeComplete] [nvarchar](max) NULL,
	[CompleteCheckList] [bit] NOT NULL,
	[NotesAccess] [bit] NOT NULL,
	[Provisional] [bit] NOT NULL,
	[Document] [nvarchar](max) NULL,
	[Form] [nvarchar](max) NULL,
	[Dependents] [bit] NOT NULL,
	[InstructionText] [nvarchar](max) NULL,
	[ApprovalText] [nvarchar](max) NULL,
	[Group] [nvarchar](max) NULL,
	[AlertDays] [nvarchar](max) NULL,
	[AlertFrenquency] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[CustomFormUrl] [nvarchar](max) NULL,
	[DueDays] [int] NOT NULL,
	[CompletingChecklist] [bit] NOT NULL,
	[Office_OfficeId] [int] NULL,
	[CheckListTemplate_CheckListTemplateId] [int] NULL,
	[SystemFile_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CheckListItemTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CheckListItemTypes]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckListItemTypes](
	[CheckListTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CheckListTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CheckLists]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckLists](
	[CheckListId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[Status] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CheckListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CheckListSessions]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckListSessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ReferenceDate] [datetime] NOT NULL,
	[DepartureDate] [datetime] NULL,
	[Completed] [bit] NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[Employee_Id] [int] NULL,
	[CheckListTemplate_CheckListTemplateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CheckListTemplates]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckListTemplates](
	[CheckListTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[Published] [bit] NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CheckListTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DependentFiles]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DependentFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[SystemFile_Id] [int] NULL,
	[Dependent_Id] [int] NULL,
	[CheckListItemSubmitionTrack_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dependents]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dependents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](65) NULL,
	[MiddleInitial] [nvarchar](1) NULL,
	[LastName] [nvarchar](66) NULL,
	[Relationship] [nvarchar](20) NULL,
	[DateOfBirth] [datetime] NULL,
	[Title] [nvarchar](65) NULL,
	[PreferredName] [nvarchar](65) NULL,
	[Nationality] [nvarchar](10) NULL,
	[HomePhone] [nvarchar](max) NULL,
	[CellPhone] [nvarchar](max) NULL,
	[HomePhone2] [nvarchar](max) NULL,
	[AgencyOrSection] [nvarchar](max) NULL,
	[HomeAddress] [nvarchar](100) NULL,
	[PersonalEmail] [nvarchar](max) NULL,
	[PassportNumber] [nvarchar](max) NULL,
	[PassportType] [nvarchar](max) NULL,
	[PassportExpiration] [datetime] NULL,
	[VisaNumber] [nvarchar](max) NULL,
	[VisaExpiration] [datetime] NULL,
	[PostOfAssignment] [nvarchar](max) NULL,
	[OfficePhone] [nvarchar](max) NULL,
	[SameECData] [bit] NOT NULL,
	[Extension] [nvarchar](5) NULL,
	[EmergencyContactEmail] [nvarchar](max) NULL,
	[EmergencyContactFirstName] [nvarchar](65) NULL,
	[EmergencyContactLastName] [nvarchar](65) NULL,
	[EmergencyContactRelationship] [nvarchar](30) NULL,
	[EmergencyContactPhone] [nvarchar](max) NULL,
	[EmergencyContactPhone2] [nvarchar](max) NULL,
	[EmergencyContactOfficePhone] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[Employee_Id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentTemplates]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentTitle] [nvarchar](66) NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[SystemFile_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DropdownItems]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DropdownItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NOT NULL,
	[ValueType] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailSubscriptions]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSubscriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CheckListItemTemplate_CheckListItemTemplateId] [int] NULL,
	[Staff_UserId] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FirstName] [nvarchar](65) NULL,
	[MiddleInitial] [nvarchar](1) NULL,
	[Agency] [nvarchar](65) NULL,
	[PreferredName] [nvarchar](65) NULL,
	[LastName] [nvarchar](65) NULL,
	[DateOfBirth] [datetime] NULL,
	[DiplomaticTitle] [nvarchar](65) NULL,
	[Title] [nvarchar](65) NULL,
	[BankAccount] [nvarchar](30) NULL,
	[RoutingNumber] [nvarchar](30) NULL,
	[Nationality] [nvarchar](60) NULL,
	[HomePhone] [nvarchar](max) NULL,
	[CellPhone] [nvarchar](max) NULL,
	[HomePhone2] [nvarchar](max) NULL,
	[OfficeCellPhone] [nvarchar](max) NULL,
	[ArrivalDate] [datetime] NULL,
	[TourEndDate] [datetime] NULL,
	[AgencyOrSection] [nvarchar](max) NULL,
	[PositionTitle] [nvarchar](65) NULL,
	[HomeAddress] [nvarchar](100) NULL,
	[Location] [nvarchar](20) NULL,
	[PriorPostCity] [nvarchar](65) NULL,
	[PriorPostCountry] [nvarchar](65) NULL,
	[PriorPostState] [nvarchar](65) NULL,
	[PersonalEmail] [nvarchar](max) NULL,
	[WorkEmail] [nvarchar](max) NULL,
	[PassportNumber] [nvarchar](max) NULL,
	[PassportType] [nvarchar](max) NULL,
	[PassportExpiration] [datetime] NULL,
	[VisaNumber] [nvarchar](max) NULL,
	[VisaExpiration] [datetime] NULL,
	[PostOfAssignment] [nvarchar](max) NULL,
	[Office] [nvarchar](65) NULL,
	[OfficePhone] [nvarchar](max) NULL,
	[Extension] [nvarchar](max) NULL,
	[PreArrivalPhone] [nvarchar](max) NULL,
	[EmergencyContactEmail] [nvarchar](max) NULL,
	[EmergencyContactFirstName] [nvarchar](65) NULL,
	[EmergencyContactLastName] [nvarchar](65) NULL,
	[EmergencyContactRelationship] [nvarchar](30) NULL,
	[EmergencyContactPhone] [nvarchar](max) NULL,
	[EmergencyContactPhone2] [nvarchar](max) NULL,
	[EmergencyContactOfficePhone] [nvarchar](max) NULL,
	[LegalResidenceState] [nvarchar](2) NULL,
	[ProxyEmail] [nvarchar](max) NULL,
	[Gender] [nvarchar](65) NULL,
	[BloodType] [nvarchar](5) NULL,
	[ResidentialSafeWord] [nvarchar](65) NULL,
	[SchoolName] [nvarchar](65) NULL,
	[RadioCallSign] [nvarchar](65) NULL,
	[ClearanceLevel] [nvarchar](65) NULL,
	[LicensePlate] [nvarchar](65) NULL,
	[SSN] [nvarchar](11) NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[staffId] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CheckListItemSubmitionTrack_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Offices]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offices](
	[OfficeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](66) NULL,
	[ContactUser] [nvarchar](65) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OfficeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reminders]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Checklisttype] [nvarchar](max) NULL,
	[DateToSend] [int] NOT NULL,
	[ChecklistDescription] [nvarchar](66) NULL,
	[MessageSubject] [nvarchar](66) NULL,
	[MessagePreface] [nvarchar](66) NULL,
	[MessageClosing] [nvarchar](66) NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CheckListItemTemplate_CheckListItemTemplateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Settings]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staffs]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staffs](
	[UserId] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[ReqireCheckList] [bit] NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[Office_OfficeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemFiles]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Path] [nvarchar](max) NULL,
	[Extension] [nvarchar](max) NULL,
	[FileType] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemRoleAppFeatures]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemRoleAppFeatures](
	[SystemRole_Name] [nvarchar](100) NOT NULL,
	[AppFeature_Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SystemRole_Name] ASC,
	[AppFeature_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemRoles]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemRoles](
	[Name] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](100) NULL,
	[DateEdited] [datetime] NULL,
	[UserEdited] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemRoleStaffs]    Script Date: 4/12/2013 7:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemRoleStaffs](
	[SystemRole_Name] [nvarchar](100) NOT NULL,
	[Staff_UserId] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SystemRole_Name] ASC,
	[Staff_UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[AppFeatures] ([Name], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (N'EDIT_CHECKLIST', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[CheckListItemSubmitionTracks] ON 

GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (1, 0, 0, CAST(0x0000A19E0139FF39 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF39 AS DateTime), NULL, 1, 2, 1, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (2, 1, 0, CAST(0x0000A19E0139FF61 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF61 AS DateTime), NULL, 1, 2, 2, 3)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (3, 0, 0, CAST(0x0000A19E0139FF62 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF62 AS DateTime), NULL, 1, 2, 3, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (4, 0, 0, CAST(0x0000A19E0139FF63 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF63 AS DateTime), NULL, 1, 2, 4, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (5, 0, 0, CAST(0x0000A19E0139FF6A AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF6A AS DateTime), NULL, 1, 2, 5, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (6, 0, 0, CAST(0x0000A19E0139FF6A AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF6A AS DateTime), NULL, 1, 2, 6, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (7, 0, 0, CAST(0x0000A19E0139FF6D AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF6D AS DateTime), NULL, 1, 2, 7, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (8, 0, 0, CAST(0x0000A19E0139FF6E AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF6E AS DateTime), NULL, 1, 2, 8, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (9, 0, 0, CAST(0x0000A19E0139FF6F AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF6F AS DateTime), NULL, 1, 2, 9, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (10, 0, 0, CAST(0x0000A19E0139FF70 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF70 AS DateTime), NULL, 1, 2, 10, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (11, 0, 0, CAST(0x0000A19E0139FF70 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF70 AS DateTime), NULL, 1, 2, 11, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (12, 0, 0, CAST(0x0000A19E0139FF71 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF71 AS DateTime), NULL, 1, 2, 12, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (13, 0, 0, CAST(0x0000A19E0139FF72 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF72 AS DateTime), NULL, 1, 2, 13, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (14, 0, 0, CAST(0x0000A19E0139FF73 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF73 AS DateTime), NULL, 1, 2, 14, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (15, 0, 0, CAST(0x0000A19E0139FF74 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF74 AS DateTime), NULL, 1, 2, 15, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (16, 0, 0, CAST(0x0000A19E0139FF75 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0139FF75 AS DateTime), NULL, 1, 2, 16, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (17, 0, 0, CAST(0x0000A19E01423B79 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B79 AS DateTime), NULL, 1, 3, 1, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (18, 0, 0, CAST(0x0000A19E01423B7C AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B7C AS DateTime), NULL, 1, 3, 2, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (19, 0, 0, CAST(0x0000A19E01423B7D AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B7D AS DateTime), NULL, 1, 3, 3, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (20, 0, 0, CAST(0x0000A19E01423B7D AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B7D AS DateTime), NULL, 1, 3, 4, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (21, 0, 0, CAST(0x0000A19E01423B7E AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B7E AS DateTime), NULL, 1, 3, 5, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (22, 0, 0, CAST(0x0000A19E01423B80 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B80 AS DateTime), NULL, 1, 3, 6, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (23, 0, 0, CAST(0x0000A19E01423B80 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B80 AS DateTime), NULL, 1, 3, 7, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (24, 0, 0, CAST(0x0000A19E01423B81 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B81 AS DateTime), NULL, 1, 3, 8, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (25, 0, 0, CAST(0x0000A19E01423B82 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B82 AS DateTime), NULL, 1, 3, 9, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (26, 0, 0, CAST(0x0000A19E01423B83 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B83 AS DateTime), NULL, 1, 3, 10, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (27, 0, 0, CAST(0x0000A19E01423B84 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B84 AS DateTime), NULL, 1, 3, 11, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (28, 0, 0, CAST(0x0000A19E01423B85 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B85 AS DateTime), NULL, 1, 3, 12, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (29, 0, 0, CAST(0x0000A19E01423B86 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B86 AS DateTime), NULL, 1, 3, 13, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (30, 0, 0, CAST(0x0000A19E01423B86 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B86 AS DateTime), NULL, 1, 3, 14, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (31, 0, 0, CAST(0x0000A19E01423B87 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B87 AS DateTime), NULL, 1, 3, 15, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (32, 0, 0, CAST(0x0000A19E01423B89 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01423B89 AS DateTime), NULL, 1, 3, 16, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (33, 0, 0, CAST(0x0000A19E0142A352 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A352 AS DateTime), NULL, 1, 4, 1, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (34, 0, 0, CAST(0x0000A19E0142A354 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A354 AS DateTime), NULL, 1, 4, 2, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (35, 0, 0, CAST(0x0000A19E0142A355 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A355 AS DateTime), NULL, 1, 4, 3, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (36, 0, 0, CAST(0x0000A19E0142A356 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A356 AS DateTime), NULL, 1, 4, 4, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (37, 0, 0, CAST(0x0000A19E0142A356 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A356 AS DateTime), NULL, 1, 4, 5, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (38, 0, 0, CAST(0x0000A19E0142A357 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A357 AS DateTime), NULL, 1, 4, 6, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (39, 0, 0, CAST(0x0000A19E0142A358 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A358 AS DateTime), NULL, 1, 4, 7, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (40, 0, 0, CAST(0x0000A19E0142A359 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A359 AS DateTime), NULL, 1, 4, 8, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (41, 0, 0, CAST(0x0000A19E0142A35A AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A35A AS DateTime), NULL, 1, 4, 9, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (42, 0, 0, CAST(0x0000A19E0142A35A AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A35A AS DateTime), NULL, 1, 4, 10, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (43, 0, 0, CAST(0x0000A19E0142A35B AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A35B AS DateTime), NULL, 1, 4, 11, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (44, 0, 0, CAST(0x0000A19E0142A35C AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A35C AS DateTime), NULL, 1, 4, 12, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (45, 0, 0, CAST(0x0000A19E0142A35D AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A35D AS DateTime), NULL, 1, 4, 13, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (46, 0, 0, CAST(0x0000A19E0142A35E AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A35E AS DateTime), NULL, 1, 4, 14, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (47, 0, 0, CAST(0x0000A19E0142A35F AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A35F AS DateTime), NULL, 1, 4, 15, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (48, 0, 0, CAST(0x0000A19E0142A360 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142A360 AS DateTime), NULL, 1, 4, 16, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (49, 1, 0, CAST(0x0000A19E0142EF57 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF57 AS DateTime), NULL, 1, 5, 1, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (50, 0, 0, CAST(0x0000A19E0142EF5B AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF5B AS DateTime), NULL, 1, 5, 2, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (51, 0, 0, CAST(0x0000A19E0142EF5C AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF5C AS DateTime), NULL, 1, 5, 3, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (52, 0, 0, CAST(0x0000A19E0142EF5C AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF5C AS DateTime), NULL, 1, 5, 4, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (53, 0, 0, CAST(0x0000A19E0142EF62 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF62 AS DateTime), NULL, 1, 5, 5, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (54, 0, 0, CAST(0x0000A19E0142EF63 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF63 AS DateTime), NULL, 1, 5, 6, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (55, 0, 0, CAST(0x0000A19E0142EF66 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF66 AS DateTime), NULL, 1, 5, 7, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (56, 0, 0, CAST(0x0000A19E0142EF67 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF67 AS DateTime), NULL, 1, 5, 8, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (57, 0, 0, CAST(0x0000A19E0142EF67 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF67 AS DateTime), NULL, 1, 5, 9, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (58, 0, 0, CAST(0x0000A19E0142EF68 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF68 AS DateTime), NULL, 1, 5, 10, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (59, 0, 0, CAST(0x0000A19E0142EF69 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF69 AS DateTime), NULL, 1, 5, 11, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (60, 0, 0, CAST(0x0000A19E0142EF69 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF69 AS DateTime), NULL, 1, 5, 12, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (61, 0, 0, CAST(0x0000A19E0142EF6A AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF6A AS DateTime), NULL, 1, 5, 13, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (62, 0, 0, CAST(0x0000A19E0142EF6B AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF6B AS DateTime), NULL, 1, 5, 14, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (63, 0, 0, CAST(0x0000A19E0142EF6C AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF6C AS DateTime), NULL, 1, 5, 15, NULL)
GO
INSERT [dbo].[CheckListItemSubmitionTracks] ([Id], [Checked], [Provisioned], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [CheckListSession_Id], [CheckListItemTemplate_CheckListItemTemplateId], [SubmittedFile_Id]) VALUES (64, 0, 0, CAST(0x0000A19E0142EF6C AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142EF6C AS DateTime), NULL, 1, 5, 16, NULL)
GO
SET IDENTITY_INSERT [dbo].[CheckListItemSubmitionTracks] OFF
GO
SET IDENTITY_INSERT [dbo].[CheckListItemTemplates] ON 

GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (1, N'DocumentApproval', N'Submit passport copy', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'<p>Upload a copy of your Passport. May be electronic copy, scanned or photographed.</p>', N'<p>approval</p>', NULL, NULL, NULL, N'DocumentSubmitted', N'custom', 0, 0, 1, 1, NULL)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (2, N'DocumentSubmitted', N'Submit visa', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Upload a copy of your Mexico Visa.  May be electronic copy, scanned or photographed.', NULL, NULL, N'3', NULL, N'DocumentSubmitted', NULL, 0, 0, 1, 1, NULL)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (3, N'DocumentWriting', N'Sign Standards of Conduct', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download the Standards of Conduct, read and sign.  Then upload copy.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 1, 1, NULL)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (4, N'DocumentSubmitted', N'Submit travel orders', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Upload a copy of your Travel Orders.  May be electronic copy, scanned or photographed.', NULL, NULL, NULL, NULL, N'DocumentSubmitted', NULL, 0, 0, 1, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (5, N'DocumentWriting', N'Submit bio form', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download & complete the Biographical History form.  Then upload copy.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 1, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (6, N'PhysicalActivity', N'Attend orientation', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, NULL, NULL, 0, N'Schedule and attend the Newcomer Orientation session.  Contact GSO at this address to schedule your attendence.', NULL, NULL, N'3', NULL, N'PhysicalActivity', NULL, 0, 0, 2, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (7, N'PhysicalActivity', N'RSO security briefing', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, NULL, NULL, 0, N'Schedule and attend the RSO Security Briefing.  Contact RSO at this address to schedule your attendence.', NULL, NULL, N'3', NULL, N'PhysicalActivity', NULL, 0, 0, 3, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (8, N'DocumentWriting', N'Submit EFT form', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download & complete the Electronic Funds Transfer form.  Then upload copy.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 4, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (9, N'DocumentSubmitted', N'Computer security requirements', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download the Computer Security Requirements document, then indicate your agreement by checking the box.', NULL, NULL, N'3', NULL, N'DocumentSubmitted', NULL, 0, 0, 5, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (10, N'DocumentWriting', N'Unclass ISC user form', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download & complete the Unclassified System User form.  Then upload copy.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 5, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (11, N'DocumentWriting', N'Deliver medical records', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, NULL, NULL, 0, N'Hand deliver a copy of your and your dependents'' medical records to the Health Unit ASAP after arrival.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 6, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (12, N'DocumentWriting', N'Family network data sheet', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download & complete the Family Network Data Sheet.  Then upload copy.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 7, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (13, N'PhysicalActivity', N'Schedule mtg with Ambassador', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, NULL, NULL, 0, N'Schedule a meeting with the Ambassador by contacting Anna in ADM at anne@state.gov', NULL, NULL, N'3', NULL, N'PhysicalActivity', NULL, 0, 0, 8, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (14, N'PhysicalActivity', N'Schedule mtg with IT', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, NULL, NULL, 0, N'Schedule a meeting with the the IT group by contacting James in IT at James@state.gov', NULL, NULL, N'3', NULL, N'PhysicalActivity', NULL, 0, 0, 8, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (15, N'DocumentWriting', N'Submit housing questionaire', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download & complete the Housing Questionaire form.  Then upload copy.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 2, 1, 1)
GO
INSERT [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId], [Item], [Description], [Priority], [Status], [Viewable], [Subscriber], [CheckListId], [DueDate], [Instructions], [EmployeeComplete], [OfficeComplete], [CompleteCheckList], [NotesAccess], [Provisional], [Document], [Form], [Dependents], [InstructionText], [ApprovalText], [Group], [AlertDays], [AlertFrenquency], [Type], [CustomFormUrl], [DueDays], [CompletingChecklist], [Office_OfficeId], [CheckListTemplate_CheckListTemplateId], [SystemFile_Id]) VALUES (16, N'DocumentWriting', N'Submit HHE inventory', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, NULL, NULL, 0, N'Download & complete the HHE/UAB Inventory form.  Then upload copy.', NULL, NULL, N'3', NULL, N'DocumentWriting', NULL, 0, 0, 2, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[CheckListItemTemplates] OFF
GO
SET IDENTITY_INSERT [dbo].[CheckListItemTypes] ON 

GO
INSERT [dbo].[CheckListItemTypes] ([CheckListTypeId], [Name], [Description]) VALUES (1, N'OnlineForm', N'Online Form')
GO
INSERT [dbo].[CheckListItemTypes] ([CheckListTypeId], [Name], [Description]) VALUES (2, N'DocumentSubmitted', N'Document Submitted')
GO
INSERT [dbo].[CheckListItemTypes] ([CheckListTypeId], [Name], [Description]) VALUES (3, N'DocumentWriting', N'Document w/Writing')
GO
INSERT [dbo].[CheckListItemTypes] ([CheckListTypeId], [Name], [Description]) VALUES (4, N'PhysicalActivity', N'Physical Activity')
GO
INSERT [dbo].[CheckListItemTypes] ([CheckListTypeId], [Name], [Description]) VALUES (5, N'DocumentApproval', N'Document w/Online Approval')
GO
SET IDENTITY_INSERT [dbo].[CheckListItemTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[CheckListSessions] ON 

GO
INSERT [dbo].[CheckListSessions] ([Id], [UserId], [ReferenceDate], [DepartureDate], [Completed], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Employee_Id], [CheckListTemplate_CheckListTemplateId]) VALUES (1, N'ABAPER-W8\Pawel', CAST(0x0000A19E00000000 AS DateTime), NULL, 0, CAST(0x0000A19E01142FA5 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01142FA5 AS DateTime), NULL, 0, 1, 1)
GO
INSERT [dbo].[CheckListSessions] ([Id], [UserId], [ReferenceDate], [DepartureDate], [Completed], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Employee_Id], [CheckListTemplate_CheckListTemplateId]) VALUES (2, N'ABAPER-W8\Pawel', CAST(0x0000A19E00000000 AS DateTime), NULL, 0, CAST(0x0000A19E01387A12 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01387A12 AS DateTime), NULL, 0, 1, 1)
GO
INSERT [dbo].[CheckListSessions] ([Id], [UserId], [ReferenceDate], [DepartureDate], [Completed], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Employee_Id], [CheckListTemplate_CheckListTemplateId]) VALUES (3, N'ABAPER-W8\Pawel', CAST(0x0000A19E00000000 AS DateTime), NULL, 0, CAST(0x0000A19E01422F7B AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E01422F7B AS DateTime), NULL, 0, 1, 1)
GO
INSERT [dbo].[CheckListSessions] ([Id], [UserId], [ReferenceDate], [DepartureDate], [Completed], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Employee_Id], [CheckListTemplate_CheckListTemplateId]) VALUES (4, N'ABAPER-W8\Pawel', CAST(0x0000A19E00000000 AS DateTime), NULL, 0, CAST(0x0000A19E0142998B AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142998B AS DateTime), NULL, 0, 1, 1)
GO
INSERT [dbo].[CheckListSessions] ([Id], [UserId], [ReferenceDate], [DepartureDate], [Completed], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Employee_Id], [CheckListTemplate_CheckListTemplateId]) VALUES (5, N'ABAPER-W8\Pawel', CAST(0x0000A19E00000000 AS DateTime), NULL, 0, CAST(0x0000A19E0142ED90 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E0142ED90 AS DateTime), NULL, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[CheckListSessions] OFF
GO
SET IDENTITY_INSERT [dbo].[CheckListTemplates] ON 

GO
INSERT [dbo].[CheckListTemplates] ([CheckListTemplateId], [Type], [Name], [StartDate], [EndDate], [DueDate], [Published], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (1, N'CheckIn', N'Check In', NULL, NULL, NULL, 1, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[CheckListTemplates] ([CheckListTemplateId], [Type], [Name], [StartDate], [EndDate], [DueDate], [Published], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (2, N'CheckOut', N'Check Out', NULL, NULL, NULL, 1, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[CheckListTemplates] OFF
GO
SET IDENTITY_INSERT [dbo].[Dependents] ON 

GO
INSERT [dbo].[Dependents] ([Id], [FirstName], [MiddleInitial], [LastName], [Relationship], [DateOfBirth], [Title], [PreferredName], [Nationality], [HomePhone], [CellPhone], [HomePhone2], [AgencyOrSection], [HomeAddress], [PersonalEmail], [PassportNumber], [PassportType], [PassportExpiration], [VisaNumber], [VisaExpiration], [PostOfAssignment], [OfficePhone], [SameECData], [Extension], [EmergencyContactEmail], [EmergencyContactFirstName], [EmergencyContactLastName], [EmergencyContactRelationship], [EmergencyContactPhone], [EmergencyContactPhone2], [EmergencyContactOfficePhone], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Employee_Id]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A19E01467C3E AS DateTime), N'ABAPER-W8\Pawel', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Dependents] OFF
GO
SET IDENTITY_INSERT [dbo].[DropdownItems] ON 

GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (1, N'AL', N'Alabama', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (2, N'AK', N'Alaska', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (3, N'AZ', N'Arizona', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (4, N'AR', N'Arkansas', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (5, N'CA', N'California', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (6, N'CO', N'Colorado', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (7, N'CT', N'Conneciticut', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (8, N'DE', N'Delaware', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (9, N'FL', N'Florida', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (10, N'GA', N'Georgia', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (11, N'HI', N'Hawaii', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (12, N'ID', N'Idaho', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (13, N'IL', N'Illinois', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (14, N'IN', N'Indiana', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (15, N'IA', N'Iowa', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (16, N'KS', N'Kansas', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (17, N'KY', N'Kentucky', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (18, N'LA', N'Louisana', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (19, N'ME', N'Maine', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (20, N'MD', N'Maryland', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (21, N'MA', N'Massachusetts', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (22, N'MI', N'Michigan', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (23, N'MN', N'Minnesota', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (24, N'MS', N'Missippi', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (25, N'MO', N'Missouri', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (26, N'MT', N'Montana', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (27, N'NE', N'Nebraska', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (28, N'NV', N'Nevada', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (29, N'NH', N'New Hampshire', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (30, N'NJ', N'New Jersey', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (31, N'NM', N'New Mexico', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (32, N'NY', N'New York', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (33, N'NC', N'North Carolina', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (34, N'ND', N'North Dakota', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (35, N'OH', N'Ohio', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (36, N'OK', N'Oklohoma', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (37, N'OR', N'Oregon', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (38, N'PA', N'Pennsylvania', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (39, N'RI', N'Rhode Island', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (40, N'SC', N'South Carolina', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (41, N'SD', N'South Dakota', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (42, N'TN', N'Tennessee', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (43, N'TX', N'Texas', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (44, N'UT', N'Utah', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (45, N'VT', N'Vermont', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (46, N'VA', N'Virginia', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (47, N'WA', N'Washington', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (48, N'MDWV', N'West Virginia', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (49, NULL, N'AMERICAN SAMOA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (50, NULL, N'ANDORRA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (51, NULL, N'ANGOLA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (52, NULL, N'ANGUILLA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (53, NULL, N'ANTARCTICA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (54, NULL, N'ANTIGUA AND BARBUDA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (55, NULL, N'ARGENTINA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (56, NULL, N'ARMENIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (57, NULL, N'ARUBA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (58, NULL, N'AUSTRALIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (59, NULL, N'AUSTRIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (60, NULL, N'AZERBAIJAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (61, NULL, N'BAHAMAS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (62, NULL, N'BAHRAIN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (63, NULL, N'BANGLADESH', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (64, NULL, N'BARBADOS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (65, NULL, N'BELARUS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (66, NULL, N'BELGIUM', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (67, NULL, N'BELIZE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (68, NULL, N'BENIN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (69, NULL, N'BERMUDA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (70, NULL, N'BHUTAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (71, NULL, N'BOLIVIA, PLURINATIONAL STATE OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (72, NULL, N'BONAIRE, SINT EUSTATIUS AND SABA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (73, NULL, N'BOSNIA AND HERZEGOVINA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (74, NULL, N'BOTSWANA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (75, NULL, N'BOUVET ISLAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (76, NULL, N'BRAZIL', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (77, NULL, N'BRITISH INDIAN OCEAN TERRITORY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (78, NULL, N'BRUNEI DARUSSALAM', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (79, NULL, N'BULGARIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (80, NULL, N'BURKINA FASO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (81, NULL, N'BURUNDI', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (82, NULL, N'CAMBODIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (83, NULL, N'CAMEROON', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (84, NULL, N'CANADA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (85, NULL, N'CAPE VERDE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (86, NULL, N'CAYMAN ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (87, NULL, N'CENTRAL AFRICAN REPUBLIC', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (88, NULL, N'CHAD', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (89, NULL, N'CHILE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (90, NULL, N'CHINA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (91, NULL, N'CHRISTMAS ISLAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (92, NULL, N'COCOS (KEELING) ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (93, NULL, N'COLOMBIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (94, NULL, N'COMOROS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (95, NULL, N'CONGO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (96, NULL, N'CONGO, THE DEMOCRATIC REPUBLIC OF THE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (97, NULL, N'COOK ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (98, NULL, N'COSTA RICA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (99, NULL, N'CÔTE D''IVOIRE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (100, NULL, N'CROATIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (101, NULL, N'CUBA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (102, NULL, N'CURAÇAO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (103, NULL, N'CYPRUS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (104, NULL, N'CZECH REPUBLIC', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (105, NULL, N'DENMARK', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (106, NULL, N'DJIBOUTI', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (107, NULL, N'DOMINICA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (108, NULL, N'DOMINICAN REPUBLIC', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (109, NULL, N'ECUADOR', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (110, NULL, N'EGYPT', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (111, NULL, N'EL SALVADOR', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (112, NULL, N'EQUATORIAL GUINEA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (113, NULL, N'ERITREA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (114, NULL, N'ESTONIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (115, NULL, N'ETHIOPIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (116, NULL, N'FALKLAND ISLANDS (MALVINAS)', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (117, NULL, N'FAROE ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (118, NULL, N'FIJI', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (119, NULL, N'FINLAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (120, NULL, N'FRANCE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (121, NULL, N'FRENCH GUIANA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (122, NULL, N'FRENCH POLYNESIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (123, NULL, N'FRENCH SOUTHERN TERRITORIES', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (124, NULL, N'GABON', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (125, NULL, N'GAMBIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (126, NULL, N'GEORGIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (127, NULL, N'GERMANY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (128, NULL, N'GHANA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (129, NULL, N'GIBRALTAR', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (130, NULL, N'GREECE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (131, NULL, N'GREENLAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (132, NULL, N'GRENADA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (133, NULL, N'GUADELOUPE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (134, NULL, N'GUAM', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (135, NULL, N'GUATEMALA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (136, NULL, N'GUERNSEY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (137, NULL, N'GUINEA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (138, NULL, N'GUINEA-BISSAU', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (139, NULL, N'GUYANA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (140, NULL, N'HAITI', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (141, NULL, N'HEARD ISLAND AND MCDONALD ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (142, NULL, N'HOLY SEE (VATICAN CITY STATE)', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (143, NULL, N'HONDURAS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (144, NULL, N'HONG KONG', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (145, NULL, N'HUNGARY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (146, NULL, N'ICELAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (147, NULL, N'INDIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (148, NULL, N'INDONESIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (149, NULL, N'IRAN, ISLAMIC REPUBLIC OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (150, NULL, N'IRAQ', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (151, NULL, N'IRELAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (152, NULL, N'ISLE OF MAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (153, NULL, N'ISRAEL', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (154, NULL, N'ITALY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (155, NULL, N'JAMAICA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (156, NULL, N'JAPAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (157, NULL, N'JERSEY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (158, NULL, N'JORDAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (159, NULL, N'KAZAKHSTAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (160, NULL, N'KENYA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (161, NULL, N'KIRIBATI', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (162, NULL, N'KOREA, DEMOCRATIC PEOPLE''S REPUBLIC OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (163, NULL, N'KOREA, REPUBLIC OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (164, NULL, N'KUWAIT', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (165, NULL, N'KYRGYZSTAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (166, NULL, N'LAO PEOPLE''S DEMOCRATIC REPUBLIC', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (167, NULL, N'LATVIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (168, NULL, N'LEBANON', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (169, NULL, N'LESOTHO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (170, NULL, N'LIBERIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (171, NULL, N'LIBYA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (172, NULL, N'LIECHTENSTEIN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (173, NULL, N'LITHUANIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (174, NULL, N'LUXEMBOURG', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (175, NULL, N'MACAO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (176, NULL, N'MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (177, NULL, N'MADAGASCAR', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (178, NULL, N'MALAWI', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (179, NULL, N'MALAYSIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (180, NULL, N'MALDIVES', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (181, NULL, N'MALI', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (182, NULL, N'MALTA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (183, NULL, N'MARSHALL ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (184, NULL, N'MARTINIQUE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (185, NULL, N'MAURITANIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (186, NULL, N'MAURITIUS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (187, NULL, N'MAYOTTE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (188, NULL, N'MEXICO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (189, NULL, N'MICRONESIA, FEDERATED STATES OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (190, NULL, N'MOLDOVA, REPUBLIC OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (191, NULL, N'MONACO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (192, NULL, N'MONGOLIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (193, NULL, N'MONTENEGRO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (194, NULL, N'MONTSERRAT', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (195, NULL, N'MOROCCO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (196, NULL, N'MOZAMBIQUE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (197, NULL, N'MYANMAR', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (198, NULL, N'NAMIBIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (199, NULL, N'NAURU', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (200, NULL, N'NEPAL', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (201, NULL, N'NETHERLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (202, NULL, N'NEW CALEDONIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (203, NULL, N'NEW ZEALAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (204, NULL, N'NICARAGUA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (205, NULL, N'NIGER', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (206, NULL, N'NIGERIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (207, NULL, N'NIUE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (208, NULL, N'NORFOLK ISLAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (209, NULL, N'NORTHERN MARIANA ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (210, NULL, N'NORWAY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (211, NULL, N'OMAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (212, NULL, N'PAKISTAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (213, NULL, N'PALAU', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (214, NULL, N'PALESTINE, STATE OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (215, NULL, N'PANAMA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (216, NULL, N'PAPUA NEW GUINEA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (217, NULL, N'PARAGUAY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (218, NULL, N'PERU', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (219, NULL, N'PHILIPPINES', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (220, NULL, N'PITCAIRN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (221, NULL, N'POLAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (222, NULL, N'PORTUGAL', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (223, NULL, N'PUERTO RICO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (224, NULL, N'QATAR', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (225, NULL, N'RÉUNION', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (226, NULL, N'ROMANIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (227, NULL, N'RUSSIAN FEDERATION', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (228, NULL, N'RWANDA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (229, NULL, N'SAINT BARTHÉLEMY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (230, NULL, N'SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (231, NULL, N'SAINT KITTS AND NEVIS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (232, NULL, N'SAINT LUCIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (233, NULL, N'SAINT MARTIN (FRENCH PART)', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (234, NULL, N'SAINT PIERRE AND MIQUELON', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (235, NULL, N'SAINT VINCENT AND THE GRENADINES', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (236, NULL, N'SAMOA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (237, NULL, N'SAN MARINO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (238, NULL, N'SAO TOME AND PRINCIPE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (239, NULL, N'SAUDI ARABIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (240, NULL, N'SENEGAL', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (241, NULL, N'SERBIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (242, NULL, N'SEYCHELLES', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (243, NULL, N'SIERRA LEONE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (244, NULL, N'SINGAPORE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (245, NULL, N'SINT MAARTEN (DUTCH PART)', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (246, NULL, N'SLOVAKIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (247, NULL, N'SLOVENIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (248, NULL, N'SOLOMON ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (249, NULL, N'SOMALIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (250, NULL, N'SOUTH AFRICA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (251, NULL, N'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (252, NULL, N'SOUTH SUDAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (253, NULL, N'SPAIN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (254, NULL, N'SRI LANKA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (255, NULL, N'SUDAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (256, NULL, N'SURINAME', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (257, NULL, N'SVALBARD AND JAN MAYEN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (258, NULL, N'SWAZILAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (259, NULL, N'SWEDEN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (260, NULL, N'SWITZERLAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (261, NULL, N'SYRIAN ARAB REPUBLIC', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (262, NULL, N'TAIWAN, PROVINCE OF CHINA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (263, NULL, N'TAJIKISTAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (264, NULL, N'TANZANIA, UNITED REPUBLIC OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (265, NULL, N'THAILAND', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (266, NULL, N'TIMOR-LESTE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (267, NULL, N'TOGO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (268, NULL, N'TOKELAU', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (269, NULL, N'TONGA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (270, NULL, N'TRINIDAD AND TOBAGO', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (271, NULL, N'TUNISIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (272, NULL, N'TURKEY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (273, NULL, N'TURKMENISTAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (274, NULL, N'TURKS AND CAICOS ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (275, NULL, N'TUVALU', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (276, NULL, N'UGANDA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (277, NULL, N'UKRAINE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (278, NULL, N'UNITED ARAB EMIRATES', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (279, NULL, N'UNITED KINGDOM', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (280, NULL, N'UNITED STATES', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (281, NULL, N'UNITED STATES MINOR OUTLYING ISLANDS', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (282, NULL, N'URUGUAY', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (283, NULL, N'UZBEKISTAN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (284, NULL, N'VANUATU', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (285, NULL, N'VENEZUELA, BOLIVARIAN REPUBLIC OF', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (286, NULL, N'VIET NAM', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (287, NULL, N'VIRGIN ISLANDS, BRITISH', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (288, NULL, N'VIRGIN ISLANDS, U.S.', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (289, NULL, N'WALLIS AND FUTUNA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (290, NULL, N'WESTERN SAHARA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (291, NULL, N'YEMEN', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (292, NULL, N'ZAMBIA', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (293, NULL, N'ZIMBABWE', N'Nations', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (294, N'WI', N'Wisconsin', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (295, N'WY', N'Wyoming', N'States', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (296, N'IT', N'IT', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (297, N'HR', N'HR', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (298, N'FMC', N'FMC', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (299, N'ADM', N'ADM', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (300, N'GSO', N'GSO', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (301, N'RSO', N'RSO', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (302, N'IM', N'IM', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (303, N'HU', N'HU', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (304, N'CLO', N'CLO', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (305, N'AMB', N'AMB', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (306, N'Other', N'Other', N'Office', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (307, N'Location1', N'Location1', N'Location', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (308, N'Location2', N'Location2', N'Location', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (309, N'Location3', N'Location3', N'Location', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (310, N'Official', N'Official', N'PassportType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (311, N'Tourist', N'Tourist', N'PassportType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (312, N'Diplomatic', N'Diplomatic', N'PassportType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (313, N'PassportType1', N'PassportType1', N'PassportType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (314, N'PassportType2', N'PassportType2', N'PassportType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (315, N'PassportType3', N'PassportType3', N'PassportType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (316, N'Agency1', N'Agency1', N'Agency', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (317, N'Agency2', N'Agency2', N'Agency', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (318, N'Agency3', N'Agency3', N'Agency', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (319, N'Spouse', N'Spouse', N'Relationship', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (320, N'Sibling', N'Sibling', N'Relationship', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (321, N'Parent', N'Parent', N'Relationship', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (322, N'Child', N'Child', N'Relationship', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (323, N'Grandparent', N'Grandparent', N'Relationship', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (324, N'Other', N'Other', N'Relationship', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (325, N'Form1', N'Form 1', N'SystemForms', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (326, N'Form2', N'Form 2', N'SystemForms', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (327, N'Form3', N'Form 3', N'SystemForms', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (328, N'EmergencyContact', N'Emergency Contact', N'SystemForms', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (329, N'Checkin', N'Checkin', N'CheckListType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[DropdownItems] ([Id], [Key], [Description], [ValueType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (330, N'Checkout', N'Checkout', N'CheckListType', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[DropdownItems] OFF
GO
SET IDENTITY_INSERT [dbo].[EdmMetadata] ON 

GO
INSERT [dbo].[EdmMetadata] ([Id], [ModelHash]) VALUES (1, N'7ECD89B142D0B35DABAE262AA99CD5DBFA6F5136EC4445CB9D80AA21A64A744F')
GO
SET IDENTITY_INSERT [dbo].[EdmMetadata] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

GO
INSERT [dbo].[Employees] ([Id], [EmployeeId], [FirstName], [MiddleInitial], [Agency], [PreferredName], [LastName], [DateOfBirth], [DiplomaticTitle], [Title], [BankAccount], [RoutingNumber], [Nationality], [HomePhone], [CellPhone], [HomePhone2], [OfficeCellPhone], [ArrivalDate], [TourEndDate], [AgencyOrSection], [PositionTitle], [HomeAddress], [Location], [PriorPostCity], [PriorPostCountry], [PriorPostState], [PersonalEmail], [WorkEmail], [PassportNumber], [PassportType], [PassportExpiration], [VisaNumber], [VisaExpiration], [PostOfAssignment], [Office], [OfficePhone], [Extension], [PreArrivalPhone], [EmergencyContactEmail], [EmergencyContactFirstName], [EmergencyContactLastName], [EmergencyContactRelationship], [EmergencyContactPhone], [EmergencyContactPhone2], [EmergencyContactOfficePhone], [LegalResidenceState], [ProxyEmail], [Gender], [BloodType], [ResidentialSafeWord], [SchoolName], [RadioCallSign], [ClearanceLevel], [LicensePlate], [SSN], [UserId], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [staffId]) VALUES (1, 1, N'sdf', NULL, NULL, NULL, N'sdf', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A19E00000000 AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'wasilewski.pawel@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'ABAPER-W8\Pawel', NULL, NULL, CAST(0x0000A19E0146723A AS DateTime), N'ABAPER-W8\Pawel', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Offices] ON 

GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (1, N'HR', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (2, N'GSO', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (3, N'RSO', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (4, N'FMC', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (5, N'IM', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (6, N'HU', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (7, N'CLO', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (8, N'ADM', N'Ken Hambright')
GO
INSERT [dbo].[Offices] ([OfficeId], [Name], [ContactUser]) VALUES (9, N'AMB', N'Ken Hambright')
GO
SET IDENTITY_INSERT [dbo].[Offices] OFF
GO
INSERT [dbo].[Settings] ([Name], [Value]) VALUES (N'AppVersion', N'1.0')
GO
INSERT [dbo].[Settings] ([Name], [Value]) VALUES (N'checklisttemplate', N'1')
GO
INSERT [dbo].[Settings] ([Name], [Value]) VALUES (N'checkouttemplate', N'2')
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'ABAPER-W8\Pawel', N'wasilewski.pawel@gmail.com', 1, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'Lightkeeperdev\Ken', N'kenhambright@gmail.com', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'LTKSERVER\GlobalAdmin', N'wasilewski.pawel@gmail.com', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'LTKSERVER\GsoAdmin', N'kenhambright@gmail.com', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'LTKSERVER\HrAdmin', N'kenhambright@yahoo.com', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'Wha\AlvaradoYA', N'AlvaradoYA@state.gov', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'Wha\CarolloJH', N'CarolloJH@state.gov', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'WHA\hambrightkw', N'hambrightkw@state.gov', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'WHA\JorgeEA', N'JorgeEA@state.gov', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'Wha\MosquedaML', N'MosquedaML@state.gov', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'WHA\OrtegaRC', N'OrtegaRC@state.gov', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
INSERT [dbo].[Staffs] ([UserId], [Email], [ReqireCheckList], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active], [Office_OfficeId]) VALUES (N'WHA\RuizJC', N'RuizJC@state.gov', 0, CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[SystemFiles] ON 

GO
INSERT [dbo].[SystemFiles] ([Id], [Description], [Path], [Extension], [FileType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (1, N'Doc Template1', N'DocTemplates/Template1.docx', NULL, N'DocTemplate', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[SystemFiles] ([Id], [Description], [Path], [Extension], [FileType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (2, N'Doc Template2', N'DocTemplates/Template2.docx', NULL, N'DocTemplate', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[SystemFiles] ([Id], [Description], [Path], [Extension], [FileType], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (3, N'README.md', N'http://sharepoint.lightkeeper.co/testlib/Pawel/635013909493439490README.md', NULL, NULL, CAST(0x0000A19E013D6EA5 AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E013D6EA5 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[SystemFiles] OFF
GO
INSERT [dbo].[SystemRoleAppFeatures] ([SystemRole_Name], [AppFeature_Name]) VALUES (N'GlobalAdmin', N'EDIT_CHECKLIST')
GO
INSERT [dbo].[SystemRoles] ([Name], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (N'CheckListEditor', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[SystemRoles] ([Name], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (N'GlobalAdmin', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[SystemRoles] ([Name], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (N'OfficeAdmin', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[SystemRoles] ([Name], [DateCreated], [UserCreated], [DateEdited], [UserEdited], [Active]) VALUES (N'UserProxy', CAST(0x0000A19E010B99EC AS DateTime), N'ABAPER-W8\Pawel', CAST(0x0000A19E010B99EC AS DateTime), NULL, 1)
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'GlobalAdmin', N'ABAPER-W8\Pawel')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'GlobalAdmin', N'Lightkeeperdev\Ken')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'GlobalAdmin', N'LTKSERVER\GlobalAdmin')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'GlobalAdmin', N'WHA\hambrightkw')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'GlobalAdmin', N'WHA\RuizJC')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'OfficeAdmin', N'LTKSERVER\GsoAdmin')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'OfficeAdmin', N'LTKSERVER\HrAdmin')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'OfficeAdmin', N'Wha\AlvaradoYA')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'OfficeAdmin', N'Wha\CarolloJH')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'OfficeAdmin', N'WHA\JorgeEA')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'OfficeAdmin', N'Wha\MosquedaML')
GO
INSERT [dbo].[SystemRoleStaffs] ([SystemRole_Name], [Staff_UserId]) VALUES (N'OfficeAdmin', N'WHA\OrtegaRC')
GO
/****** Object:  Index [PK_ELMAH_Error]    Script Date: 4/12/2013 7:51:26 PM ******/
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ELMAH_Error_App_Time_Seq]    Script Date: 4/12/2013 7:51:26 PM ******/
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error]
(
	[Application] ASC,
	[TimeUtc] DESC,
	[Sequence] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
ALTER TABLE [dbo].[CheckListItemSubmitionTracks]  WITH CHECK ADD  CONSTRAINT [CheckListItemSubmitionTrack_CheckListItemTemplate] FOREIGN KEY([CheckListItemTemplate_CheckListItemTemplateId])
REFERENCES [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId])
GO
ALTER TABLE [dbo].[CheckListItemSubmitionTracks] CHECK CONSTRAINT [CheckListItemSubmitionTrack_CheckListItemTemplate]
GO
ALTER TABLE [dbo].[CheckListItemSubmitionTracks]  WITH CHECK ADD  CONSTRAINT [CheckListItemSubmitionTrack_CheckListSession] FOREIGN KEY([CheckListSession_Id])
REFERENCES [dbo].[CheckListSessions] ([Id])
GO
ALTER TABLE [dbo].[CheckListItemSubmitionTracks] CHECK CONSTRAINT [CheckListItemSubmitionTrack_CheckListSession]
GO
ALTER TABLE [dbo].[CheckListItemSubmitionTracks]  WITH CHECK ADD  CONSTRAINT [CheckListItemSubmitionTrack_SubmittedFile] FOREIGN KEY([SubmittedFile_Id])
REFERENCES [dbo].[SystemFiles] ([Id])
GO
ALTER TABLE [dbo].[CheckListItemSubmitionTracks] CHECK CONSTRAINT [CheckListItemSubmitionTrack_SubmittedFile]
GO
ALTER TABLE [dbo].[CheckListItemTemplates]  WITH CHECK ADD  CONSTRAINT [CheckListItemTemplate_Office] FOREIGN KEY([Office_OfficeId])
REFERENCES [dbo].[Offices] ([OfficeId])
GO
ALTER TABLE [dbo].[CheckListItemTemplates] CHECK CONSTRAINT [CheckListItemTemplate_Office]
GO
ALTER TABLE [dbo].[CheckListItemTemplates]  WITH CHECK ADD  CONSTRAINT [CheckListItemTemplate_SystemFile] FOREIGN KEY([SystemFile_Id])
REFERENCES [dbo].[SystemFiles] ([Id])
GO
ALTER TABLE [dbo].[CheckListItemTemplates] CHECK CONSTRAINT [CheckListItemTemplate_SystemFile]
GO
ALTER TABLE [dbo].[CheckListItemTemplates]  WITH CHECK ADD  CONSTRAINT [CheckListTemplate_CheckListItemTemplates] FOREIGN KEY([CheckListTemplate_CheckListTemplateId])
REFERENCES [dbo].[CheckListTemplates] ([CheckListTemplateId])
GO
ALTER TABLE [dbo].[CheckListItemTemplates] CHECK CONSTRAINT [CheckListTemplate_CheckListItemTemplates]
GO
ALTER TABLE [dbo].[CheckListSessions]  WITH CHECK ADD  CONSTRAINT [CheckListSession_CheckListTemplate] FOREIGN KEY([CheckListTemplate_CheckListTemplateId])
REFERENCES [dbo].[CheckListTemplates] ([CheckListTemplateId])
GO
ALTER TABLE [dbo].[CheckListSessions] CHECK CONSTRAINT [CheckListSession_CheckListTemplate]
GO
ALTER TABLE [dbo].[CheckListSessions]  WITH CHECK ADD  CONSTRAINT [Employee_CheckListSessions] FOREIGN KEY([Employee_Id])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[CheckListSessions] CHECK CONSTRAINT [Employee_CheckListSessions]
GO
ALTER TABLE [dbo].[DependentFiles]  WITH CHECK ADD  CONSTRAINT [DependentFile_CheckListItemSubmitionTrack] FOREIGN KEY([CheckListItemSubmitionTrack_Id])
REFERENCES [dbo].[CheckListItemSubmitionTracks] ([Id])
GO
ALTER TABLE [dbo].[DependentFiles] CHECK CONSTRAINT [DependentFile_CheckListItemSubmitionTrack]
GO
ALTER TABLE [dbo].[DependentFiles]  WITH CHECK ADD  CONSTRAINT [DependentFile_Dependent] FOREIGN KEY([Dependent_Id])
REFERENCES [dbo].[Dependents] ([Id])
GO
ALTER TABLE [dbo].[DependentFiles] CHECK CONSTRAINT [DependentFile_Dependent]
GO
ALTER TABLE [dbo].[DependentFiles]  WITH CHECK ADD  CONSTRAINT [DependentFile_SystemFile] FOREIGN KEY([SystemFile_Id])
REFERENCES [dbo].[SystemFiles] ([Id])
GO
ALTER TABLE [dbo].[DependentFiles] CHECK CONSTRAINT [DependentFile_SystemFile]
GO
ALTER TABLE [dbo].[Dependents]  WITH CHECK ADD  CONSTRAINT [Dependent_Employee] FOREIGN KEY([Employee_Id])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Dependents] CHECK CONSTRAINT [Dependent_Employee]
GO
ALTER TABLE [dbo].[DocumentTemplates]  WITH CHECK ADD  CONSTRAINT [DocumentTemplate_SystemFile] FOREIGN KEY([SystemFile_Id])
REFERENCES [dbo].[SystemFiles] ([Id])
GO
ALTER TABLE [dbo].[DocumentTemplates] CHECK CONSTRAINT [DocumentTemplate_SystemFile]
GO
ALTER TABLE [dbo].[EmailSubscriptions]  WITH CHECK ADD  CONSTRAINT [EmailSubscription_CheckListItemTemplate] FOREIGN KEY([CheckListItemTemplate_CheckListItemTemplateId])
REFERENCES [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId])
GO
ALTER TABLE [dbo].[EmailSubscriptions] CHECK CONSTRAINT [EmailSubscription_CheckListItemTemplate]
GO
ALTER TABLE [dbo].[EmailSubscriptions]  WITH CHECK ADD  CONSTRAINT [EmailSubscription_Staff] FOREIGN KEY([Staff_UserId])
REFERENCES [dbo].[Staffs] ([UserId])
GO
ALTER TABLE [dbo].[EmailSubscriptions] CHECK CONSTRAINT [EmailSubscription_Staff]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [Employee_Proxy] FOREIGN KEY([staffId])
REFERENCES [dbo].[Staffs] ([UserId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [Employee_Proxy]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [Note_CheckListItemSubmitionTrack] FOREIGN KEY([CheckListItemSubmitionTrack_Id])
REFERENCES [dbo].[CheckListItemSubmitionTracks] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [Note_CheckListItemSubmitionTrack]
GO
ALTER TABLE [dbo].[Reminders]  WITH CHECK ADD  CONSTRAINT [Reminder_CheckListItemTemplate] FOREIGN KEY([CheckListItemTemplate_CheckListItemTemplateId])
REFERENCES [dbo].[CheckListItemTemplates] ([CheckListItemTemplateId])
GO
ALTER TABLE [dbo].[Reminders] CHECK CONSTRAINT [Reminder_CheckListItemTemplate]
GO
ALTER TABLE [dbo].[Staffs]  WITH CHECK ADD  CONSTRAINT [Office_Staffs] FOREIGN KEY([Office_OfficeId])
REFERENCES [dbo].[Offices] ([OfficeId])
GO
ALTER TABLE [dbo].[Staffs] CHECK CONSTRAINT [Office_Staffs]
GO
ALTER TABLE [dbo].[SystemRoleAppFeatures]  WITH CHECK ADD  CONSTRAINT [SystemRole_AppFeatures_Source] FOREIGN KEY([SystemRole_Name])
REFERENCES [dbo].[SystemRoles] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemRoleAppFeatures] CHECK CONSTRAINT [SystemRole_AppFeatures_Source]
GO
ALTER TABLE [dbo].[SystemRoleAppFeatures]  WITH CHECK ADD  CONSTRAINT [SystemRole_AppFeatures_Target] FOREIGN KEY([AppFeature_Name])
REFERENCES [dbo].[AppFeatures] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemRoleAppFeatures] CHECK CONSTRAINT [SystemRole_AppFeatures_Target]
GO
ALTER TABLE [dbo].[SystemRoleStaffs]  WITH CHECK ADD  CONSTRAINT [SystemRole_Staffs_Source] FOREIGN KEY([SystemRole_Name])
REFERENCES [dbo].[SystemRoles] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemRoleStaffs] CHECK CONSTRAINT [SystemRole_Staffs_Source]
GO
ALTER TABLE [dbo].[SystemRoleStaffs]  WITH CHECK ADD  CONSTRAINT [SystemRole_Staffs_Target] FOREIGN KEY([Staff_UserId])
REFERENCES [dbo].[Staffs] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemRoleStaffs] CHECK CONSTRAINT [SystemRole_Staffs_Target]
GO
