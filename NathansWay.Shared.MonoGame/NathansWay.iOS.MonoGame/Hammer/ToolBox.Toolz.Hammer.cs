#region File Description

#endregion

#region Using Statements
// Core
using System;
using System.Collections.Generic;
using System.Linq;
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
	/// <summary>
	/// Codename : Hammer
	/// Info : Hammer is the main counting tool. 
	/// </summary>

	public class Hammer : ITool
	{
		#region Private Variables

		private Game _maingame;

		#endregion

		#region Constructor
		public Hammer()
		{
			this.MainGame = new HammerGame ();
		}
		#endregion

		public Game MainGame
		{
			get { return _maingame; }
		}

		private class HammerGame : Game
		{

			#region Private Variables

			private HammerPhysics hammerphysics;
			private GraphicsDeviceManager graphics;
			private SpriteBatch spriteBatch;
			private Texture2D logoTexturem, wallTexture, playerTexture, groundBottom;
			private TouchCollection previousTouches;
			private TouchCollection currentTouches;
			private Rectangle bottomGroundRect;
			private ParallaxingBackground ground;
			private ParallaxingBackground clouds1;
			private ParallaxingBackground clouds2;

			private bool accelActive;
			private int wallHeight;
			private int scoreBoardPadding = 0;
			private int maxGap;

			const int MaxWallheight = 920;

			#endregion

			#region Initialization

			public HammerGame()  
	        {
				graphics = new GraphicsDeviceManager(this);
				hammerphysics = new HammerPhysics ();			
				Content.RootDirectory = "Content";
				graphics.IsFullScreen = false;
			}

			/// <summary>
			/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
			/// we'll use the viewport to initialize some values.
			/// </summary>
			protected override void Initialize ()
			{
				wallHeight = Math.Min (GraphicsDevice.Viewport.Height, MaxWallheight);
				bottomGroundRect = new Rectangle 
					(
						0, 
						(wallHeight + 1), 
						GraphicsDevice.Viewport.Width, 
						(GraphicsDevice.Viewport.Height - MaxWallheight)
					);
				//player = new Player ();
				ground = new ParallaxingBackground ();
				clouds1 = new ParallaxingBackground ();
				clouds2 = new ParallaxingBackground ();
				base.Initialize ();
			}

			/// <summary>
			/// Load your graphics content.
			/// </summary>
			protected override void LoadContent ()
			{
				// Create a new SpriteBatch, which can be use to draw textures.
				this.spriteBatch = new SpriteBatch (graphics.GraphicsDevice);
				this.groundBottom = Content.Load<Texture2D> ("bottomGround");
				this.ground.Initialize 
				(
					Content, 
					"ground", 
					wallHeight, 
					GraphicsDevice.Viewport.Width, 
					GraphicsDevice.Viewport.Height, 
					-this.hammerphysics.WallSpeed, 
					false
				);
				this.clouds1.Initialize 
				(
					Content, 
					"clouds1", 
					0, 
					GraphicsDevice.Viewport.Width, 
					GraphicsDevice.Viewport.Height, 
					-.25f, 
					true
				);
				this.clouds2.Initialize 
				(
					Content, 
					"clouds2", 
					0, 
					GraphicsDevice.Viewport.Width, 
					GraphicsDevice.Viewport.Height, 
					-(hammerphysics.WallSpeed + .5f), 
					true
				);

				Reset ();
			}

			public void Reset ()
			{
			}

			#endregion

			#region Update and Draw

			/// <summary>
			/// Allows the game to run logic such as updating the world,
			/// checking for collisions, gathering input, and playing audio.
			/// </summary>
			/// <param name="gameTime">Provides a snapshot of timing values.</param>
			protected override void Update (GameTime gameTime)
			{
				base.Update (gameTime);

				ground.Update ();
				clouds1.Update ();
				clouds2.Update ();

				// Save the previous state of the keyboard and game pad so we can determine single key/button presses	
				previousTouches = currentTouches;
	
				// Read the current state of the keyboard and gamepad and store it
				currentTouches = TouchPanel.GetState ();

			}

			private void UpdateCollision ()
			{
				// Use the Rectangle's built-in intersect function to 
				// determine if two objects are overlapping
	//			var rectangle1 = player.Rectangle;
	//
	//			//If it collides with a wall, you die
	//			foreach (var wall in walls.Where(x=> x.Collides(rectangle1))) {
	//				gameOver ();
	//			}
	//
	//			var points = walls.Sum (x => x.CollectPoints ());
	//			score += points;
	//
	//			if (rectangle1.Bottom >= wallHeight) {
	//				gameOver ();
	//			}
			}

			/// <summary>
			/// This is called when the game should draw itself. 
			/// </summary>
			/// <param name="gameTime">Provides a snapshot of timing values.</param>
			protected override void Draw (GameTime gameTime)
			{
				GraphicsDevice.Clear (Color.CornflowerBlue);
				// Start drawing
				spriteBatch.Begin
					(
						SpriteSortMode.Deferred, 
						BlendState.AlphaBlend, 
						SamplerState.PointClamp, 
						DepthStencilState.None, 
						RasterizerState.CullNone
					);

				clouds1.Draw (spriteBatch);
				clouds2.Draw (spriteBatch);
				//spriteBatch.Draw (groundBottom, bottomGroundRect, Color.White);
				ground.Draw (spriteBatch);

				// Stop drawing
				spriteBatch.End ();

				base.Draw (gameTime);
			}
			#endregion
		}

	}
}
