#region File Description
//-----------------------------------------------------------------------------
// iOS_MonogameViewGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using Microsoft.Xna.Framework;
using NathansWay.MonoGame.Shared;
using UIKit;

using MonoGame.Extended;

#endregion

namespace NathansWay.MonoGame.iOS
{

    public class ToolFactory
    {
        private UIWindow _localWindow;
        private UIViewController _vcMainWorkSpace;
        private UIViewController _vcMonoGameWorkSpace;

        public ToolFactory()
        {
            this.Intialize();
        }

        private void Intialize()
        {
        }

        // Not needed now as we are modding MonogameiOS platform directly
        private BaseTool WindowSwitcher(BaseTool _tool, UIWindow _newWindow)
        {
            var _vcToolSpace = _tool.Services.GetService<UIViewController>();
            var _wToolSpaceWindow = _tool.Services.GetService<UIWindow>();
            _vcToolSpace.WillMoveToParentViewController(null);
            _vcToolSpace.View.RemoveFromSuperview();

            _vcToolSpace.WillMoveToParentViewController(_vcMainWorkSpace);
            _vcMainWorkSpace.Add(_vcToolSpace.View);

            _tool.Services.RemoveService(typeof(UIWindow));
            _tool.Services.AddService(typeof(UIWindow), _localWindow);

            return _tool;
        }

        #region Public Members

        public BaseTool CreateNewTool(E__ToolBoxTool newTool, UIViewController _vcWorkSpace)
        {
            switch (newTool)
            {
                case E__ToolBoxTool.Hammerz:
                    {
                        var _newTool = new Hammer();
                        this.SetGameView(_newTool);
                        return _newTool;
                    }
                case E__ToolBoxTool.Plierz:
                    {
                        var _newTool = new Pliers();
                        return _newTool;
                    }
                case E__ToolBoxTool.ScrewDriverz:
                    {
                        var _newTool = new Hammer();
                        return _newTool;
                    }
                case E__ToolBoxTool.SideCutterz:
                    {
                        var _newTool = new Hammer();
                        return _newTool;
                    }
                default:
                    {
                        var _newTool = new Hammer();
                        return _newTool;
                    }
            }
        }

        public void SetGameView(BaseTool _gametool)
        {
            this._vcMonoGameWorkSpace = _gametool.Services.GetService<UIViewController>();
            //_vcToolSpace.View.Layer.CornerRadius = 2.0f;



        }

        #endregion

        #region Public Properties

        public UIViewController MonoGameWorkSpace
        {
            get { return this._vcMonoGameWorkSpace; }

        }

        #endregion

    }
}

