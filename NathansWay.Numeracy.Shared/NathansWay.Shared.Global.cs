using System;

namespace NathansWay.Shared.Utilities
{
    public class SharedGlobal : ISharedGlobal
    {
        
        #region PrivateVariables

		protected string rootPath;
        protected string folderNameLibrary;
        protected string folderNameImageData;
        protected string documentsPath;
        protected string versionFileName;
        protected string databaseName;
        protected string iOSDatabasePath;
        protected string winDatabasePath;
        protected string androidDatabasePath;   
        protected string fulldbPath;

        public readonly _iOSDimensions GS__iOSDimensions;

        #endregion

        #region Contructors

        public SharedGlobal()
        {
            this.GS__iOSDimensions = new _iOSDimensions();
        }

        #endregion
      
        #region Public Properties

		public string GS__RootAppPath
		{
			get { return rootPath; }
			set { rootPath = value; }
		}
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
        public string GS__DocumentsPath
        {
            get { return documentsPath; }
            set { documentsPath = value; }
        }
        public string GS__FullDbPath
        {
            get { return fulldbPath; }
            set { fulldbPath = value; }
        }
        #endregion   

        #region Structs

        // iOS dimensions
        // Heights and widths of the initial number text box.
        // 
        public struct _iOSDimensions
        {
            public float _fCtrlNumberTextHeight;
            public float _fNumberPickerHeight;
            public float _fTxtNumberHeight;
            public float _fUpDownButtonHeight;
            public float _fGlobalWidth;

            public _iOSDimensions ()
            {
                this._fCtrlNumberTextHeight = 60.0f;
                this._fNumberPickerHeight = 200.0f;
                this._fTxtNumberHeight = 60.0f;
                this._fUpDownButtonHeight = 30.0f;
                this._fGlobalWidth = 46.0f;
            }
        }

        #endregion

    }   

}

