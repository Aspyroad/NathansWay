using System;
using Microsoft.Xna.Framework;



namespace NathansWay.MonoGame.Shared
{
    // MAIN MONOGAME BACKING CLASS
	public abstract class ToolBase : Game
	{
		#region Declarations
		
		protected String ToolName;
		protected DateTime Instance_CreateTime;
		protected GraphicsDeviceManager graphics;

		#endregion

		#region Construction

        protected ToolBase ()
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
		
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        } 

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }



		#endregion
	}
}

