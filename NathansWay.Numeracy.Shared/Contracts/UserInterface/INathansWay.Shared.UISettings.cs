// System
using System;
//using System.Collections.Generic;
// Xamarin



namespace NathansWay.Numeracy.Shared
{
	public interface IAppSettings
	{
        // Prefix GA = global application settings

        /// <summary>
        /// Number size - the current size of our Numbers.
        /// Returns a number size enum
        /// </summary>
        /// 
        G__DisplaySizeLevels GA__NumberDisplaySize { get; set; }

        /// <summary>
        /// Gets or sets the size of the G a number label display.
        /// </summary>
        /// <value>The size of the G a number label display.</value>
        G__DisplaySizeLevels GA__NumberLabelDisplaySize { get; set; }

        /// <summary>
        /// Edit type - the type of edit method for numbers, 3 settings
        /// Returns a  enum
        /// </summary>
        G__NumberEditMode GA__NumberEditMode{ get; set; }

        /// <summary>
        /// Gets or sets the student logged in.
        /// </summary>
        /// <value>The student logged in.</value>
        string StudentLoggedIn { get; set; }

        /// <summary>
        /// /
        /// </summary>
        /// <value>The is student logged in.</value>
        bool IsStudentLoggedIn { get; }

        /// <summary>
        /// Gets or sets the teacher logged in.
        /// </summary>
        /// <value>The teacher logged in.</value>
        string TeacherLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets the is teacher logged in.
        /// </summary>
        /// <value>The is teacher logged in.</value>
        bool IsTeacherLoggedIn { get; }

        /// <summary>
        /// True if we want to show the answer button numlet in the workspace
        /// </summary>
        /// <value><c>true</c> if G a show answer numlet; otherwise, <c>false</c>.</value>
        bool GA__ShowAnswerNumlet { get; set; }

        /// <summary>
        /// Variable to assist edit mode, do we want the selection to jump to 
        /// the next text box on a multinumbered text
        ///</summary>
        bool GA__MoveToNextNumber { get; set; }

        /// <summary>
        /// When checking the answer, this will highlite the particular number
        /// which is wrong. Eg if the answer was 20, but the user selected 21
        /// the "1" would be highlited as incorrect but the "2" would be dislplyed correct
        ///</summary>
        bool GA__SingleDigitErrorUIDisplay { get; set; }

        /// <summary>
        /// When we move to the next or previous question after moving, should we retain
        /// the UI correct state of the equationset
        ///</summary>
        ///<value><c>true</c> if persist UI, otherwise to reset to neutral <c>false</c></value>
        bool GA__PersistUICorrectStateOnMove { get; set; }

        /// <summary>
        /// When we move to the next or previous question after moving, should we retain
        /// the UI incorrect state of the equationset
        ///</summary>
        ///<value><c>true</c> if persist UI, otherwise to reset to neutral <c>false</c></value>
        bool GA__PersistUIInCorrectStateOnMove { get; set; }

        /// <summary>
        /// When we solve, do we only solve the numlets with answers or all numlets
        ///</summary>
        ///<value><c>true</c> all numlets are solved, otherwise <c>false</c>, only the numlet selected.</value>
        bool GA__SolveAllNumlets { get; set; }

        /// <summary>
        /// When overridden ToString() is called on NumberContainers this variable when set to true
        /// will return the current value not the original, if empty is returns an "x".
        ///</summary>
        ///<value><c>true</c> Current number value is returned <c>false</c>, Normal Original value is returned.</value>
        bool GA__ToStringReturnsCurrentValue { get; set; }
    }


}
