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

        #region Events

        public delegate void BuildStartedEventHandler (Object sender, EventArgs e);
        public delegate void BuildCompletedEventHandler (Object sender, EventArgs e);

        public event BuildStartedEventHandler BuildStarted;
        public event BuildCompletedEventHandler BuildCompleted;

        #endregion

        #region Class Variables

        // Shared Factory
        private ExpressionFactory _expressionFactory;
        private NumberFactoryClient _numberFactoryClient;

        // UI
        private List<object> _uiOutputEquation;
        private List<List<object>> _uiOutputMethods;
        private iOSNumberDimensions _globalSizeDimensions;
        private G__NumberDisplaySize _numberDisplaySize;
        private float _fNumberSpacing;

        // Logic



        #endregion

        #region Contructors

        public UINumletFactory()
        {
            // Factory Classes for expression building
            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(this._numberFactoryClient);
            this._globalSizeDimensions = iOSCoreServiceContainer.Resolve<iOSNumberDimensions> ();
        }

        #endregion

        #region Public Members

        // TODO : Create an Entity class with 
        //        private EntityLesson _wsLesson;
        //        private EntityLessonResults _wsLessonResults;
        //        private EntityLessonDetail _wsLessonDetail;
        //        private EntityLessonDetailResults _wsLessonDetailResults;
        // All being members, it will be easier to pass this one object as a set.

        public vcWorkNumlet CreateNumlet(string expression)
        {
            var numlet = new vcWorkNumlet();
            float _xPos = this._globalSizeDimensions.NumletNumberSpacing;

            // TODO : We need to set this Numlets width somewhere???? Might be kindve important.    
            for (int i = 0; i < this._uiOutputEquation.Count; i++) // Loop with for.
            {
                var _control = (BaseContainer)this._uiOutputEquation[i];
                _control.SizeClass.SetCenterRelativeParentVcPosY = true;

                // TODO : Hook up the control resizing events so that all controls are messaged by this numlet

                // TODO : Do we need to set the ApplyUIWhere variable for each object?
                // Most of these should ApplyUI in ViewWillAppear

                _control.SizeClass.SetPositions(_xPos, this._globalSizeDimensions.NumletHeight);
                //_control.SizeClass.StartPoint = new PointF(_XPos, this.SizeClass.CurrentHeight);
                numlet.AddAndDisplayController(_control);
                _xPos = _xPos + _control.SizeClass.CurrentWidth;
            }

            return numlet;
        }

        public void ExpressionToUIEditable(string _strExpression)
        {
            this._uiOutputEquation = this._expressionFactory.CreateExpressionEquation(_strExpression, false);
        }

        public void ExpressionToUIReadOnly(string _strExpression)
        {
            this._uiOutputMethods = this._expressionFactory.CreateExpressionMethod(_strExpression, true);
        }

        public void BuildExpression(List<object> UIInternalOutput)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Private Members

        protected void FireBuildStartedEvent()
        {
            // Thread safety.
            var x = this.BuildStarted;
            // Check for null before firing.
            if (x != null)
            {
                x (this, new EventArgs ());
            }   
        }

        protected void FireBuildCompletedEvent()
        {
            // Thread safety.
            var x = this.BuildCompleted;
            // Check for null before firing.
            if (x != null)
            {
                x (this, new EventArgs ());
            }   
        }

        #endregion
    }
}

