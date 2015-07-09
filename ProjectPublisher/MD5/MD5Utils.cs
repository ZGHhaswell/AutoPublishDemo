/*----------------------------------------------------------------
// Copyright (C) 2013 ZGH
// 版权所有。
//
// 文件名：MD5Utils.cs
// 功能描述：
// 
// 创建时间： 6/30/2015 9:26:55 PM
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPublisher.MD5
{
    public static class MD5Utils
    {
        public static string GetMD5(string filePath)
        {
            string strResult = "";
            string strHashData = "";
 
            byte[] arrbytHashValue;
            System.IO.FileStream oFileStream = null;
 
            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                       new System.Security.Cryptography.MD5CryptoServiceProvider();
 
            try
            {
                oFileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open,
                      System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);//计算指定Stream 对象的哈希值
                oFileStream.Close();
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                strHashData = System.BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (System.Exception ex)
            {

            }
 
            return strResult;
        }
    }
}
