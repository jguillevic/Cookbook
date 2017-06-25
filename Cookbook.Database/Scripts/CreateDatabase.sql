USE [master]

IF EXISTS(SELECT * FROM sys.databases WHERE name='Cookbook')
BEGIN
ALTER DATABASE [Cookbook] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP DATABASE [Cookbook]
END
GO

CREATE DATABASE [Cookbook]
COLLATE Latin1_General_100_CS_AS
GO

USE [Cookbook]

CREATE TABLE [dbo].[Cost](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Cost] PRIMARY KEY ([Id]))
GO

CREATE TABLE [dbo].[Difficulty](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Difficulty] PRIMARY KEY ([Id]))
GO

CREATE TABLE [dbo].[Feature](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Feature] PRIMARY KEY ([Id]))
GO

CREATE TABLE [dbo].[Season](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Season] PRIMARY KEY ([Id]))
GO

CREATE TABLE [dbo].[RecipeKind](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_RecipeKind] PRIMARY KEY ([Id]))
GO

CREATE TABLE [dbo].[IngredientKind](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_IngredientKind] PRIMARY KEY ([Id]))
GO

CREATE TABLE [dbo].[Ingredient](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[NationalName] [nvarchar](200) NULL,
	[NationalCode] [nvarchar](30) NULL,
	[IngredientKindId] [uniqueidentifier] NOT NULL,
	[Calories] [decimal](5,2) NOT NULL,
	[Protein] [decimal](3,2) NOT NULL,
	[Carbohydrate] [decimal](3,2) NOT NULL,
	[Lipid] [decimal](3,2) NOT NULL,
	[Water] [decimal](3,2) NOT NULL,
	CONSTRAINT [PK_Ingredient] PRIMARY KEY ([Id]))
GO

ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD CONSTRAINT [FK_Ingredient_IngredientKind] FOREIGN KEY([IngredientKindId])
REFERENCES [dbo].[IngredientKind] ([Id])
GO

CREATE TABLE [dbo].[Measure](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Measure] PRIMARY KEY ([Id]))
GO

CREATE TABLE [dbo].[Recipe](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[PreparationTime] [int] NOT NULL,
	[CookingTime] [int] NOT NULL,
	[CostId] [uniqueidentifier] NOT NULL,
	[DifficultyId] [uniqueidentifier] NOT NULL,
	[RecipeKindId] [uniqueidentifier] NOT NULL,
	[ExternalUrl] [nvarchar](500) NULL,
	[UserId] [uniqueidentifier] NULL,
	CONSTRAINT [PK_Recipe] PRIMARY KEY ([Id]))
GO

ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD CONSTRAINT [FK_Recipe_Cost] FOREIGN KEY([CostId])
REFERENCES [dbo].[Cost] ([Id])
GO

ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD CONSTRAINT [FK_Recipe_Difficulty] FOREIGN KEY([DifficultyId])
REFERENCES [dbo].[Difficulty] ([Id])
GO

ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD CONSTRAINT [FK_Recipe_RecipeKind] FOREIGN KEY([RecipeKindId])
REFERENCES [dbo].[RecipeKind] ([Id])
GO

CREATE TABLE [dbo].[RecipeIngredient](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[IngredientId] [uniqueidentifier] NOT NULL,
	[MeasureId] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[Amount] [decimal](18,7) NOT NULL,
	CONSTRAINT [PK_RecipeIngredient] PRIMARY KEY ([RecipeId], [IngredientId], [MeasureId], [Order]))
GO

ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD CONSTRAINT [FK_RecipeIngredient_Ingredient] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredient] ([Id])
GO

ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD CONSTRAINT [FK_RecipeIngredient_Measure] FOREIGN KEY([MeasureId])
REFERENCES [dbo].[Measure] ([Id])
GO

ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD CONSTRAINT [FK_RecipeIngredient_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

CREATE TABLE [dbo].[RecipeInstruction](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[Instruction] [nvarchar](MAX) NOT NULL,
	CONSTRAINT [PK_RecipeInstruction] PRIMARY KEY ([RecipeId], [Order]))
GO

ALTER TABLE [dbo].[RecipeInstruction]  WITH CHECK ADD CONSTRAINT [FK_RecipeInstruction_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

CREATE TABLE [dbo].[RecipeFeature](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[FeatureId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_RecipeFeature] PRIMARY KEY ([RecipeId], [FeatureId]))
GO

ALTER TABLE [dbo].[RecipeFeature]  WITH CHECK ADD CONSTRAINT [FK_RecipeFeature_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

ALTER TABLE [dbo].[RecipeFeature]  WITH CHECK ADD CONSTRAINT [FK_RecipeFeature_Feature] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Feature] ([Id])
GO

CREATE TABLE [dbo].[RecipeSeason](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[SeasonId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_RecipeSeason] PRIMARY KEY ([RecipeId], [SeasonId]))
GO

ALTER TABLE [dbo].[RecipeSeason]  WITH CHECK ADD CONSTRAINT [FK_RecipeSeason_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

ALTER TABLE [dbo].[RecipeSeason]  WITH CHECK ADD CONSTRAINT [FK_RecipeSeason_Season] FOREIGN KEY([SeasonId])
REFERENCES [dbo].[Season] ([Id])
GO