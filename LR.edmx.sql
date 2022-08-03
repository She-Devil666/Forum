
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/09/2021 01:18:50
-- Generated from EDMX file: C:\Users\ASUS\source\repos\LR_SERVER\LR.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\Users\ASUS\source\repos\LR_SERVER\bin\Debug\Forum.mdf];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserSpeaker]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpeakerSet] DROP CONSTRAINT [FK_UserSpeaker];
GO
IF OBJECT_ID(N'[dbo].[FK_UserParticipant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParticipantSet] DROP CONSTRAINT [FK_UserParticipant];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStaff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaffSet] DROP CONSTRAINT [FK_UserStaff];
GO
IF OBJECT_ID(N'[dbo].[FK_SpeakerSchedule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScheduleSet] DROP CONSTRAINT [FK_SpeakerSchedule];
GO
IF OBJECT_ID(N'[dbo].[FK_TutotUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet] DROP CONSTRAINT [FK_TutotUser];
GO
IF OBJECT_ID(N'[dbo].[FK_TutotGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupsSet] DROP CONSTRAINT [FK_TutotGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupsSpeaker]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpeakerSet] DROP CONSTRAINT [FK_GroupsSpeaker];
GO
IF OBJECT_ID(N'[dbo].[FK_ParticipantGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupsSet] DROP CONSTRAINT [FK_ParticipantGroups];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[SpeakerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpeakerSet];
GO
IF OBJECT_ID(N'[dbo].[ParticipantSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParticipantSet];
GO
IF OBJECT_ID(N'[dbo].[StaffSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffSet];
GO
IF OBJECT_ID(N'[dbo].[ScheduleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScheduleSet];
GO
IF OBJECT_ID(N'[dbo].[TutorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TutorSet];
GO
IF OBJECT_ID(N'[dbo].[GroupsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupsSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Image] varbinary(max)  NOT NULL,
    [Tutor_Id] int  NULL
);
GO

-- Creating table 'SpeakerSet'
CREATE TABLE [dbo].[SpeakerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Birthday] datetime  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [Transfer] bit  NOT NULL,
    [Salary] int  NOT NULL,
    [Direction] nvarchar(max)  NOT NULL,
    [User_Id] int  NULL,
    [Groups_Id_gr] int  NULL
);
GO

-- Creating table 'ParticipantSet'
CREATE TABLE [dbo].[ParticipantSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Birtday] datetime  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Direction] nvarchar(max)  NOT NULL,
    [Transfer] bit  NOT NULL,
    [Documents] bit  NOT NULL,
    [User_Id] int  NULL
);
GO

-- Creating table 'StaffSet'
CREATE TABLE [dbo].[StaffSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NULL,
    [Position] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Birthday] datetime  NOT NULL,
    [Documents] bit  NOT NULL,
    [R_site] nvarchar(max)  NOT NULL,
    [Place] nvarchar(max)  NOT NULL,
    [User_Id] int  NULL
);
GO

-- Creating table 'ScheduleSet'
CREATE TABLE [dbo].[ScheduleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Date_time] datetime  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Speaker_Id] int  NULL
);
GO

-- Creating table 'TutorSet'
CREATE TABLE [dbo].[TutorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Birthday] datetime  NOT NULL,
    [Documents] bit  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Direction] nvarchar(max)  NOT NULL,
    [Childrens] int  NOT NULL
);
GO

-- Creating table 'GroupsSet'
CREATE TABLE [dbo].[GroupsSet] (
    [Id_gr] int IDENTITY(1,1) NOT NULL,
    [Direction] nvarchar(max)  NOT NULL,
    [Tutor_Id] int  NULL,
    [Participant_Id] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SpeakerSet'
ALTER TABLE [dbo].[SpeakerSet]
ADD CONSTRAINT [PK_SpeakerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ParticipantSet'
ALTER TABLE [dbo].[ParticipantSet]
ADD CONSTRAINT [PK_ParticipantSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffSet'
ALTER TABLE [dbo].[StaffSet]
ADD CONSTRAINT [PK_StaffSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScheduleSet'
ALTER TABLE [dbo].[ScheduleSet]
ADD CONSTRAINT [PK_ScheduleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TutorSet'
ALTER TABLE [dbo].[TutorSet]
ADD CONSTRAINT [PK_TutorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id_gr] in table 'GroupsSet'
ALTER TABLE [dbo].[GroupsSet]
ADD CONSTRAINT [PK_GroupsSet]
    PRIMARY KEY CLUSTERED ([Id_gr] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'SpeakerSet'
ALTER TABLE [dbo].[SpeakerSet]
ADD CONSTRAINT [FK_UserSpeaker]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSpeaker'
CREATE INDEX [IX_FK_UserSpeaker]
ON [dbo].[SpeakerSet]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'ParticipantSet'
ALTER TABLE [dbo].[ParticipantSet]
ADD CONSTRAINT [FK_UserParticipant]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserParticipant'
CREATE INDEX [IX_FK_UserParticipant]
ON [dbo].[ParticipantSet]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'StaffSet'
ALTER TABLE [dbo].[StaffSet]
ADD CONSTRAINT [FK_UserStaff]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStaff'
CREATE INDEX [IX_FK_UserStaff]
ON [dbo].[StaffSet]
    ([User_Id]);
GO

-- Creating foreign key on [Speaker_Id] in table 'ScheduleSet'
ALTER TABLE [dbo].[ScheduleSet]
ADD CONSTRAINT [FK_SpeakerSchedule]
    FOREIGN KEY ([Speaker_Id])
    REFERENCES [dbo].[SpeakerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpeakerSchedule'
CREATE INDEX [IX_FK_SpeakerSchedule]
ON [dbo].[ScheduleSet]
    ([Speaker_Id]);
GO

-- Creating foreign key on [Tutor_Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_TutotUser]
    FOREIGN KEY ([Tutor_Id])
    REFERENCES [dbo].[TutorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TutotUser'
CREATE INDEX [IX_FK_TutotUser]
ON [dbo].[UserSet]
    ([Tutor_Id]);
GO

-- Creating foreign key on [Tutor_Id] in table 'GroupsSet'
ALTER TABLE [dbo].[GroupsSet]
ADD CONSTRAINT [FK_TutotGroups]
    FOREIGN KEY ([Tutor_Id])
    REFERENCES [dbo].[TutorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TutotGroups'
CREATE INDEX [IX_FK_TutotGroups]
ON [dbo].[GroupsSet]
    ([Tutor_Id]);
GO

-- Creating foreign key on [Groups_Id_gr] in table 'SpeakerSet'
ALTER TABLE [dbo].[SpeakerSet]
ADD CONSTRAINT [FK_GroupsSpeaker]
    FOREIGN KEY ([Groups_Id_gr])
    REFERENCES [dbo].[GroupsSet]
        ([Id_gr])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsSpeaker'
CREATE INDEX [IX_FK_GroupsSpeaker]
ON [dbo].[SpeakerSet]
    ([Groups_Id_gr]);
GO

-- Creating foreign key on [Participant_Id] in table 'GroupsSet'
ALTER TABLE [dbo].[GroupsSet]
ADD CONSTRAINT [FK_ParticipantGroups]
    FOREIGN KEY ([Participant_Id])
    REFERENCES [dbo].[ParticipantSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ParticipantGroups'
CREATE INDEX [IX_FK_ParticipantGroups]
ON [dbo].[GroupsSet]
    ([Participant_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------