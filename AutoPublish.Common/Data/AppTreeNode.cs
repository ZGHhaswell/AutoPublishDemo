/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：AppTreeNode.cs
// 功能描述：
// 
// 创建时间： 7/9/2015 9:50:45 PM
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPublish.Common.Data
{
    public class AppTreeNode : FileTreeNode
    {
        private bool _isAutoRun;
        public bool IsAutoRun
        {
            get
            {
                return _isAutoRun;
            }
            set
            {
                _isAutoRun = value;
                RaisePropertyChanged("IsAutoRun");
            }
        }
    }
}
