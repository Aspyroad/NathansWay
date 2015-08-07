﻿// System
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
using NathansWay.Shared.Factories;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcWorkSpace")]
	public class vcWorkSpace : BaseContainer
	{
		#region Private Variables
		
        private ExpressionFactory _expressionFactory;
        private NumberFactoryClient _numberFactoryClient;

        private EntityLesson _wsLesson;
        private EntityLessonResults _wsLessonResults;
        private EntityLessonDetail _wsLessonDetail;
        private EntityLessonDetailResults _wsLessonDetailResults;

        private string _strExpression;
        private SizeWorkSpace _sizeWorkSpace;

        private List<vcWorkWidget> _lsWorkWidgets;

		#endregion

		#region Constructors

		public vcWorkSpace(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public vcWorkSpace(NSCoder coder) : base(coder)
		{
			Initialize();
		}

        public vcWorkSpace(EntityLesson _entLesson, EntityLessonDetail _LessonDetail) 
        {
            _wsLesson = _entLesson;
            _wsLessonDetail = _LessonDetail;
            Initialize();
        }

		public vcWorkSpace() 
		{   
			Initialize();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
			this.AspyTag1 = 60022;
			this.AspyName = "VC_WorkSpace";
            // Size Class Init
            this._sizeWorkSpace = new SizeWorkSpace(this);
            this._sizeClass = this._sizeWorkSpace;
            // Factory Classes for expression building
            // Number factory client is platform specific.
            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(_numberFactoryClient);

		}

		#endregion

        #region Public Members

        public void LoadLessonData(int _entLessonSeq)
        {
            
        }

        public void LoadLessonExpression()
        {
            /* 
             *  Grab the lesson dataset
             *  Grab the lesson detail dataset
             *  Create a lesson and lesson detail results records
             * 
             *  Load the current lesson and lesson detail into widgets
             * 
             *  
             *  If the user cancels half way thru the lesson we need to close off
             *  the lesson result sets. Shoudl they be able to pause them and reload?
             *  I think this may be needed, what id the kid has an episode/toilet etc
             * */
            
        }

        public void BuildExpressionEquationWidget()
        {
            float _XPos = this.SizeClass.GlobalSizeDimensions.GlobalWorkSpaceNumberSpacing;
                
            for (int i = 0; i < UIInternalOutput.Count; i++) // Loop with for.
            {
                var _control = (BaseContainer)UIInternalOutput[i];
                _control.SizeClass.SetCenterRelativeParentVcPosY = true;
                // This call only calls the BASE SetPositions not any derives
                // You may need to call any frame creation methods in the 
                // controls ViewWillApppear method
                _control.SizeClass.SetPositions(_XPos, this.SizeClass.CurrentHeight);
                //_control.SizeClass.StartPoint = new PointF(_XPos, this.SizeClass.CurrentHeight);
                this.AddAndDisplayController(_control);
                _XPos = _XPos + 4.0f + _control.SizeClass.CurrentWidth;
            }
        }

        public void BuildExpressionMethodWidgets()
        {
            if (this.SizeClass.CurrentHeight <= 0.0f)
            {
                // TODO : Is this needed
                // Cant set sizes without WorkSpace Startpoint
                // I have no idea why I wpuld be here if it wasnt created?
                return;
            }

            // TODO : Local horizontal position. Do we need a buffer/padding??
            float _XPos = 2.0f;

            for (int i = 0; i < UIInternalOutput.Count; i++) // Loop with for.
            {
                var _control = (BaseContainer)UIInternalOutput[i];
                _control.SizeClass.SetCenterRelativeParentVcPosY = true;
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

        public SizeWorkSpace WorkSpaceSize 
        {
            get { return (SizeWorkSpace)this._sizeClass; }
        }

        public ExpressionFactory ExpressFactory
        {
            get { return _expressionFactory; }
            set { _expressionFactory = value; }
        }

        public EntityLesson WsLesson
        {
            get { return _wsLesson; }
            set { WsLesson = value; }
        }

        public EntityLessonResults WsLessonResults
        {
            get { return _wsLessonResults; }
            set { WsLessonResults = value; }
        }

        public EntityLessonDetail WsLessonDetail
        {
            get { return _wsLessonDetail; }
            set { WsLessonDetail = value; }
        }

        public EntityLessonDetailResults WsLessonDetailResults
        {
            get { return _wsLessonDetailResults; }
            set { WsLessonDetailResults = value; }
        }

        public string ExpressionString 
        { 
            get { return this._strExpression; } 
            set 
            { 
                this._strExpression = value; 
                // Build our expression

                //this.BuildExpression(this._expressionFactory.UIOutput);
            }
        }

        #endregion

		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void LoadView()
		{
			base.LoadView(); 
            this.View.BackgroundColor = UIColor.White;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            // UI
            this.HasRoundedCorners = true;
            this.CornerRadius = 5.0f;
            this.HasBorder = true;
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

		#endregion
	}

    public class SizeWorkSpace : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeWorkSpace()
        {
            Initialize();
        }

        public SizeWorkSpace(BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Overrides

        public override void SetHeightWidth ()
        {
            this.CurrentWidth = this.GlobalSizeDimensions.GlobalWorkSpaceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalWorkSpaceHeight;
        }

        #endregion
    }
}

