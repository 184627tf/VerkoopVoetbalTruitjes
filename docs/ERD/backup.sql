USE [master]
GO
/****** Object:  Database [VerkoopVoetbalTruitjes]    Script Date: 18/11/2022 17:37:41 ******/
CREATE DATABASE [VerkoopVoetbalTruitjes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VerkoopVoetbalTruitjes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VerkoopVoetbalTruitjes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VerkoopVoetbalTruitjes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VerkoopVoetbalTruitjes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VerkoopVoetbalTruitjes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET ARITHABORT OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET  MULTI_USER 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET QUERY_STORE = OFF
GO
USE [VerkoopVoetbalTruitjes]
GO
/****** Object:  Table [dbo].[Adres]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adres](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[adres] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Adres] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bestelling]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bestelling](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[klant_id] [int] NOT NULL,
	[prijs] [decimal](18, 2) NOT NULL,
	[betaald] [bit] NOT NULL,
	[tijdstip] [datetime] NOT NULL,
 CONSTRAINT [PK_Bestelling] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BestellingDetail]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BestellingDetail](
	[bestelling_id] [int] NOT NULL,
	[voetbaltruitje_id] [int] NOT NULL,
	[aantal] [int] NOT NULL,
 CONSTRAINT [PK_BestellingDetail] PRIMARY KEY CLUSTERED 
(
	[bestelling_id] ASC,
	[voetbaltruitje_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Competitie]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Competitie](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[naam] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Competitie] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Klant]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Klant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[naam] [varchar](255) NOT NULL,
	[adres_id] [int] NOT NULL,
 CONSTRAINT [PK_Klant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KledingMaat]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KledingMaat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[maat] [varchar](255) NOT NULL,
 CONSTRAINT [PK_KledingMaat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ontwerp]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ontwerp](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ploeg_id] [int] NOT NULL,
	[seizoen_id] [int] NOT NULL,
	[competitie_id] [int] NOT NULL,
	[uit] [bit] NOT NULL,
	[versie] [int] NOT NULL,
 CONSTRAINT [PK_Ontwerp] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ploeg]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ploeg](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[naam] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Ploeg] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seizoen]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seizoen](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[seizoen] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Seizoen] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoetbalTruitje]    Script Date: 18/11/2022 17:37:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoetbalTruitje](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[prijs] [decimal](18, 2) NOT NULL,
	[ontwerp_id] [int] NOT NULL,
	[kledingmaat_id] [int] NOT NULL,
 CONSTRAINT [PK_VoetbalTruitje] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Adres] ON 

INSERT [dbo].[Adres] ([id], [adres]) VALUES (1, N'Kerkstraat 5, 8000 Brugge')
INSERT [dbo].[Adres] ([id], [adres]) VALUES (2, N'Schoolstraat 13, 9000 Gent')
INSERT [dbo].[Adres] ([id], [adres]) VALUES (3, N'adres')
SET IDENTITY_INSERT [dbo].[Adres] OFF
GO
SET IDENTITY_INSERT [dbo].[Competitie] ON 

INSERT [dbo].[Competitie] ([id], [naam]) VALUES (1, N'Premier League')
INSERT [dbo].[Competitie] ([id], [naam]) VALUES (2, N'Pro League')
INSERT [dbo].[Competitie] ([id], [naam]) VALUES (3, N'Challenger League')
SET IDENTITY_INSERT [dbo].[Competitie] OFF
GO
SET IDENTITY_INSERT [dbo].[Klant] ON 

INSERT [dbo].[Klant] ([id], [naam], [adres_id]) VALUES (3, N'Thibo Frans', 1)
INSERT [dbo].[Klant] ([id], [naam], [adres_id]) VALUES (4, N'Tom Vande Wiele', 2)
SET IDENTITY_INSERT [dbo].[Klant] OFF
GO
SET IDENTITY_INSERT [dbo].[KledingMaat] ON 

INSERT [dbo].[KledingMaat] ([id], [maat]) VALUES (1, N'S')
INSERT [dbo].[KledingMaat] ([id], [maat]) VALUES (2, N'M')
INSERT [dbo].[KledingMaat] ([id], [maat]) VALUES (3, N'L')
INSERT [dbo].[KledingMaat] ([id], [maat]) VALUES (4, N'XL')
SET IDENTITY_INSERT [dbo].[KledingMaat] OFF
GO
SET IDENTITY_INSERT [dbo].[Ontwerp] ON 

INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (1, 1, 2, 1, 0, 1)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (2, 1, 2, 1, 1, 1)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (3, 1, 2, 1, 0, 2)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (4, 1, 2, 1, 1, 2)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (5, 2, 2, 1, 1, 1)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (6, 3, 2, 2, 0, 1)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (7, 4, 1, 2, 1, 1)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (8, 5, 2, 2, 1, 1)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (9, 6, 2, 2, 0, 1)
INSERT [dbo].[Ontwerp] ([id], [ploeg_id], [seizoen_id], [competitie_id], [uit], [versie]) VALUES (10, 6, 1, 3, 1, 1)
SET IDENTITY_INSERT [dbo].[Ontwerp] OFF
GO
SET IDENTITY_INSERT [dbo].[Ploeg] ON 

INSERT [dbo].[Ploeg] ([id], [naam]) VALUES (1, N'Manchester United')
INSERT [dbo].[Ploeg] ([id], [naam]) VALUES (2, N'Manchester City')
INSERT [dbo].[Ploeg] ([id], [naam]) VALUES (3, N'KAA Gent')
INSERT [dbo].[Ploeg] ([id], [naam]) VALUES (4, N'KVO')
INSERT [dbo].[Ploeg] ([id], [naam]) VALUES (5, N'Zulte Waregem')
INSERT [dbo].[Ploeg] ([id], [naam]) VALUES (6, N'Westerlo')
SET IDENTITY_INSERT [dbo].[Ploeg] OFF
GO
SET IDENTITY_INSERT [dbo].[Seizoen] ON 

INSERT [dbo].[Seizoen] ([id], [seizoen]) VALUES (1, N'2021-2022')
INSERT [dbo].[Seizoen] ([id], [seizoen]) VALUES (2, N'2022-2023')
SET IDENTITY_INSERT [dbo].[Seizoen] OFF
GO
SET IDENTITY_INSERT [dbo].[VoetbalTruitje] ON 

INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (1, CAST(10.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (2, CAST(15.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (3, CAST(18.50 AS Decimal(18, 2)), 2, 3)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (4, CAST(21.25 AS Decimal(18, 2)), 3, 4)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (5, CAST(20.00 AS Decimal(18, 2)), 4, 1)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (6, CAST(28.00 AS Decimal(18, 2)), 5, 2)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (7, CAST(7.99 AS Decimal(18, 2)), 6, 3)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (8, CAST(11.99 AS Decimal(18, 2)), 7, 4)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (9, CAST(5.00 AS Decimal(18, 2)), 8, 1)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (10, CAST(10.00 AS Decimal(18, 2)), 9, 2)
INSERT [dbo].[VoetbalTruitje] ([id], [prijs], [ontwerp_id], [kledingmaat_id]) VALUES (11, CAST(15.00 AS Decimal(18, 2)), 10, 3)
SET IDENTITY_INSERT [dbo].[VoetbalTruitje] OFF
GO
ALTER TABLE [dbo].[Bestelling]  WITH CHECK ADD  CONSTRAINT [FK_Bestelling_Klant] FOREIGN KEY([klant_id])
REFERENCES [dbo].[Klant] ([id])
GO
ALTER TABLE [dbo].[Bestelling] CHECK CONSTRAINT [FK_Bestelling_Klant]
GO
ALTER TABLE [dbo].[BestellingDetail]  WITH CHECK ADD  CONSTRAINT [FK_BestellingDetail_Bestelling] FOREIGN KEY([bestelling_id])
REFERENCES [dbo].[Bestelling] ([id])
GO
ALTER TABLE [dbo].[BestellingDetail] CHECK CONSTRAINT [FK_BestellingDetail_Bestelling]
GO
ALTER TABLE [dbo].[BestellingDetail]  WITH CHECK ADD  CONSTRAINT [FK_BestellingDetail_VoetbalTruitje] FOREIGN KEY([voetbaltruitje_id])
REFERENCES [dbo].[VoetbalTruitje] ([id])
GO
ALTER TABLE [dbo].[BestellingDetail] CHECK CONSTRAINT [FK_BestellingDetail_VoetbalTruitje]
GO
ALTER TABLE [dbo].[Klant]  WITH CHECK ADD  CONSTRAINT [FK_Klant_Adres] FOREIGN KEY([adres_id])
REFERENCES [dbo].[Adres] ([id])
GO
ALTER TABLE [dbo].[Klant] CHECK CONSTRAINT [FK_Klant_Adres]
GO
ALTER TABLE [dbo].[Ontwerp]  WITH CHECK ADD  CONSTRAINT [FK_Ontwerp_Competitie] FOREIGN KEY([competitie_id])
REFERENCES [dbo].[Competitie] ([id])
GO
ALTER TABLE [dbo].[Ontwerp] CHECK CONSTRAINT [FK_Ontwerp_Competitie]
GO
ALTER TABLE [dbo].[Ontwerp]  WITH CHECK ADD  CONSTRAINT [FK_Ontwerp_Ploeg] FOREIGN KEY([ploeg_id])
REFERENCES [dbo].[Ploeg] ([id])
GO
ALTER TABLE [dbo].[Ontwerp] CHECK CONSTRAINT [FK_Ontwerp_Ploeg]
GO
ALTER TABLE [dbo].[Ontwerp]  WITH CHECK ADD  CONSTRAINT [FK_Ontwerp_Seizoen] FOREIGN KEY([seizoen_id])
REFERENCES [dbo].[Seizoen] ([id])
GO
ALTER TABLE [dbo].[Ontwerp] CHECK CONSTRAINT [FK_Ontwerp_Seizoen]
GO
ALTER TABLE [dbo].[VoetbalTruitje]  WITH CHECK ADD  CONSTRAINT [FK_VoetbalTruitje_KledingMaat] FOREIGN KEY([kledingmaat_id])
REFERENCES [dbo].[KledingMaat] ([id])
GO
ALTER TABLE [dbo].[VoetbalTruitje] CHECK CONSTRAINT [FK_VoetbalTruitje_KledingMaat]
GO
ALTER TABLE [dbo].[VoetbalTruitje]  WITH CHECK ADD  CONSTRAINT [FK_VoetbalTruitje_Ontwerp] FOREIGN KEY([ontwerp_id])
REFERENCES [dbo].[Ontwerp] ([id])
GO
ALTER TABLE [dbo].[VoetbalTruitje] CHECK CONSTRAINT [FK_VoetbalTruitje_Ontwerp]
GO
USE [master]
GO
ALTER DATABASE [VerkoopVoetbalTruitjes] SET  READ_WRITE 
GO
