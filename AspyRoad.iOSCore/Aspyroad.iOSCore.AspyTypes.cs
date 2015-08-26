using System;

namespace AspyRoad.iOSCore
{
	public enum G__GestureTypes
	{
		UITap = 0,
		UIPinch = 1,
		UIPan = 2,
		UISwipe = 3,
		UIRotation = 4,
		UILongPress = 5		
	}
	
	public enum G__Orientation
	{
		Portait = 0,
		Landscape = 1,
		Both = 2
	}

    public enum G__ApplyUI : int
    {
        DoNotApply = 1,
        ViewDidLoad = 2,
        ViewWillAppear = 3,
        AlwaysApply = 4
    }
}

