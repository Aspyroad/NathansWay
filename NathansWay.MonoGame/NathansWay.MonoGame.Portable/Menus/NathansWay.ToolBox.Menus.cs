using System;
using Microsoft.Xna.Framework;

using MonoGame.Extended;
using MonoGame.Extended.NuclexGui;

using NathansWay.MonoGame.Shared;

namespace NathansWay.ToolBox.Menu
{
	public abstract class Menu : ToolBase
	{
		#region Declarations
		
		protected String ToolName;
		protected DateTime Instance_CreateTime;
		protected GraphicsDeviceManager graphics;

		#endregion

		#region Construction

		protected Menu ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = false;
			//this.EndRun
		}
		
		#endregion
		
		#region AspyMethods
		
		#endregion
		
		#region Game Overrides
		
		#endregion
	}
}

