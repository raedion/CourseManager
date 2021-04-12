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
        private CreditCalc _totalCredit = new CreditTotalCalc(x => x >= 30);

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
        private CreditCalc _expertCredit = new CreditExpertCalc(x => x >= 22);

        public bool IsFillRequireExpert { get; set; }
        public int SelectedCredit
        {
            get { return _SelectedCredit.Value; }
            set
            {
                if (_SelectedCredit.Value == value)
                    return;
                _SelectedCredit.Value = value;
                IsFillSelected = _SelectedCredit.CheckRequire();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFillSelected));
            }
        }
        private CreditCalc _SelectedCredit = new CreditExpertCalc(x => x >= 0);
        public bool IsFillSelected { get; private set; }

        public int SelectedRequiredMajorBasic
        {
            get { return _SelectedRequiredMajorBasic.Value; }
            set
            {
                if (_SelectedRequiredMajorBasic.Value == value)
                    return;
                _SelectedRequiredMajorBasic.Value = value;
                IsFillSelectedRequiredMajorBasic = _SelectedRequiredMajorBasic.CheckRequire();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFillSelectedRequiredMajorBasic));
            }
        }
        private CreditCalc _SelectedRequiredMajorBasic = new CreditExpertCalc(x => x >= 4);
        public bool IsFillSelectedRequiredMajorBasic { get; private set; }

        public int SelectedMajorBasicCredit
        {
            get { return _SelectedMajorBasicCredit.Value; }
            set
            {
                if (_SelectedMajorBasicCredit.Value == value)
                    return;
                _SelectedMajorBasicCredit.Value = value;
                IsFillSelectedMajorBasic = _SelectedMajorBasicCredit.CheckRequire();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFillSelectedMajorBasic));
            }
        }
        private CreditCalc _SelectedMajorBasicCredit = new CreditExpertCalc(x => x >= 4);
        public bool IsFillSelectedMajorBasic { get; private set; }

        public int RequiredMajorBasicCredit
        {
            get
            { return _requiredMajorBasicCredit.Value; }
            set
            { 
                if (_requiredMajorBasicCredit.Value == value)
                    return;
                _requiredMajorBasicCredit.Value = value;
                IsFillRequiredMajorBasicCredit = _requiredMajorBasicCredit.CheckRequire();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsFillRequiredMajorBasicCredit));
            }
        }
        private CreditCalc _requiredMajorBasicCredit = new CreditExpertCalc(x => x >= 4);
        public bool IsFillRequiredMajorBasicCredit { get; set; }


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
        private CreditCalc _internationalCredit = new CreditInternationalCalc(x => x >= 2);
        public bool IsFillRequireInternational { get; set; }


        private int _advancedCredit;

        public int AdvancedCredit
        {
            get
            { return _advancedCredit; }
            set
            { 
                if (_advancedCredit == value)
                    return;
                TotalCredit += value - _advancedCredit;
                _advancedCredit = value;
                RaisePropertyChanged();
            }
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
            if (SelectedItem.IsExpert is Enums.EExpert.RequiredMajorBasic ||
                SelectedItem.IsExpert is Enums.EExpert.SelectedMajorBasic ||
                SelectedItem.IsExpert is Enums.EExpert.SelectedRequiredMajorBasic1 ||
                SelectedItem.IsExpert is Enums.EExpert.SelectedRequiredMajorBasic2)
            ExpertCredit = _expertCredit.ExecCalc(SelectedItem);
            InternationalCredit = _internationalCredit.ExecCalc(SelectedItem);
            if (SelectedItem.IsExpert is Enums.EExpert.Selected)
            {
                SelectedCredit = _SelectedCredit.ExecCalc(SelectedItem);
            }
            else if (SelectedItem.IsExpert is Enums.EExpert.SelectedRequiredMajorBasic1)
            {
                SelectedRequiredMajorBasic = _SelectedRequiredMajorBasic.ExecCalc(SelectedItem);
            }
            else if (SelectedItem.IsExpert is Enums.EExpert.SelectedRequiredMajorBasic2)
            {
                SelectedRequiredMajorBasic = _SelectedRequiredMajorBasic.ExecCalc(SelectedItem);
            }
            else if (SelectedItem.IsExpert is Enums.EExpert.SelectedMajorBasic)
            {
                SelectedMajorBasicCredit = _SelectedMajorBasicCredit.ExecCalc(SelectedItem);
            }
            else if (SelectedItem.IsExpert is Enums.EExpert.RequiredMajorBasic)
            {
                RequiredMajorBasicCredit = _requiredMajorBasicCredit.ExecCalc(SelectedItem);
            }
        }
    }
}
