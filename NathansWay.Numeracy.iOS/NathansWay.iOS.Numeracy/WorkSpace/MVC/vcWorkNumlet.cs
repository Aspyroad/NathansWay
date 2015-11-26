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
	[Register("vcWorkNumlet")]
	public class vcWorkNumlet : BaseContainer
	{
		#region Private Variables
		// Control
        private G__WorkNumletType _workNumletType;
        private string _strExpression;
        private List<object> _lsContainers;

        // Data
        private EntityLesson _wsLesson;
        //private EntityLessonResults _wsLessonResults;
        private EntityLessonDetail _wsLessonDetail;
        //private EntityLessonDetailResults _wsLessonDetailResults;

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

        #region Deconstruction

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {                
                // Remove the possible event hook to sizechange.
                foreach (vcNumberText _Container in this._lsContainers) 
                {
                    // Event Hooks
                    _Container.SizeClass.eResizing -= _Container.SizeClass.OnResize;
                }
            }
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
            this.HasRoundedCorners = true;
		}

		#endregion

        #region Public Members



        #endregion

        #region Public Properties

        public List<object> OutputContainers
        {
            get { return this._lsContainers; }
            set { this._lsContainers = value; }
        }

        public SizeWorkNumlet WorkNumletSize 
        {
            get { return (SizeWorkNumlet)this._sizeClass; }
        }

        public G__WorkNumletType NumletType
        {
            get { return this._workNumletType;}
            set { this._workNumletType = value;}
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

        public EntityLessonDetail WsLessonDetail
        {
            get { return this._wsLessonDetail; }
            set { this._wsLessonDetail = value; }
        }

//        public EntityLessonDetailResults WsLessonDetailResults
//        {
//            get { return this._wsLessonDetailResults; }
//            set { this._wsLessonDetailResults = value; }
//        }

        public string ExpressionString 
        { 
            get { return this._wsLessonDetail.Equation.ToString(); } 
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
            this.SizeClass.SetFrames();
            this.View.BackgroundColor = UIColor.White;
            this.HasBorder = true;
            this.SetBorderColor = UIColor.Black;
            this.BorderWidth = 5.0f;
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        public override void OnControlSelectedChange()
        {
            base.OnControlSelectedChange();

            // Release any UI to children losing select
            if (this.MyWorkSpaceContainer.SelectedContainer != null)
            {
                this.MyWorkSpaceContainer.SelectedContainer.OnControlUnSelectedChange();
            }

            // Let WorkSpace know whos the boss
            this.MyWorkSpaceContainer.SelectedContainer = this;

            // UI Changes
            this.View.BackgroundColor = UIColor.LightGray;

            // Parent call 
            this.MyWorkSpaceContainer.OnControlSelectedChange();

        }

        public override void OnControlUnSelectedChange()
        {  
            base.OnControlUnSelectedChange();

            // Release any UI to children losing select
            if (this.SelectedContainer != null)
            {
                this.SelectedContainer.OnControlUnSelectedChange();
            }

            // UI Changes
            this.View.BackgroundColor = UIColor.White;

            // Clear out the child
            this.MyWorkSpaceContainer.SelectedContainer = null;

            // Parent call 
            this.MyWorkSpaceContainer.OnControlUnSelectedChange();
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
            this._fCurrentHeight = this.GlobalSizeDimensions.NumletHeight;
        }

        #endregion
    }
}

