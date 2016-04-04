// Core
using System;
using System.IO;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;
// Mono
using Foundation;
using UIKit;
using ObjCRuntime;
// NathansWay
using NathansWay.Shared.MonoToolz;



namespace NathansWay.iOS.Numeracy.ToolBox
{
	public class ToolFactory
	{
		private ITool _newTool;
		private ITool _oldTool;

		public ToolFactory ()
		{
			//this.Intialize ();
		}

		private void Intialize ()
		{
		}

		#region Public Members
		public ITool CreateNewTool (E__ToolBoxToolz newTool)
		{
			switch (newTool)
			{
				case E__ToolBoxToolz.Hammerz:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
				case E__ToolBoxToolz.Plierz:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
				case E__ToolBoxToolz.ScrewDriverz:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
				case E__ToolBoxToolz.SideCutterz:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
				default:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
			}
		}
		#endregion
	}
}

