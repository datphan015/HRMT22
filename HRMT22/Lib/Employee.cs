using HRMT22.CommonTypes;
using HRMT22.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMT22.Lib
{
    public class Employee 
    {        

        public bool Add(Models.Employee employee, out string error)
        { 
            bool status = employee.Validate(out error);
            if (status)
            {
                var row = new DataSet.HRM.Employee();
                employee.CopyTo(row);
                try
                {
                    using (var context = new DataSet.HRM.HRMContext())
                    {
                        status = context != null;
                        if (status)
                        {
                            context.Employees.Add(row);
                            status = context.SaveChanges() > 0;
                        }
                        else
                        {
                            error = "Connection string is not exist";
                        }
                    }
                }
                catch (Exception ex)
                {
                    error = !string.IsNullOrWhiteSpace( ex.Message) ? ex.Message : ex.InnerException.Message ;
                    status = false;
                }
                
                if (status)
                {
                    employee.Id = row.Id;
                }
            }
            
            return status;
        }

        public Models.Employee? Load(int employeeId)
        {
            bool status = false;
            DataSet.HRM.Employee row = null;
            Models.Employee employee = null;

            using (var context = new DataSet.HRM.HRMContext())
            {
                status = context != null;
                if (status)
                {
                    row = context.Employees.FirstOrDefault(x => x.Id == employeeId);
                }
                status = row != null;
            }
            if (status)
            {
                employee = new Models.Employee();
                employee.CopyFrom(row);
            }
            return employee;
        }

        public decimal Allowance(CommonTypes.WorkType workType, CommonTypes.PositionType positionType)
        {
            decimal allowance = 0;
            bool status = false;

            allowance = workType switch
            {
                WorkType.Normal => CommonTypes.Setting.NormalMoney,
                WorkType.Hard => CommonTypes.Setting.HardMoney,
                _ => 0
            };

            allowance += (positionType & PositionType.GiamDoc) > 0 ? CommonTypes.Setting.GDMoney : 0;
            allowance += (positionType & PositionType.PhoGiamDoc) > 0 ? CommonTypes.Setting.PGDMoney : 0;
            allowance += (positionType & PositionType.TruongPhong) > 0 ? CommonTypes.Setting.TPMoney : 0;
            allowance += (positionType & PositionType.PhoPhong) > 0 ? CommonTypes.Setting.PPMoney : 0;
            allowance += (positionType & PositionType.NhanVien) > 0 ? CommonTypes.Setting.NVMoney : 0;
            if (allowance > CommonTypes.Setting.MaxMoneyAllowance)
                allowance = CommonTypes.Setting.MaxMoneyAllowance;
            return allowance;
        }

        public int WorkMinutes(int employeeId, int month, int year)
        {
            int minute = 0;
            bool status = false;
            string cmd = string.Format("SELECT SUM(DATEDIFF(MINUTE,a.checkin,a.checkout)) AS minute  " +
                "FROM workLogs a " +
                "WHERE a.employeeId = {0} AND MONTH(a.checkin) = {1} and YEAR(a.checkin) = {2}",
                employeeId, month, year);

            using (var context = new DataSet.HRM.HRMContext())
            {
                 var rows = context.View.FromSqlRaw(cmd).ToList();
                status = rows != null;
                if (status && rows.Count == 1)
                {
                    minute = rows[0].Minute;
                }
            }
            
            return minute;
        }

        public decimal? SalaryOfMonth(int employeeId, int month, int year)
        {
            decimal? salary = 0;
            bool status = false;
            DataSet.HRM.Employee row = null;
            Models.Employee employee = null;

            using (var context = new DataSet.HRM.HRMContext())
            {
                row = context.Employees.FirstOrDefault(x => x.Id == employeeId);
                status = row != null;
            }
            if (status)
            {
                employee = new Models.Employee();
                employee.CopyFrom(row);
                salary = employee.SalaryType switch
                {
                    SalaryType.FullTime => employee.Salary.Value / (WorkTimes.WorkDays * 8 * 60) * WorkMinutes(employeeId, month, year)
                            + Allowance(employee.WorkType.Value, employee.PositionType.Value) + WorkTimes.HoliDays * employee.Salary / (WorkTimes.WorkDays),
                    SalaryType.PartTime => employee.Salary / (60) * WorkMinutes(employeeId, month, year),
                    _ => 0
                };
                
            }            
            return salary;
        }
    }
}
