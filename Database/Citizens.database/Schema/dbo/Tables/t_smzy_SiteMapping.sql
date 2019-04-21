
CREATE TABLE [dbo].[t_smzy_SiteMapping](
	[MappingID] [nvarchar](50) NOT NULL,
	[SvrID] [nvarchar](50) NOT NULL,
	[AppID] [nvarchar](50) NOT NULL,
	[ChannelID] [nvarchar](50) NOT NULL,
	[ArticleID] [nvarchar](50) NOT NULL,
	[Summary] [nvarchar](2000) NULL,
	[Images] [nvarchar](2000) NULL,
	[ArticleTitle] [nvarchar](200) NULL,
	[ArticleSourceName] [nvarchar](200) NULL,
	[ArticleSource] [nvarchar](200) NULL,
	[OverdueTime] [datetime] NULL,
	[ArticleTime] [datetime] NULL,
	[Category] [nvarchar](50) NOT NULL,
	[OpenStyle] [nvarchar](50) NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[IsDeleted] [bit] NULL,
	[VersionNo] [int] NULL,
	[TransactionID] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdatedBy] [nvarchar](50) NULL,
	[LastUpdatedTime] [datetime] NULL,
	[PublishUserName] [nvarchar](50) NULL,
	[ArticleVisit] [int] NULL,
	[IsAnswer] [int] NULL,
 CONSTRAINT [PK_t_smzy_PageMapping] PRIMARY KEY CLUSTERED 
(
	[MappingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点映射关系ID GUID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_SiteMapping', @level2type=N'COLUMN',@level2name=N'MappingID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属应用程序ID ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_SiteMapping', @level2type=N'COLUMN',@level2name=N'AppID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_SiteMapping', @level2type=N'COLUMN',@level2name=N'ChannelID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文稿ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_SiteMapping', @level2type=N'COLUMN',@level2name=N'ArticleID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'映射类型 SiteHome 站点首页   ChannelHome 栏目首页  ContentDetails 内容详细页 ChannelSeachPage 栏目搜素页 NoDefine 没有指定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_SiteMapping', @level2type=N'COLUMN',@level2name=N'Category'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面详细地址(完整地址)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_SiteMapping', @level2type=N'COLUMN',@level2name=N'Url'
GO

ALTER TABLE [dbo].[t_smzy_SiteMapping] ADD  CONSTRAINT [DF_t_smzy_SiteMapping_IsAnswer]  DEFAULT ((0)) FOR [IsAnswer]
GO