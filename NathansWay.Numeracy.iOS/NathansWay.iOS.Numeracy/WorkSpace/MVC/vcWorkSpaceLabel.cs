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
    [Register("vcWorkSpaceLabel")]
	public class vcWorkSpaceLabel : BaseContainer
	{
		#region Private Variables
		// Control
        private ExpressionFactory _expressionFactory;
        private string _strExpression;
        private NumberFactoryClient _numberFactoryClient;

        // Data
        private EntityLesson _wsLesson;
        private EntityLessonDetail _wsLessonDetail;

        // UI
        private SizeWorkSpaceLabel _sizeWorkSpaceLabel;
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
			this.AspyTag1 = 600110;
			this.AspyName = "VC_WorkSpaceLabel";
            // Size Class Init
            this._sizeWorkSpaceLabel = new SizeWorkSpaceLabel(this);
            this._sizeClass = this._sizeWorkSpaceLabel;
            // Factory Classes for expression building
            this._numberFactoryClient = new NumberFactoryClient();
            this._expressionFactory = new ExpressionFactory(_numberFactoryClient);
		}

		#endregion

        #region Public Members


        #endregion

        #region Public Properties

        public SizeWorkSpaceLabel WorkSpaceLabelSize 
        {
            get { return (SizeWorkSpaceLabel)this._sizeClass; }
        }

        public EntityLesson WsLesson
        {
            get { return this._wsLesson; }
            set { this._wsLesson = value; }
        }

        public EntityLessonDetail WsLessonDetail
        {
            get { return this._wsLessonDetail; }
            set { this._wsLessonDetail = value; }
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

    public class SizeWorkSpaceLabel : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeWorkSpaceLabel()
        {
            Initialize();
        }

        public SizeWorkSpaceLabel(BaseContainer _vc) : base (_vc)
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

