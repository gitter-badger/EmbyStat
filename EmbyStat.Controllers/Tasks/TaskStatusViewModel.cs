﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmbyStat.Controllers.Tasks
{
    public class TaskStatusViewModel
    {
        public string Id { get; set; }
        public int State { get; set; }
        public double? CurrentProgress { get; set; }
    }
}