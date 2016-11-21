using Logs;
//using LogsRailWay;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamir.SharpSsh;

namespace MetallurgTrans.SFTP
{
    [Serializable()]
    public class connectSFTP
    {
        public string Host
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public string User
        {
            get;
            set;
        }
        public string PSW
        {
            get;
            set;
        }
    }

    public class SFTPClient
    {
        //Log log = new Log();
        private int eventID = 0; //3
        public int EventID { get { return this.eventID; } set { this.eventID = value; } }
        private Sftp client_sftp;
        private connectSFTP connect_SFTP;
        private string _fromPathsHost;  // Путь для чтения файлов из host
        public string fromPathsHost { get { return this._fromPathsHost; } set { this._fromPathsHost = value; } }
        private string _toPathsHost;    // Путь для записи файлов в host
        public string toPathsHost { get { return this._toPathsHost; } set { this._toPathsHost = value; } }
        private string _FileFiltrHost = "*.*";  // Фильтр файлов из host
        public string FileFiltrHost { get { return this._FileFiltrHost; } set { this._FileFiltrHost = value; } }
        private string _fromDirPath;   // Путь для чтения файлов для загрузки в host
        public string fromDirPath { get { return this._fromDirPath; } set { this._fromDirPath = value; } }
        private string _toTMPDirPath = Path.GetTempPath();     // Путь к временной папки для записи файлов из host для дальнейшей обработки
        public string toTMPDirPath { get { return this._toTMPDirPath; } set { this._toTMPDirPath = value; } }
        private string _toDirPath = null; // Путь для записи файлов из host для постоянного хранения
        public string toDirPath { get { return this._toDirPath; } set { this._toDirPath = value; } }
        private string _FileFiltr = "*.*";  // Фильтр файлов для загрузки в host
        public string FileFiltr { get { return this._FileFiltr; } set { this._FileFiltr = value; } }
        private bool _DeleteFileHost = false; // Признак удаления файлов после копирования из host
        public bool DeleteFileHost { get { return this._DeleteFileHost; } set { this._DeleteFileHost = value; } }
        private bool _DeleteFileDir = false; // Признак удаления файлов после копирования из папки
        public bool DeleteFileDir { get { return this._DeleteFileDir; } set { this._DeleteFileDir = value; } }
        private bool _RewriteFile = false;  // Признак перезаписи файлов в директории приемнике если совподает название файда
        public bool RewriteFile { get { return this._RewriteFile; } set { this._RewriteFile = value; } }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="user"></param>
        /// <param name="psw"></param>
        public SFTPClient(connectSFTP con_sftp)
        {
            this.connect_SFTP = con_sftp;
        }

        /// <summary>
        /// Подключение к серверу SFTP
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {

            this.client_sftp = new Sftp(this.connect_SFTP.Host, this.connect_SFTP.User);
            this.client_sftp.Password = this.connect_SFTP.PSW;
            try
            {
                this.client_sftp.Connect(this.connect_SFTP.Port);
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[SFTPClient.Connect] :Ошибка подключения sftp-клиента, Host {0} (источник: {1}, № {2}, описание:  {3})", this.connect_SFTP.Host, e.Source, e.HResult, e.Message), this.eventID);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Закрыть соединение к серверу SFTP
        /// </summary>
        public void Close()
        {
            this.client_sftp.Close();

        }
        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected bool ExistFile(string file)
        {
            FileInfo fInfo = new FileInfo(file);
            return fInfo.Exists;
        }

        public int CopySFTPFile(string fromFilePaths, string fromFileFiltr, string toTMPDirPath, string toDirPath, bool fromDeleteFile, bool toRewriteFile)
        {

            if (String.IsNullOrWhiteSpace(fromFilePaths) | String.IsNullOrWhiteSpace(toTMPDirPath))
            {
                LogRW.LogError(String.Format("[SFTPClient.CopySFTPFile] :Не определен путь копирования fromFilePaths:{0}, toDirPath:{1}.", fromFilePaths, toDirPath), this.eventID);
                return -1;
            }
            if (toDirPath == toTMPDirPath)
            {
                LogRW.LogError(String.Format("[SFTPClient.CopySFTPFile] :Путь для постоянного хранения перенесённых файлов toDirPath:{0}, совпадает с временным хранилищем для обработки toTMPDirPath:{1}.", toDirPath, toTMPDirPath), this.eventID);
                return -1;
            }
            string[] listfromFile = this.client_sftp.GetFileList(fromFilePaths + "//" + fromFileFiltr);
            if (listfromFile == null | listfromFile.Count() == 0)
            {
                //LogRW.LogInformation(String.Format("На сервере SFTP отсутствуют файлы для копирования"), this.eventID);
                return 0;
            }
            int count = 0;
            int cdel = 0;
            foreach (string file in listfromFile)
            {
                // Если указана папка перенос в постоянное хранилище
                if (!String.IsNullOrWhiteSpace(toDirPath))
                {
                    client_sftp.Get(fromFilePaths + "//" + file, toDirPath + "\\");
                }
                // Переносим во временное хранилище
                if ((toRewriteFile) | (!toRewriteFile & !ExistFile(toTMPDirPath + "\\" + file)))
                {
                    client_sftp.Get(fromFilePaths + "//" + file, toTMPDirPath + "\\");
                    count++;
                }
                // Удалим файлы из host
                if (fromDeleteFile)
                {
                    client_sftp.Rm(fromFilePaths + "//" + file);
                    cdel++;
                }
            }
            string mess = String.Format("На сервере SFTP:{0} найдено {1} файлов, перенесено {2}", connect_SFTP.Host, listfromFile.Count(), count);
            if (fromDeleteFile) { mess = String.Format(mess + ", удаленно {0}", cdel); }
            LogRW.LogWarning(mess, this.eventID);
            return count;
        }
        /// <summary>
        /// Копировать из SFTP в указаную папку
        /// </summary>
        /// <returns></returns>
        public int CopySFTPFile()
        {
            return CopySFTPFile(this._fromPathsHost, this._FileFiltrHost, this._toTMPDirPath, this._toDirPath, this._DeleteFileHost, this._RewriteFile);
        }
        /// <summary>
        /// Полное копирование из SFTP в указаную папку 
        /// </summary>
        /// <param name="fromFilePaths"></param>
        /// <param name="fromFileFiltr"></param>
        /// <param name="toDirPath"></param>
        /// <param name="fromDeleteFile"></param>
        /// <param name="toRewriteFile"></param>
        /// <returns></returns>
        public int CopyToDir(string fromFilePaths, string fromFileFiltr, string toTMPDirPath, string toDirPath, bool fromDeleteFile, bool toRewriteFile)
        {
            int res = 0;
            if (Connect())
            {
                res = CopySFTPFile(fromFilePaths, fromFileFiltr, toTMPDirPath, toDirPath, fromDeleteFile, toRewriteFile);
                Close();
            }
            return res;
        }
        /// <summary>
        /// Полное копирование из SFTP в указаную папку 
        /// </summary>
        /// <returns></returns>
        public int CopyToDir()
        {
            return CopyToDir(this._fromPathsHost, this._FileFiltrHost, this._toTMPDirPath, this._toDirPath, this._DeleteFileHost, this._RewriteFile);
        }
    }
}
