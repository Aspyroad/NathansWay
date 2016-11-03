#region File Description
//-----------------------------------------------------------------------------
// iOS_MonogameViewGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace NathansWay.MonoGame.Shared
                    
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

