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
using CourseManager.Models.Calc;

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


        public int TotalCredit {
            get { return _totalCredit.Value; }
            set { 
                if (_totalCredit.Value == value)
                    return;
                _totalCredit.Value = value;
                IsFillRequireTotal = _totalCredit.CheckRequire();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFillRequireTotal));
            }
        }
        private CreditCalc _totalCredit = new CreditTotalCalc();

        public bool IsFillRequireTotal { get; private set; }

        public int ExpertCredit 
        {
            get { return _expertCredit.Value; }
            set { 
                if (_expertCredit.Value == value)
                    return;
                _expertCredit.Value = value;
                IsFillRequireExpert = _expertCredit.CheckRequire();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFillRequireExpert));
            }
        }
        private CreditCalc _expertCredit = new CreditExpertCalc();

        public bool IsFillRequireExpert { get; set; }

        public int InternationalCredit
        {
            get { return _internationalCredit.Value; }
            set
            { 
                if (_internationalCredit.Value == value)
                    return;
                _internationalCredit.Value = value;
                IsFillRequireInternational = _internationalCredit.CheckRequire();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFillRequireInternational));
            }
        }
        private CreditCalc _internationalCredit = new CreditInternationalCalc();
        public bool IsFillRequireInternational { get; set; }

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

        private void CalcCredit(bool val)
        {
            TotalCredit = _totalCredit.ExecCalc(SelectedItem);
            ExpertCredit = _expertCredit.ExecCalc(SelectedItem);
            InternationalCredit = _internationalCredit.ExecCalc(SelectedItem);
        }
    }
}
