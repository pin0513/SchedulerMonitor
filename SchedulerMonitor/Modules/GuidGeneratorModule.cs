using Nancy;
using System;

namespace SchedulerMonitor
{
    public class GuidGeneratorModule : NancyModule
    {
        public GuidGeneratorModule()
        {
            Get["/Guid"] = (p) =>
            {
                return Guid.NewGuid().ToString();
            };
        }
    }
}
