--CLear old data
DELETE FROM [dbo].[t_smzy_SiteMapping]
WHERE ArticleID IN 
(
	SELECT ArticleID FROM [dbo].[t_smzy_Article] WHERE ArticleType='AutoCollection'
);
DELETE FROM [t_smzy_Article] WHERE ArticleType='AutoCollection'


MERGE INTO [dbo].[t_smzy_Channels] [target]
USING(
	VALUES
	('337D97EC-85EC-4AC0-8EE7-1006E3E00099','http://train.qunar.com/?ex_track=bd_aladding_train_tb_title'),
	('6FE6FAC0-2776-4F0C-9551-F9C5196257A3','https://yixing.changtu.com'),
	('0C99CDC7-26F6-48C4-AB6F-6C14F93D8B96','http://yixing.meituan.com/?utm_campaign=baidu&utm_medium=organic&utm_source=baidu&utm_content=homepage&utm_term='),
	('36997F4F-FC49-4280-B255-320546E6BF46','http://www.guahao.com/search/hospital?q=%E5%AE%9C%E5%85%B4'),
	('41FD62F8-C404-4647-8011-1D4FB349DF88','http://www.dianping.com/search/keyword/428/0_%E5%AE%9C%E5%85%B4'),
	('A9E6D9B6-23CC-4692-85CD-C6B476A41116','http://www.yxhr.com.cn'),
	('ebb78107-f453-11e4-aa81-0010031f0160','http://ggfw.wuxi.gov.cn/wx_checkName/checkName/service/checkName.queryIndex.do'),
	('EF15BC8A-C258-4594-92CD-5E4F235089DA','http://ggfw.wuxi.gov.cn/wx_portal/'),
	('AF5527F1-6F6F-4680-8F8A-5D90D62D495F','http://web.jiangsuedu.net/educloud2/ucenter/user/toLogin')
)[source]([ChannelId],[ChannelHref])
ON [target].[ChannelId]=[source].[ChannelId]
WHEN MATCHED THEN UPDATE SET [target].[ChannelHref]=[source].[ChannelHref], [target].[ChannelDesc]=[source].[ChannelHref];