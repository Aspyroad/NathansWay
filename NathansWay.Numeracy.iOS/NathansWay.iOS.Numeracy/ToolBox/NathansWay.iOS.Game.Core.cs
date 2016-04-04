using System;
using CoreGraphics;
using Microsoft.Xna.Framework;
using UIKit;
using AspyRoad.iOSCore;
using NathansWay;

namespace NathansWay.iOS.Numeracy
{
	public class GameViewControl
	{
		// Delcares
		public Game game;
		public UIViewController _gameViewController;

		// Constructors
		// Default		
		public GameViewControl ()
		{
			game = new Game ();
			_gameViewController = (UIViewController)game.Services.GetService (typeof(UIViewController));
			//game.Run ();
		}

		// Takes a bounds rect object for the view frame.
		// Not sure of Monogames view should be sized here or elsewhere?
		public GameViewControl (CGRect scrnBounds)
		{
			throw new NotImplementedException ();
		}
	}

	public abstract class AspyTool : Game
	{
		#region Declarations
		
		private String ToolName;
		private  DateTime Instance_CreateTime;		
		
		#endregion
		
		#region Construction
		
		#endregion
		
		#region AspyMethods
		
		#endregion
		
		#region Game Overrides
		
		#endregion
	}
}

