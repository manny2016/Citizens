using System;
using System.Linq;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var entities = ExcelReader.ReadExcel<Channel>("Channels.xlsx", "Sheet1", (dictnoary) =>
            {
                return new Channel()
                {
                    ChannelID = dictnoary["ChannelID"],
                    ChannelHref = dictnoary["ChannelHref"],
                    ChannelName = dictnoary["ChannelName"],
                    ParentChannelID = dictnoary["ParentChannelID"]
                };
            });
            foreach (var entity in entities)
            {
                Console.WriteLine($"{entity.ChannelID},{entity.ChannelName},{entity.ChannelHref},{entity.ParentChannelID}");
            }
            var values = entities.Where(o => !o.ChannelHref.StartsWith("../Site")).Select(ctx =>
            {
                return $"('{ctx.ChannelID}',\t\t'{ctx.ChannelName}',\t\t'{ctx.ParentChannelID}',\t\t'{ctx.ChannelHref}')";
            });
            var queryString = string.Format(@"
MERGE INTO [dbo].[t_smzy_Channels] [target]
USING(
	VALUES
    {0}
)[source](ChannelId,ChannelName,ParentChannelID,ChannelHref)
ON [target].[ChannelId]= [source].[ChannelId]
WHEN MATCHED THEN UPDATE SET [target].[ChannelHref]=[source].[ChannelHref];", string.Join(",\r\n",values));

        }
    }
}
