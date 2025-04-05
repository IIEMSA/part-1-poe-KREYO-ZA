/****** Object:  Database [EventEaseWebAppDbST10433968]    Script Date: 05 Apr 2025 20:57:05 PM ******/
CREATE DATABASE [EventEaseWebAppDbST10433968]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 100 MB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET COMPATIBILITY_LEVEL = 160
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET ARITHABORT OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET  MULTI_USER 
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET ENCRYPTION ON
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET QUERY_STORE = ON
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 05 Apr 2025 20:57:06 PM ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 05 Apr 2025 20:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[VenueId] [int] NOT NULL,
	[BookingDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 05 Apr 2025 20:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [nvarchar](max) NOT NULL,
	[EventDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[VenueId] [int] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venues]    Script Date: 05 Apr 2025 20:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venues](
	[VenueId] [int] IDENTITY(1,1) NOT NULL,
	[VenueName] [nvarchar](max) NOT NULL,
	[Location] [nvarchar](max) NOT NULL,
	[Capacity] [int] NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Venues] PRIMARY KEY CLUSTERED 
(
	[VenueId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250404135301_InitialCreate', N'9.0.3')
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 
GO
INSERT [dbo].[Bookings] ([BookingId], [EventId], [VenueId], [BookingDate]) VALUES (1, 3, 3, CAST(N'2025-04-18T21:17:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Bookings] ([BookingId], [EventId], [VenueId], [BookingDate]) VALUES (2, 2, 3, CAST(N'2025-04-23T09:19:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Events] ON 
GO
INSERT [dbo].[Events] ([EventId], [EventName], [EventDate], [Description], [VenueId]) VALUES (1, N'Dinner', CAST(N'2025-04-07T00:00:00.0000000' AS DateTime2), N'Eat dinner with friends ', 1)
GO
INSERT [dbo].[Events] ([EventId], [EventName], [EventDate], [Description], [VenueId]) VALUES (2, N'Lunch Break', CAST(N'2025-04-08T14:20:00.0000000' AS DateTime2), N'Lunch break after work', 3)
GO
INSERT [dbo].[Events] ([EventId], [EventName], [EventDate], [Description], [VenueId]) VALUES (3, N'Recover from this Assignment ', CAST(N'2025-04-08T13:00:00.0000000' AS DateTime2), N'Take time to rest and recover from this painful assignment ', 2)
GO
SET IDENTITY_INSERT [dbo].[Events] OFF
GO
SET IDENTITY_INSERT [dbo].[Venues] ON 
GO
INSERT [dbo].[Venues] ([VenueId], [VenueName], [Location], [Capacity], [ImageUrl]) VALUES (1, N'Mountain View', N'Somewhere over the rainbow', 800, N'https://i.natgeofe.com/n/cfa19a0d-eff0-4628-8fdd-2ad8d66845dd/mountain-range-appenzell-switzerland.jpg')
GO
INSERT [dbo].[Venues] ([VenueId], [VenueName], [Location], [Capacity], [ImageUrl]) VALUES (2, N'Maldives Chalet', N'Maldives ', 30, N'https://media.cntraveler.com/photos/66aa859b257a60dbb6105d8f/16:9/w_2944,h_1656,c_limit/Six%20Senses%20Kanuhura_The%20Point%20aerial.jpg')
GO
INSERT [dbo].[Venues] ([VenueId], [VenueName], [Location], [Capacity], [ImageUrl]) VALUES (3, N'Mabalingwe', N'Limpopo', 1000, N'https://serenesafarilodges.com/wp-content/uploads/2021/07/Shammah-Lodge_Featured-Image-1.jpg')
GO
INSERT [dbo].[Venues] ([VenueId], [VenueName], [Location], [Capacity], [ImageUrl]) VALUES (8, N'Mansion', N'Lagos', 15, N'https://images.ctfassets.net/n2ifzifcqscw/2qhxwsp5hOBghbdEOOLSux/ef05c74d95b6b4540fc0d563dbbd7d26/P1753785.jpg')
GO
SET IDENTITY_INSERT [dbo].[Venues] OFF
GO
/****** Object:  Index [IX_Bookings_EventId]    Script Date: 05 Apr 2025 20:57:07 PM ******/
CREATE NONCLUSTERED INDEX [IX_Bookings_EventId] ON [dbo].[Bookings]
(
	[EventId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bookings_VenueId]    Script Date: 05 Apr 2025 20:57:07 PM ******/
CREATE NONCLUSTERED INDEX [IX_Bookings_VenueId] ON [dbo].[Bookings]
(
	[VenueId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Events_VenueId]    Script Date: 05 Apr 2025 20:57:07 PM ******/
CREATE NONCLUSTERED INDEX [IX_Events_VenueId] ON [dbo].[Events]
(
	[VenueId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Events_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Events_EventId]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Venues_VenueId] FOREIGN KEY([VenueId])
REFERENCES [dbo].[Venues] ([VenueId])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Venues_VenueId]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Venues_VenueId] FOREIGN KEY([VenueId])
REFERENCES [dbo].[Venues] ([VenueId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Venues_VenueId]
GO
ALTER DATABASE [EventEaseWebAppDbST10433968] SET  READ_WRITE 
GO
