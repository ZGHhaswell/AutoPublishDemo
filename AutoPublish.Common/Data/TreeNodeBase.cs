/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：TreeNodeBase.cs
// 功能描述：
// 
// 创建时间： 7/1/2015 12:26:53 AM
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Practices.Prism.ViewModel;

namespace AutoPublish.Common.Data
{
    [XmlType]
    [XmlInclude(typeof(DirectoryTreeNode)), XmlInclude(typeof(FileTreeNode)), XmlInclude(typeof(AppTreeNode))]
    public class TreeNodeBase : NotificationObject
    {
        public string Name { get; set; }

        public List<TreeNodeBase> Children { get; set; }

        public TreeNodeBase()
        {
            Children = new List<TreeNodeBase>();
        }
    }
}
