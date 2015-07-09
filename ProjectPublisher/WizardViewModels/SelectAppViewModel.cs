/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：SelectAppViewModel.cs
// 功能描述：
// 
// 创建时间： 7/9/2015 10:24:04 PM
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Windows.Input;
using AutoPublish.Common.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace ProjectPublisher.WizardViewModels
{
    public class SelectAppViewModel : NotificationObject
    {
        private AppTreeNodes _apps;

        public AppTreeNodes Apps
        {
            get
            {
                return _apps;
            }
            set
            {
                _apps = value;
                RaisePropertyChanged("Apps");
            }
        }

        public Action<AppUpdateConfig> NextEvent;

        public ICommand NextCommand { get; set; }

        private void NextCommandExecute()
        {
            if (NextEvent != null)
            {
                NextEvent(_appUpdateConfig);
            }
        }

        public bool IsAllowNext
        {
            get
            {
                if (Apps == null)
                    return false;
                return Apps.IsAllowRun;
            }
        }

        public Action PreviousEvent;

        public ICommand PreviousCommand { get; set; }

        private void PreviousCommandExecute()
        {
            if (PreviousEvent != null)
            {
                PreviousEvent();
            }
        }

        private AppUpdateConfig _appUpdateConfig;

        public void Init(AppUpdateConfig appUpdateConfig)
        {
            _appUpdateConfig = appUpdateConfig;

            Apps = new AppTreeNodes(GetAppNodes(appUpdateConfig));

            Apps.PropertiesChangedInCollection += AppsOnPropertiesChangedInCollection;
        }

        private void AppsOnPropertiesChangedInCollection()
        {
            RaisePropertyChanged("IsAllowNext");
        }

        private IList<AppTreeNode> GetAppNodes(AppUpdateConfig appUpdateConfig)
        {
            var list = new List<AppTreeNode>();
            Search(list, appUpdateConfig.RootDirectory);
            return list;
        }

        private void Search(List<AppTreeNode> list, TreeNodeBase treeNodeBase)
        {
            foreach (var nodeBase in treeNodeBase.Children)
            {
                if (nodeBase is AppTreeNode)
                {
                    list.Add(nodeBase as AppTreeNode);
                }
                else if (nodeBase is DirectoryTreeNode)
                {
                    Search(list, nodeBase);
                }
            }
        }

        public SelectAppViewModel()
        {
            NextCommand = new DelegateCommand(NextCommandExecute);
            PreviousCommand = new DelegateCommand(PreviousCommandExecute);
        }

    }
}
