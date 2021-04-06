using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace CourseManager.Models
{
    public enum EDayOfWeek
    {
        Monday = 0, 
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6,
        Nothing = 7
    }
    public class DataAnalyzer : NotificationObject
    {
        public List<int[]> DayAnalyze(string target)
        {
            List<int[]> retVal = new List<int[]>();
            var splitedVal = target.Split(',');
            foreach (string e in splitedVal) {
                retVal.Add(SpecifyTime(e));
            }
            return retVal;
        }
        private int[] SpecifyTime(string val)
        {
            EDayOfWeek dow = EDayOfWeek.Nothing;
            switch (val[0]) {
            case '月':
                dow = EDayOfWeek.Monday;
                break;
            case '火':
                dow = EDayOfWeek.Tuesday;
                break;
            case '水':
                dow = EDayOfWeek.Wednesday;
                break;
            case '木':
                dow = EDayOfWeek.Thursday;
                break;
            case '金':
                dow = EDayOfWeek.Friday;
                break;
            case '土':
                dow = EDayOfWeek.Saturday;
                break;
            case '日':
                dow = EDayOfWeek.Sunday;
                break;
            default:
                break;
            }
            if (dow == EDayOfWeek.Nothing) {
                return null;
            }
            return new int[2] { (int)dow, int.Parse(val[1].ToString()) };
        }
    }
}
