CREATE TABLE [dbo].[AzureSQLServers](
	[AzureSQLServerId] [int] IDENTITY(1,1) NOT NULL,
	[ServerName] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](4000) NOT NULL,
	[CreatedUser] [int] NULL,
	[CreatedDatetime] [datetime] NULL CONSTRAINT [DF_AzureSQLServers_CreatedDatetime]  DEFAULT (getutcdate()),
	[ModifiedUser] [int] NULL,
	[ModifiedDatetime] [datetime] NULL,
 CONSTRAINT [PK_AzureSQLServers] PRIMARY KEY CLUSTERED 
(
	[AzureSQLServerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[SiteName] [varchar](50) NULL,
	[AzureSQLServerId] [int] NULL,
	[IsEnabled] [bit] NULL CONSTRAINT [DF_Customers_IsEnabled]  DEFAULT ((0)),
	[CreatedUser] [int] NULL,
	[CreatedDatetime] [datetime] NULL CONSTRAINT [DF_Customers_CreatedDatetime]  DEFAULT (getutcdate()),
	[ModifiedUser] [int] NULL,
	[ModifiedDatetime] [datetime] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
