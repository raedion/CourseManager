using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseManager.Models
{
    public class CreditCalc : NotificationObject
    {
        public static int ExecTotal(int baseNum, Data data)
        {
            return baseNum + data.Credit * (data.IsChecked ? 1 : -1);
        }
        public static int ExecExpert(int baseNum, Data data)
        {
            return baseNum + (data.IsExpert ? data.Credit * (data.IsChecked ? 1 : -1) : 0);
        }
        public static int ExecInternational(int baseNum, Data data)
        {
            return baseNum + (data.IsInternational ? data.Credit * (data.IsChecked ? 1 : -1) : 0);
        }
    }
}
