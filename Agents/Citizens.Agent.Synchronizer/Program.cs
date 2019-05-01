

namespace Citizens.Agent.Synchronizer
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.DependencyInjection;

    using Citizens.Core;
    using Citizens.Core.Sync;
    using Citizens.Core.Sync.Models;
    using Citizens.Core.Models;

    class Program
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(Program));
        //private static readonly bool SyncHistory = true;
        static void Main(string[] args)
        {
#if DEBUG
            StartSyncHistory();
#else
            StartAuto();
#endif            

        }
        static void StartAuto()
        {
            Configure();
            var workitems = CitizensHost.GetServices<ICitizensWorkItem>();
            var scheduler = new WebJobScheduler((cancellation) =>
            {
                Parallel.ForEach(workitems, new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 5
                }, (workitem) =>
                {
                    while (cancellation.IsCancellationRequested == false)
                    {

                        try
                        {
                            var offset = 60D * 60;//5 mins                        
                            workitem.Execute();
                            for (var i = 0; ((cancellation.IsCancellationRequested == false) && (i < offset)); i++)
                            {
                                Thread.Sleep(1000);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex.Message, ex);
                        }
                    }
                });
            });
            scheduler.Shutdown += (sender, args) =>
            {
                foreach (var workitem in workitems)
                {
                    workitem.Abort();
                }
            };
            scheduler.Start();
        }

        static void Configure()
        {
            CitizensHost.ConfigureServiceProvider((configure) =>
            {
                configure.AddCoreServices();
                var yxhouse = new YxhouseHtmlSynchronousSettings();
                configure.Add(new ServiceDescriptor(typeof(ICitizensWorkItem),
                    new WorkItemWithDataflow<YxhouseHtmlSynchronousState, WebArticle>(new YxhouseHtmlSynchronousState(yxhouse))));

                var govnews = new GovnewsHtmlSynchronousSettings();
                configure.Add(new ServiceDescriptor(typeof(ICitizensWorkItem),
                    new WorkItemWithDataflow<GovnewsHtmlSynchronousState, WebArticle>(new GovnewsHtmlSynchronousState(govnews))));

                var yxcic = new YxcicHtmlSynchronousSettings();
                configure.Add(new ServiceDescriptor(typeof(ICitizensWorkItem),
                    new WorkItemWithDataflow<YxcicHtmlSynchronousState, WebArticle>(new YxcicHtmlSynchronousState(yxcic))));
            });
        }
        static void ConfigureforSyncHistory()
        {
            CitizensHost.ConfigureServiceProvider((configure) =>
            {
                configure.AddCoreServices();
                var yxhouse = new YxhouseHtmlSynchronousSettings()
                {
                    LeadSources = new LeadSource[]
                    {
                        new LeadSource("yxhouse.newplan.","http://www.jsmlr.gov.cn/gtapp/nrglIndex.action?classID=2c9082b55bfc47ac015bffac4f23005b&type=1"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=first&&currentPage=0"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=1"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=3"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=4"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=5"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=6"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=7"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=8"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=9"),
                        new LeadSource("yxhouse.news.","https://www.yxhouse.com/tbs/morenews.action?typeId=6&&pageIndexName=down&&currentPage=10"),

                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=first&&currentPage=0"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=1"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=2"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=3"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=4"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=5"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=7"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=8"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=9"),
                        new LeadSource("yxhouse.newhouse.","https://www.yxhouse.com/tbs/morebuilding.action?typeId=3&&pageIndexName=down&&currentPage=10"),
                    }
                };
                configure.Add(new ServiceDescriptor(typeof(ICitizensWorkItem),
                    new WorkItemWithDataflow<YxhouseHtmlSynchronousState, WebArticle>(new YxhouseHtmlSynchronousState(yxhouse))));

                var govnews = new GovnewsHtmlSynchronousSettings()
                {
                    LeadSources = new LeadSource[] {
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_2.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_3.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_4.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_5.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_6.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_7.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_8.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_9.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_10.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_11.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_12.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_13.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_15.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_16.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_17.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_18.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_19.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_20.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_21.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_22.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_23.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_24.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_25.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_26.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_27.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_28.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_29.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_30.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_31.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_32.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_33.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_34.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_35.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_36.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_37.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_38.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_39.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_40.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_41.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_42.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_43.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_44.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_45.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_46.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_47.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_48.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_49.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_50.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_51.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_52.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_53.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_54.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_56.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_57.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_58.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_59.shtml"),

                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_60.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_61.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_62.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_63.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_64.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_65.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_66.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_67.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_68.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_69.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_70.shtml"),


                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_71.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_72.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_73.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_74.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_75.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_76.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_77.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_78.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_79.shtml"),
                        new LeadSource("govcn.news.","http://www.yixing.gov.cn/zgyx/zxzx/ywdt/index_80.shtml"),
                    }
                };

                configure.Add(new ServiceDescriptor(typeof(ICitizensWorkItem),
                    new WorkItemWithDataflow<GovnewsHtmlSynchronousState, WebArticle>(new GovnewsHtmlSynchronousState(govnews))));

                var yxcic = new YxcicHtmlSynchronousSettings()
                {
                    LeadSources = new LeadSource[] {
                        new LeadSource("yxcic.","http://www.yxcic.com/tzgg/index.jhtml"),
                        new LeadSource("yxcic.","http://www.yxcic.com/tzgg/index_2.jhtml"),
                        new LeadSource("yxcic.","http://www.yxcic.com/tzgg/index_3.jhtml"),
                     }
                };
                configure.Add(new ServiceDescriptor(typeof(ICitizensWorkItem),
                    new WorkItemWithDataflow<YxcicHtmlSynchronousState, WebArticle>(new YxcicHtmlSynchronousState(yxcic))));
            });

        }
        static void StartSyncHistory()
        {
            ConfigureforSyncHistory();
            var workitems = CitizensHost.GetServices<ICitizensWorkItem>();
            var scheduler = new WebJobScheduler((cancellation) =>
            {
                Parallel.ForEach(workitems, new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 5
                }, (workitem) =>
                {

                    try
                    {                        
                        workitem.Execute();                      
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message, ex);
                    }

                });
            });
            scheduler.Shutdown += (sender, args) =>
            {
                foreach (var workitem in workitems)
                {
                    workitem.Abort();
                }
            };
            scheduler.Start();
            Console.Read();
        }
    }
}
