using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HRMT22.Models
{
    public class WorkLog
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }

        internal bool Validate(out string error)
        {
            error = string.Empty;
            bool validate = true;
            if (this.EmployeeId == 0)
            {
                error += string.IsNullOrWhiteSpace(error) ? "EmployeeId is required." : ", EmployeeId is required.";
                validate = false;
            }
            else
            {
                using (var context = new DataSet.HRM.HRMContext())
                {
                    if (context.Employees.Count(c => c.Id == this.EmployeeId) == 0)
                    {
                        {
                            error += string.IsNullOrWhiteSpace(error) ? "EmployeeId is invalid." : ", EmployeeId is invalid.";
                            validate = false;
                        }
                    }
                }                
            }
            if (this.Checkin == DateTime.MinValue || this.Checkin.Hour < 7)
            {
                validate = false;
                error += string.IsNullOrWhiteSpace(error) ? "Checkin value is invalid." : ", Checkin value is invalid.";
            }
            if (this.Checkout == DateTime.MinValue)
            {
                validate = false;
                error += string.IsNullOrWhiteSpace(error) ? "Checkin value is invalid." : ", Checkin value is invalid.";
            }
            if(this.Checkout <= this.Checkin || this.Checkout.Hour > 17)
            {
                validate = false;
                error += string.IsNullOrWhiteSpace(error) ? "Checkin or checkout is invalid." : ", Checkin or checkout is invalid.";
            }

            // Thêm các kiểm tra khác tùy thuộc vào yêu cầu của ứng dụng
            return validate;
        }

        internal void CopyTo(DataSet.HRM.WorkLog row)
        {
            row.Id = Id;
            row.EmployeeId = EmployeeId;
            row.Checkin = Checkin;
            row.Checkout = Checkout;
        }

        internal void CopyFrom(DataSet.HRM.WorkLog row)
        {
            this.Id = row.Id;
            this.EmployeeId = row.EmployeeId;
            this.Checkin = row.Checkin;
            this.Checkout = row.Checkout;
        }

    }
}
