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

CREATE TABLE [dbo].[RolePermissions](
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
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
	[RoleId] [int] NULL,
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

