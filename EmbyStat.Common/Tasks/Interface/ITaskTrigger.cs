﻿using System;

namespace EmbyStat.Common.Tasks.Interface
{
    public interface ITaskTrigger
    {
        TaskOptions TaskOptions { get; set; }

        event EventHandler<EventArgs> Triggered;

        void Start(TaskResult lastResult, string taskName, bool isApplicationStartup);
        void Stop();
    }
}
