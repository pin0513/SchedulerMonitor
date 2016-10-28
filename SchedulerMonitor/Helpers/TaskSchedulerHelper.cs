using Microsoft.Win32.TaskScheduler;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMonitor
{

    


    public static class TaskSchedulerHelper
    {
        private static List<Microsoft.Win32.TaskScheduler.Task> GetReadyAndRunningTasks(
        TaskFolder folder,
        List<Microsoft.Win32.TaskScheduler.Task> taskCollection = null)
            {
                if (taskCollection == null)
                {
                    taskCollection = new List<Microsoft.Win32.TaskScheduler.Task>();
                }

                var tasks = folder.Tasks;

                foreach (Microsoft.Win32.TaskScheduler.Task task in tasks)
                {
                    if (task.State != TaskState.Disabled)
                    {
                        taskCollection.Add(task);
                    }
                }

                foreach (TaskFolder subFolder in folder.SubFolders)
                {
                    GetReadyAndRunningTasks(subFolder, taskCollection);
                }

                return taskCollection;
            }


        public static List<TaskModel> getTasks(string taskName = null)
        {
            var result = new List<TaskModel>();

            var user = ConfigurationManager.AppSettings["user"] ?? string.Empty;
            var passwd = ConfigurationManager.AppSettings["passwd"] ?? string.Empty;
            
            TaskService ts = null;


            if (string.IsNullOrEmpty(user))
                ts = new TaskService("");
            else
                ts = new TaskService("localhost", user, passwd);

            List<Microsoft.Win32.TaskScheduler.Task> taskList
                    = GetReadyAndRunningTasks(ts.GetFolder(ConfigurationManager.AppSettings["TaskSchedulerPath"]));

            foreach (Microsoft.Win32.TaskScheduler.Task task in taskList)
            {
                result.Add(new TaskModel { TaskName = task.Name, TaskState = task.State, LastRunTime = task.LastRunTime, LastTaskResult = task.LastTaskResult, NextRunTime = task.NextRunTime});
            }

            if (string.IsNullOrEmpty(taskName) == false)
                result = result.Where(a => a.TaskName.Equals(taskName, StringComparison.CurrentCultureIgnoreCase)).ToList();

            return result;
        }
          
    }
}
