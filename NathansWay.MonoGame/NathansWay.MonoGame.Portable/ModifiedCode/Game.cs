// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.



        #region Public Methods

#if IOS
        //***ASPYMOD
        //[Obsolete("This platform's policy does not allow programmatically closing.", true)]
        // This is not the case for Nathansway, as Nathansway controls program flow, not Monogame.
#endif
        public void Exit()
        {
            _shouldExit = true;
            _suppressDraw = true;
        }
