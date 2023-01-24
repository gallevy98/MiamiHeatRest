-- 2.b.i: Create the necessary database structure to store Scouting Reports

USE ScoutingReports
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ScoutingReport](
	[PlayerKey] [int] NOT NULL,
	[TeamKey] [int] NOT NULL,
	[ScoutId] [int] NOT NULL,
	[DateCreated] [datetime] NULL,
	[Comment] [text] NULL,
	[Defense] [int] NOT NULL,
	[Rebound] [int] NOT NULL,
	[Shooting] [int] NOT NULL,
	[Assist] [int] NOT NULL,
 CONSTRAINT [PK_ScoutingReport] PRIMARY KEY  
(
	[PlayerKey] ASC,
	[TeamKey] ASC,
	[ScoutId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [ScoutingReport] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [ScoutingReport] ADD  CONSTRAINT CHK_DEFENSE CHECK (Defense>=0 AND Defense<=10)
GO
ALTER TABLE [ScoutingReport] ADD  CONSTRAINT CHK_REBOUND CHECK (Rebound>=0 AND Rebound<=10)
GO
ALTER TABLE [ScoutingReport] ADD  CONSTRAINT CHK_SHOOTING CHECK (Shooting>=0 AND Shooting<=10)
GO
ALTER TABLE [ScoutingReport] ADD  CONSTRAINT CHK_ASSIST CHECK (Assist>=0 AND Assist<=10)
GO