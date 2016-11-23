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
        public EntityLessonDetail LessonDetail { get; set; }
        public EntityLessonResults LessonResults { get; set; }
        //public EntityLessonDetailResults LessonDetailResults { get; set; }


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
            this.vcNumletResult = new vcWorkNumlet();
            this.vcNumletMethods = new List<vcWorkNumlet>();
        }
    }

    // Override List class for lessonList
    // http://stackoverflow.com/questions/22165015/how-to-override-list-add-method
    public class LessonList<LessonNumletSet> : List<LessonNumletSet>
    {
        #region Variables

        // Data
        public EntityLesson Lesson { get; set; }
        public EntityLessonResults LessonResults { get; set; }
        public List<EntityLessonDetail> LessonDetail { get; set; }
        public List<EntityLessonDetailResults> LessonDetailResults { get; set; }

        #endregion

        public LessonList(LessonViewModel _lesson)
        {
            this.Lesson = _lesson;

        }

        public LessonNumletSet Add(LessonNumletSet _numletSet)
        {
            return new LessonNumletSet();
        }

        public LessonNumletSet Next()
        {
            return new LessonNumletSet();
        }

        public LessonNumletSet Prev()
        {
            return new LessonNumletSet();
        }
    }
}
