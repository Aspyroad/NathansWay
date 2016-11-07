#region File Description
//-----------------------------------------------------------------------------
// iOS_MonogameViewGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using NathansWay.MonoGame.Shared;

#endregion

namespace NathansWay.MonoGame.iOS
{
	public class ToolFactory
	{
		private BaseTool _newTool;
		private BaseTool _oldTool;

		public ToolFactory ()
		{
			//this.Intialize ();
		}

		private void Intialize ()
		{
		}

		#region Public Members
		public BaseTool CreateNewTool (E__ToolBoxTool newTool)
		{
			switch (newTool)
			{
				case E__ToolBoxTool.Hammerz:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
				case E__ToolBoxTool.Plierz:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
				case E__ToolBoxTool.ScrewDriverz:
				{
					_newTool = new Hammer ();
					return _newTool;
				}
				case E__ToolBoxTool.SideCutterz:
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

