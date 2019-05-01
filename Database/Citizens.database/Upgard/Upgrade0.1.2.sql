--CLear old data
DELETE FROM [dbo].[t_smzy_SiteMapping]
WHERE ArticleID IN 
(
	SELECT ArticleID FROM [dbo].[t_smzy_Article] WHERE ArticleType='AutoCollection'
);
DELETE FROM [t_smzy_Article] WHERE ArticleType='AutoCollection'