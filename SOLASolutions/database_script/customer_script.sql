CREATE TABLE [dbo].[ApplicationClients](
	[ApplicationClientId] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[ClientKey] [nvarchar](4000) NULL,
	[IsEnabled] [bit] NULL CONSTRAINT [DF_ApplicationClients_IsEnabled]  DEFAULT ((1)),
	[CreatedUser] [int] NULL,
	[CreatedDatetime] [datetime] NULL CONSTRAINT [DF_ApplicationClients_CreatedDatetime]  DEFAULT (getutcdate()),
	[ModifiedUser] [int] NULL,
	[ModifiedDatetime] [datetime] NULL,
 CONSTRAINT [PK_ApplicationClients] PRIMARY KEY CLUSTERED 
(
	[ApplicationClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Pages](
	[PageId] [int] NOT NULL,
	[ParentId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](4000) NULL,
	[StateName] [nvarchar](255) NULL,
	[IconUrl] [nvarchar](4000) NULL,
	[IsEnabled] [bit] NULL CONSTRAINT [DF_Pages_IsEnabled]  DEFAULT ((1)),
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] NOT NULL,
	[PageId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[PermissionType] [int] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PermissionsInRole](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_PermissionsInRole] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[IsSystemRole] [bit] NULL CONSTRAINT [DF_Roles_IsSystemRole]  DEFAULT ((0)),
	[IsEnabled] [bit] NULL CONSTRAINT [DF_Roles_IsEnabled]  DEFAULT ((1)),
	[CreatedUser] [int] NULL,
	[CreatedDatetime] [datetime] NULL CONSTRAINT [DF_Roles_CreatedDatetime]  DEFAULT (getutcdate()),
	[ModifiedUser] [int] NULL,
	[ModifiedDatetime] [datetime] NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[Password] [nvarchar](4000) NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	--[RoleId] [int] NULL,
	[ResetPasswordToken] [nvarchar](4000) NULL,
	[ResetPasswordTokenExpriresDatetime] [datetime] NULL,
	[IsEnabled] [bit] NULL CONSTRAINT [DF_Users_IsEnabled]  DEFAULT ((1)),
	[CreatedUser] [int] NULL,
	[CreatedDatetime] [datetime] NULL CONSTRAINT [DF_Users_CreatedDatetime]  DEFAULT (getutcdate()),
	[ModifiedUser] [int] NULL,
	[ModifiedDatetime] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].Locations(
	LocationId [int] identity(1,1) NOT NULL,
	Name nvarchar(4000) NOT NULL,
	Address nvarchar(4000),
	Phone varchar(20)
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	LocationId ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].UserInRole(
	RecordId [int] identity(1,1) NOT NULL,
	UserId int NOT NULL,
	RoleId int not null,
	LocationId int
 CONSTRAINT [PK_UserInRole] PRIMARY KEY CLUSTERED 
(
	RecordId ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

