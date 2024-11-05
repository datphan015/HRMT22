using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HRMT22.Lib
{
    public class WorkLog
    {

        public bool Add(Models.WorkLog workLog)
        {
            bool status = false;
            var row = new DataSet.HRM.WorkLog();
            workLog.CopyTo(row);
            using (var context = new DataSet.HRM.HRMContext())
            {
                context.WorkLogs.Add(row);
                status = context.SaveChanges() > 0;
            }
            if (status)
            {
                workLog.Id = row.Id;
            }
            return status;
        }

        public bool Load(int id)
        {
            bool status = false;
            DataSet.HRM.WorkLog row;
            Models.WorkLog workLog = null;
            using (var context = new DataSet.HRM.HRMContext())
            {
                row = context.WorkLogs.FirstOrDefault(x => x.Id == id);
                status = row != null;
            }
            if (status)
            {
                workLog = new Models.WorkLog();
                workLog.CopyFrom(row);
            }
            return status;
        }
    }
}
