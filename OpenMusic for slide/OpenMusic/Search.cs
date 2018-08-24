using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMusic
{
    /// <summary>
    /// .Net/C# 实现磁盘目录文件搜索的工具类 (搜索事件)
    /// </summary>
    public class Search
    {
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="SourceDirectory">被搜索的源目录</param>
        /// <param name="DirectoryPatterns">源目录下面的所有子目录的搜索匹配模式</param>
        /// <param name="FilePatterns">源目录下面的所有文件的搜索匹配模式</param>
        /// <param name="DestinationDirectory">存储相对路径</param>
        private void Searching(string SourceDirectory, string DirectoryPatterns, string FilePatterns, string DestinationDirectory)
        {
            this._DirectoryPatterns = DirectoryPatterns;
            this._FilePatterns = FilePatterns;

            string[] Patterns = this._DirectoryPatterns.Split(';');
            string[] patterns = this._FilePatterns.Split(';');

            this._DirectoryID = 0;
            this._FileID = 0;

            DestinationDirectory += (DestinationDirectory.EndsWith(@"\") ? "" : @"\");

            if (this._DirectoriesCount == 0) //处理源目录的当前的文件
            {
                this._DirectoriesCount++;
                this._DirectoryID++;
                this._CurrentDirectoryName = SourceDirectory;

                if (SourceDirectory.EndsWith(@"\"))
                {
                    this._CurrentDirectoryName = SourceDirectory.Substring(0, SourceDirectory.Length - 1);
                }

                this._ParentDirectoryName = this._CurrentDirectoryName.Substring(this._CurrentDirectoryName.LastIndexOf(@"\") + 1);
                DestinationDirectory += this._ParentDirectoryName + @"\";
                this._CurrentDestinationDirectoryName = DestinationDirectory;

                if (this.AddSearchedDirectory(this._CurrentDirectoryName))
                {
                    this._DirectoryUID++;
                }

                if (SearchedDirectory != null) //触发一次找到源目录的事件
                {
                    OnSearchedDirectory(SourceDirectory, this._DirectoriesCount, this._DirectoryID, this._CurrentDestinationDirectoryName);
                }
                foreach (string p in patterns)
                {
                    foreach (string f in System.IO.Directory.GetFiles(SourceDirectory, p.Trim()))
                    {
                        this._FilesCount++;
                        this._FileID++;

                        if (this.AddSearchedFile(f))
                        {
                            this._FileUID++;
                        }

                        if (SearchedFile != null)
                        {
                            OnSearchedFile(f, DirectoryUID, FileUID, this._DirectoryID, this.FileID, this._CurrentDestinationDirectoryName);
                        }
                        if (this._Cancel != CancelActions.No)
                        {
                            break;
                        }
                    }
                    if (this._Cancel != CancelActions.No)
                    {
                        break;
                    }
                }
            }
            if (this._Cancel != CancelActions.AllDirectories)
            {
                this._FileID = 0;
                this._DirectoryID = 0;

                foreach (string P in Patterns)
                {
                    if (this._Cancel != CancelActions.AllDirectories)
                    {
                        foreach (string d in System.IO.Directory.GetDirectories(SourceDirectory, P.Trim()))
                        {
                            if (this._Cancel != CancelActions.AllDirectories)
                            {
                                this._DirectoriesCount++;
                                this._DirectoryID++;

                                this._CurrentDirectoryName = d + (d.EndsWith(@"\") ? "" : @"\");
                                this._CurrentDestinationDirectoryName = DestinationDirectory + d.Substring(d.LastIndexOf(@"\") + 1) + @"\";

                                if (this.AddSearchedDirectory(this._CurrentDirectoryName))
                                {
                                    this._DirectoryUID++;
                                }

                                if (SearchedDirectory != null)
                                {
                                    OnSearchedDirectory(d, DirectoryUID, this._DirectoryID, this._CurrentDestinationDirectoryName);
                                }
                                if (this._Cancel == CancelActions.CurrentDirectory)
                                {
                                    this._Cancel = CancelActions.No;
                                    continue;
                                }
                                else if (this._Cancel == CancelActions.AllDirectories)
                                {
                                    break;
                                }
                                if (this._Cancel != CancelActions.AllDirectories)
                                    foreach (string p in patterns)
                                    {
                                        foreach (string f in System.IO.Directory.GetFiles(d, p.Trim()))
                                        {
                                            this._FilesCount++;
                                            this._FileID++;

                                            if (this.AddSearchedFile(f))
                                            {
                                                this._FileUID++;
                                            }

                                            if (SearchedFile != null)
                                            {
                                                OnSearchedFile(f, DirectoryUID, FileUID, this._DirectoryID, this.FileID, this._CurrentDestinationDirectoryName);
                                            }
                                            if (this._Cancel != CancelActions.No)
                                            {
                                                break;
                                            }
                                        }
                                        if (this._Cancel != CancelActions.No)
                                        {
                                            break;
                                        }
                                    }
                                if (this._Cancel == CancelActions.CurrentDirectory)
                                {
                                    this._Cancel = CancelActions.No;
                                    continue;
                                }
                                else if (this._Cancel == CancelActions.AllDirectories)
                                {
                                    break;
                                }
                                if (this._Cancel != CancelActions.AllDirectories)
                                {
                                    this._Nest++;
                                    Searching(d, this._DirectoryPatterns, this._FilePatterns, this._CurrentDestinationDirectoryName);
                                    this._Nest--;
                                }

                            }
                        }
                    }
                    if (this._Cancel == CancelActions.CurrentDirectory)
                    {
                        this._Cancel = CancelActions.No;
                        continue;
                    }
                    else if (this._Cancel == CancelActions.AllDirectories)
                    {
                        break;
                    }

                }
            }

            if ((this._Nest == 0))
            {
                if (Searched != null)
                {
                    OnSearched(this.DirectoryUID, this.FileUID, this._CurrentDestinationDirectoryName);
                }
            }
        }

        public void Searching(string SourceDirectory)
        {
            Searching(SourceDirectory, "*", "*", this._DestinationDirectory);
        }

        public void Searching(string SourceDirectory, string FilePatterns)
        {
            Searching(SourceDirectory, "*", FilePatterns, this._DestinationDirectory);
        }

        public void Searching(string SourceDirectory, string DirectoryPatterns, string FilePatterns)
        {
            Searching(SourceDirectory, DirectoryPatterns, FilePatterns, this._DestinationDirectory);
        }

        private void OnSearched(int DirectoryUID, int FileUID, string CurrentDestinationDirectoryName)
        {
            SearchEventArgs sea = new SearchEventArgs(DirectoryUID, FileUID, CurrentDestinationDirectoryName);
            Searched(this, sea);
        }

        private void OnSearchedFile(string f, int DirectoryUID, int FileUID, int DirectoryID, int FileID, string CurrentDestinationDirectoryName)
        {
            SearchEventArgs sea = new SearchEventArgs(f, DirectoryUID, FileUID, DirectoryID, FileID, CurrentDestinationDirectoryName);
            //new SearchEventHandler(SearchedFile).BeginInvoke(this,sea,new System.AsyncCallback(this.SearchedFileCallBack),sea);
            SearchedFile(this, sea);
        }

        private void SearchedFileCallBack(System.IAsyncResult iar)
        {
            throw new System.NotImplementedException();
        }

        private void SearchedDirectoryCallBack(System.IAsyncResult iar)
        {
            throw new System.NotImplementedException();
        }

        private void OnSearchedDirectory(string d, int DirectoryUID, int DirectoryID, string CurrentDestinationDirectoryName)
        {
            SearchEventArgs sea = new SearchEventArgs(d, DirectoryUID, DirectoryID, CurrentDestinationDirectoryName);
            //new SearchEventHandler(SearchedDirectory).BeginInvoke(this,sea,new System.AsyncCallback(this.SearchedDirectoryCallBack),sea);
            SearchedDirectory(this, sea);
        }

        public delegate void SearchEventHandler(Search Sender, SearchEventArgs e);

        private int _Nest; //递归嵌套层数
        public event SearchEventHandler SearchedDirectory; //"搜索到某个目录" 的事件
        public event SearchEventHandler SearchedFile; //"搜索到某个文件" 的事件
        public event SearchEventHandler Searched; //"搜索完毕" 的事件

        private int _DirectoriesCount; //搜索目录的次数
        private int _FilesCount; //搜索文件的次数

        private string _FilePatterns = "*"; //文件名匹配模式
        private string _DirectoryPatterns = "*"; //目录名匹配模式
        private CancelActions _Cancel; //取消

        private string _CurrentDirectoryName; //搜索的当前目录名
        private string _FileName = null;
        private int _FileID; //搜索的文件在当前目录的 ID
        private int _DirectoryID; //搜索的目录在当前目录的父目录的 ID
        private string _CurrentDestinationDirectoryName; //存储相对路径目录,可由于复制目录

        private string _DestinationDirectory;
        private string _ParentDirectoryName;

        private int _FileUID; //本次搜索的"文件的唯一 ID"
        private int _DirectoryUID; //本次搜索的"目录的唯一 ID"
        private System.Collections.ArrayList _SearchedDirectories; //存储已搜索的目录
        private System.Collections.ArrayList _SearchedFiles; //存储已搜索的文件

        public System.Collections.ArrayList SearchedDirectories
        {
            get
            {
                //SearchedDirectories is ReadOnly
                return System.Collections.ArrayList.ReadOnly(this._SearchedDirectories);
            }
        }
        public System.Collections.ArrayList SearchedFiles
        {
            get
            {
                //SearchedFiles is ReadOnly
                return System.Collections.ArrayList.ReadOnly(this._SearchedFiles);
            }
        }

        public int DirectoriesCount
        {
            get
            {
                return _DirectoriesCount;
            }
        }

        public int FilesCount
        {
            get
            {
                return _FilesCount;
            }
        }

        public string DirectoriesPatterns
        {
            get
            {
                return _DirectoryPatterns;
            }
            set
            {
                _DirectoryPatterns = value;
            }
        }

        public string DestinationDirectory
        {
            get
            {
                return _DestinationDirectory;
            }
            set
            {
                _DestinationDirectory = value;
            }
        }

        public string CurrentDirectoryName
        {
            get
            {
                return _CurrentDirectoryName + (_CurrentDirectoryName.EndsWith(@"\") ? "" : @"\");
            }
            set
            {
                _CurrentDirectoryName = value;
            }
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }
        }

        public string ParentDirectoryName
        {
            get
            {
                return _ParentDirectoryName;
            }
        }

        /// <summary>
        /// 根据源目录的目录结构信息存储相对路径信息
        /// </summary>
        public string CurrentDestinationDirectoryName
        {
            get
            {
                return _CurrentDestinationDirectoryName + (_CurrentDestinationDirectoryName.EndsWith(@"\") ? "" : @"\");
            }
        }

        public int FileID
        {
            get
            {
                return _FileID;
            }
        }

        public int DirectoryID
        {
            get
            {
                return _DirectoryID;
            }
        }

        public CancelActions Cancel
        {
            get
            {
                return _Cancel;
            }
            set
            {
                _Cancel = value;
            }
        }

        public int DirectoryUID
        {
            get
            {
                return _DirectoryUID;
            }
        }

        public int FileUID
        {
            get
            {
                return _FileUID;
            }
        }

        public string FilesPatterns
        {
            get
            {
                return _FilePatterns;
            }
            set
            {
                _FilePatterns = value;
            }
        }

        private bool AddSearchedDirectory(string Key)
        {
            if (this._SearchedDirectories == null)
            {
                this._SearchedDirectories = new System.Collections.ArrayList();

            }
            bool b = this._SearchedDirectories.Contains(Key);
            if (!b)
            {
                this._SearchedDirectories.Add(Key);
            }
            return !b;
        }

        private bool AddSearchedFile(string Key)
        {
            if (this._SearchedFiles == null)
            {
                this._SearchedFiles = new System.Collections.ArrayList();
            }
            bool b = this._SearchedFiles.Contains(Key);
            if (!b)
            {
                this._SearchedFiles.Add(Key);
            }
            return !b;
        }

    }

    public enum CancelActions
    {
        No //不取消,继续
         ,
        CurrentDirectory //只取消当前目录
         ,
        AllDirectories //取消后面的所有搜索
    }

    public class SearchEventArgs : System.EventArgs
    {
        private int _FileID;
        private int _DirectoryID;
        private string _CurrentDirectoryName;
        private string _CurrentDestinationDirectoryName;
        private string _FileName;
        private int _DirectoriesCount = 0;
        private int _FileUID;
        private int _DirectoryUID;
        private int _FilesCount = 0;

        public int FilesCount
        {
            get
            {
                return _FilesCount;
            }
        }

        public int DirectoriesCount
        {
            get
            {
                return _DirectoriesCount;
            }
        }

        public string CurrentDirectoryName
        {
            get
            {
                return _CurrentDirectoryName + (_CurrentDirectoryName.EndsWith(@"\") ? "" : @"\");
            }
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }
        }

        public string ParentDirectoryName
        {
            get
            {
                return _CurrentDirectoryName.Substring(_CurrentDirectoryName.LastIndexOf(@"\") + 1);
            }
        }

        public string CurrentDestinationDirectoryName
        {
            get
            {
                return _CurrentDestinationDirectoryName + (_CurrentDestinationDirectoryName.EndsWith(@"\") ? "" : @"\");
            }
        }

        public int FileUID
        {
            get
            {
                return _FileUID;
            }
        }

        public int DirectoryUID
        {
            get
            {
                return _DirectoryUID;
            }
        }

        public int FileID
        {
            get
            {
                return _FileID;
            }
        }

        public int DirectoryID
        {
            get
            {
                return _DirectoryID;
            }
        }

        internal SearchEventArgs(int DirectoryUID, int FileUID, string CurrentDestinationDirectoryName)
        {
            this._FileUID = FileUID;
            this._DirectoryUID = DirectoryUID;
            this._CurrentDestinationDirectoryName = CurrentDestinationDirectoryName;
        }

        internal SearchEventArgs(string FileName, int DirectoryUID, int FileUID, int DirectoryID, int FileID, string CurrentDestinationDirectoryName)
        {
            this._FileName = System.IO.Path.GetFileName(FileName);
            this._CurrentDirectoryName = System.IO.Path.GetDirectoryName(FileName);
            this._FileUID = FileUID;
            this._DirectoryUID = DirectoryUID;
            this._DirectoryID = DirectoryID;
            this._FileID = FileID;
            this._CurrentDestinationDirectoryName = CurrentDestinationDirectoryName;
        }

        internal SearchEventArgs(string DirectoryName, int DirectoryUID, int DirectoryID, string CurrentDestinationDirectoryName)
        {
            this._CurrentDirectoryName = DirectoryName;
            this._DirectoryUID = DirectoryUID;
            this._DirectoryID = DirectoryID;
            this._CurrentDestinationDirectoryName = CurrentDestinationDirectoryName;
        }
    }
}
