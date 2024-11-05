using System;
using System.Collections.Generic;

namespace HRMT22.DataSet.HRM
{
    internal partial class WorkLog
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
    }
}
