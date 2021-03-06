USE [master]
GO
IF DB_ID (N'SchoolDB') IS NOT NULL
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'SchoolDB'
	ALTER DATABASE [SchoolDB] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE SchoolDB;
END
/****** Object:  Database [SchoolDB]    Script Date: 11/8/2012 6:20:35 PM ******/
CREATE DATABASE [SchoolDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolDB', FILENAME = N'C:\Databases\SchoolDB.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SchoolDB_Log', FILENAME = N'C:\Databases\SchoolDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SchoolDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SchoolDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SchoolDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SchoolDB] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SchoolDB]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 11/8/2012 6:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[ClassId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 11/8/2012 6:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Class] [varchar](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([ClassId])
REFERENCES [dbo].[Teacher] ([Id])
GO
USE [master]
GO
ALTER DATABASE [SchoolDB] SET  READ_WRITE 
GO

USE SchoolDB
GO

INSERT [Teacher] ([FirstName], [LastName], [Class]) VALUES('Esther', 'Valle', '3C')
INSERT [Teacher] ([FirstName], [LastName], [Class]) VALUES('David', 'Waite', '4B')
INSERT [Teacher] ([FirstName], [LastName], [Class]) VALUES('Belinda', 'Newman', '2A')
GO

INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Kevin', 'Liu', '10/10/2005', 1)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Martin', 'Weber', '9/7/2005', 1)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('George', 'Li', '8/10/2005', 1)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Lisa', 'Miller', '5/4/2005', 1)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Run', 'Liu', '10/23/2005', 1)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Sean', 'Stewart', '02/18/2003', 2)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Olinda', 'Turner', '05/17/2003', 2)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Jon', 'Orton', '08/10/2002', 2)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Toby', 'Nixon', '12/15/2002', 2)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Daniela', 'Guinot', '08/01/2002', 2)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Vijay', 'Sundaram', '02/03/2007', 3)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Eric', 'Gruber', '05/26/2007', 3)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Chris', 'Meyer', '05/09/2006', 3)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Yuhong', 'Li', '05/28/2007', 3)
INSERT [Student] ([FirstName], [LastName], [DateOfBirth], [ClassId]) VALUES('Yan', 'Li', '03/31/2007', 3)
GO