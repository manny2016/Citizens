

CREATE TABLE [dbo].[t_smzy_Article](
	[ArticleId] [nvarchar](50) NOT NULL,
	[ArticleTitle] [nvarchar](200) NULL,
	[ArticleSubtitle] [nvarchar](256) NULL,
	[CoverImage] [nvarchar](256) NULL,
	[AllowComent] [nvarchar](50) NULL,
	[Visits] [int] NULL,
	[ArticleStatus] [nvarchar](50) NULL,
	[OverdueTime] [datetime] NULL,
	[HtmlContent] [varchar](max) NULL,
	[TextContent] [text] NULL,
	[VisitRights] [nvarchar](256) NULL,
	[Keyword] [nvarchar](100) NULL,
	[ArticleType] [varchar](50) NULL,
	[ArticleTime] [datetime] NULL,
	[ArticleWriter] [nvarchar](50) NULL,
	[ArticleSourceName] [nvarchar](50) NULL,
	[ArticleSource] [nvarchar](256) NULL,
	[IsDeleted] [bit] NULL,
	[VersionNo] [int] NULL,
	[TransactionID] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[LastUpdatedBy] [nvarchar](50) NULL,
	[LastUpdatedTime] [datetime] NULL,
	[ArticleVisit] [int] NULL,
	[IsAnswer] [int] NULL,
 CONSTRAINT [PK_T_SMZY_ARTICLE] PRIMARY KEY NONCLUSTERED 
(
	[ArticleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问次数总计，访问历史记录存放于其他表
   定期进行统一汇总并清理' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'Visits'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Editing 草稿或编辑中
   Pending Audit 待审核
   Auditing 审核通过
   Published 已发布
   
   文稿编辑时候 默认保存为草稿，只有提交以后才到待审核状态 审核通过以后 状态变为 Auditing 审核通过，只有审核通过的文稿才允许发布
   发布以后生产前台今天页面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'ArticleStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文稿详细内容，存储文稿的 Html 代码 用于在 WEB 门户展示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'HtmlContent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文稿纯文字内容，如果有图片的话则转换问固定标签' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'TextContent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问权限，存储运行访问的角色ID，有多个则以逗号分割
   为空则为所有人可见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'VisitRights'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文稿关键字，用于搜索引擎优化 有多个则以逗号分割' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'Keyword'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文稿来源
   
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'ArticleSource'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'IsDeleted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据版本 没修改一次 该值递增 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'VersionNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否回复' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article', @level2type=N'COLUMN',@level2name=N'IsAnswer'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文稿数据表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_smzy_Article'
GO


