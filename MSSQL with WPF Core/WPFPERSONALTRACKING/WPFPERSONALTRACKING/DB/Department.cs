﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WPFPERSONALTRACKING.DB
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            Positions = new HashSet<Position>();
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
