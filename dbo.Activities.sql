USE [ActivitiesAPI]
GO

/****** Object: Table [dbo].[Activities] Script Date: 5/17/2020 2:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Activities];


GO
CREATE TABLE [dbo].[Activities] (
    [ActivityId]    INT            IDENTITY (1, 1) NOT NULL,
    [Price]         FLOAT (53)     NULL,
    [Date]          DATETIME2 (7)  NULL,
    [Company]       NVARCHAR (MAX) NULL,
    [SiteURL]       NVARCHAR (MAX) NULL,
    [Season]        NVARCHAR (MAX) NULL,
    [ZipCode]       INT            NULL,
    [Address]       NVARCHAR (MAX) NULL,
    [Indoor]        BIT            NULL,
    [EventName]     NVARCHAR (MAX) NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [ActivityTypes] NVARCHAR (MAX) NULL,
    [CityName]      NVARCHAR (MAX) NULL,
    [Lat]           FLOAT (53)     NOT NULL,
    [Long]          FLOAT (53)     NOT NULL
);


