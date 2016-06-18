using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace QDF.Compress.Rar
{
    public class WinRarHelper
    {
        #region 属性
        private string _winRarPath;

        /// <summary>
        /// WinRAR安装路径，可以自己设置，默认读取系统注册表
        /// </summary>
        public string WinRarPath
        {
            get
            {
                return string.IsNullOrEmpty(_winRarPath) ? GetWinRarInstallPath() : _winRarPath;
            }
            set
            {
                _winRarPath = value;
            }
        }
        #endregion

        /// <summary>
        /// 解压到某个文件夹中
        /// </summary>
        /// <param name="rarFilePath">rar文件全路径</param>
        /// <param name="unRarPath">解压到哪个文件夹</param>
        /// <param name="password">解压密码</param>
        /// <param name="isOverride">是否覆盖</param>
        public void UnRar(string rarFilePath, string unRarPath, string password = null, bool isOverride = false)
        {
            if (IsSetUpWinRar())
            {
                throw new ArgumentNullException("WinRAR未安装");
            }
            RunCmd(string.Format("x{0} -o{1} {2} {3}", (password == null ? "" : " -p" + password), (isOverride ? "+" : "-"), rarFilePath, unRarPath));
        }

        /// <summary>
        /// 压缩文件或者文件夹为压缩包
        /// </summary>
        /// <param name="filePath">需要压缩的文件/文件夹全路径</param>
        /// <param name="saveFilePath">压缩文件保存全路径</param>
        /// <param name="isOverride">是否覆盖</param>
        /// <param name="password">压缩文件密码</param>
        public void Rar(string filePath, string saveFilePath, bool isOverride = false, string password = null)
        {
            if (IsSetUpWinRar())
            {
                throw new ArgumentNullException("WinRAR未安装");
            }
            RunCmd(string.Format("a{0} -o{1} -ep2 -r {2} {3}", (password == null ? "" : " -p" + password), (isOverride ? "+" : "-"), saveFilePath, filePath));
        }

        /// <summary>
        /// 解压是否安装了WinRAR程序
        /// </summary>
        /// <returns></returns>
        public bool IsSetUpWinRar()
        {
            if (!string.IsNullOrEmpty(WinRarPath))
            {
                return File.Exists(WinRarPath);
            }
            var inistallPath = GetWinRarInstallPath();
            if (string.IsNullOrEmpty(inistallPath))
            {
                return false;
            }
            WinRarPath = inistallPath;
            return true;
        }

        /// <summary>
        /// 从注册表中获取WinRAR的安装路径
        /// </summary>
        /// <returns></returns>
        public string GetWinRarInstallPath()
        {
            var openKey = @"SOFTWARE\Wow6432Node\WinRAR";//64位注册表
            if (IntPtr.Size == 4)
            {
                openKey = @"SOFTWARE\WinRAR";//32位注册表路径
            }
            var appPath = Registry.LocalMachine.OpenSubKey(openKey);
            if (appPath != null)
            {
                // WinRAR安装具体路径
                string path = appPath.GetValue("exe32").ToString();
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return null;
        }

        /// <summary>
        /// 执行rar内部命令
        /// </summary>
        /// <param name="cmd">要执行的命令</param>
        public void RunCmd(string cmd)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = WinRarPath;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = cmd;
                p.Start();
                p.WaitForExit();
            }
        } 
    }
}