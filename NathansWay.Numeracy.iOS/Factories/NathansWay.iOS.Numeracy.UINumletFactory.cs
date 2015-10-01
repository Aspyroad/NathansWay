// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
// Shared
using NathansWay.Shared.Factories;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    public class UINumletFactory 
    {
        #region Class Variables

        private ExpressionFactory _expressionFactory;
        private NumberFactoryClient _numberFactoryClient;

        #endregion

        #region Contructors

        public UINumletFactory()
        {
            // Factory Classes for expression building
            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(this._numberFactoryClient);
        }

        #endregion

        #region Public Members

        public vcWorkNumlet CreateNumlet(string expression)
        {
            return new vcWorkNumlet();
        }

        public void LoadExpression(string _strExpression)
        {
            this._expressionFactory.CreateExpressionEquation(_strExpression, false);
        }

        public void LoadExpressionLabel(string _strExpression)
        {
            this._expressionFactory.CreateExpressionEquation(_strExpression, true);
        }

        public void BuildExpression(List<object> UIInternalOutput)
        {
            // TODO : Where is this going to get set? Depending on size?
            if (this.SizeClass.CurrentHeight <= 0.0f)
            {
                // Cant set sizes without WorkSpace Startpoint
                return;
            }

            // TODO : Local horizontal position. Do we need a buffer/padding??
            float _XPos = 2.0f;

            // TODO : We need to set this Numlets width somewhere???? Might be kindve important.    
            for (int i = 0; i < UIInternalOutput.Count; i++) // Loop with for.
            {
                var _control = (BaseContainer)UIInternalOutput[i];
                _control.SizeClass.SetCenterRelativeParentVcPosY = true;

                // TODO : Hook up the control resizing events so that all controls are messaged by this numlet

                // This call only calls the BASE SetPositions not any derives
                // You may need to call any frame creation methods in the 
                // controls ViewWillApppear method
                _control.SizeClass.SetPositions(_XPos, this.SizeClass.CurrentHeight);
                //_control.SizeClass.StartPoint = new PointF(_XPos, this.SizeClass.CurrentHeight);
                this.AddAndDisplayController(_control);
                _XPos = _XPos + 4.0f + _control.SizeClass.CurrentWidth;
            }
        }

        #endregion

        #region Public Properties

        #endregion

        #region Private Members

        #endregion
    }
}

