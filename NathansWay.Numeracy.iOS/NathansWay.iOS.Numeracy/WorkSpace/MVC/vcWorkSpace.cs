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

        //private SizeWorkSpace _sizeClass;

        public string strTestExpression;

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

            //this._workSpaceSize = new SizeWorkSpace(this);
            this._sizeClass = new SizeWorkSpace(this);

            this.strTestExpression = "1.2,+,1,=,13";

            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(_numberFactoryClient,
                this._sizeClass.GlobalSizeDimensions.Size);

            //this.LoadExpression(this.strTestExpression);
		}

		#endregion

        #region Public Members

        public void LoadExpression(string _strExpression)
        {
            this._expressionFactory.CreateExpression(_strExpression);

            for (int i = 0; i < this._expressionFactory.UIOutput.Count; i++) // Loop with for.
            {
                var x = (BaseContainer)this._expressionFactory.UIOutput[i];
                // TODO : We need to call RefreshDisplay on x here!!
                this.AddAndDisplayController(x);
            }
        }

        #endregion

        #region Public Properties

        public SizeNumberContainer NumberContainerSize 
        {
            get { return (SizeNumberContainer)this._sizeClass; }
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

        #endregion

		#region Overrides

		public override void WillMoveToParentViewController (UIViewController parent)
		{
			base.WillMoveToParentViewController (parent);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void LoadView()
		{
			base.LoadView();            
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.LoadExpression(this.strTestExpression);
            // Pretty UI to see the view
            this.View.BackgroundColor = UIColor.Blue;
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
            this.CurrentHeigth = this.GlobalSizeDimensions.GlobalWorkSpaceHeight;
        }

        public override void SetScale (int _scale)
        {
            base.SetScale(_scale);
            //var x = _vc.txtNumber.Font.PointSize;
            //x = x + 50.0f;
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
            //_vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
        }

        public override void RefreshDisplay (PointF _startPoint)
        {
            base.RefreshDisplay(_startPoint);
        }

        public override void SetMainFrame ()
        {
            base.SetMainFrame();
        }

        #endregion
    }
}

