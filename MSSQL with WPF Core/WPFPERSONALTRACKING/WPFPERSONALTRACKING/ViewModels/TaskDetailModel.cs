﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPERSONALTRACKING.ViewModels
{
    public class TaskDetailModel
    {
        public int Id{ get; set; }
        public int EmployeeId { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime? TaskDeliveryDate{ get; set; }
        public int TaskState { get; set; }
        public string TaskTitle { get; set; }
        public string TaskContent { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int UserNumber { get; set; }
        public string StateName { get; set; }
    }
}
