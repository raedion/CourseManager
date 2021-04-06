using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using CourseManager.Models;
using System.Collections.ObjectModel;

namespace CourseManager.ViewModels
{
    public class TimeTableViewModel : ViewModel
    {
        // Some useful code snippets for ViewModel are defined as l*(llcom, llcomn, lvcomm, lsprop, etc...).

        // This method would be called from View, when ContentRendered event was raised.
        public void Initialize()
        {
        }
        public TimeTableViewModel()
        {

        }
        public TimeTableViewModel(IEnumerable<Data> datas)
        {
            //TimeTableList = new ObservableCollection<string[]>();
            //for (int i = 0; i < 5; i++) {
            //    TimeTableList.Add(new string[6]);
            //    for (int j = 0; j < 6; j++) {
            //        TimeTableList[i][j] = "";
            //    }
            //}

            foreach (Data e in datas) {
                DataAnalyzer analyzer = new DataAnalyzer();
                var analyzedData = analyzer.DayAnalyze(e.Day);
                analyzedData.ForEach(dayData => {
                    //TimeTableList.Add(new TimeTableData(dayData[0], dayData[1], e.SubjectName));
                    if (dayData != null)
                        //TimeTableList[dayData[0]][dayData[1] - 1] = e.SubjectName;
                        switch (dayData[0]) {
                        case 0:
                            MondaySubj[dayData[1] -1 ] = e.SubjectName;
                            break;
                        case 1:
                            TuesdaySubj[dayData[1] - 1] = e.SubjectName;
                            break;
                        case 2:
                            WednesdaySubj[dayData[1] - 1] = e.SubjectName;
                            break;
                        case 3:
                            ThursdaySubj[dayData[1] - 1] = e.SubjectName;
                            break;
                        case 4:
                            FridaySubj[dayData[1] - 1] = e.SubjectName;
                            break;
                        default:
                            break;
                        }
                });
            }
        }

        private ObservableCollection<string> _mondaySubj = new ObservableCollection<string>() {"", "", "", "", "", "" };

        public ObservableCollection<string> MondaySubj {
            get { return _mondaySubj; }
            set {
                if (_mondaySubj == value)
                    return;
                _mondaySubj = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _tuesdaySubj = new ObservableCollection<string>(){"", "", "", "", "", "" };

        public ObservableCollection<string> TuesdaySubj {
            get { return _tuesdaySubj; }
            set {
                if (_tuesdaySubj == value)
                    return;
                _tuesdaySubj = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _wednesdaySubj = new ObservableCollection<string>(){"", "", "", "", "", "" };

        public ObservableCollection<string> WednesdaySubj {
            get { return _wednesdaySubj; }
            set {
                if (_wednesdaySubj == value)
                    return;
                _wednesdaySubj = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _thursdaySubj = new ObservableCollection<string>(){"", "", "", "", "", "" };

        public ObservableCollection<string> ThursdaySubj {
            get { return _thursdaySubj; }
            set {
                if (_thursdaySubj == value)
                    return;
                _thursdaySubj = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _fridaySubj = new ObservableCollection<string>(){"", "", "", "", "", "" };

        public ObservableCollection<string> FridaySubj {
            get { return _fridaySubj; }
            set {
                if (_fridaySubj == value)
                    return;
                _fridaySubj = value;
                RaisePropertyChanged();
            }
        }



    }
    //public class TimeTableData
    //{
    //    public TimeTableData(int weekOfDay, int period, string content)
    //    {
    //        WeekOfDay = weekOfDay;
    //        Period = period;
    //        Content = content;
    //    }
    //    public int WeekOfDay { get; private set; } 
    //    public int Period { get; private set; }
    //    public string Content { get; private set; }
    //}
}
