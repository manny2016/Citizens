CREATE PROCEDURE [dbo].[spImportArticle]
	@articles [dbo].[ImportArticleStructure] READONLY
AS
BEGIN TRY
	BEGIN TRANSACTION
	MERGE INTO [dbo].[t_smzy_Article] [target]
	USING(
		SELECT * FROM 	@articles
	) [source] ON [target].[ArticleId] = [source].[ArticleId]
	WHEN MATCHED THEN UPDATE SET	
			[target].[ArticleTitle]=[source].[ArticleTitle],
			[target].[ArticleSubtitle]  =[source].	[ArticleSubtitle],
			[target].[CoverImage]=[source].[CoverImage],
			[target].[AllowComent]=[source].[AllowComent],
			[target].[Visits] 			=[source].[Visits],
			[target].[ArticleStatus] 	=[source].[ArticleStatus],
			[target].[OverdueTime] 		=[source].[OverdueTime],
			[target].[HtmlContent] 		=[source].[HtmlContent],
			[target].[TextContent]		=[source].[TextContent],
			[target].[VisitRights] 		=[source].[VisitRights],
			[target].[Keyword] 			=[source].[Keyword],
			[target].[ArticleType] 		=[source].[ArticleType],
			[target].[ArticleTime] 		=[source].[ArticleTime],
			[target].[ArticleWriter] 	=[source].[ArticleWriter],
			[target].[ArticleSourceName] =[source].	[ArticleSourceName],
			[target].[ArticleSource] 	=[source].[ArticleSource],
			[target].[IsDeleted] 		=[source].[IsDeleted],
			[target].[VersionNo] 		=[target].[VersionNo] + 1,
			[target].[TransactionID] 	=[source].	[TransactionID],
			[target].[LastUpdatedBy] 	=[source].[LastUpdatedBy],
			[target].[LastUpdatedTime]  =[source].[LastUpdatedTime],
			[target].[ArticleVisit] 	=[source].[ArticleVisit],
			[target].[IsAnswer] 		=[source].[IsAnswer]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ArticleId],[ArticleTitle],[ArticleSubtitle],[CoverImage],[AllowComent],[Visits],[ArticleStatus],[OverdueTime],[HtmlContent],
			[TextContent],[VisitRights],[Keyword],[ArticleType],[ArticleTime],[ArticleWriter],	[ArticleSourceName],[ArticleSource],[IsDeleted],
			[VersionNo],[TransactionID],[CreatedBy],[CreatedTime],[LastUpdatedBy],[LastUpdatedTime],[ArticleVisit],	[IsAnswer])
		VALUES([source].[ArticleId],[source].[ArticleTitle],[source].[ArticleSubtitle],[source].[CoverImage],[source].[AllowComent],
		[source].[Visits],[source].[ArticleStatus],[source].[OverdueTime],[source].[HtmlContent],[source].[TextContent],[source].[VisitRights] ,
		[source].[Keyword],[source].[ArticleType],[source].[ArticleTime],[source].[ArticleWriter],[source].[ArticleSourceName],[source].[ArticleSource] ,
		[source].[IsDeleted],[source].[VersionNo],[source].[TransactionID],[source].[CreatedBy],[source].[CreatedTime],[source].[LastUpdatedBy],
		[source].[LastUpdatedTime],[source].[ArticleVisit],[source].[IsAnswer]);
	
	MERGE INTO [dbo].[t_smzy_SiteMapping] [target]
	USING(
		SELECT * FROM @articles
	) AS [source]
	ON [target].[ArticleId] = [source].[ArticleId]
	WHEN MATCHED THEN UPDATE SET	
		[target].[SvrID]=[source].[SvrID],
		[target].[AppID]=[source].[AppID],
		[target].[ChannelID]=[source].[ChannelID],
		[target].[ArticleId]=[source].[ArticleId],
		[target].[Summary]=[source].[Summary],
		[target].[Images]=[source].[Images],
		[target].[ArticleTitle]=[source].[ArticleTitle],
		[target].[ArticleSourceName]=[source].[ArticleSourceName],
		[target].[ArticleSource]=[source].[ArticleSource],
		[target].[OverdueTime]=[source].[OverdueTime],
		[target].[ArticleTime]=[source].[ArticleTime],
		[target].[Category]=[source].[Category],
		[target].[OpenStyle]=[source].[OpenStyle],
		[target].[Url]=[source].[Url],
		[target].[IsDeleted]=[source].[IsDeleted],
		[target].[VersionNo]=[source].[VersionNo],
		[target].[TransactionID]=[source].[TransactionID],
		[target].[CreatedBy]=[source].[CreatedBy],
		[target].[CreatedTime]=[source].[CreatedTime],
		[target].[LastUpdatedBy]=[source].[LastUpdatedBy],
		[target].[LastUpdatedTime]=[source].[LastUpdatedTime],
		[target].[PublishUserName]=[source].[PublishUserName],
		[target].[ArticleVisit]=[source].[ArticleVisit],
		[target].[IsAnswer]=[source].[IsAnswer]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([MappingID] ,[SvrID] ,[AppID] ,[ChannelID] ,[ArticleId] ,[Summary] ,[Images] ,[ArticleTitle] ,[ArticleSourceName] ,[ArticleSource] ,[OverdueTime] ,[ArticleTime] ,[Category] ,[OpenStyle] ,[Url] ,[IsDeleted] ,[VersionNo] ,[TransactionID] ,[CreatedBy] ,[CreatedTime] ,[LastUpdatedBy] ,[LastUpdatedTime] ,[PublishUserName] ,[ArticleVisit] ,[IsAnswer])
		VALUES([source].[MappingID] ,[source].[SvrID] ,[source].[AppID] ,[source].[ChannelID] ,[source].[ArticleId] ,[source].[Summary] ,[source].[Images] ,[source].[ArticleTitle] ,[source].[ArticleSourceName] ,[source].[ArticleSource] ,[source].[OverdueTime] ,[source].[ArticleTime] ,[source].[Category] ,[source].[OpenStyle] ,[source].[Url] ,[source].[IsDeleted] ,[source].[VersionNo] ,[source].[TransactionID] ,[source].[CreatedBy] ,[source].[CreatedTime] ,[source].[LastUpdatedBy] ,[source].[LastUpdatedTime] ,[source].[PublishUserName] ,[source].[ArticleVisit] ,[source].[IsAnswer]);

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION

	DECLARE @ErrMsg NVARCHAR(4000), @ErrSeverity INT
	SELECT @ErrMsg = ERROR_MESSAGE(), @ErrSeverity = ERROR_SEVERITY()
	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH