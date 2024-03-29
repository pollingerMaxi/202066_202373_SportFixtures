CREATE DATABASE [SportFixturesTest]
GO
ALTER DATABASE [SportFixturesTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SportFixturesTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SportFixturesTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SportFixturesTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SportFixturesTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [SportFixturesTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SportFixturesTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SportFixturesTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SportFixturesTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SportFixturesTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SportFixturesTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SportFixturesTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SportFixturesTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SportFixturesTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SportFixturesTest] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SportFixturesTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SportFixturesTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SportFixturesTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SportFixturesTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SportFixturesTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SportFixturesTest] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SportFixturesTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SportFixturesTest] SET RECOVERY FULL 
GO
ALTER DATABASE [SportFixturesTest] SET  MULTI_USER 
GO
ALTER DATABASE [SportFixturesTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SportFixturesTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SportFixturesTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SportFixturesTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SportFixturesTest] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SportFixturesTest', N'ON'
GO
USE [SportFixturesTest]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/10/2018 10:21:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 10/10/2018 10:21:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[EncounterId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Encounters]    Script Date: 10/10/2018 10:21:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Encounters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[SportId] [int] NOT NULL,
	[Team1Id] [int] NULL,
	[Team2Id] [int] NULL,
 CONSTRAINT [PK_Encounters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sports]    Script Date: 10/10/2018 10:21:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 10/10/2018 10:21:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PhotoPath] [nvarchar](max) NULL,
	[SportId] [int] NOT NULL,
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/10/2018 10:21:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Role] [int] NOT NULL,
	[Token] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersTeams]    Script Date: 10/10/2018 10:21:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersTeams](
	[UserId] [int] NOT NULL,
	[TeamId] [int] NOT NULL,
 CONSTRAINT [PK_UsersTeams] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20180910232610_InitialCreate', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20180930222632_Update with all entities', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181003041428_Added many-to-many relationship', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181010045035_Removed IsDeleted', N'2.1.4-rtm-31024')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181010210032_Seed data and cascade delete for comments', N'2.1.4-rtm-31024')
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [LastName], [Username], [Password], [Email], [Role], [Token]) VALUES (1, N'Admins Name', N'Admins LastName', N'admin', N'admin', N'admin@admin.com', 1, NULL)
INSERT [dbo].[Users] ([Id], [Name], [LastName], [Username], [Password], [Email], [Role], [Token]) VALUES (2, N'Normal user', N'Users LastName', N'user', N'user', N'user@user.com', 0, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Index [IX_Comments_UserId]    Script Date: 10/10/2018 10:21:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Encounters_Team1Id]    Script Date: 10/10/2018 10:21:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_Encounters_Team1Id] ON [dbo].[Encounters]
(
	[Team1Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Encounters_Team2Id]    Script Date: 10/10/2018 10:21:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_Encounters_Team2Id] ON [dbo].[Encounters]
(
	[Team2Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Teams_SportId]    Script Date: 10/10/2018 10:21:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_Teams_SportId] ON [dbo].[Teams]
(
	[SportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsersTeams_TeamId]    Script Date: 10/10/2018 10:21:32 PM ******/
CREATE NONCLUSTERED INDEX [IX_UsersTeams_TeamId] ON [dbo].[UsersTeams]
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT ((0)) FOR [SportId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Encounters_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Encounters] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Encounters_UserId]
GO
ALTER TABLE [dbo].[Encounters]  WITH CHECK ADD  CONSTRAINT [FK_Encounters_Teams_Team1Id] FOREIGN KEY([Team1Id])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[Encounters] CHECK CONSTRAINT [FK_Encounters_Teams_Team1Id]
GO
ALTER TABLE [dbo].[Encounters]  WITH CHECK ADD  CONSTRAINT [FK_Encounters_Teams_Team2Id] FOREIGN KEY([Team2Id])
REFERENCES [dbo].[Teams] ([Id])
GO
ALTER TABLE [dbo].[Encounters] CHECK CONSTRAINT [FK_Encounters_Teams_Team2Id]
GO
ALTER TABLE [dbo].[Teams]  WITH CHECK ADD  CONSTRAINT [FK_Teams_Sports_SportId] FOREIGN KEY([SportId])
REFERENCES [dbo].[Sports] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Teams] CHECK CONSTRAINT [FK_Teams_Sports_SportId]
GO
ALTER TABLE [dbo].[UsersTeams]  WITH CHECK ADD  CONSTRAINT [FK_UsersTeams_Teams_TeamId] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Teams] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersTeams] CHECK CONSTRAINT [FK_UsersTeams_Teams_TeamId]
GO
ALTER TABLE [dbo].[UsersTeams]  WITH CHECK ADD  CONSTRAINT [FK_UsersTeams_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersTeams] CHECK CONSTRAINT [FK_UsersTeams_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [SportFixturesTest] SET  READ_WRITE 
GO
