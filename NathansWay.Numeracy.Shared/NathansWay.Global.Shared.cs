using System;

namespace NathansWay.Numeracy.Shared
{
    public class SharedGlobal : ISharedGlobal
    {
        
        #region PrivateVariables
        protected string folderNameLibrary;
        protected string folderNameImageData;
        protected string versionFileName;
        protected string databaseName;
        protected string iOSDatabasePath;
        protected string winDatabasePath;
        protected string androidDatabasePath;        
        #endregion

        #region Contructor
        public SharedGlobal()
        {
        }
        #endregion
      
        #region ConstantVariables
        public string GS__FolderNameLibrary
        {
            get { return folderNameLibrary; }
            set { folderNameLibrary = value; }
        }
        public string GS__FolderNameImageData
        {
            get { return folderNameImageData; }
            set { folderNameImageData = value; }
        }
        public string GS__VersionFileName 
        {
            get { return versionFileName; }
            set { versionFileName = value; }
        }
        public string GS__DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }
        public string GS__iOSDatabasePath
        {
            get { return iOSDatabasePath; }
            set { iOSDatabasePath = value; }
        }
        public string GS__WinDatabasePath
        {
            get { return winDatabasePath; }
            set { winDatabasePath = value; }
        }
        public string GS__AndroidDatabasePath
        {
            get { return androidDatabasePath; }
            set { androidDatabasePath = value; }
        }
        #endregion   

    }   

}

