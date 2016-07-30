// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
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
        private string _strExpression;
        private bool _bDataPassedIn = false;

        // Data
        private EntityLesson _wsLesson;
        private EntityLessonDetail _wsLessonDetail;
        // There is no need for results here

        // UI
        private SizeWorkSpaceLabel _sizeWorkSpaceLabel;
        private G__NumberDisplaySize _numberDisplaySize;

		#endregion

		#region Constructors

        public vcWorkSpaceLabel (IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
        public vcWorkSpaceLabel (NSCoder coder) : base(coder)
		{
			Initialize();
		}

        public vcWorkSpaceLabel (string _expression) 
        {            
            this._strExpression = _expression;
            Initialize();
        }

        public vcWorkSpaceLabel (EntityLesson wsLesson, EntityLessonDetail wsLessonDetail) 
        {            
            this._wsLesson = wsLesson;
            this._wsLessonDetail = wsLessonDetail;
            // Logic so we know this constructor was used
            // This may not be needed but it "may" be handy in the future depending on "where I create this class
            this._bDataPassedIn = true;
            Initialize();
        }
         
        public vcWorkSpaceLabel () 
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
		}

		#endregion

        #region Public Members

        public void CreateWorkSpace ()
        {
            // TODO : Implement the creatyion of workspace numlets and position them
            throw new NotImplementedException();
        }

        public void ChangeDisplaySize(G__NumberDisplaySize numberlabeldisplaysize)
        {


        }

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
                this._numberAppSettings.GA__NumberLabelDisplaySize = value;
                this.ChangeDisplaySize(value);
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

        public override void SetSubHeightWidthPositions ()
        {
            // We have no fuckin idea how big this will be!!!!!
        }

        #endregion
    }
}

