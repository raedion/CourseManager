using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Livet;

namespace CourseManager.Models
{
    public class Data : NotificationObject
    {
        public Data()
        {

        }
        public Data(int id, string section, string subjectName, string day,
            string teacherName, int credit, int grade, Enums.EExpert isExpert, bool isInternational)
        {
            Id = id;
            Section = section;
            Day = day;
            SubjectName = subjectName;
            TeacherName = teacherName;
            Credit = credit;
            Grade = grade;
            IsExpert = isExpert;
            IsInternational = isInternational;
        }
        public void OutputData()
        {
                System.Diagnostics.Debug.WriteLine($"{SubjectName}, {Id}, {Section}");
        }
        public Action<bool> IsCheckedAction;
        public int Id { get;  set; }
        public string Section { get;  set; }
        public string SubjectName { get;  set; }
        public string Day { get; set; }
        public string TeacherName { get;  set; }
        public int Credit { get;  set; }
        public int Grade { get;  set; }
        public Enums.EExpert IsExpert { get;  set; }
        public bool IsInternational { get;  set; }

        public bool IsChecked {
            get { return _isChecked; }
            set { 
                if (_isChecked == value)
                    return;
                _isChecked = value;
                IsCheckedAction.Invoke(value);
                RaisePropertyChanged();
            }
        }
        private bool _isChecked = false;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
