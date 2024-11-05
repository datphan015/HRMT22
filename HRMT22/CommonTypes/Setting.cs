using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMT22.CommonTypes
{
    public static class Setting
    {
        public static decimal NormalMoney { get; set; } = 0;
        public static decimal HardMoney { get; set; } = 500_000;
        public static decimal GDMoney { get; set; } = 7_000_000;
        public static decimal PGDMoney { get; set; } = 5_000_000;
        public static decimal TPMoney { get; set; } = 3_000_000;
        public static decimal PPMoney { get; set; } = 1_000_000;
        public static decimal NVMoney { get; set; } = 0;
        public static decimal MaxMoneyAllowance { get; set; } = 10_000_000;
    }
}
