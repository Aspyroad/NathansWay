#region License
/*
Microsoft Public License (Ms-PL)
MonoGame - Copyright © 2009-2012 The MonoGame Team

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
#endregion
using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

#region Modification 23-06-2014
// Include AspyCore
using AspyRoad.iOSCore;

#endregion

namespace Microsoft.Xna.Framework
{
	#region Modification 24-06-2014
	// Make iOSViewController inherit from AspyCore VC
	//class iOSGameViewController : UIViewController
	class iOSGameViewController : AspyViewController
	#endregion
	{
		iOSGamePlatform _platform;
		AspySettings _aspySettings;

		public iOSGameViewController (iOSGamePlatform platform)
		{
			if (platform == null)
			{
				throw new ArgumentNullException ("platform");
			}
			_platform = platform;

			#region Modification 30-06-2014
			// Unsure if this is needed, but Ill leave it, rotation is TOTALY Nathansways problem, not MonoGames anymore.
			// MonoGame implements an XNA Enum for orientation called DislplayOrientation
			// We would prefer to use this
			// SupportedOrientations = ConvertMeToSupportedOrientations(this.iOSGlobals.G__6_SupportedOrientationMasks);
			// But it may not be a concern, as AspyCore handles orientation fine under AspyViewController base.
			SupportedOrientations = DisplayOrientation.Default;

			// Add Initialize() for AspySettings
			this.Initialize();

			#endregion
		}

		public event EventHandler<EventArgs> InterfaceOrientationChanged;

		public DisplayOrientation SupportedOrientations { get; set; }

		#region Private Members

		private void Initialize()
		{
			this.AspyTag1 = 5;
			this.AspyName = "VC_MainGame";

			// Create our settings class
			this._aspySettings = (AspySettings)this.iOSSettings;
			this._vcSettings = this._aspySettings.FindVCSettings (this.AspyTag1);
		}

		#endregion

		public override void LoadView ()
		{
			#region Modification 30-06-2014
			// Technically, going by the below code, if I grabbed a reference to iOSGameViewController
			// and thumped into a parent view controller I get the parents frame size.
			// Other then a slighly more complex VC structure, this would work fine, however..
			// I'd rather go clean and build it using AspySettings class

			//RectangleF frame;
			//if (ParentViewController != null && ParentViewController.View != null)
			//{
			//	frame = new RectangleF (PointF.Empty, ParentViewController.View.Frame.Size);
			//}
			//else
			//{
			//	UIScreen screen = UIScreen.MainScreen;
			//	if (InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
			//	{
			//		frame = new RectangleF (0, 0, screen.Bounds.Height, screen.Bounds.Width);
			//	}
			//	else
			//	{
			//		frame = new RectangleF (0, 0, screen.Bounds.Width, screen.Bounds.Height);
			//	}
			//}
			#endregion

			base.View = new iOSGameView (_platform, this._vcSettings.FrameSize);
		}

		public new iOSGameView View
		{
			get { return (iOSGameView)base.View; }
		}

		#region Modification 29-06-2014
		//#region Autorotation for iOS 5 or older

		//[Obsolete]
		//public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		//{
		//	DisplayOrientation supportedOrientations = OrientationConverter.Normalize (SupportedOrientations);
		//	var toOrientation = OrientationConverter.ToDisplayOrientation (toInterfaceOrientation);
		//	return (toOrientation & supportedOrientations) == toOrientation;
		//}

		//#endregion


		//#region Autorotation for iOS 6 or newer

		//public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		//{
		//	return OrientationConverter.ToUIInterfaceOrientationMask (this.SupportedOrientations);
		//}

		//public override bool ShouldAutorotate ()
		//{
		//	return _platform.Game.Initialized;
		//}

		//#endregion

		#endregion


		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);

			var handler = InterfaceOrientationChanged;
			if (handler != null)
			{
				handler (this, EventArgs.Empty);
		  }
		}

		#region Modification 29-06-2014
		// Comment out this method, it isnt needed and is handled by Aspy base.

		//#region Hide statusbar for iOS 7 or newer

		//public override bool PrefersStatusBarHidden ()
		//{
		//	return _platform.Game.graphicsDeviceManager.IsFullScreen;
		//}

		//#endregion

		#endregion
	}

	#region VCSettings

/*	public class vcs_maingame : VcSettings
	{
		public vcs_maingame (IAspyGlobals iOSGlobals)
		{
			this.VcTag = 5;
			this.VcName = "VC_MainGame";

			this.HasBorder = true;
			this.BorderColor = UIColor.Black;
			this.BorderSize = 1.0f;
			this.BackColor = UIColor.White;

			this.FrameSize = 
				new RectangleF 
				(
					0,
					0,
					iOSGlobals.G__RectWindowLandscape.Width,
					((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3)
				);
		}
	}*/

	#endregion
}
