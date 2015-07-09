/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：FileTreeNode.cs
// 功能描述：
// 
// 创建时间： 7/1/2015 12:28:19 AM
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPublish.Common.Data
{
    public class FileTreeNode : TreeNodeBase
    {
        public string Md5 { get; set; }
    }
}
