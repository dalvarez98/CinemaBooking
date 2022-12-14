USE [master]
GO
/****** Object:  Database [CinemaBooking]    Script Date: 10/16/2022 3:43:58 PM ******/
CREATE DATABASE [CinemaBooking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CinemaBooking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CinemaBooking.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CinemaBooking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CinemaBooking_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CinemaBooking] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CinemaBooking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CinemaBooking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CinemaBooking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CinemaBooking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CinemaBooking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CinemaBooking] SET ARITHABORT OFF 
GO
ALTER DATABASE [CinemaBooking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CinemaBooking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CinemaBooking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CinemaBooking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CinemaBooking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CinemaBooking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CinemaBooking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CinemaBooking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CinemaBooking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CinemaBooking] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CinemaBooking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CinemaBooking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CinemaBooking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CinemaBooking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CinemaBooking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CinemaBooking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CinemaBooking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CinemaBooking] SET RECOVERY FULL 
GO
ALTER DATABASE [CinemaBooking] SET  MULTI_USER 
GO
ALTER DATABASE [CinemaBooking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CinemaBooking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CinemaBooking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CinemaBooking] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CinemaBooking] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CinemaBooking] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CinemaBooking', N'ON'
GO
ALTER DATABASE [CinemaBooking] SET QUERY_STORE = OFF
GO
USE [CinemaBooking]
GO
/****** Object:  Table [dbo].[BuysTicket]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuysTicket](
	[CustID] [int] NOT NULL,
	[TransactionID] [int] NOT NULL,
	[TicketNum] [int] NOT NULL,
 CONSTRAINT [pk_buys] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC,
	[TransactionID] ASC,
	[TicketNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cinema]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cinema](
	[CinemaID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](30) NULL,
	[State] [char](2) NULL,
	[ZipCode] [nvarchar](9) NULL,
	[PhoneNum] [nvarchar](15) NULL,
	[ManagerNum] [int] NULL,
 CONSTRAINT [PK_Cinema] PRIMARY KEY CLUSTERED 
(
	[CinemaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Crewmember]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Crewmember](
	[EmpID] [int] IDENTITY(1,1) NOT NULL,
	[FirstN] [nvarchar](50) NULL,
	[LastN] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](30) NULL,
	[State] [char](2) NULL,
	[ZipCode] [nvarchar](9) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNum] [nvarchar](15) NULL,
	[Salary] [int] NULL,
	[Username] [nvarchar](30) NULL,
	[Password] [char](8) NULL,
	[CinemaID] [int] NULL,
 CONSTRAINT [PK_Crewmember] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustID] [int] IDENTITY(1,1) NOT NULL,
	[FirstN] [nvarchar](50) NULL,
	[LastN] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](30) NULL,
	[State] [char](2) NULL,
	[ZipCode] [nvarchar](9) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNum] [nvarchar](15) NULL,
	[Salary] [int] NULL,
	[Username] [nvarchar](30) NULL,
	[Password] [char](8) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[EmpID] [int] IDENTITY(1,1) NOT NULL,
	[FirstN] [nvarchar](50) NULL,
	[LastN] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](30) NULL,
	[State] [char](2) NULL,
	[ZipCode] [nvarchar](9) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNum] [nvarchar](15) NULL,
	[Salary] [int] NULL,
	[Username] [nvarchar](30) NULL,
	[Password] [char](8) NULL,
	[CinemaID] [int] NULL,
 CONSTRAINT [PK_Manager] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[MovieTitle] [nvarchar](30) NULL,
	[Genre] [nvarchar](50) NULL,
	[Rating] [int] NULL,
	[Director] [nvarchar](50) NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[SeatNum] [int] NOT NULL,
	[TheaterID] [int] NOT NULL,
	[RowNum] [nvarchar](3) NULL,
	[Availabe] [int] NULL,
	[TicketNum] [int] NULL,
 CONSTRAINT [pk_seats] PRIMARY KEY CLUSTERED 
(
	[SeatNum] ASC,
	[TheaterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shows]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shows](
	[CinemaID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
 CONSTRAINT [pk_shows] PRIMARY KEY CLUSTERED 
(
	[CinemaID] ASC,
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheaterRoom]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheaterRoom](
	[TheaterRoom] [int] NOT NULL,
	[Capacity] [int] NULL,
	[MovieID] [int] NOT NULL,
	[CinemaID] [int] NOT NULL,
 CONSTRAINT [PK_TheaterRoom] PRIMARY KEY CLUSTERED 
(
	[TheaterRoom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[TicketNum] [int] IDENTITY(1,1) NOT NULL,
	[Showtime] [datetime] NULL,
	[ShowDate] [date] NULL,
	[Price] [numeric](2, 2) NULL,
	[CinemaID] [int] NOT NULL,
	[TheaterID] [int] NOT NULL,
	[SeatNum] [int] NOT NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[TicketNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 10/16/2022 3:43:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[CardType] [nvarchar](20) NULL,
	[CreditCNum] [char](19) NULL,
	[Date] [date] NULL,
	[Total] [numeric](4, 2) NULL,
	[CustID] [int] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BuysTicket]  WITH CHECK ADD  CONSTRAINT [FK_BuysTicket_Customer] FOREIGN KEY([CustID])
REFERENCES [dbo].[Customer] ([CustID])
GO
ALTER TABLE [dbo].[BuysTicket] CHECK CONSTRAINT [FK_BuysTicket_Customer]
GO
ALTER TABLE [dbo].[BuysTicket]  WITH CHECK ADD  CONSTRAINT [FK_BuysTicket_Tickets] FOREIGN KEY([TicketNum])
REFERENCES [dbo].[Tickets] ([TicketNum])
GO
ALTER TABLE [dbo].[BuysTicket] CHECK CONSTRAINT [FK_BuysTicket_Tickets]
GO
ALTER TABLE [dbo].[BuysTicket]  WITH CHECK ADD  CONSTRAINT [FK_BuysTicket_Transaction] FOREIGN KEY([TransactionID])
REFERENCES [dbo].[Transaction] ([TransactionID])
GO
ALTER TABLE [dbo].[BuysTicket] CHECK CONSTRAINT [FK_BuysTicket_Transaction]
GO
ALTER TABLE [dbo].[Cinema]  WITH CHECK ADD  CONSTRAINT [FK_Cinema_Manager] FOREIGN KEY([ManagerNum])
REFERENCES [dbo].[Manager] ([EmpID])
GO
ALTER TABLE [dbo].[Cinema] CHECK CONSTRAINT [FK_Cinema_Manager]
GO
ALTER TABLE [dbo].[Crewmember]  WITH CHECK ADD  CONSTRAINT [FK_Crewmember_Cinema] FOREIGN KEY([CinemaID])
REFERENCES [dbo].[Cinema] ([CinemaID])
GO
ALTER TABLE [dbo].[Crewmember] CHECK CONSTRAINT [FK_Crewmember_Cinema]
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD  CONSTRAINT [FK_Seats_TheaterRoom] FOREIGN KEY([TheaterID])
REFERENCES [dbo].[TheaterRoom] ([TheaterRoom])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Seats] CHECK CONSTRAINT [FK_Seats_TheaterRoom]
GO
ALTER TABLE [dbo].[Shows]  WITH CHECK ADD  CONSTRAINT [FK_Shows_Cinema] FOREIGN KEY([CinemaID])
REFERENCES [dbo].[Cinema] ([CinemaID])
GO
ALTER TABLE [dbo].[Shows] CHECK CONSTRAINT [FK_Shows_Cinema]
GO
ALTER TABLE [dbo].[Shows]  WITH CHECK ADD  CONSTRAINT [FK_Shows_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[Shows] CHECK CONSTRAINT [FK_Shows_Movie]
GO
ALTER TABLE [dbo].[TheaterRoom]  WITH CHECK ADD  CONSTRAINT [FK_TheaterRoom_Cinema] FOREIGN KEY([CinemaID])
REFERENCES [dbo].[Cinema] ([CinemaID])
GO
ALTER TABLE [dbo].[TheaterRoom] CHECK CONSTRAINT [FK_TheaterRoom_Cinema]
GO
ALTER TABLE [dbo].[TheaterRoom]  WITH CHECK ADD  CONSTRAINT [FK_TheaterRoom_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[TheaterRoom] CHECK CONSTRAINT [FK_TheaterRoom_Movie]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Cinema] FOREIGN KEY([CinemaID])
REFERENCES [dbo].[Cinema] ([CinemaID])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Cinema]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Seats] FOREIGN KEY([SeatNum], [TheaterID])
REFERENCES [dbo].[Seats] ([SeatNum], [TheaterID])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Seats]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Customer] FOREIGN KEY([CustID])
REFERENCES [dbo].[Customer] ([CustID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Customer]
GO
USE [master]
GO
ALTER DATABASE [CinemaBooking] SET  READ_WRITE 
GO
