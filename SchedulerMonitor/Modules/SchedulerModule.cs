using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SchedulerMonitor
{
    public class SchedulerModule : NancyModule
    {
        public SchedulerModule()
        {
            //http://127.0.0.1:9988/SchedulersState/CCleanerSkipUAC
            Get["/SchedulersState/{TaskName}"]  = (p) =>
            {
                string result = string.Empty;

                var taskName = p.TaskName.Value;

                List< TaskModel> tasks = TaskSchedulerHelper.getTasks(taskName);

                result = JsonConvert.SerializeObject(tasks);

                return result;
            };

            //http://127.0.0.1:9988/SchedulersState?TaskName=CCleanerSkipUAC
            Get["SchedulersState"] = Post["TaskName"] = (p) =>
            {
                string result = string.Empty;

                var taskName = Request.Query.TaskName.Value;

                List<TaskModel> tasks = TaskSchedulerHelper.getTasks(taskName);

                result = JsonConvert.SerializeObject(tasks);

                return result;
            };
        }


    }
}
