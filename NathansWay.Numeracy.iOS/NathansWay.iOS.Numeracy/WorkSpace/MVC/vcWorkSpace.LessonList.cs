// System
using System;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
// Shared
using NathansWay.Numeracy.Shared.Factories;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.BUS.ViewModel;
using NathansWay.Numeracy.Shared;


namespace NathansWay.iOS.Numeracy.WorkSpace
{
    public class LessonNumletSet
    {
        public vcWorkNumlet vcNumletEquation { get; set; }
        public vcWorkNumlet vcNumletResult { get; set; }
        public List<vcWorkNumlet> vcNumletMethods { get; set; }

        //public EntityLesson Lesson { get; set; }
        // TODO : Use getter setters to setup our lessondetailresult
        private EntityLessonDetail _lessonDetail { get; set; }
        public EntityLessonDetailResults LessonDetailResults { get; set; }
        //public EntityLessonDetailResults LessonDetailResults { get; set; }

        private vcWorkSpace _myWorkSpaceParent;

        // Publics
        public bool NumletsLoaded { get; private set; }
        public bool ControlsLoaded { get; private set; }
        public string strExpression;
        // Expression breakdown
        public string strEquation;
        public string strMethods;
        public string strResult;

        public LessonNumletSet()
        {
            this.strEquation = "";
            this.strMethods = "";
            this.strResult = "";

            // Results
            this.LessonDetailResults = new EntityLessonDetailResults();
            this.ControlsLoaded = false;
        }

        public void LoadAllNumlets()
        {
            if (!this.NumletsLoaded)
            {
                this.vcNumletEquation = new vcWorkNumlet();
                this.vcNumletEquation.NumletType = G__WorkNumletType.Equation;

                this.vcNumletResult = new vcWorkNumlet();
                this.vcNumletResult.NumletType = G__WorkNumletType.Result;
                this.NumletsLoaded = true;
            }
        }

        public void LoadAllNumletControls()
        {
            this.vcNumletResult.LoadControls(strResult);
            this.vcNumletEquation.LoadControls(strEquation);
            // TODO: Load NumletMethods here
            this.ControlsLoaded = true;
        }

        public vcWorkSpace MyWorkSpaceParent
        {
            set
            {
                this._myWorkSpaceParent = value;
                this.vcNumletResult.MyImmediateParent = value;
                this.vcNumletResult.MyWorkSpaceParent = value;
                this.vcNumletEquation.MyImmediateParent = value;
                this.vcNumletEquation.MyWorkSpaceParent = value;

                if (vcNumletMethods != null)
                {
                    for (int i = 0; i < vcNumletMethods.Count; i++)
                    {
                        vcNumletMethods[i].MyWorkSpaceParent = value;
                        vcNumletMethods[i].MyImmediateParent = value;
                    }
                }
            }
            get
            {
                return this._myWorkSpaceParent;
            }
        }

        public EntityLessonDetail LessonDetail 
        { 
            get
            {
                return this._lessonDetail;
            }
            set
            {
                this._lessonDetail = value;
                this.strResult = value.Result;
                this.strMethods = value.Method;
                this.strEquation = value.Equation;
            }
        }
    }

    // Override List class for lessonList
    // http://stackoverflow.com/questions/22165015/how-to-override-list-add-method
    public class LessonList<T> : List<T> where T : LessonNumletSet
    {
        #region Variables

        private LessonNumletSet _currentLessonDetailSet;

        // Data
        public EntityLesson Lesson { get; set; }
        public EntityLessonResults LessonResults { get; set; }
        public List<EntityLessonDetail> LessonDetail { get; set; }
        public List<EntityLessonDetailResults> LessonDetailResults { get; set; }

        public LessonViewModel _lessonModel;

        public vcWorkSpace _myWorkSpaceParent;


        #endregion

        #region Constructor

        public LessonList()
        {
            this._lessonModel = SharedServiceContainer.Resolve<LessonViewModel>();
            this.Initialize();
        }

        public LessonList(LessonViewModel lessonModel)
        {
            this._lessonModel = lessonModel;
            this.Initialize();
        }

        #endregion

        #region Public Members

        public void Add (EntityLessonDetail _lessonDetail)
        {
            // Check if this lessondetail has been loaded
            if (!this.Any(y => y.LessonDetail.LessonSeq == _lessonDetail.LessonSeq))
            {
                // *******************
                // Creation of LessonNumletSet
                var x = new LessonNumletSet();
                x.MyWorkSpaceParent = this.MyWorkSpaceParent;
                x.LessonDetail = _lessonDetail;
            }
        }

        public void LoadAllLessonDetailSets()
        {
            // Walk through all controls in 
            for (int i = 0; i< this.LessonDetail.Count; i++)
            {
                var x = new LessonNumletSet();
                x.MyWorkSpaceParent = this.MyWorkSpaceParent;
                x.LessonDetail = this.LessonDetail[i];
                base.Add((T)x);
            }
        }

        public LessonNumletSet Next()
        {
            // Set a current Lesson 
            var intNextIndex = this.CurrentIndex + 1;
            if (intNextIndex > this.Count)
            {
                this._currentLessonDetailSet = this[this.Count];
            }
            else
            {
                this._currentLessonDetailSet = this[intNextIndex];
            }

            return this._currentLessonDetailSet;
        }

        public LessonNumletSet Prev()
        {
            // Set a current Lesson 
            var intNextIndex = this.CurrentIndex - 1;
            if (intNextIndex < 0)
            {
                this._currentLessonDetailSet = this[0];
            }
            else
            {
                this._currentLessonDetailSet = this[intNextIndex];
            }
            return this._currentLessonDetailSet;
        }

        #endregion

        #region Public Properties

        public vcWorkSpace MyWorkSpaceParent
        {
            set
            {
                this._myWorkSpaceParent = value;
            }
            get
            {
                return this._myWorkSpaceParent;
            }
        }

        public LessonNumletSet CurrentLessonDetailSet 
        { 
            // Always return the first element
            get
            {
                if (this._currentLessonDetailSet == null)
                {
                    this._currentLessonDetailSet = this[0];
                }

                if (!this._currentLessonDetailSet.NumletsLoaded)
                {
                    this._currentLessonDetailSet.LoadAllNumlets();
                }

                return this._currentLessonDetailSet;
            }
            //private set; 
        }

        public int CurrentIndex
        {
            get
            {
                return this.IndexOf((T)this._currentLessonDetailSet);
            }
        }


        #endregion

        #region Private Members

        private void Initialize()
        {
            this.Lesson = _lessonModel.SelectedLesson;
            this.LessonDetail = _lessonModel.LessonDetail;
            this.LessonDetail.Sort();
        }


        #endregion
    }
}
