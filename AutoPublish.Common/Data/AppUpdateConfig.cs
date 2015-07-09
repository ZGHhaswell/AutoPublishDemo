/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：AppUpdateConfig.cs
// 功能描述：
// 
// 创建时间： 7/9/2015 9:41:52 PM
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoPublish.Common.Data
{
    [XmlType]
    public class AppUpdateConfig
    {
        public string RootPath { get; set; }

        public DirectoryTreeNode RootDirectory { get; set; }

        public AppUpdateConfig()
        {
            
        }
    }
}
