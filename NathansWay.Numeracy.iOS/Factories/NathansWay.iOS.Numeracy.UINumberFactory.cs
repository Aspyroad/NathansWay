// System
using System;
using System.Collections.Generic;
// Mono
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
// Shared
using NathansWay.Numeracy.Shared.Factories;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.BUS.ViewModel;
using NathansWay.Numeracy.Shared;


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
        public List<object> UIOutputEquation { get; set; }

        private List<List<object>> _uiOutputMethods;
        private vcWorkSpace _vcCurrentWorkSpace;

        private iOSNumberDimensions _globalSizeDimensions;
        private G__DisplaySizeLevels _numberDisplaySize;
        private nfloat _fNumberSpacing;
        protected IAppSettings _numberAppSettings;

        // Data
        // TODO: These MUST be all populated for this class to work do we need security
        private EntityLesson _wsLesson;
        //private EntityLessonResults _wsLessonResults;
        private List<EntityLessonDetail> _wsLessonDetail;
        //private EntityLessonDetailResults _wsLessonDetailResults;
        private nint _intLessonDetailSeq;

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
            //throw new NotImplementedException();
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

        //public vcWorkNumlet GetEquationNumlet(string _strEquation)
        //{            
        //    var _vcNumletEquation = this.CreateNumletEquation(_strEquation);
        //    return _vcNumletEquation;
        //}

        //public vcWorkNumlet GetResultNumlet(string _strResult)
        //{
        //    var _vcNumletResult = this.CreateNumletResult(_strResult);
        //    return _vcNumletResult;
        //}

        //public vcWorkNumlet GetSolveNumlet()
        //{
        //    var _vcNumletSolve = this.CreateNumletSolve();
        //    return _vcNumletSolve;
        //}

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

        public nint LessonDetailSeq
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

        public void CreateNumletEquation(string _strEquation, vcWorkNumlet numlet)
        {            
            // Create all our expression symbols, numbers etc
            this.StringToObjects(_strEquation);

            // Setup the numlet

            //var numlet = new vcWorkNumlet();
            //numlet.NumletType = G__WorkNumletType.Equation;



            //// Set Parent
            //numlet.MyWorkSpaceParent = this._vcCurrentWorkSpace;
            //numlet.MyImmediateParent = this._vcCurrentWorkSpace;
            //numlet.OutputContainers = this._uiOutputEquation;
            // Sizing
            //G__NumberDisplaySize _displaySize;
            nfloat _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
            nfloat _xPos = _xSpacing;

            for (int i = 0; i < this.UIOutputEquation.Count; i++)
            {                
                var _control = (BaseContainer)this.UIOutputEquation[i];
                // Set Parents
                _control.MyNumletParent = numlet;
                _control.MyImmediateParent = numlet;
                _control.MyWorkSpaceParent = this._vcCurrentWorkSpace;

                // Let the numlet know its the answer
                if (_control.IsAnswer)
                {
                    numlet.OutputAnswerContainers.Add(_control);
                    numlet.IsAnswer = true;
                    numlet.IsReadOnly = false;
                }

                _control.SizeClass.SetCenterRelativeParentViewPosY = true;

                // Event Hooks ************************************************************************
                // Value and selection changes - FLOW - FROM CONTROL UP TO NUMLET
                _control.eValueChanged += numlet.OnValueChange;

                // Hook up the control resizing - FLOW - FROM NUMLET DOWN TO CONTROL
                numlet.eSizeChanged += _control.OnSizeChange;
                numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

                // Most of these should ApplyUI in ViewWillAppear
                _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 

                _control.SizeClass.SetViewPosition(_xPos, this._globalSizeDimensions.NumletHeight);

                // DEATH!
                // This is the cause of the problem. The NUMLET! hasnt been added to the view heirachy...
                //numlet.AddAndDisplayController(_control);

                // OK here.. what do we do?
                // Do I keep the this._uiOutputEquation object as it technically has all the controls
                // And the sizing done.
                // OR
                // Do I create a new List<object> of the newly changed objects. IM thinking th later


                _xPos = _xPos + _control.SizeClass.CurrentWidth + _xSpacing;
            }

            // Event Hooks ************************************************************************
            // Value and selection changes - FLOW - FROM NUMLET UP TO WORKSPACE
            numlet.eValueChanged += this._vcCurrentWorkSpace.OnValueChange;

            // Resizing - FLOW - FROM WORKSPACE DOWN TO NUMLET
            this._vcCurrentWorkSpace.eSizeChanged += numlet.OnSizeChange;
            this._vcCurrentWorkSpace.SizeClass.eResizing += numlet.SizeClass.OnResize;

            // Pad out the end
            numlet.SizeClass.CurrentWidth = _xPos; 
            numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

            // Shouldnt need to return anything, this will be fired form Numlet
            // Return completed numnut!
            // Numlet has no size params set. SetPositions must be called before use!
            //return numlet;
        }

        private void CreateNumletMethods(string _strExpression)
        {
//            // Create all our expression symbols, numbers etc
//            this.ExpressionToUIEditable(_strExpression);
//
//            // Setup the numlet
//            var numlet = new vcWorkNumlet();
//            numlet.NumletType = G__WorkNumletType.Equation;
//
//            G__NumberDisplaySize _displaySize;
//            nfloat _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
//            nfloat _xPos = _xSpacing;
//            nfloat _yPos = this._globalSizeDimensions.NumletHeight;
//
//            for (nint i = 0; i < this._uiOutputEquation.Count; i++)
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
              //return new List<vcWorkNumlet>();
        }

        //private void CreateNumletResult(string _strResult)
        //{
        //    // Create all our expression symbols, numbers etc
        //    // REMEMBER:  do we add the equals sign here?? Not sure
        //    _strResult = ("=," + _strResult);
        //    // Build...
        //    this.ResultStringToObjects(_strResult);

        //    // Setup the numlet
        //    var numlet = new vcWorkNumlet();
        //    numlet.NumletType = G__WorkNumletType.Result;

        //    // Set Parent
        //    numlet.MyWorkSpaceParent = this._vcCurrentWorkSpace;
        //    numlet.MyImmediateParent = this._vcCurrentWorkSpace;
        //    numlet.OutputContainers = this._uiOutputResult;
        //    // Sizing
        //    //G__NumberDisplaySize _displaySize;
        //    nfloat _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
        //    nfloat _xPos = _xSpacing;

        //    for (int i = 0; i < this._uiOutputResult.Count; i++)
        //    {                
        //        var _control = (BaseContainer)this._uiOutputResult[i];
        //        // Set Parents
        //        _control.MyNumletParent = numlet;
        //        _control.MyImmediateParent = numlet;
        //        _control.MyWorkSpaceParent = this._vcCurrentWorkSpace;
        //        _control.SizeClass.SetCenterRelativeParentViewPosY = true;
        //        //_control.CurrentEditMode = this._numberAppSettings.GA__NumberEditMode;

        //        // Let the numlet know its the answer
        //        if (_control.IsAnswer)
        //        {
        //            numlet.OutputAnswerContainers.Add(_control);
        //            numlet.IsAnswer = true;
        //            numlet.IsReadOnly = false;
        //        }

        //        // Event Hooks ************************************************************************
        //        // Value and selection changes - FLOW - FROM CONTROL UP TO NUMLET
        //        _control.eValueChanged += numlet.OnValueChange;

        //        // Hook up the control resizing - FLOW - FROM NUMLET DOWN TO CONTROL
        //        numlet.eSizeChanged += _control.OnSizeChange;
        //        numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

        //        // Most of these should ApplyUI in ViewWillAppear
        //        _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 

        //        _control.SizeClass.SetViewPosition(_xPos, this._globalSizeDimensions.NumletHeight);
        //        numlet.AddAndDisplayController(_control);
        //        _xPos = _xPos + _control.SizeClass.CurrentWidth + _xSpacing;
        //    }

        //    // Event Hooks ************************************************************************
        //    // Value and selection changes - FLOW - FROM NUMLET UP TO WORKSPACE
        //    numlet.eValueChanged += this._vcCurrentWorkSpace.OnValueChange;

        //    // Resizing - FLOW - FROM WORKSPACE DOWN TO NUMLET
        //    this._vcCurrentWorkSpace.eSizeChanged += numlet.OnSizeChange;
        //    this._vcCurrentWorkSpace.SizeClass.eResizing += numlet.SizeClass.OnResize;

        //    // Pad out the end
        //    numlet.SizeClass.CurrentWidth = _xPos; 
        //    numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

        //    // Return completed numnut!
        //    // Numlet has no size params set. SetPositions must be called before use!
        //    // return numlet;
        //}

        private void CreateNumletSolve(vcWorkNumlet numlet)
        {
            // Setup the numlet
            //var numlet = new vcWorkNumlet();
            var _control = new vcSolveContainer();

            numlet.NumletType = G__WorkNumletType.Solve;
            numlet.IsAnswer = false;
            // Set Parent
            numlet.MyWorkSpaceParent = this._vcCurrentWorkSpace;
            numlet.MyImmediateParent = this._vcCurrentWorkSpace;

            _control.MyNumletParent = numlet;
            _control.MyImmediateParent = numlet;
            _control.MyWorkSpaceParent = this._vcCurrentWorkSpace;

            // Sizing
            nfloat _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
            nfloat _xPos = _xSpacing;
            nfloat _yPos = (_xSpacing / 2.0f);

            // Hook up the control resizing events so that all controls are messaged by this numlet
            numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

            // Most of these should ApplyUI in ViewWillAppear
            _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear; 

            _control.SizeClass.SetViewPosition(_xPos, _yPos);

            // DEATH!
            //numlet.AddAndDisplayController(_control);

            // Pad out the end
            numlet.SizeClass.CurrentWidth = this._globalSizeDimensions.GlobalNumberWidth + (2 * _xSpacing); 
            numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

            // Return completed numnut!
            // Numlet has no size params set. SetPositions must be called before use!
            // return numlet;
        }

        private List<object> StringToObjects(string _strExpression)
        {
            return this.UIOutputEquation = this._expressionFactory.CreateExpressionEquation(_strExpression, false);
        }

        //private void EquationStringToObjects(string _strExpression)
        //{
        //    this._uiOutputEquation = this._expressionFactory.CreateExpressionEquation(_strExpression, false);
        //}
            
        //private void ResultStringToObjects(string _strResult)
        //{
        //    this._uiOutputResult = this._expressionFactory.CreateExpressionEquation(_strResult, false);
        //}
            
        private void ExpressionToUIReadOnly(string _strExpression)
        {
            this._uiOutputMethods = this._expressionFactory.CreateExpressionMethod(_strExpression, true);
        }

        //        private string GetEquation(nint Seq)
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

