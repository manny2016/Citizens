MERGE INTO [dbo].[t_smzy_Channels] [target]
USING(
	VALUES
	('14595689-789B-433D-A4A4-7ED32182E24F','环保局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282HB#fw_jump'),
	('14B27103-41BE-43CB-80F8-65B5B4384BB0','民政局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282MZ#fw_jump'),
	('190ADAC1-8F92-44A3-ACC3-795EB4E18B93','人社局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282RS#fw_jump'),
	('3709D8F7-B230-404E-97F4-7BD40E3E83E0','卫生局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/fr_index.do?webId=40&themid=B220&deptid='),
	('387A09A0-75B5-4117-BDDC-C6FC1C63442C','市场监督局',		'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282JG#fw_jump'),
	('3A458731-EFDA-4871-A1C0-AE8C08784AF6','供电公司',			'http://www.yixing.gov.cn/default.php?mod=c&s=ssb6f088a&amp;did=109'),
	('3CAA3F55-8F96-4082-AF9C-11A5ED2A3893','住房局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282FGJ#fw_jump'),
	('43CF435A-1773-4B20-95CC-06EE5097965E','民宗局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282MV#fw_jump'),
	('6493E071-FAD7-42C9-9225-31F475B92195','建设局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282SL#fw_jump' ),
	('6B11868A-BCA7-49CB-A944-180D74242D28','教育局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282JY1#fw_jump'),
	('853B5527-5170-4466-B51A-4EF3A4A5F39E','公共事业局',		'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282SG#fw_jump'),
	('8C41B651-E8C3-4B38-9681-5309A37B1504','广电局',			'http://wxyx.jszwfw.gov.cn/jszwfw/publicservice/itemlist/gg_index.do?webId=40&themid=&deptid=320282WHWX&fwtype='),
	('AF8D13E2-92B8-4F8C-AA44-9192BEF7C0FF','旅游局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282LY#fw_jump'),
	('AFA7A3BE-005E-4B49-9AFF-922C8AF106B8','财政局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282CZ#fw_jump'),
	('BFA46C92-D14F-46E0-82FA-C6B2E7B9AE41','计生局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282WS#fw_jump'),
	('ED66E893-5373-4ED0-B60A-C8A3198D151D','规划局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282GH#fw_jump'),
	('F52C116A-BF51-4489-B285-40A627FD05AD','城管局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282CG#fw_jump'),
	('F7B2884F-CDA9-4E64-903C-2F27295383EE','公安局',			'http://wxyx.jszwfw.gov.cn/jszwfw/bscx/itemlist/gr_index.do?webId=40&themid=&deptid=320282GA#fw_jump')
)[source](ChannelId,ChannelName,ChannelHref)
ON [target].[ChannelId]= [source].[ChannelId]
WHEN MATCHED THEN UPDATE SET [target].[ChannelHref]=[source].[ChannelHref];
