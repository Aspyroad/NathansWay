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

namespace ToolBox.Toolz.Hammer
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

	//			// Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
	//
	//			previousTouches = currentTouches;
	//
	//			// Read the current state of the keyboard and gamepad and store it
	//			currentTouches = TouchPanel.GetState ();
	//
	//
	//			if (State != GameState.Score){
	//				ground.Update ();
	//				buildings.Update ();
	//				bushes.Update ();
	//				clouds1.Update ();
	//				clouds2.Update ();
	//			}
	//
	//			if (State == GameState.Playing) {
	//				UpdateWalls (gameTime);
	//				// Update the collision
	//				UpdateCollision ();
	//			} else if (State == GameState.Score) {
	//				UpdateGameOver (gameTime);
	//				if (gameOverAnimationDuration <= gameOverTimer && Toggled ())
	//					Reset ();
	//
	//			}

			}

	//		bool Toggled (Buttons button)
	//		{
	//			return previousGamePadState.IsButtonUp (button) && currentGamePadState.IsButtonDown (button);
	//		}
	//
	//		bool ToggledTappped ()
	//		{
	//			return !previousTouches.Any () && currentTouches.Any ();
	//		}
	//
	//		bool Toggled (Keys key)
	//		{
	//			return previousKeyboardState.IsKeyUp (key) && currentKeyboardState.IsKeyDown (key);
	//		}
	//
	//		bool Toggled ()
	//		{
	//			return ToggledTappped () || Toggled (Buttons.A) || Toggled (Keys.Space);
	//		}




			private void AddWall ()
			{
	//			maxGap = MathHelper.Clamp (maxGap - 10,  GamePhysics.MinimumGapSize,  GamePhysics.MaximumGapSize);
	//			int gapSize = random.Next ( GamePhysics.MinimumGapSize, maxGap);
	//			var gapY = random.Next (100, wallHeight - (gapSize + 100));
	//			var wall = new Wall ();
	//
	//			var position = new Vector2 (GraphicsDevice.Viewport.Width + wallTexture.Width / 2, 0);
	//			// Initialize the animation with the correct animation information
	//			wall.Initialize (wallTexture, topWallCapTexture, bottomWallCapTexture, new Rectangle ((int)position.X, gapY, wallTexture.Width, gapSize), GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Height);
	//
	//			walls.Add (wall);
			}

			private void UpdateWalls (GameTime gameTime)
			{
	//			// Spawn a new enemy enemy every 1.5 seconds
	//			previousWallSpawnTime += gameTime.ElapsedGameTime.TotalMilliseconds;
	//			if (previousWallSpawnTime > wallSpanTime) {
	//				previousWallSpawnTime = 0;
	//				wallSpanTime -= 200;
	//				wallSpanTime = Math.Max (wallSpanTime, GamePhysics.MinimumWallSpawnRate);
	//				// Add an Enemy
	//				AddWall ();
	//			}
	//			var deadWalls = new List<Wall> ();
	//			foreach (var wall in walls) {
	//				wall.Update (gameTime);
	//				if (wall.Position.X < -wall.Width)
	//					deadWalls.Add (wall);
	//			}
	//
	//			foreach (var wall in deadWalls)
	//				walls.Remove (wall);

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


			void UpdateGameOver(GameTime gameTime)
			{
	//			gameOverTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
	//			if (gameOverTimer > gameOverAnimationDuration + 10)
	//				return;
	//			var sin = (float)Math.Sin (gameOverTimer * .7 * Math.PI / gameOverAnimationDuration);
	//			var y = (int)((GraphicsDevice.Viewport.Height/3) * sin);
	//			gameOverPosition.Y = y;
	//			scoreBoardRect.Y = GraphicsDevice.Viewport.Height - y - scoreBoardRect.Height;
			}

			/// <summary>
			/// This is called when the game should draw itself. 
			/// </summary>
			/// <param name="gameTime">Provides a snapshot of timing values.</param>

			protected override void Draw (GameTime gameTime)
			{
				GraphicsDevice.Clear (Color.CornflowerBlue);
				// Start drawing
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, 
					SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

	//			buildings.Draw (spriteBatch);
	//			bushes.Draw (spriteBatch);
	//
	//			clouds1.Draw (spriteBatch);
	//
	//			walls.ForEach (x => x.Draw (spriteBatch));
	//
	//
	//			// Draw the Player
	//			//uncomment to draw the hitbox.
	//			//spriteBatch.Draw (groundBottom, player.Rectangle, Color.Red);
	//			player.Draw (spriteBatch);
	//
	//			clouds2.Draw (spriteBatch);
	//
	//			spriteBatch.Draw (groundBottom, bottomGroundRect, Color.White);
	//			ground.Draw (spriteBatch);


				//			// Draw the score
	//			if (State == GameState.Playing)
	//				Number.Draw (spriteBatch, score, Number.Alignment.Center, new Rectangle (0, GraphicsDevice.Viewport.TitleSafeArea.Height / 4, GraphicsDevice.Viewport.TitleSafeArea.Width, 0), scoreScale);
	//
	//			if (State == GameState.Score) {
	//				spriteBatch.Draw (gameOverTexture, gameOverPosition,null,null,null,0,new Vector2(8,8), Color.White);
	//
	//				spriteBatch.Draw (scoreBoardTexture, null,scoreBoardRect,null,null,0,new Vector2(1,1), Color.White);
	//
	//				var y = scoreBoardRect.Top + scoreBoardPadding;
	//				var x = (GraphicsDevice.Viewport.TitleSafeArea.Width - scoreTexture.Width) / 2;
	//				spriteBatch.Draw (scoreTexture, new Vector2(x,y), Color.White);
	//
	//				y += scoreTexture.Height + scoreBoardPadding;
	//				Number.Draw (spriteBatch, score, Number.Alignment.Center, new Rectangle (0, y, GraphicsDevice.Viewport.TitleSafeArea.Width, 0), scoreScale);
	//
	//				y += (Number.Height * scoreScale) + scoreBoardPadding;
	//				x = (GraphicsDevice.Viewport.TitleSafeArea.Width - highScoreTexture.Width) / 2;
	//				spriteBatch.Draw (highScoreTexture, new Vector2(x,y), Color.White);
	//
	//				y += highScoreTexture.Height + scoreBoardPadding;
	//				Number.Draw (spriteBatch, TopScore.Current, Number.Alignment.Center, new Rectangle (0, y, GraphicsDevice.Viewport.TitleSafeArea.Width, 0), scoreScale);


				//}
				//			// Draw the player health
				//			spriteBatch.DrawString (font, "health: " + player.Health, new Vector2 (GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);

				// Stop drawing
				spriteBatch.End ();
				// TODO: Add your drawing code here

				base.Draw (gameTime);
			}
			#endregion
		}

	}
}
