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
using NathansWay.Shared.Factories;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcWorkNumlet")]
	public class vcWorkNumlet : BaseContainer
	{
		#region Private Variables
		
        private G__WorkWidgetType _workWidgetType;
        private ExpressionFactory _expressionFactory;
        private NumberFactoryClient _numberFactoryClient;

        private EntityLesson _wsLesson;
        private EntityLessonResults _wsLessonResults;
        private EntityLessonDetail _wsLessonDetail;
        private EntityLessonDetailResults _wsLessonDetailResults;

        private string _strExpression;
        private SizeWorkWidget _sizeWorkWidget;

		#endregion

		#region Constructors

        public vcWorkNumlet(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
        public vcWorkNumlet(NSCoder coder) : base(coder)
		{
			Initialize();
		}

        public vcWorkNumlet(string _expression) 
        {            
            this._strExpression = _expression;
            Initialize();
        }

        public vcWorkNumlet() 
		{   
			Initialize();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
			this.AspyTag1 = 60024;
			this.AspyName = "VC_WorkWidget";
            // Size Class Init
            this._sizeWorkWidget = new SizeWorkWidget(this);
            this._sizeClass = this._sizeWorkWidget;
            // Factory Classes for expression building
            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(_numberFactoryClient);
		}

		#endregion

        #region Public Members

        public void LoadExpression(string _strExpression)
        {
            this._expressionFactory.CreateExpressionEquation(_strExpression);
        }

        public void BuildExpression(List<object> UIInternalOutput)
        {
            if (this.SizeClass.CurrentHeight <= 0.0f)
            {
                // Cant set sizes without WorkSpace Startpoint
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

        public SizeWorkWidget WorkWidgetSize 
        {
            get { return (SizeWorkWidget)this._sizeClass; }
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
                this.BuildExpression(this._expressionFactory.UIOutputEquation);
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
            //this.View.BackgroundColor = UIColor.White;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

		#endregion
	}

    public class SizeWorkWidget : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeWorkWidget()
        {
            Initialize();
        }

        public SizeWorkWidget(BaseContainer _vc) : base (_vc)
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

