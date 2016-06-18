using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace QDF.Ftp
{
    public class FTPHelper
    {
        #region 字段
        private string _ftpUri;
        private readonly string _ftpUserId;
        private readonly string _ftpServerIP;
        private readonly string _ftpPassword;
        private string _ftpRemotePath;
        #endregion

        /// <summary>  
        /// 连接FTP服务器
        /// </summary>  
        /// <param name="ftpServerIP">FTP连接地址</param>  
        /// <param name="ftpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>  
        /// <param name="ftpUserID">用户名</param>  
        /// <param name="ftpPassword">密码</param>  
        public FTPHelper(string ftpServerIP, string ftpRemotePath, string ftpUserID, string ftpPassword)
        {
            _ftpServerIP = ftpServerIP;
            _ftpRemotePath = ftpRemotePath;
            _ftpUserId = ftpUserID;
            _ftpPassword = ftpPassword;
            this._ftpServerIP = ftpServerIP;
            _ftpUri = "ftp://" + ftpServerIP + "/" + _ftpRemotePath + "/";
        }

        /// <summary>  
        /// 上传  
        /// </summary>   
        public void Upload(string filename)
        {
            var fileInf = new FileInfo(filename);
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpUri + fileInf.Name));
            reqFTP.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.KeepAlive = false;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            const int buffLength = 2048;
            var buff = new byte[buffLength];
            var fs = fileInf.OpenRead();
            try
            {
                var strm = reqFTP.GetRequestStream();
                var contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 下载  
        /// </summary>   
        public void Download(string filePath, string fileName)
        {
            try
            {
                var outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpUri + fileName));
                reqFTP.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                var response = (FtpWebResponse)reqFTP.GetResponse();
                var ftpStream = response.GetResponseStream();
                var cl = response.ContentLength;
                const int bufferSize = 2048;
                var buffer = new byte[bufferSize];
                var readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 删除文件  
        /// </summary>  
        public void Delete(string fileName)
        {
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpUri + fileName));
                reqFTP.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.KeepAlive = false;
                var response = (FtpWebResponse)reqFTP.GetResponse();
                var datastream = response.GetResponseStream();
                if (datastream != null)
                {
                    var sr = new StreamReader(datastream);
                    sr.ReadToEnd();
                    sr.Close();
                }

                if (datastream != null) datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 获取当前目录下明细(包含文件和文件夹)  
        /// </summary>  
        public string[] GetFilesDetailList()
        {
            try
            {
                var result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpUri));
                ftp.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                var response = ftp.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var line = reader.ReadLine();
                line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 获取FTP文件列表(包括文件夹)
        /// </summary>   
        private string[] GetAllList(string url)
        {
            var list = new List<string>();
            var req = (FtpWebRequest)WebRequest.Create(new Uri(url));
            req.Credentials = new NetworkCredential(_ftpPassword, _ftpPassword);
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            req.UseBinary = true;
            req.UsePassive = true;
            try
            {
                using (var res = (FtpWebResponse)req.GetResponse())
                {
                    using (var sr = new StreamReader(res.GetResponseStream()))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            list.Add(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return list.ToArray();
        }

        /// <summary>  
        /// 获取当前目录下文件列表(不包括文件夹)  
        /// </summary>  
        public string[] GetFileList(string url)
        {
            var result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_ftpPassword, _ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                var response = reqFTP.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var line = reader.ReadLine();
                while (line != null)
                {

                    if (line.IndexOf("<DIR>") == -1)
                    {
                        result.Append(Regex.Match(line, @"[\S]+ [\S]+", RegexOptions.IgnoreCase).Value.Split(' ')[1]);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result.ToString().Split('\n');
        }

        /// <summary>  
        /// 判断当前目录下指定的文件是否存在  
        /// </summary>  
        /// <param name="RemoteFileName">远程文件名</param>  
        public bool FileExist(string RemoteFileName)
        {
            var fileList = GetFileList("*.*");
            foreach (var str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>  
        /// 创建文件夹  
        /// </summary>   
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpUri + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
                var response = (FtpWebResponse)reqFTP.GetResponse();
                var ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>  
        /// 获取指定文件大小  
        /// </summary>  
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_ftpUri + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
                var response = (FtpWebResponse)reqFTP.GetResponse();
                var ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;
                if (ftpStream != null) ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            { }
            return fileSize;
        }

        /// <summary>  
        /// 更改文件名  
        /// </summary> 
        public void ReName(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest) FtpWebRequest.Create(new Uri(_ftpUri + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_ftpUserId, _ftpPassword);
                var response = (FtpWebResponse) reqFTP.GetResponse();
                var ftpStream = response.GetResponseStream();
                if (ftpStream != null) ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>  
        /// 移动文件  
        /// </summary>  
        public void MovieFile(string currentFilename, string newDirectory)
        {
            ReName(currentFilename, newDirectory);
        }

        /// <summary>  
        /// 切换当前目录  
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="isRoot">true:绝对路径 false:相对路径</param>   
        public void GotoDirectory(string directoryName, bool isRoot)
        {
            if (isRoot)
            {
                _ftpRemotePath = directoryName;
            }
            else
            {
                _ftpRemotePath += directoryName + "/";
            }
            _ftpUri = "ftp://" + _ftpServerIP + "/" + _ftpRemotePath + "/";
        } 
    }
}