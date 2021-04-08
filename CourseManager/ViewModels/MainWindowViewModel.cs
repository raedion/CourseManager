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
using CourseManager.Views;

namespace CourseManager.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        // Some useful code snippets for ViewModel are defined as l*(llcom, llcomn, lvcomm, lsprop, etc...).
        /// <summary>
        /// 読み込むJSONファイル名
        /// </summary>
        private const string JSON_FILE_NAME = "list.json";
        public void Initialize()
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(JSON_FILE_NAME, Encoding.GetEncoding("utf-8"));
            string allLine = sr.ReadToEnd();
            sr.Close();

            ObservableCollection<Data> datas = new ObservableCollection<Data>();
            Newtonsoft.Json.JsonReader jsonTextReader = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(allLine));
            jsonTextReader.SupportMultipleContent = true;

            while (true)
            {
                if (!jsonTextReader.Read())
                {
                    break;
                }
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                Data data = serializer.Deserialize<Data>(jsonTextReader);
                data.IsCheckedAction += CalcCredit;

                datas.Add(data);
            }
            DataList = datas;
        }

        private ObservableCollection<Data> _dataList;

        public ObservableCollection<Data> DataList {
            get { return _dataList; }
            set { 
                if (_dataList == value)
                    return;
                _dataList = value;
                RaisePropertyChanged();
            }
        }

        private ViewModelCommand _ShowTimeTableCommand;

        public ViewModelCommand ShowTimeTableCommand {
            get {
                if (_ShowTimeTableCommand == null) {
                    _ShowTimeTableCommand = new ViewModelCommand(ShowTimeTable);
                }
                return _ShowTimeTableCommand;
            }
        }


        public string TotalCredit {
            get { return _totalCredit; }
            set { 
                if (_totalCredit == value)
                    return;
                _totalCredit = value;
                RaisePropertyChanged();
            }
        }
        private string _totalCredit = "0";

        public string ExpertCredit
        {
            get { return _expertCredit; }
            set { 
                if (_expertCredit == value)
                    return;
                _expertCredit = value;
                RaisePropertyChanged();
            }
        }
        private string _expertCredit = "0";

        public string InternationalCredit
        {
            get
            { return _internationalCredit; }
            set
            { 
                if (_internationalCredit == value)
                    return;
                _internationalCredit = value;
                RaisePropertyChanged();
            }
        }
        private string _internationalCredit = "0";

        private ViewModelCommand _DoubleClickCommand;

        public ViewModelCommand DoubleClickCommand {
            get {
                if (_DoubleClickCommand == null) {
                    _DoubleClickCommand = new ViewModelCommand(DoubleClick);
                }
                return _DoubleClickCommand;
            }
        }

        public void DoubleClick()
        {
            SelectedItem.IsChecked = !SelectedItem.IsChecked;
        }

        public Data SelectedItem {
            get { return _selectedItem; }
            set { 
                if (_selectedItem == value)
                    return;
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
        private Data _selectedItem;


        public void ShowTimeTable()
        {
            Messenger.Raise(new TransitionMessage(typeof(TimeTableView), 
                new TimeTableViewModel(DataList.Where(e => e.IsChecked)), TransitionMode.NewOrActive));
        }


        private ViewModelCommand _SingleClickCommand;

        public ViewModelCommand SingleClickCommand
        {
            get
            {
                if (_SingleClickCommand == null)
                {
                    _SingleClickCommand = new ViewModelCommand(SingleClick);
                }
                return _SingleClickCommand;
            }
        }

        private void CalcCredit(bool val)
        {
            TotalCredit = CreditCalc.ExecTotal(int.Parse(TotalCredit), SelectedItem).ToString();
            ExpertCredit = CreditCalc.ExecExpert(int.Parse(ExpertCredit), SelectedItem).ToString();
            InternationalCredit = CreditCalc.ExecInternational(int.Parse(InternationalCredit), SelectedItem).ToString();
        }
        public void SingleClick()
        {
            TotalCredit = CreditCalc.ExecTotal(int.Parse(TotalCredit), SelectedItem).ToString();
        }

    }
}
