-- Insertion des co�ts.
BULK INSERT [Cookbook].[dbo].[Cost]  
   FROM N'$(DataPath)\Cost.csv'  
   WITH   
      (  
         FIELDTERMINATOR =';',  
         ROWTERMINATOR ='\n',
		 CODEPAGE = 'ACP'
      );

-- Insertion des difficult�s.
BULK INSERT [Cookbook].[dbo].[Difficulty]  
   FROM N'$(DataPath)\Difficulty.csv'  
   WITH   
      (  
         FIELDTERMINATOR =';',  
         ROWTERMINATOR ='\n',
		 CODEPAGE = 'ACP'
      );

-- Insertion des sp�cificit�s.
BULK INSERT [Cookbook].[dbo].[Feature]  
   FROM N'$(DataPath)\Feature.csv'  
   WITH   
      (  
         FIELDTERMINATOR =';',  
         ROWTERMINATOR ='\n',
		 CODEPAGE = 'ACP'
      );

-- Insertion des types d'ingr�dients.
BULK INSERT [Cookbook].[dbo].[IngredientKind]  
   FROM N'$(DataPath)\IngredientKind.csv'  
   WITH   
      (  
         FIELDTERMINATOR =';',  
         ROWTERMINATOR ='\n',
		 CODEPAGE = 'ACP'
      );

-- Insertion des types de recettes.
BULK INSERT [Cookbook].[dbo].[RecipeKind]  
   FROM N'$(DataPath)\RecipeKind.csv'  
   WITH   
      (  
         FIELDTERMINATOR =';',  
         ROWTERMINATOR ='\n',
		 CODEPAGE = 'ACP'
      );

-- Insertion des saisons.
BULK INSERT [Cookbook].[dbo].[Season]  
   FROM N'$(DataPath)\Season.csv'  
   WITH   
      (  
         FIELDTERMINATOR =';',  
         ROWTERMINATOR ='\n',
		 CODEPAGE = 'ACP'
      );

-- Insertion des mesures.
BULK INSERT [Cookbook].[dbo].[Measure]  
   FROM N'$(DataPath)\Measure.csv'  
   WITH   
      (  
         FIELDTERMINATOR =';',  
         ROWTERMINATOR ='\n',
		 CODEPAGE = 'ACP'
      );