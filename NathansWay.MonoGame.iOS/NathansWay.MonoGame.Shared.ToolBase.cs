using System;
using Microsoft.Xna.Framework;


namespace NathansWay.MonoGame.Shared
{
	public abstract class BaseTool : Game
	{
		#region Declarations
		
		protected String ToolName;
		protected DateTime Instance_CreateTime;
		protected GraphicsDeviceManager graphics;

		#endregion

		#region Construction

		protected BaseTool ()
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

