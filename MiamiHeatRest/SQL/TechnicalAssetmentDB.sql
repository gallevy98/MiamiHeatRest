CREATE DATABASE ScoutingReports
GO
USE ScoutingReports
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [League](
	[LeagueKey] [int] NOT NULL,
	[LeagueName] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[ActiveSource] [bit] NULL,
	[LeagueGroupKey] [int] NULL,
	[LeagueCustomGroupKey] [int] NULL,
	[SearchDisplayFlag] [bit] NOT NULL,
 CONSTRAINT [PK_League] PRIMARY KEY 
(
	[LeagueKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Player]    Script Date: 11/29/2022 4:52:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Player](
	[PlayerKey] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[BirthDate] [date] NULL,
	[PositionKey] [int] NULL,
	[AgentKey] [int] NULL,
	[FreeAgentYear] [int] NULL,
	[Height] [decimal](6, 4) NULL,
	[Weight] [decimal](6, 2) NULL,
	[YearsOfService] [int] NULL,
	[Wing] [decimal](6, 4) NULL,
	[BodyFat] [decimal](5, 2) NULL,
	[StandingReach] [decimal](6, 4) NULL,
	[CourtRunTime_3_4] [decimal](5, 2) NULL,
	[VerticalJumpNoStep] [decimal](6, 4) NULL,
	[VerticalJumpMax] [decimal](6, 4) NULL,
	[HandWidth] [decimal](5, 2) NULL,
	[HandLength] [decimal](5, 2) NULL,
	[URLPhoto] [nvarchar](250) NULL,
	[ActiveAnalysisFlg] [bit] NOT NULL,
	[LeagueCustomGroupKey] [int] NULL,
	[BboPlayerKey] [int] NULL,
	[dwh_insert_datetime] [datetime] NOT NULL,
	[dwh_update_datetime] [datetime] NULL,
	[AgentName] [nvarchar](200) NULL,
	[AgentPhone] [nvarchar](50) NULL,
	[CommittedTo] [nvarchar](200) NULL,
	[Handedness] [nvarchar](10) NULL,
	[GLPlayerKey] [int] NULL,
	[PlayerStatusKey] [int] NULL,
	[Height_Source] [nvarchar](100) NULL,
	[Weight_Source] [nvarchar](100) NULL,
	[Wing_Source] [nvarchar](100) NULL,
	[BodyFat_Source] [nvarchar](100) NULL,
	[StandingReach_Source] [nvarchar](100) NULL,
	[CourtRunTime_3_4_Source] [nvarchar](100) NULL,
	[VerticalJumpNoStep_Source] [nvarchar](100) NULL,
	[VerticalJumpMax_Source] [nvarchar](100) NULL,
	[Hand_W_H_Source] [nvarchar](100) NULL,
	[Hand] [nvarchar](10) NULL,
	[IsCustomData] [bit] NOT NULL,
	[Handedness_Source] [nvarchar](100) NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY 
(
	[PlayerKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Team](
	[TeamKey] [int] NOT NULL,
	[LeagueKey] [int] NULL,
	[LeagueKey_Domestic] [int] NULL,
	[ArenaKey] [int] NULL,
	[TeamName] [nvarchar](50) NOT NULL,
	[TeamNickname] [nvarchar](100) NULL,
	[Conference] [nvarchar](100) NULL,
	[SubConference] [nvarchar](100) NULL,
	[TeamCity] [nvarchar](100) NULL,
	[TeamCountry] [nvarchar](100) NULL,
	[CoachName] [nvarchar](100) NULL,
	[URLPhoto] [nvarchar](250) NULL,
	[CurrentNBATeamFlg] [bit] NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY 
(
	[TeamKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_DimTeam] UNIQUE NONCLUSTERED 
(
	[TeamKey] ASC,
	[LeagueKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TeamPlayer](
	[PlayerKey] [int] NOT NULL,
	[TeamKey] [int] NOT NULL,
	[SeasonKey] [int] NOT NULL,
	[ActiveTeamFlg] [bit] NULL,
	[dwh_insert_datetime] [datetime] NOT NULL,
 CONSTRAINT [PK_TeamPlayer] PRIMARY KEY  
(
	[PlayerKey] ASC,
	[TeamKey] ASC,
	[SeasonKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User](
	[AzureAdUserId] [nvarchar](450) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ActiveFlag] [bit] NOT NULL,
	[Order] [int] NULL,
PRIMARY KEY  
(
	[AzureAdUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [League] ADD  DEFAULT ((1)) FOR [SearchDisplayFlag]
GO
ALTER TABLE [Player] ADD  CONSTRAINT [Default_ActiveAnalysisFlgN]  DEFAULT ((0)) FOR [ActiveAnalysisFlg]
GO
ALTER TABLE [Player] ADD  DEFAULT (getdate()) FOR [dwh_insert_datetime]
GO
ALTER TABLE [Player] ADD  CONSTRAINT [PLAYER_CUSTOMDATA_FALSE]  DEFAULT ((0)) FOR [IsCustomData]
GO
ALTER TABLE [Team] ADD  DEFAULT ((0)) FOR [CurrentNBATeamFlg]
GO
ALTER TABLE [TeamPlayer] ADD  DEFAULT (getdate()) FOR [dwh_insert_datetime]
GO
ALTER TABLE [User] ADD  CONSTRAINT [DF__User__CreatedDat__561752D2]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [User] ADD  CONSTRAINT [DF__User__ModifiedDa__63714DF0]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [User] ADD  CONSTRAINT [DF_User_ActiveFlag]  DEFAULT ((1)) FOR [ActiveFlag]
GO
ALTER TABLE [Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_League] FOREIGN KEY([LeagueKey])
REFERENCES [League] ([LeagueKey])
GO
ALTER TABLE [Team] CHECK CONSTRAINT [FK_Team_League]
GO
ALTER TABLE [Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_LeagueDomestic] FOREIGN KEY([LeagueKey_Domestic])
REFERENCES [League] ([LeagueKey])
GO
ALTER TABLE [Team] CHECK CONSTRAINT [FK_Team_LeagueDomestic]
GO
ALTER TABLE [TeamPlayer]  WITH CHECK ADD  CONSTRAINT [FK_TeamPlayer_Player] FOREIGN KEY([PlayerKey])
REFERENCES [Player] ([PlayerKey])
GO
ALTER TABLE [TeamPlayer] CHECK CONSTRAINT [FK_TeamPlayer_Player]
GO
ALTER TABLE [TeamPlayer]  WITH CHECK ADD  CONSTRAINT [FK_TeamPlayer_Team] FOREIGN KEY([TeamKey])
REFERENCES [Team] ([TeamKey])
GO
ALTER TABLE [TeamPlayer] CHECK CONSTRAINT [FK_TeamPlayer_Team]
GO
