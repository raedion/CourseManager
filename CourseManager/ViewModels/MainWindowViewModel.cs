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
        private DataManager _dataManager;
        private const string JSON_FILE_NAME = "list.json";
        public void Initialize()
        {
        }
        public MainWindowViewModel()
        {
            _dataManager = new DataManager();
            DataList = _dataManager.ReadDataFromFile(JSON_FILE_NAME);
        }

        private ObservableCollection<Data> _dataList;// = new ObservableCollection<Data>();

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


        private string _totalCredit;

        public string TotalCredit {
            get { return _totalCredit; }
            set { 
                if (_totalCredit == value)
                    return;
                _totalCredit = value;
                RaisePropertyChanged();
            }
        }


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

    }
}
