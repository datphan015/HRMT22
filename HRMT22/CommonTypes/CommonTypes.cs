using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMT22.CommonTypes
{
    public enum WorkType : short { Normal = 1, Hard = 2 }
    public enum PositionType : short
    {
        GiamDoc = 1,
        PhoGiamDoc = 2,
        TruongPhong = 4,
        PhoPhong = 8,
        NhanVien = 16
    }
    public enum SalaryType : short
    {
        FullTime = 1, 
        PartTime = 2
    }

}
