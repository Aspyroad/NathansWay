using System;

namespace NathansWay.Shared
{
    public enum G__Entities : int
    {
        Teacher = 0,
        Student = 1,
        School = 2,
        Lesson = 3,
        LessonDetail = 4,
        LessonDetailResults = 5,
		LessonResults = 6,
		Block = 7,
		BlockDetail = 8,
		BlockResults = 9,
		Toolz = 10
    }

	public enum G__LessonTypes : int
	{
		Practice = 0,
		SpeedTest = 1,
		Assesment = 2
	}

	public enum G__Difficulty : int
	{
		Easy = 0,
		Normal = 1,
		Hard = 2
	}
     

}

