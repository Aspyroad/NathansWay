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
using NathansWay.Shared.BUS.ViewModel;
using NathansWay.Shared.Utilities;
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
        private List<object> _uiOutputResult;
        private List<List<object>> _uiOutputMethods;
        private vcWorkSpace _vcCurrentWorkSpace;

        private iOSNumberDimensions _globalSizeDimensions;
        private G__NumberDisplaySize _numberDisplaySize;
        private float _fNumberSpacing;
        protected IAppSettings _numberAppSettings;

        // Logic

        private bool bDocked_SolveNumlet;
        private bool bDocked_ResultNumlet;

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
            this._numberAppSettings = SharedServiceContainer.Resolve<IAppSettings>();
            this._intLessonDetailSeq = 1;
        }

        #endregion

        #region Deconstruction

        public void Dispose ()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Members

        public vcWorkSpace UILoadWorkSpace (LessonViewModel _vmLesson)
        {
            // Create our workspace object
            this._vcCurrentWorkSpace = this._storyBoard.InstantiateViewController("vcWorkSpace") as vcWorkSpace;
            // Load the lesson detail for the selected lesson
            this._vcCurrentWorkSpace.WsLesson = _vmLesson.SelectedLesson;
            this._vcCurrentWorkSpace.WsLessonDetail = _vmLesson.LessonDetail;
            this._vcCurrentWorkSpace.NumberFactory = this;

            // TODO: Code here to load the first lesson detail automatically if needed goes here I think

            return this._vcCurrentWorkSpace;
        }

        public vcWorkNumlet GetEquationNumlet(string _strEquation, bool _readonly)
        {            
            var _vcNumletEquation = this.CreateNumletEquation(_strEquation, _readonly);
            _vcNumletEquation.SizeClass.SetCenterRelativeParentViewPosY = true;
            _vcNumletEquation.SizeClass.SetLeftRelativeMiddleParentViewPosX = true;
            _vcNumletEquation.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
            _vcNumletEquation.SizeClass.SetPositions(this._globalSizeDimensions.WorkSpaceCanvasWidth, this._globalSizeDimensions.WorkSpaceCanvasHeight);
            return _vcNumletEquation;
        }

        public vcWorkNumlet GetResultNumlet(string _strResult, bool _readonly)
        {
            var _vcNumletResult = this.CreateNumletResult(_strResult);
            _vcNumletResult.SizeClass.SetCenterRelativeParentViewPosY = true;
            _vcNumletResult.SizeClass.SetRightRelativeMiddleParentViewPosX = true;
            _vcNumletResult.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Left;
            _vcNumletResult.SizeClass.SetPositions(this._globalSizeDimensions.WorkSpaceCanvasWidth, this._globalSizeDimensions.WorkSpaceCanvasHeight);
            return _vcNumletResult;
        }

        public vcWorkNumlet GetSolveNumlet(float _resultNumletWidth)
        {
            // This solver Numlet always sits to the left of the result numlet for the time being.
            // Maybe its own position is needed on the extreme left of workspace.
            var _vcNumletSolve = this.CreateNumletSolve();

            if (bDocked_SolveNumlet)
            {
                
            }
            else
            {
                _vcNumletSolve.SizeClass.SetCenterRelativeParentViewPosY = true;
                _vcNumletSolve.SizeClass.SetRightRelativeMiddleParentViewPosX = true;
                _vcNumletSolve.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Left;
                float w = ((_resultNumletWidth * 2) + this._globalSizeDimensions.WorkSpaceCanvasWidth + this._globalSizeDimensions.NumletNumberSpacing);
                _vcNumletSolve.SizeClass.SetPositions(w, this._globalSizeDimensions.WorkSpaceCanvasHeight);
            }
            return _vcNumletSolve;
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

        private vcWorkNumlet CreateNumletEquation(string _strEquation, bool _readonly)
        {            
            // Create all our expression symbols, numbers etc
            this.EquationStringToObjects(_strEquation);

            // Setup the numlet
            var numlet = new vcWorkNumlet();
            numlet.NumletType = G__WorkNumletType.Equation;
            numlet.IsReadOnly = _readonly;
            // Set Parent
            numlet.MyWorkSpaceParent = this._vcCurrentWorkSpace;
            numlet.MyImmediateParent = this._vcCurrentWorkSpace;
            numlet.OutputContainers = this._uiOutputEquation;
            // Sizing
            G__NumberDisplaySize _displaySize;
            float _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
            float _xPos = _xSpacing;
            float _yPos = this._globalSizeDimensions.NumletHeight;

            for (int i = 0; i < this._uiOutputEquation.Count; i++)
            {                
                var _control = (BaseContainer)this._uiOutputEquation[i];
                // Set Parents
                _control.MyNumletParent = numlet;
                _control.MyImmediateParent = numlet;
                _control.MyWorkSpaceParent = this._vcCurrentWorkSpace;

                _control.IsReadOnly = _readonly;
                _control.SizeClass.SetCenterRelativeParentViewPosY = true;

                if ((_control.ContainerType == G__ContainerType.Number) || (_control.ContainerType == G__ContainerType.Fraction))
                {                    
                    _control.IsInitialLoad = true;
                    _control.IsAnswer = false;
                    _control.IsReadOnly = true;
                }

                // Hook up the control resizing events so that all controls are messaged by this numlet
                numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

                // Most of these should ApplyUI in ViewWillAppear
                _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 

                _control.SizeClass.SetPositions(_xPos, _yPos);
                numlet.AddAndDisplayController(_control);
                _xPos = _xPos + _control.SizeClass.CurrentWidth + _xSpacing;
            }

            // Pad out the end
            numlet.SizeClass.CurrentWidth = _xPos; 
            numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

            // Return completed numnut!
            // Numlet has no size params set. SetPositions must be called before use!
            return numlet;
        }

//        private List<vcWorkNumlet> CreateNumletMethods(string _strExpression)
//        {
//            // Create all our expression symbols, numbers etc
//            this.ExpressionToUIEditable(_strExpression);
//
//            // Setup the numlet
//            var numlet = new vcWorkNumlet();
//            numlet.NumletType = G__WorkNumletType.Equation;
//
//            G__NumberDisplaySize _displaySize;
//            float _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
//            float _xPos = _xSpacing;
//            float _yPos = this._globalSizeDimensions.NumletHeight;
//
//            for (int i = 0; i < this._uiOutputEquation.Count; i++)
//            {                
//                var _control = (BaseContainer)this._uiOutputEquation[i];
//                _control.SizeClass.SetCenterRelativeParentViewPosY = true;
//
//                // Hook up the control resizing events so that all controls are messaged by this numlet
//                numlet.SizeClass.eResizing += _control.SizeClass.OnResize;
//
//                // Most of these should ApplyUI in ViewWillAppear
//                _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 
//
//                _control.SizeClass.SetPositions(_xPos, _yPos);
//                numlet.AddAndDisplayController(_control);
//                _xPos = _xPos + _control.SizeClass.CurrentWidth + _xSpacing;
//            }
//
//            // Pad out the end
//            numlet.SizeClass.CurrentWidth = _xPos; 
//            numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;
//
//            // Return completed numnut!
//            // Numlet has no size params set. SetPositions must be called before use!
//            return numlet;
//        }

        private vcWorkNumlet CreateNumletResult(string _strResult)
        {
            // Create all our expression symbols, numbers etc
            // REMEMBER:  do we add the equals sign here?? Not sure
            _strResult = ("=," + _strResult);
            // Build...
            this.ResultStringToObjects(_strResult);

            // Setup the numlet
            var numlet = new vcWorkNumlet();
            numlet.NumletType = G__WorkNumletType.Result;
            numlet.IsAnswer = true;
            // Set Parent
            numlet.MyWorkSpaceParent = this._vcCurrentWorkSpace;
            numlet.MyImmediateParent = this._vcCurrentWorkSpace;
            // Sizing
            G__NumberDisplaySize _displaySize;
            float _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
            float _xPos = _xSpacing;
            float _yPos = this._globalSizeDimensions.NumletHeight;

            for (int i = 0; i < this._uiOutputResult.Count; i++)
            {                
                var _control = (BaseContainer)this._uiOutputResult[i];
                // Set Parents
                _control.MyNumletParent = numlet;
                _control.MyImmediateParent = numlet;
                _control.MyWorkSpaceParent = this._vcCurrentWorkSpace;
                _control.SizeClass.SetCenterRelativeParentViewPosY = true;

                if (_control.ContainerType == G__ContainerType.Number || _control.ContainerType == G__ContainerType.Fraction)
                {
                    numlet.ResultContainer = _control;
                    _control.IsInitialLoad = true;
                    _control.IsAnswer = true;
                    _control.IsReadOnly = false;
                    _control.CurrentEditMode = this._numberAppSettings.GA__NumberEditMode;

                    _control.ClearValue();
                }
                else
                {
                    _control.IsReadOnly = true;
                    _control.CurrentEditMode = this._numberAppSettings.GA__NumberEditMode;
                }

                // Hook up the control resizing events so that all controls are messaged by this numlet
                numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

                // Most of these should ApplyUI in ViewWillAppear
                _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 

                _control.SizeClass.SetPositions(_xPos, _yPos);
                numlet.AddAndDisplayController(_control);
                _xPos = _xPos + _control.SizeClass.CurrentWidth + _xSpacing;
            }

            // Pad out the end
            numlet.SizeClass.CurrentWidth = _xPos; 
            numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

            // Return completed numnut!
            // Numlet has no size params set. SetPositions must be called before use!
            return numlet;
        }

        private vcWorkNumlet CreateNumletSolve()
        {
            // Setup the numlet
            var numlet = new vcWorkNumlet();
            var _control = new vcSolveContainer();

            numlet.NumletType = G__WorkNumletType.Solve;
            numlet.IsAnswer = false;
            // Set Parent
            numlet.MyWorkSpaceParent = this._vcCurrentWorkSpace;
            numlet.MyImmediateParent = this._vcCurrentWorkSpace;
            // Sizing
            G__NumberDisplaySize _displaySize;
            float _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
            float _xPos = _xSpacing;
            float _yPos = (_xSpacing/2.0f);

            // Hook up the control resizing events so that all controls are messaged by this numlet
            numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

            // Most of these should ApplyUI in ViewWillAppear
            _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 

            _control.SizeClass.SetPositions(_xPos, _yPos);
            numlet.AddAndDisplayController(_control);

            // Pad out the end
            numlet.SizeClass.CurrentWidth = this._globalSizeDimensions.GlobalNumberWidth + (2 * _xSpacing); 
            numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

            // Return completed numnut!
            // Numlet has no size params set. SetPositions must be called before use!
            return numlet;
        }

        /// <summary>
        /// Builds an list<object> of our Equation from a string expression to an Editable UI.
        /// </summary>
        /// <param name="_strExpression">String expression.</param>
        private void EquationStringToObjects(string _strExpression)
        {
            this._uiOutputEquation = this._expressionFactory.CreateExpressionEquation(_strExpression, false);
        }

        private void ResultStringToObjects(string _strResult)
        {
            this._uiOutputResult = this._expressionFactory.CreateExpressionEquation(_strResult, false);
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

