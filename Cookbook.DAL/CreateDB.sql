CREATE DATABASE [Cookbook]
GO

USE [Cookbook]

CREATE TABLE [dbo].[Cost](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Cost] PRIMARY KEY ([Id]))
GO

INSERT INTO dbo.Cost (Id, Name)
VALUES (0, 'None'), (1, 'Cheap'), (2, 'Medium'), (4, 'Expensive');


CREATE TABLE [dbo].[Difficulty](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Difficulty] PRIMARY KEY ([Id]))
GO

INSERT INTO dbo.Difficulty (Id, Name)
VALUES (0, 'None'), (1, 'VeryEasy'), (2, 'Easy'), (4, 'Medium'), (8, 'Difficult'), (16, 'VeryDifficult');


CREATE TABLE [dbo].[Feature](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Feature] PRIMARY KEY ([Id]))
GO

INSERT INTO dbo.Feature (Id, Name)
VALUES (0, 'None'), (1, 'Vegan'), (2, 'Festive');


CREATE TABLE [dbo].[Season](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_Season] PRIMARY KEY ([Id]))
GO

INSERT INTO dbo.Season (Id, Name)
VALUES (0, 'None'), (1, 'Winter'), (2, 'Spring'), (4, 'Summer'), (8, 'Autumn');


CREATE TABLE [dbo].[RecipeKind](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	CONSTRAINT [PK_RecipeKind] PRIMARY KEY ([Id]))
GO

INSERT INTO dbo.RecipeKind (Id, Name)
VALUES (0, 'None'), (1, 'Starter'), (2, 'MainCourse'), (4, 'Dessert'), (8, 'Sauce'), (16, 'Drink'), (32, 'SideDish'), (64, 'AmuseGueule'), (128, 'Sweet');


CREATE TABLE [dbo].[Ingredient](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_Ingredient] PRIMARY KEY ([Id]))
GO


CREATE TABLE [dbo].[Measure](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_Measure] PRIMARY KEY ([Id]))
GO


CREATE TABLE [dbo].[Recipe](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[PreparationTime] [int] NOT NULL,
	[CookingTime] [int] NOT NULL,
	[ExternalUrl] [nvarchar](500) NULL,
	[UserId] [uniqueidentifier] NULL,
	CONSTRAINT [PK_Recipe] PRIMARY KEY ([Id]))
GO


CREATE TABLE [dbo].[RecipeIngredient](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[IngredientId] [uniqueidentifier] NOT NULL,
	[MeasureId] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	CONSTRAINT [PK_RecipeIngredient] PRIMARY KEY ([RecipeId], [IngredientId], [MeasureId], [Order]))
GO

ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredient_Ingredient] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredient] ([Id])
GO

ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredient_Measure] FOREIGN KEY([MeasureId])
REFERENCES [dbo].[Measure] ([Id])
GO

ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredient_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO


CREATE TABLE [dbo].[RecipeInstruction](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[Instruction] [nvarchar](MAX) NOT NULL,
	CONSTRAINT [PK_RecipeInstruction] PRIMARY KEY ([RecipeId], [Order]))
GO

ALTER TABLE [dbo].[RecipeInstruction]  WITH CHECK ADD  CONSTRAINT [FK_RecipeInstruction_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO


CREATE TABLE [dbo].[RecipeFeature](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[FeatureId] [int] NOT NULL,
	CONSTRAINT [PK_RecipeFeature] PRIMARY KEY ([RecipeId], [FeatureId]))
GO

ALTER TABLE [dbo].[RecipeFeature]  WITH CHECK ADD  CONSTRAINT [FK_RecipeFeature_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

ALTER TABLE [dbo].[RecipeFeature]  WITH CHECK ADD  CONSTRAINT [FK_RecipeFeature_Feature] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Feature] ([Id])
GO


CREATE TABLE [dbo].[RecipeSeason](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[SeasonId] [int] NOT NULL,
	CONSTRAINT [PK_RecipeSeason] PRIMARY KEY ([RecipeId], [SeasonId]))
GO

ALTER TABLE [dbo].[RecipeSeason]  WITH CHECK ADD  CONSTRAINT [FK_RecipeSeason_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

ALTER TABLE [dbo].[RecipeSeason]  WITH CHECK ADD  CONSTRAINT [FK_RecipeSeason_Season] FOREIGN KEY([SeasonId])
REFERENCES [dbo].[Season] ([Id])
GO


CREATE TABLE [dbo].[RecipeCost](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[CostId] [int] NOT NULL,
	CONSTRAINT [PK_RecipeCost] PRIMARY KEY ([RecipeId], [CostId]))
GO

ALTER TABLE [dbo].[RecipeCost]  WITH CHECK ADD  CONSTRAINT [FK_RecipeCost_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

ALTER TABLE [dbo].[RecipeCost]  WITH CHECK ADD  CONSTRAINT [FK_RecipeCost_Cost] FOREIGN KEY([CostId])
REFERENCES [dbo].[Cost] ([Id])
GO


CREATE TABLE [dbo].[RecipeDifficulty](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[DifficultyId] [int] NOT NULL,
	CONSTRAINT [PK_RecipeDifficulty] PRIMARY KEY ([RecipeId], [DifficultyId]))
GO

ALTER TABLE [dbo].[RecipeDifficulty]  WITH CHECK ADD  CONSTRAINT [FK_RecipeDifficulty_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

ALTER TABLE [dbo].[RecipeDifficulty]  WITH CHECK ADD  CONSTRAINT [FK_RecipeDifficulty_Difficulty] FOREIGN KEY([DifficultyId])
REFERENCES [dbo].[Difficulty] ([Id])
GO


CREATE TABLE [dbo].[RecipeRecipeKind](
	[RecipeId] [uniqueidentifier] NOT NULL,
	[RecipeKindId] [int] NOT NULL,
	CONSTRAINT [PK_RecipeRecipeKind] PRIMARY KEY ([RecipeId], [RecipeKindId]))
GO

ALTER TABLE [dbo].[RecipeRecipeKind]  WITH CHECK ADD  CONSTRAINT [FK_RecipeRecipeKind_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
GO

ALTER TABLE [dbo].[RecipeRecipeKind]  WITH CHECK ADD  CONSTRAINT [FK_RecipeRecipeKind_RecipeKind] FOREIGN KEY([RecipeKindId])
REFERENCES [dbo].[RecipeKind] ([Id])
GO