using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

public class TaskModel
{
    public string TaskName { get; set; }
    public DateTime LastRunTime { get; set; }
    public DateTime NextRunTime { get; set; }

    public TaskState TaskState { get; set; }
    public int LastTaskResult { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public TaskState TaskStateDisplay { get { return TaskState; } }
}