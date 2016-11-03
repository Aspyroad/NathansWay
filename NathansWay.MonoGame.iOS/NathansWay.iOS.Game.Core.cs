using System;
using CoreGraphics;
using Microsoft.Xna.Framework;
using UIKit;

namespace NathansWay.iOS.MonoGame
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

}

