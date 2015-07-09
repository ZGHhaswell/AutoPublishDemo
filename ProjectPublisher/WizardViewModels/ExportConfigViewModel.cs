/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：ExportConfigViewModel.cs
// 功能描述：
// 
// 创建时间： 7/9/2015 11:04:47 PM
//
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;
using AutoPublish.Common.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace ProjectPublisher.WizardViewModels
{
    public class ExportConfigViewModel : NotificationObject
    {
        private string _outFullPath;

        public string OutFullPath
        {
            get
            {
                return _outFullPath;
            }
            set
            {
                _outFullPath = value;
                RaisePropertyChanged("OutFullPath");
            }
        }


        /// <summary>
        /// 导出Command
        /// </summary>
        public ICommand ExportCommand { get; set; }

        private void ExportCommandExecute()
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OutFullPath = Path.Combine(dialog.SelectedPath, "AppUpdate.config");

                    if (File.Exists(OutFullPath))
                        File.Delete(OutFullPath);
                    using (var fileStream = new FileStream(OutFullPath, FileMode.CreateNew))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(AppUpdateConfig));
                        xmlSerializer.Serialize(fileStream, _appUpdateConfig);
                    }

                    Result = "生成成功！";
                }
            }
            catch (Exception)
            {
                Result = "生成失败！";
            }
            
        }

        private string _result;

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                RaisePropertyChanged("Result");
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

        public Action<AppUpdateConfig> NextEvent;

        public ICommand NextCommand { get; set; }

        private void NextCommandExecute()
        {
            if (NextEvent != null)
            {
                NextEvent(_appUpdateConfig);
            }
        }

        private AppUpdateConfig _appUpdateConfig;

        public void Init(AppUpdateConfig appUpdateConfig)
        {
            _appUpdateConfig = appUpdateConfig;
        }

        public ExportConfigViewModel()
        {
            ExportCommand = new DelegateCommand(ExportCommandExecute);
            PreviousCommand = new DelegateCommand(PreviousCommandExecute);
            NextCommand = new DelegateCommand(NextCommandExecute);
        }
    }
}
