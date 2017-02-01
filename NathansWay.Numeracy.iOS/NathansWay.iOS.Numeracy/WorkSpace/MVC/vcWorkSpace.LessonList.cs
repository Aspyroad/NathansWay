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
    public class LessonNumletSet : IComparable<LessonNumletSet>
    {
        public vcWorkNumlet vcNumletEquation { get; set; }
        public vcWorkNumlet vcNumletResult { get; set; }
        public List<vcWorkNumlet> vcNumletMethods { get; set; }

        public EntityLessonDetail _lessonDetail { get; set; }
        public EntityLessonDetailResults LessonDetailResults { get; set; }

        public vcWorkSpace MyWorkSpaceParent { get; set; }
        public LessonList<LessonNumletSet> MyLessonListParent { get; set; }

        // Publics
        public bool NumletsLoaded { get; private set; }
        public bool ControlsLoaded { get; private set; }
        public string lnsExpression;
        // Expression breakdown
        public string lnsEquation;
        public string lnsMethods;
        public string lnsResult;
        public G__SolveAttempted lnsAttempted { get; set; }

        public LessonNumletSet()
        {
            this.lnsEquation = "";
            this.lnsMethods = "";
            this.lnsResult = "";
            this.lnsAttempted = G__SolveAttempted.UnAttempted;

            // Results
            this.LessonDetailResults = new EntityLessonDetailResults();
            this.ControlsLoaded = false;
            this.NumletsLoaded = false;
        }

        public void LoadAllNumlets()
        {
            if (!this.NumletsLoaded)
            {
                this.vcNumletEquation = new vcWorkNumlet();
                this.vcNumletEquation.NumletType = G__WorkNumletType.Equation;

                this.vcNumletResult = new vcWorkNumlet();
                this.vcNumletResult.NumletType = G__WorkNumletType.Result;

                this.vcNumletResult.MyImmediateParent = this.MyWorkSpaceParent;
                this.vcNumletResult.MyWorkSpaceParent = this.MyWorkSpaceParent;
                this.vcNumletEquation.MyImmediateParent = this.MyWorkSpaceParent;
                this.vcNumletEquation.MyWorkSpaceParent = this.MyWorkSpaceParent;

                this.NumletsLoaded = true;
            }
        }

        public void LoadAllNumletControls()
        {
            if (!this.ControlsLoaded)
            {
                this.vcNumletResult.LoadControls(lnsResult);
                this.vcNumletEquation.LoadControls(lnsEquation);
                // TODO: Load NumletMethods here
                this.ControlsLoaded = true;
            }
        }

        public void ResetAll()
        {
            // TODO: Reset function
            // Reload all controls, clear all state and recreate an answer dataset
        }

        public int CompareTo(LessonNumletSet other)
        {
            return other._lessonDetail.SEQ.CompareTo(this._lessonDetail.SEQ);
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
                this.lnsResult = value.Result;
                this.lnsMethods = value.Method;
                this.lnsEquation = value.Equation;
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
        public List<EntityLessonDetail> LessonDetail { get; set; }

        public EntityLessonResults LessonResults { get; set; }
        public List<EntityLessonDetailResults> LessonDetailResults { get; set; }

        public LessonViewModel _lessonModel;
        public LessonResultsViewModel _lessonResultsModel;

        public vcWorkSpace _myWorkSpaceParent;
        public G__SortOrder sortOrder;
        public bool bOverIndex { get; private set; }

        #endregion

        #region Constructor

        public LessonList(vcWorkSpace _workSpace)
        {
            this._lessonModel = SharedServiceContainer.Resolve<LessonViewModel>();
            this._lessonResultsModel = SharedServiceContainer.Resolve<LessonResultsViewModel>();
            this._myWorkSpaceParent = _workSpace;
            this.Initialize();
        }

        public LessonList(LessonViewModel lessonsModel, LessonResultsViewModel lessonResultsModel, vcWorkSpace _workSpace)
        {
            this._myWorkSpaceParent = _workSpace;
            this._lessonModel = lessonsModel;
            this._lessonResultsModel = lessonResultsModel;
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
                x.MyLessonListParent = this as LessonList<LessonNumletSet>;
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
                x.MyLessonListParent = this as LessonList<LessonNumletSet>;
                x.LessonDetail = this.LessonDetail[i];
                base.Add((T)x);
            }
        }

        public void LoadLessonDetailSet(int _index)
        {
            var x = new LessonNumletSet();
            x.MyWorkSpaceParent = this.MyWorkSpaceParent;
            x.MyLessonListParent = this as LessonList<LessonNumletSet>;
            x.LessonDetail = this.LessonDetail[_index];
            this._currentLessonDetailSet = x;
            base.Add((T)x);
        }

        public LessonNumletSet Next()
        {
            // Set a current Lesson 
            var intNextIndex = this.CurrentIndex + 1;
            if (intNextIndex >= (this.Count))
            {
                this.bOverIndex = true;
                this._currentLessonDetailSet = this[this.Count - 1];
            }
            else
            {
                this.bOverIndex = false;
                this._currentLessonDetailSet = this[intNextIndex];
            }

            return this.CurrentLessonDetailSet;
        }

        public LessonNumletSet Prev()
        {
            // Set a current Lesson 
            var intNextIndex = this.CurrentIndex - 1;
            if (intNextIndex < 0)
            {
                this.bOverIndex = true;
                this._currentLessonDetailSet = this[0];
            }
            else
            {
                this.bOverIndex = false;
                this._currentLessonDetailSet = this[intNextIndex];
            }
            return this.CurrentLessonDetailSet;
        }

        public void OrderLessons(G__SortOrder _order)
        {
            if (_order == G__SortOrder.Ascending)
            {
                this.OrderBy(d => d); 
            }
            else
            {
                this.OrderByDescending(d => d);
            }
        }

        public LessonNumletSet FindByLessonSeq(int _seq)
        {
            return this.Find(x => x.LessonDetail.LessonSeq == _seq);
        }

        public void CreateCurrentLessonResult()
        {

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
                // Init - only run at on first access.
                if (this._currentLessonDetailSet == null)
                {
                    //this._currentLessonDetailSet = this.Min();
                    this._currentLessonDetailSet = this[0];
                }

                if (!this._currentLessonDetailSet.NumletsLoaded)
                {
                    this._currentLessonDetailSet.LoadAllNumlets();
                }
                if (!this._currentLessonDetailSet.ControlsLoaded)
                {
                    this._currentLessonDetailSet.LoadAllNumletControls();
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

        public int LessonIndex
        {
            get
            {
                return this._currentLessonDetailSet.LessonDetail.LessonSeq;
            }
        }

        public int CurrentCorrect
        {
            get
            {
                LessonNumletSet x;
                int y = 0;

                for (int i = 0; i < this.Count; i++)
                {
                    x = this[i];
                    if (x.LessonDetailResults.ScoreEquation || x.LessonDetailResults.ScoreMethod || x.LessonDetailResults.ScoreResult)
                    {
                        y++;
                    }
                };

                return y;
            }
        }

        public int CurrentInCorrect
        {
            get
            {
                LessonNumletSet x;
                int y = 0;

                for (int i = 0; i < this.Count; i++)
                {
                    x = this[i];
                    if (!x.LessonDetailResults.ScoreEquation || !x.LessonDetailResults.ScoreMethod || !x.LessonDetailResults.ScoreResult)
                    {
                        y++;
                    }
                };

                return y;
            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.Lesson = _lessonModel.SelectedLesson;
            this.LessonDetail = _lessonModel.LessonDetail;
            this.sortOrder = G__SortOrder.Descending;
            //this.sortOrder = G__SortOrder.Ascending;
            this.bOverIndex = false;
            this.OrderLessons(this.sortOrder);
            this.LoadAllLessonDetailSets();
        }


        #endregion
    }
}
