/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：SelectPublishDirViewModel.cs
// 功能描述：
// 
// 创建时间： 7/9/2015 9:56:31 PM
//
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using AutoPublish.Common.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using ProjectPublisher.MD5;

namespace ProjectPublisher.WizardViewModels
{
    public class SelectPublishDirViewModel : NotificationObject
    {
        /// <summary>
        /// 加载的文件夹路径
        /// </summary>
        private string _fullPath;
        public string FullPath
        {
            get
            {
                return _fullPath;
            }
            set
            {
                _fullPath = value;
                RaisePropertyChanged("FullPath");
            }
        }

        private string _treeDatas;

        public string TreeDatas
        {
            get
            {
                return _treeDatas;
            }
            set
            {
                _treeDatas = value;
                RaisePropertyChanged("TreeDatas", "IsAllowNext");
            }
        }

        public ICommand NextCommand { get; set; }

        public Action<AppUpdateConfig> NextEvent;

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
                return !string.IsNullOrEmpty(TreeDatas);
            }
        }

        private AppUpdateConfig _appUpdateConfig;

        /// <summary>
        /// 加载Command
        /// </summary>
        public ICommand LoadCommand { get; set; }

        private void LoadCommandExecute()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FullPath = dialog.SelectedPath;

                //解析文件夹下的路径
                var treeNode = ListFiles(FullPath);

                var stringBuilder = new StringBuilder();
                ToString(treeNode, stringBuilder);
                TreeDatas = stringBuilder.ToString();

                _appUpdateConfig = new AppUpdateConfig()
                {
                    RootPath = "",
                    RootDirectory = treeNode as DirectoryTreeNode
                };
            }
        }

        public string RepeatString(string str, int n)
        {
            char[] arr = str.ToCharArray();
            char[] arrDest = new char[arr.Length * n];

            for (int i = 0; i < n; i++)
            {
                Buffer.BlockCopy(arr, 0, arrDest, i * arr.Length * 2, arr.Length * 2);
            }
            return new string(arrDest);
        }

        private void ToString(TreeNodeBase treeNode, StringBuilder stringBuilder, string tab = "      ", int count = 0)
        {
            if (treeNode != null)
            {
                stringBuilder.AppendLine(RepeatString(tab, count) + treeNode.Name);
                ++count;
                foreach (var treeNodeBase in treeNode.Children)
                {
                    ToString(treeNodeBase, stringBuilder, tab, count);
                }
            }
        }

        private TreeNodeBase ListFiles(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);
            var directoryTreeNode = new DirectoryTreeNode() { Name = directoryInfo.Name };
            if (directoryInfo.Exists)
            {
                var files = directoryInfo.GetFiles("*.*");
                foreach (var fileInfo in files)
                {
                    FileTreeNode fileNode = null;
                    if (fileInfo.Extension.Contains("exe"))
                    {
                        fileNode = new AppTreeNode();
                    }
                    else
                    {
                        fileNode = new FileTreeNode();

                    }
                    fileNode.Name = fileInfo.Name;
                    fileNode.Md5 = MD5Utils.GetMD5(fileInfo.FullName);
                    directoryTreeNode.Children.Add(fileNode);
                }
                var directorys = directoryInfo.GetDirectories();
                foreach (var info in directorys)
                {
                    var directoryNode = ListFiles(info.FullName);
                    directoryTreeNode.Children.Add(directoryNode);
                }
            }
            return directoryTreeNode;
        }

        public SelectPublishDirViewModel()
        {
            LoadCommand = new DelegateCommand(LoadCommandExecute);
            NextCommand = new DelegateCommand(NextCommandExecute);
        }
    }
}
