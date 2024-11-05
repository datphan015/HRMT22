using HRMT22.CommonTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMT22.Models
{
    public class Employee 
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public HRMT22.CommonTypes.WorkType? WorkType { get; set; }
        public HRMT22.CommonTypes.PositionType? PositionType { get; set; }
        public HRMT22.CommonTypes.SalaryType? SalaryType { get; set; }
        public decimal? Salary { get; set; }

        public Employee()
        { }

        public Employee(string? name, string? address, string? phone, WorkType? workType,
            PositionType? positionType, SalaryType? salaryType, decimal? salary)
        {
            Name = name;
            Address = address;
            Phone = phone;
            WorkType = workType;
            PositionType = positionType;
            SalaryType = salaryType;
            Salary = salary;
        }

        internal bool Validate(out string error)
        {
            error = string.Empty;
            bool validate = true;
            if (string.IsNullOrEmpty(this.Name))
            {
                error += "Employee name is required.";
                validate = false;
            }
            else
            {
                if (this.Name.Length > 500)
                {
                    error += "Employee name is invalid.";
                    validate = false;
                }
            }
            if (Enum.IsDefined(typeof(CommonTypes.WorkType), this.WorkType.ToString()) == false)
            {
                validate = false;
                error += "Employee workType is invalid.";
            }

            // Thêm các kiểm tra khác tùy thuộc vào yêu cầu của ứng dụng
            return validate;
        }

        internal void CopyTo( DataSet.HRM.Employee row)
        {
            row.Id = Id == null? 0 : Id.Value;
            row.Name = Name;
            row.Address = Address;
            row.Phone = Phone;
            row.WorkType = (short)WorkType;
            row.PositionType = (short)PositionType;
            row.SalaryType = (short)SalaryType;
            row.Salary = ( Salary == null? 0 : Salary.Value);
        }

        internal void CopyFrom(DataSet.HRM.Employee row)
        {
            this.Id = row.Id;
            this.Name = row.Name;
            this.Address = row.Address;
            this.Phone = row.Phone;
            this.WorkType =( HRMT22.CommonTypes.WorkType ?) row.WorkType;
            this.PositionType = (HRMT22.CommonTypes.PositionType?) row.PositionType;
            this.SalaryType = (HRMT22.CommonTypes.SalaryType?) row.SalaryType;
            this.Salary = row.Salary;
        }

    }
}
