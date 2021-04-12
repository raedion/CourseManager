using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.Models.Calc
{
    class CreditTotalCalc : CreditCalc
    {
        public CreditTotalCalc(Func<int, bool> func) : base(func)
        {
        }
        public override int ExecCalc(Data data)
        {
            return Value + data.Credit * (data.IsChecked ? 1 : -1);
        }
        public override bool CheckRequire()
        {
            return CalcMethod(Value);
        }
    }
}
