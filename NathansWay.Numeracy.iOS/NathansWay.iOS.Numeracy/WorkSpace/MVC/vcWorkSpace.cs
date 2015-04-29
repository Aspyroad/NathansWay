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

        public vcWorkSpace(string _expression) 
        {   
            // TODO : Not sure about this? Bitch to build at init...without Startpoint?
            this._strExpression = _expression;
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
			//base.Initialize ();
			this.AspyTag1 = 60022;
			this.AspyName = "VC_WorkSpace";

            this._sizeClass = new SizeWorkSpace(this);           

            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(_numberFactoryClient);

		}

		#endregion

        #region Public Members

        public void LoadExpression(string _strExpression)
        {
            this._expressionFactory.CreateExpression(_strExpression);
        }

        public void BuildExpression(List<object> UIInternalOutput)
        {
            if (this.SizeClass.CurrentHeight <= 0.0f)
            {
                // Cant set sizes without WorkSpace Startpoint
                return;
            }

            // Local horizontal position. Do we need a buffer.
            float _XPos = 2.0f;
                
            for (int i = 0; i < UIInternalOutput.Count; i++) // Loop with for.
            {
                var _control = (BaseContainer)UIInternalOutput[i];

                _control.SizeClass.RefreshDisplayAndPosition(_XPos, this.SizeClass.CurrentHeight);
                this.AddAndDisplayController(_control);
                _XPos = _XPos + 1.0f + _control.SizeClass.CurrentWidth;
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
                this.LoadExpression(value);
                this.BuildExpression(this._expressionFactory.UIOutput);
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
            this.View.BackgroundColor = UIColor.Blue;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            // Build our expression
            this.LoadExpression(this._strExpression);
            this.BuildExpression(_expressionFactory.UIOutput);
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

