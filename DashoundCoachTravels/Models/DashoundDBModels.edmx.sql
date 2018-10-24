
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/24/2018 16:59:49
-- Generated from EDMX file: D:\TEMP\semestr2_mag\Aplikacje Internetowe\Projekt\MAINPROJECT\DashoundCoachTravels\DashoundCoachTravels\Models\DashoundDBModels.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-DashoundCoachTravels-20181023074319];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationsTrips_Locations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trips_Locations] DROP CONSTRAINT [FK_LocationsTrips_Locations];
GO
IF OBJECT_ID(N'[dbo].[FK_TripsTrips_Locations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trips_Locations] DROP CONSTRAINT [FK_TripsTrips_Locations];
GO
IF OBJECT_ID(N'[dbo].[FK_CoachesTrips]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Coaches] DROP CONSTRAINT [FK_CoachesTrips];
GO
IF OBJECT_ID(N'[dbo].[FK_TripsReservations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_TripsReservations];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationsAspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_ReservationsAspNetUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetRoleUsersAspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetRoleUsers] DROP CONSTRAINT [FK_AspNetRoleUsersAspNetUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetRoleUsersAspNetRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetRoleUsers] DROP CONSTRAINT [FK_AspNetRoleUsersAspNetRole];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Trips]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trips];
GO
IF OBJECT_ID(N'[dbo].[Trips_Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trips_Locations];
GO
IF OBJECT_ID(N'[dbo].[Coaches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Coaches];
GO
IF OBJECT_ID(N'[dbo].[Reservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservations];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoleUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoleUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [Town] nvarchar(max)  NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [NumHouse] nvarchar(max)  NOT NULL,
    [NumFlat] nvarchar(max)  NOT NULL,
    [ZIPCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [Town] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Trips'
CREATE TABLE [dbo].[Trips] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateDeparture] nvarchar(max)  NOT NULL,
    [DateBack] nvarchar(max)  NOT NULL,
    [SpotsAvaliable] nvarchar(max)  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [BannerBig] nvarchar(max)  NOT NULL,
    [BannerSmall] nvarchar(max)  NOT NULL,
    [CoachType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Trips_Locations'
CREATE TABLE [dbo].[Trips_Locations] (
    [Id] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Location_Id] int  NOT NULL,
    [Trip_Id] int  NOT NULL
);
GO

-- Creating table 'Coaches'
CREATE TABLE [dbo].[Coaches] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Brand] nvarchar(max)  NOT NULL,
    [Seats] nvarchar(max)  NOT NULL,
    [DateAdded] nvarchar(max)  NOT NULL,
    [VehicleNumber] nvarchar(max)  NOT NULL,
    [Trips_Id] int  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateBooked] nvarchar(max)  NOT NULL,
    [NumPeople] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [DatePayedAdvance] nvarchar(max)  NOT NULL,
    [DatePayedFull] nvarchar(max)  NOT NULL,
    [Advance] nvarchar(max)  NOT NULL,
    [Trip_Id] int  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetRoleUsers'
CREATE TABLE [dbo].[AspNetRoleUsers] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL,
    [AspNetRoles_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Trips'
ALTER TABLE [dbo].[Trips]
ADD CONSTRAINT [PK_Trips]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Trips_Locations'
ALTER TABLE [dbo].[Trips_Locations]
ADD CONSTRAINT [PK_Trips_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Coaches'
ALTER TABLE [dbo].[Coaches]
ADD CONSTRAINT [PK_Coaches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [RoleId], [UserId] in table 'AspNetRoleUsers'
ALTER TABLE [dbo].[AspNetRoleUsers]
ADD CONSTRAINT [PK_AspNetRoleUsers]
    PRIMARY KEY CLUSTERED ([RoleId], [UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [Location_Id] in table 'Trips_Locations'
ALTER TABLE [dbo].[Trips_Locations]
ADD CONSTRAINT [FK_LocationsTrips_Locations]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationsTrips_Locations'
CREATE INDEX [IX_FK_LocationsTrips_Locations]
ON [dbo].[Trips_Locations]
    ([Location_Id]);
GO

-- Creating foreign key on [Trip_Id] in table 'Trips_Locations'
ALTER TABLE [dbo].[Trips_Locations]
ADD CONSTRAINT [FK_TripsTrips_Locations]
    FOREIGN KEY ([Trip_Id])
    REFERENCES [dbo].[Trips]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TripsTrips_Locations'
CREATE INDEX [IX_FK_TripsTrips_Locations]
ON [dbo].[Trips_Locations]
    ([Trip_Id]);
GO

-- Creating foreign key on [Trips_Id] in table 'Coaches'
ALTER TABLE [dbo].[Coaches]
ADD CONSTRAINT [FK_CoachesTrips]
    FOREIGN KEY ([Trips_Id])
    REFERENCES [dbo].[Trips]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CoachesTrips'
CREATE INDEX [IX_FK_CoachesTrips]
ON [dbo].[Coaches]
    ([Trips_Id]);
GO

-- Creating foreign key on [Trip_Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_TripsReservations]
    FOREIGN KEY ([Trip_Id])
    REFERENCES [dbo].[Trips]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TripsReservations'
CREATE INDEX [IX_FK_TripsReservations]
ON [dbo].[Reservations]
    ([Trip_Id]);
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ReservationsAspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationsAspNetUser'
CREATE INDEX [IX_FK_ReservationsAspNetUser]
ON [dbo].[Reservations]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetRoleUsers'
ALTER TABLE [dbo].[AspNetRoleUsers]
ADD CONSTRAINT [FK_AspNetRoleUsersAspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetRoleUsersAspNetUser'
CREATE INDEX [IX_FK_AspNetRoleUsersAspNetUser]
ON [dbo].[AspNetRoleUsers]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetRoleUsers'
ALTER TABLE [dbo].[AspNetRoleUsers]
ADD CONSTRAINT [FK_AspNetRoleUsersAspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetRoleUsersAspNetRole'
CREATE INDEX [IX_FK_AspNetRoleUsersAspNetRole]
ON [dbo].[AspNetRoleUsers]
    ([AspNetRoles_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------