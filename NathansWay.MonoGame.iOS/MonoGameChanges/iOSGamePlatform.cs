#region License
/*
Microsoft Public License (Ms-PL)
MonoGame - Copyright © 2009-2011 The MonoGame Team

All rights reserved.

This license governs use of the accompanying software. If you use the software,
you accept this license. If you do not accept the license, do not use the
software.

1. Definitions

The terms "reproduce," "reproduction," "derivative works," and "distribution"
have the same meaning here as under U.S. copyright law.

A "contribution" is the original software, or any additions or changes to the
software.

A "contributor" is any person that distributes its contribution under this
license.

"Licensed patents" are a contributor's patent claims that read directly on its
contribution.

2. Grant of Rights

(A) Copyright Grant- Subject to the terms of this license, including the
license conditions and limitations in section 3, each contributor grants you a
non-exclusive, worldwide, royalty-free copyright license to reproduce its
contribution, prepare derivative works of its contribution, and distribute its
contribution or any derivative works that you create.

(B) Patent Grant- Subject to the terms of this license, including the license
conditions and limitations in section 3, each contributor grants you a
non-exclusive, worldwide, royalty-free license under its licensed patents to
make, have made, use, sell, offer for sale, import, and/or otherwise dispose of
its contribution in the software or derivative works of the contribution in the
software.

3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any
contributors' name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you
claim are infringed by the software, your patent license from such contributor
to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all
copyright, patent, trademark, and attribution notices that are present in the
software.

(D) If you distribute any portion of the software in source code form, you may
do so only under this license by including a complete copy of this license with
your distribution. If you distribute any portion of the software in compiled or
object code form, you may only do so under a license that complies with this
license.

(E) The software is licensed "as-is." You bear the risk of using it. The
contributors give no express warranties, guarantees or conditions. You may have
additional consumer rights under your local laws which this license cannot
change. To the extent permitted under your local laws, the contributors exclude
the implied warranties of merchantability, fitness for a particular purpose and
non-infringement.
*/
#endregion License

using System;
using System.Collections.Generic;
using System.IO;

using Foundation;
using OpenGLES;
using UIKit;
using CoreAnimation;
using ObjCRuntime;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
//using Microsoft.Xna.Framework.GamerServices;

namespace Microsoft.Xna.Framework
{
    class iOSGamePlatform : GamePlatform
    {

        public iOSGamePlatform(Game game) :
            base(game)
        {
            game.Services.AddService(typeof(iOSGamePlatform), this);
			
			// Setup our OpenALSoundController to handle our SoundBuffer pools
            try
            {
                OpenALSoundController soundControllerInstance = OpenALSoundController.GetInstance;
            }
            catch (DllNotFoundException ex)
            {
                throw (new NoAudioHardwareException("Failed to init OpenALSoundController", ex));
            }

            //This also runs the TitleContainer static constructor, ensuring it is done on the main thread
            Directory.SetCurrentDirectory(TitleContainer.Location);

            _applicationObservers = new List<NSObject>();

            #if !TVOS
            UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Fade);
            #endif

            // Create a full-screen window


            // ***ASPYMOD 
            //_mainWindow = new UIWindow (UIScreen.MainScreen.Bounds);
            _mainWindow = UIApplication.SharedApplication.KeyWindow;
			//_mainWindow.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			
            game.Services.AddService (typeof(UIWindow), _mainWindow);

            _viewController = new iOSGameViewController(this);
            game.Services.AddService (typeof(UIViewController), _viewController);
            Window = new iOSGameWindow (_viewController);

            _mainWindow.Add (_viewController.View);

            // ***ASPYMOD
            //_viewController.InterfaceOrientationChanged += ViewController_InterfaceOrientationChanged;

            //(SJ) Why is this called here when it's not in any other project
            //Guide.Initialise(game);
        }



        public override void StartRunLoop()
        {
            // Show the window
            // ***ASPYMOD 
            //_mainWindow.MakeKeyAndVisible();

            // In iOS 8+ we need to set the root view controller *after* Window MakeKey
            // This ensures that the viewController's supported interface orientations
            // will be respected at launch

            //***ASPYMOD
            //_mainWindow.RootViewController = _viewController;

            BeginObservingUIApplication();

            _viewController.View.BecomeFirstResponder();
            CreateDisplayLink();
        }



        //***ASPYMOD 
        // We need to impiment Exit as MONOGame does not controll program flow, Numbers does.

        //public override void Exit()
        //{
        //    // Do Nothing: iOS games do not "exit" or shut down.
        //}

        //***ASPYMOD 
        // New exit method
        public override void Exit()
        {
            if (_displayLink != null)
            {
                _displayLink.RemoveFromRunLoop(NSRunLoop.Main, NSRunLoop.NSDefaultRunLoopMode);
            }
            this._viewController.WillMoveToParentViewController(null);
            this._viewController.View.RemoveFromSuperview();
            //this._viewController = null;
        }

  
	}
}
