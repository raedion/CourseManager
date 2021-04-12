using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseManager.Models
{
    public class Enums : NotificationObject
    {
        public enum EExpert
        {
            RequiredMajorBasic,
            SelectedRequiredMajorBasic1,
            SelectedRequiredMajorBasic2,
            SelectedMajorBasic,
            Selected,
            Except,
        }
    }
}
