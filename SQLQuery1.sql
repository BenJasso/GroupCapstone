USE [ActivitiesAPI]
GO
/****** Object: Table [dbo].[ActivityTypes] Script Date: 5/16/2020 2:24:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityTypes] (
    [ActivityTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [ActivityTypes]  NVARCHAR (MAX) NULL
);