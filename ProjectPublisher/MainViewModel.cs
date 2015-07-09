/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：MainViewModel.cs
// 功能描述：
// 
// 创建时间： 6/30/2015 9:31:32 PM
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using ProjectPublisher.WizardViewModels;

namespace ProjectPublisher
{
    public class MainViewModel : NotificationObject
    {
        private Visibility _selectPublishVis;

        public Visibility SelectPublishDirVis
        {
            get
            {
                return _selectPublishVis;
            }
            set
            {
                _selectPublishVis = value;
                RaisePropertyChanged("SelectPublishDirVis");
            }
        }

        private SelectPublishDirViewModel _selectPublishDirViewModel;

        public SelectPublishDirViewModel SelectPublishDirViewModel
        {
            get
            {
                return _selectPublishDirViewModel;
            }
            set
            {
                _selectPublishDirViewModel = value;
                RaisePropertyChanged("SelectPublishDirViewModel");
            }
        }

        private SelectAppViewModel _selectAppViewModel;

        public SelectAppViewModel SelectAppViewModel
        {
            get
            {
                return _selectAppViewModel;
            }
            set
            {
                _selectAppViewModel = value;
                RaisePropertyChanged("SelectAppViewModel");
            }
        }

        private Visibility _selectAppVis;

        public Visibility SelectAppVis
        {
            get
            {
                return _selectAppVis;
            }
            set
            {
                _selectAppVis = value;
                RaisePropertyChanged("SelectAppVis");
            }
        }

        private ExportConfigViewModel  _exportConfigViewModel;

        public ExportConfigViewModel ExportConfigViewModel
        {
            get
            {
                return _exportConfigViewModel;
            }
            set
            {
                _exportConfigViewModel = value;
                RaisePropertyChanged("ExportConfigViewModel");
            }
        }

        private Visibility _exportConfigVis;

        public Visibility ExportConfigVis
        {
            get
            {
                return _exportConfigVis;
            }
            set
            {
                _exportConfigVis = value;
                RaisePropertyChanged("ExportConfigVis");
            }
        }

        
        
        public Dictionary<object, Action<Visibility>> Switchers { get; set; }

        public void Show(object target)
        {
            foreach (var switcher in Switchers)
            {
                if (switcher.Key.Equals(target))
                {
                    switcher.Value(Visibility.Visible);
                }
                else
                {
                    switcher.Value(Visibility.Hidden);
                }
            }
        }

        public Window OwnerWindow { get; set; }

        public MainViewModel()
        {
            Switchers = new Dictionary<object, Action<Visibility>>();

            SelectPublishDirViewModel = new SelectPublishDirViewModel();

            SelectPublishDirViewModel.NextEvent += config =>
            {
                Show(SelectAppViewModel);
                SelectAppViewModel.Init(config);
            };

            SelectAppViewModel = new SelectAppViewModel();

            SelectAppViewModel.PreviousEvent += () =>
            {
                Show(SelectPublishDirViewModel);
            };

            SelectAppViewModel.NextEvent += config =>
            {
                Show(ExportConfigViewModel);
                ExportConfigViewModel.Init(config);
            };

            ExportConfigViewModel = new ExportConfigViewModel();

            ExportConfigViewModel.PreviousEvent += () =>
            {
                Show(SelectAppViewModel);
            };
            ExportConfigViewModel.NextEvent += config =>
            {
                OwnerWindow.Close();
            };


            Switchers.Add(SelectPublishDirViewModel, vis => SelectPublishDirVis = vis);

            Switchers.Add(SelectAppViewModel, vis => SelectAppVis = vis);

            Switchers.Add(ExportConfigViewModel, vis => ExportConfigVis = vis);

            Show(SelectPublishDirViewModel);
        }
    }
}
