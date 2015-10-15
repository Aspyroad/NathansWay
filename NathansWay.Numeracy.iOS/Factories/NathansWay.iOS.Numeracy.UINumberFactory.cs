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
    // Class formaly known as UINumletFactory
    public class UINumberFactory : IDisposable
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
        public UIStoryboard _storyBoard;

        // UI
        private List<object> _uiOutputEquation;
        private List<List<object>> _uiOutputMethods;
        private iOSNumberDimensions _globalSizeDimensions;
        private G__NumberDisplaySize _numberDisplaySize;
        private float _fNumberSpacing;

        // Logic

        // Data
        // TODO: These MUST be all populated for this class to work do we need security
        private EntityLesson _wsLesson;
        //private EntityLessonResults _wsLessonResults;
        private List<EntityLessonDetail> _wsLessonDetail;
        //private EntityLessonDetailResults _wsLessonDetailResults;
        private int _intLessonDetailSeq;

        #endregion

        #region Contructors

        public UINumberFactory()
        {
            // Factory Classes for expression building
            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(this._numberFactoryClient);
            this._globalSizeDimensions = iOSCoreServiceContainer.Resolve<iOSNumberDimensions> ();
            this._storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard> ();
            this._intLessonDetailSeq = 0;
        }

        #endregion

        #region Deconstruction

        public void Dispose ()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Members

        public vcWorkSpace UILoadEquation (EntityLesson entLesson, List<EntityLessonDetail> entLessonDetail)
        {
            // Fire start event
            this.FireBuildStartedEvent();

            this._wsLesson = entLesson;
            this._wsLessonDetail = entLessonDetail;

            // Create our workspace object
            vcWorkSpace _vcWorkSpace = this._storyBoard.InstantiateViewController("vcWorkSpace") as vcWorkSpace;

            // Create our Nunmlet
            // TODO: Get the SEQ lessondetail from lessondetail list
            // 
            // this.CreateNumlet(

            // Fire completed event
            this.FireBuildCompletedEvent();

            return _vcWorkSpace;
        }

        public vcWorkSpaceLabel UILoadEquationDisplayOnly (EntityLesson entLesson, List<EntityLessonDetail> entLessonDetail)
        {
            // Fire start event
            this.FireBuildStartedEvent();

            this._wsLesson = entLesson;
            this._wsLessonDetail = entLessonDetail;

            // Create our workspace label object
            vcWorkSpaceLabel x = new vcWorkSpaceLabel();

            // Fire completed event
            this.FireBuildCompletedEvent();

            return x;
        }

        #endregion

        #region Public Properties

        public int LessonDetailSeq
        {
            get { return this._intLessonDetailSeq; }
            set { this._intLessonDetailSeq = value; }
        }

        public EntityLesson WsLesson
        {
            get { return this._wsLesson; }
            set { this._wsLesson = value; }
        }

        //        public EntityLessonResults WsLessonResults
        //        {
        //            get { return this._wsLessonResults; }
        //            set { this._wsLessonResults = value; }
        //        }

        public List<EntityLessonDetail> WsLessonDetail
        {
            get { return this._wsLessonDetail; }
            set { this._wsLessonDetail = value; }
        }

        //        public EntityLessonDetailResults WsLessonDetailResults
        //        {
        //            get { return this._wsLessonDetailResults; }
        //            set { this._wsLessonDetailResults = value; }
        //        }

        #endregion

        #region Private Members

        private vcWorkNumlet CreateNumlet(string _strExpression)
        {
            // Create all our expression symbols, numbers etc
            this.ExpressionToUIEditable(_strExpression);
            // The actual numlet
            var numlet = new vcWorkNumlet();
            G__NumberDisplaySize _displaySize;
            float _aPos = this._globalSizeDimensions.NumletNumberSpacing;
            float _bPos = _aPos;

            for (int i = 0; i < this._uiOutputEquation.Count; i++)
            {                
                var _control = (BaseContainer)this._uiOutputEquation[i];
                _control.SizeClass.SetCenterRelativeParentVcPosY = true;

                // Hook up the control resizing events so that all controls are messaged by this numlet
                numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

                // Most of these should ApplyUI in ViewWillAppear
                _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 

                _control.SizeClass.SetPositions(_aPos, _bPos);
                numlet.AddAndDisplayController(_control);
                _aPos = _aPos + _control.SizeClass.CurrentWidth;
            }

            // Pad out the end
            numlet.SizeClass.CurrentWidth = _aPos + this._globalSizeDimensions.NumletNumberSpacing; 
            numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

            // Return completed numnut!
            // Numlet has no size params set. SetPositions must be called before use!
            return numlet;
        }

        /// <summary>
        /// Builds an list<object> of our Equation from a string expression to an Editable UI.
        /// </summary>
        /// <param name="_strExpression">String expression.</param>
        private void ExpressionToUIEditable(string _strExpression)
        {
            this._uiOutputEquation = this._expressionFactory.CreateExpressionEquation(_strExpression, false);
        }

        /// <summary>
        /// Builds an list<object> of our Equation from a string expression to a ReadOnly UI.
        /// </summary>
        /// <param name="_strExpression">String expression.</param>
        private void ExpressionToUIReadOnly(string _strExpression)
        {
            this._uiOutputMethods = this._expressionFactory.CreateExpressionMethod(_strExpression, true);
        }

        //        private string GetEquation(int Seq)
        //        {
        //            #if DEBUG
        //            if (this._wsLesson == null)
        //            {
        //                throw new NullReferenceException("UINumberFactory : Lesson Entity is null, check your assign");
        //            }
        //            if (this._wsLessonDetail == null)
        //            {
        //                throw new NullReferenceException("UINumberFactory : Lesson Detail Entity is null, check your assign");
        //            }
        //            #endif
        //
        //            return (this._wsLessonDetail.Equation.Trim());
        //        }
        //
        //        private string GetResult()
        //        {
        //            #if DEBUG
        //            if (this._wsLesson == null)
        //            {
        //                throw new NullReferenceException("UINumberFactory : Lesson Entity is null, check your assign");
        //            }
        //            if (this._wsLessonDetail == null)
        //            {
        //                throw new NullReferenceException("UINumberFactory : Lesson Detail Entity is null, check your assign");
        //            }
        //            #endif
        //
        //            return (this._wsLessonDetail.Result.Trim());
        //        }

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

