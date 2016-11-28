// System
using System;
using CoreGraphics;
using System.Collections.Generic;
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
        private EntityLessonDetail _lessonDetail { get; set; }
        public EntityLessonResults LessonResults { get; set; }
        //public EntityLessonDetailResults LessonDetailResults { get; set; }

        private vcWorkSpace _myWorkSpaceParent;


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

            this.vcNumletEquation = new vcWorkNumlet();
            this.vcNumletEquation.NumletType = G__WorkNumletType.Equation;

            this.vcNumletResult = new vcWorkNumlet();
            this.vcNumletResult.NumletType = G__WorkNumletType.Result;

            //this.vcNumletMethods = new List<vcWorkNumlet>();
        }

        public void LoadAllNumlets()
        {
            this.vcNumletResult.LoadControlsResult(strResult);
            this.vcNumletEquation.LoadControlsEquation(strEquation);
            // TODO: Load NumletMethods here
        }

        public vcWorkSpace MyWorkSpaceParent
        {
            set
            {
                this._myWorkSpaceParent = value;
                this.vcNumletResult.MyWorkSpaceParent = value;
                this.vcNumletEquation.MyWorkSpaceParent = value;

                if (vcNumletMethods != null)
                {
                    for (int i = 0; i < vcNumletMethods.Count; i++)
                    {
                        vcNumletMethods[i].MyWorkSpaceParent = value;
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
    public class LessonList<TLessonNumletSet> : List<TLessonNumletSet> where TLessonNumletSet : LessonNumletSet
    {
        #region Variables

        // Data
        public EntityLesson Lesson { get; set; }
        public EntityLessonResults LessonResults { get; set; }
        public List<EntityLessonDetail> LessonDetail { get; set; }
        public List<EntityLessonDetailResults> LessonDetailResults { get; set; }

        public LessonViewModel _lessonModel;

        public vcWorkSpace MyWorkSpaceParent { get; set; }


        #endregion

        #region Constructor

        public LessonList()
        {
            this._lessonModel = SharedServiceContainer.Resolve<LessonViewModel>();
            this.Lesson = _lessonModel.SelectedLesson;
            this.LessonDetail = _lessonModel.LessonDetail;
            this.LessonDetail.Sort();
            this.Initialize();
        }

        public LessonList(LessonViewModel lessonModel)
        {
            this._lessonModel = lessonModel;
            this.Lesson = this._lessonModel.SelectedLesson;
            this.LessonDetail = _lessonModel.LessonDetail;
            this.LessonDetail.Sort();
            this.Initialize();
        }

        #endregion

        #region Public Members

        public void Add (LessonNumletSet _numletSet)
        {
            //return new LessonNumletSet();
        }

        public LessonNumletSet Next()
        {
            // Set a current Lesson   
            return new LessonNumletSet();
        }

        public LessonNumletSet Prev()
        {
            // Set a current Lesson
            return new LessonNumletSet();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            var x = new LessonNumletSet();
            x.MyWorkSpaceParent = this.MyWorkSpaceParent;
            // Grab the first lesson
            x.LessonDetail = this.LessonDetail[0];
            //x.MyWorkSpaceParent.CurrentLessonDetail = x.LessonDetail;
            //x.LoadAllNumlets();
        }


        #endregion
    }
}
