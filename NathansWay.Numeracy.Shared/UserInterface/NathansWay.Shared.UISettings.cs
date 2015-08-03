// Core
using System;


namespace NathansWay.Shared
{
	public class NWNumberAppSettings : IAppSettings
	{
        #region Private Variables

        private G__NumberDisplaySize _GA__NumberDisplaySize;
        private G__NumberEditMode _GA__NumberEditMode;

        #endregion

        #region Constructors

        public NWNumberAppSettings ()
        {

        }

        #endregion

        #region Properties

        public G__NumberDisplaySize GA__NumberDisplaySize
        {
            get
            {
                return _GA__NumberDisplaySize;
            }
            set
            {
                _GA__NumberDisplaySize = value;
            }
        }

        public G__NumberEditMode GA__NumberEditMode
        {
            get
            {
                return _GA__NumberEditMode;
            }
            set
            {
                _GA__NumberEditMode = value;
            }
        }

        #endregion

        #region Private Members

        #endregion
	}
}

