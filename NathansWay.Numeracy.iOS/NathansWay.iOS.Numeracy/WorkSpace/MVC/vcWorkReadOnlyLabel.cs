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
		// Control
        private G__WorkNumletType _workNumletType;
        private ExpressionFactory _expressionFactory;
        private string _strExpression;
        private NumberFactoryClient _numberFactoryClient;

        // Data
        private EntityLesson _wsLesson;
        private EntityLessonResults _wsLessonResults;
        private EntityLessonDetail _wsLessonDetail;
        private EntityLessonDetailResults _wsLessonDetailResults;

        // UI
        private SizeWorkNumlet _sizeWorkNumlet;
        private G__NumberDisplaySize _numberDisplaySize;

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
			this.AspyName = "VC_WorkNumlet";
            // Size Class Init
            this._sizeWorkNumlet = new SizeWorkNumlet(this);
            this._sizeClass = this._sizeWorkNumlet;
            // Factory Classes for expression building
            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(_numberFactoryClient);
		}

		#endregion

        #region Public Members

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

        public SizeWorkNumlet WorkNumletSize 
        {
            get { return (SizeWorkNumlet)this._sizeClass; }
        }

        public ExpressionFactory ExpressFactory
        {
            get { return this._expressionFactory; }
            set { this._expressionFactory = value; }
        }

        public EntityLesson WsLesson
        {
            get { return this._wsLesson; }
            set { this._wsLesson = value; }
        }

        public EntityLessonResults WsLessonResults
        {
            get { return this._wsLessonResults; }
            set { this._wsLessonResults = value; }
        }

        public EntityLessonDetail WsLessonDetail
        {
            get { return this._wsLessonDetail; }
            set { this._wsLessonDetail = value; }
        }

        public EntityLessonDetailResults WsLessonDetailResults
        {
            get { return this._wsLessonDetailResults; }
            set { this._wsLessonDetailResults = value; }
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

        public G__NumberDisplaySize  NumberDisplaySize
        {
            get { return this._numberDisplaySize; }
            set 
            { 
                this._numberDisplaySize = value; 

                // TODO : We need to message all controls under this numlet to let them know the size has changed.
            }
        }


        #endregion

		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
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

    public class SizeWorkNumlet : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeWorkNumlet()
        {
            Initialize();
        }

        public SizeWorkNumlet(BaseContainer _vc) : base (_vc)
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
            // We have no fuckin idea how big this will be!!!!!
        }

        #endregion
    }
}

