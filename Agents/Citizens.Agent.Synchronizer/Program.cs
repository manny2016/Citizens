

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
        static void Main(string[] args)
        {
            CitizensHost.ConfigureServiceProvider((configure) =>
            {
                configure.AddCoreServices();
                var settings = new YxhouseHtmlSynchronousSettings();                
                configure.Add(new ServiceDescriptor(typeof(ICitizensWorkItem),
                    new WorkItemWithDataflow<YxhouseHtmlSynchronousState,HtmlContext>(new YxhouseHtmlSynchronousState(settings))));
            });
            StartAuto();
        }
        static void StartAuto()
        {

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
                            var offset = 60D * 15;//5 mins                        
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
    }
}
