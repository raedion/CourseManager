using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseManager.Models
{
    public abstract class CreditCalc : NotificationObject
    {
        /// <summary>
        /// 単位計算に関するメソッド
        /// </summary>
        /// <param name="baseNum">加算対象の数字</param>
        /// <param name="data">加算するときに読み取る対象データ</param>
        /// <returns></returns>
        public abstract int ExecCalc(Data data);
        /// <summary>
        /// 卒業案件を満たしているかを確認するためのメソッド
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckRequire();
        /// <summary>
        /// Viewに表示する単位数
        /// </summary>
        public int Value { get; set; }
    }
}
