USE [master]
GO
/****** Object:  Database [CryptoSwapMaster]    Script Date: 3/2/2019 8:03:26 PM ******/
CREATE DATABASE [CryptoSwapMaster]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CryptoSwapMaster', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CryptoSwapMaster.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CryptoSwapMaster_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CryptoSwapMaster_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CryptoSwapMaster] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CryptoSwapMaster].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CryptoSwapMaster] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET ARITHABORT OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CryptoSwapMaster] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CryptoSwapMaster] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CryptoSwapMaster] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CryptoSwapMaster] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CryptoSwapMaster] SET  MULTI_USER 
GO
ALTER DATABASE [CryptoSwapMaster] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CryptoSwapMaster] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CryptoSwapMaster] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CryptoSwapMaster] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CryptoSwapMaster] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CryptoSwapMaster] SET QUERY_STORE = OFF
GO
USE [CryptoSwapMaster]
GO
/****** Object:  Table [dbo].[ExchangeOrders]    Script Date: 3/2/2019 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[ExchangeOrderId] [nvarchar](200) NULL,
	[Symbol] [nvarchar](10) NOT NULL,
	[Side] [nvarchar](10) NOT NULL,
	[BaseQty] [decimal](18, 8) NOT NULL,
	[QuoteQty] [decimal](18, 8) NOT NULL,
	[Status] [int] NOT NULL,
	[StatusMsg] [nvarchar](1000) NULL,
	[Created] [datetime] NOT NULL,
	[LastModified] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exchanges]    Script Date: 3/2/2019 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exchanges](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_Exchanges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Keys]    Script Date: 3/2/2019 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Keys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExchangeId] [int] NOT NULL,
	[ApiKey] [nvarchar](100) NOT NULL,
	[SecretKey] [nvarchar](100) NOT NULL,
	[Created] [datetime] NOT NULL,
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK_UserExchanges] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 3/2/2019 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/2/2019 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExchangeId] [int] NOT NULL,
	[BaseAsset] [nvarchar](10) NOT NULL,
	[Pool] [int] NOT NULL,
	[Type] [varchar](20) NOT NULL,
	[BaseQty] [decimal](18, 8) NOT NULL,
	[ExecutedBaseQty] [decimal](18, 8) NULL,
	[QuoteAsset] [nvarchar](10) NOT NULL,
	[ExpectedQuoteQty] [decimal](18, 8) NOT NULL,
	[ReceivedQuoteQty] [decimal](18, 8) NULL,
	[Status] [int] NOT NULL,
	[StatusMsg] [nvarchar](1000) NULL,
	[Created] [datetime] NOT NULL,
	[LastModified] [datetime] NULL,
 CONSTRAINT [PK__Orders__3214EC072F3EFAE3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteAssets]    Script Date: 3/2/2019 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteAssets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExchangeId] [int] NOT NULL,
	[Asset] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK__QuoteAss__3214EC07ECB75215] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/2/2019 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ip] [nvarchar](25) NOT NULL,
	[BotStatus] [int] NOT NULL,
	[BotStatusMsg] [nvarchar](1000) NULL,
	[Created] [datetime] NOT NULL,
	[LastModified] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ExchangeOrders]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeOrders_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[ExchangeOrders] CHECK CONSTRAINT [FK_ExchangeOrders_Orders]
GO
ALTER TABLE [dbo].[Keys]  WITH CHECK ADD  CONSTRAINT [FK_Keys_Exchanges] FOREIGN KEY([ExchangeId])
REFERENCES [dbo].[Exchanges] ([Id])
GO
ALTER TABLE [dbo].[Keys] CHECK CONSTRAINT [FK_Keys_Exchanges]
GO
ALTER TABLE [dbo].[Keys]  WITH CHECK ADD  CONSTRAINT [FK_Keys_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Keys] CHECK CONSTRAINT [FK_Keys_Users]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Exchanges] FOREIGN KEY([ExchangeId])
REFERENCES [dbo].[Exchanges] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Exchanges]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[QuoteAssets]  WITH CHECK ADD  CONSTRAINT [FK_QuoteAssets_Exchanges] FOREIGN KEY([ExchangeId])
REFERENCES [dbo].[Exchanges] ([Id])
GO
ALTER TABLE [dbo].[QuoteAssets] CHECK CONSTRAINT [FK_QuoteAssets_Exchanges]
GO
ALTER TABLE [dbo].[QuoteAssets]  WITH CHECK ADD  CONSTRAINT [FK_QuoteAssets_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[QuoteAssets] CHECK CONSTRAINT [FK_QuoteAssets_Users]
GO
USE [master]
GO
ALTER DATABASE [CryptoSwapMaster] SET  READ_WRITE 
GO
