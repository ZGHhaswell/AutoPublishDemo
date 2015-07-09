/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：AppTreeNodes.cs
// 功能描述：
// 
// 创建时间： 7/9/2015 10:43:36 PM
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPublish.Common.Data
{
    public class AppTreeNodes : ObservableCollection<AppTreeNode>
    {
        private IList<AppTreeNode> _treeNodes;

        public bool IsAllowRun
        {
            get
            {
                return _treeNodes.Count(node => node.IsAutoRun) > 0;
            }
        }

        public event Action PropertiesChangedInCollection;

        public AppTreeNodes(IList<AppTreeNode> treeNodes)
            : base(treeNodes)
        {
            _treeNodes = treeNodes;

            foreach (var appTreeNode in treeNodes)
            {
                appTreeNode.PropertyChanged += AppTreeNodeOnPropertyChanged;
            }
        }

        private void AppTreeNodeOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsAutoRun")
            {
                if (PropertiesChangedInCollection != null)
                {
                    PropertiesChangedInCollection();
                }
            }
        }
    }
}
