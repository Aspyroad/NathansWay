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
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.MonoGame.iOS;
using NathansWay.MonoGame.Shared;


namespace NathansWay.iOS.Numeracy.WorkSpace
{
    public class LessonNumletSet
    {
        public vcWorkNumlet vcNumletEquation { get; set; }
        public vcWorkNumlet vcNumletResult { get; set; }
        public List<vcWorkNumlet> vcNumletMethods { get; set; }

        public EntityLesson Lesson { get; set; }
        public EntityLessonDetail LessonDetail { get; set; }
        public EntityLessonResults LessonResults { get; set; }
        public EntityLessonDetailResults LessonDetailResults { get; set; }


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
    public class LessonList<LessonNumlets> : List<LessonNumlets>
    {
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
