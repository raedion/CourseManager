using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseManager.Models.Calc
{
    public class CreditExpertCalc : CreditCalc
    {
        public override int ExecCalc(Data data)
        {
            return Value + (data.IsExpert ? data.Credit * (data.IsChecked ? 1 : -1) : 0);
        }
        public override bool CheckRequire()
        {
            return false;
        }
    }
}
