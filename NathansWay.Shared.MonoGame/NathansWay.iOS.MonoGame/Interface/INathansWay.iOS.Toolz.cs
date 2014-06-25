#region Using Statements
// System
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

// MonoGame
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

#endregion

namespace NathansWay.Shared.MonoToolz
{
	public interface ITool
	{
		E__ToolBoxToolz ToolType { get; }
		Game MainGame { get; }
	}
}

