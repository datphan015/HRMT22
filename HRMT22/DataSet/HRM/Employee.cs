using System;
using System.Collections.Generic;

namespace HRMT22.DataSet.HRM
{
    internal partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public short WorkType { get; set; }
        public short PositionType { get; set; }
        public short SalaryType { get; set; }
        public decimal Salary { get; set; }
    }
}
