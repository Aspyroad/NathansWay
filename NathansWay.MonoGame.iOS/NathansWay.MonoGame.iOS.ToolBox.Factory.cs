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
using UIKit;

#endregion

namespace NathansWay.MonoGame.iOS
{
	public class ToolFactory
	{
		private UIWindow _localWindow;
		private UIViewController _vcMainWorkSpace;

		public ToolFactory ()
		{
			this.Intialize ();
		}

		private void Intialize ()
		{
		}

		private BaseTool WindowSwitcher (BaseTool _tool, UIWindow _newWindow)
		{
			var _vcToolSpace = _tool.Services.GetService<UIViewController> ();
			var _wToolSpaceWindow = _tool.Services.GetService<UIWindow> ();
			_vcToolSpace.WillMoveToParentViewController (null);
			_vcToolSpace.View.RemoveFromSuperview ();

			_vcToolSpace.WillMoveToParentViewController (_vcMainWorkSpace);
			_vcMainWorkSpace.Add (_vcToolSpace.View);

			_tool.Services.RemoveService (typeof(UIWindow));
			_tool.Services.AddService(typeof (UIWindow), _localWindow);

			return _tool;
		}

		#region Public Members
		public BaseTool CreateNewTool (E__ToolBoxTool newTool, UIViewController _vcWorkSpace)
		{
			this._vcMainWorkSpace = _vcWorkSpace;
			this._localWindow = UIApplication.SharedApplication.KeyWindow;

			switch (newTool)
			{
				case E__ToolBoxTool.Hammerz:
				{
					var _newTool = new Hammer ();

					return this.WindowSwitcher(_newTool, this._localWindow);
				}
				case E__ToolBoxTool.Plierz:
				{
					var _newTool = new Hammer ();
					return this.WindowSwitcher (_newTool, this._localWindow);
				}
				case E__ToolBoxTool.ScrewDriverz:
				{
					var _newTool = new Hammer ();
					return this.WindowSwitcher (_newTool, this._localWindow);
				}
				case E__ToolBoxTool.SideCutterz:
				{
					var _newTool = new Hammer ();
					return this.WindowSwitcher (_newTool, this._localWindow);
				}
				default:
				{
					var _newTool = new Hammer ();
					return _newTool;
				}
			}
		}
		#endregion
	}
}

