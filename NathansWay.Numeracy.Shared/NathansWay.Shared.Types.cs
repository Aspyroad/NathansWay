﻿using System;
using System.Collections.Generic;
using System.Globalization;


// NathansWay Type Library

namespace NathansWay.Numeracy.Shared
{
    #region Drawing

    public enum G__FactoryDrawings : int
    {
        All = G__MathOperator.All,
        Addition = G__MathOperator.Addition,
        Subtraction = G__MathOperator.Subtraction,
        Division = G__MathOperator.Division,
        Multiplication = G__MathOperator.Multiplication,
        AddSub = G__MathOperator.AddSub,
        DivMulti = G__MathOperator.DivMulti,
        Equals = G__MathOperator.Equals,
        Circle = 8,
        Tick = 9,
        Cross = 10

    }

    public static class G__FactoryDrawingSizes
    {
        static Dictionary<G__FactoryDrawings, G__Size> _dict = new Dictionary<G__FactoryDrawings, G__Size>
        {
            {G__FactoryDrawings.All, new G__Size(0.0f, 0.0f)},
            {G__FactoryDrawings.Addition, new G__Size(30.0f, 30.0f)},
            {G__FactoryDrawings.Subtraction, new G__Size(30.0f, 30.0f)},
            {G__FactoryDrawings.Division, new G__Size(30.0f, 30.0f)},
            {G__FactoryDrawings.Multiplication, new G__Size(30.0f, 30.0f)},
            {G__FactoryDrawings.AddSub, new G__Size(60.0f, 30.0f)},
            {G__FactoryDrawings.DivMulti, new G__Size(60.0f, 30.0f)},
            {G__FactoryDrawings.Equals, new G__Size(30.0f, 30.0f)},
            {G__FactoryDrawings.Circle, new G__Size(44.0f, 44.0f)},
            {G__FactoryDrawings.Tick, new G__Size(44.0f, 44.0f)},
            {G__FactoryDrawings.Cross, new G__Size(44.0f, 44.0f)}
        };

        public static G__Size GetSize(G__FactoryDrawings x)
        {
            // Try to get the result in the static Dictionary
            G__Size result;
            if (_dict.TryGetValue(x, out result))
            {
                return result;
            }
            else
            {
                return new G__Size(0.0f, 0.0f);
            }
        }
    }

    public struct G__Size
    {
        public float height { get; set; }
        public float width { get; set; }

        public G__Size(float _width, float _height)
        {
            this.height = _height;
            this.width = _width;
        }
    }

    #endregion

    #region DataBase

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

    public enum G__SortOrder : int
    {
        Ascending = 1,
        Descending = 2
    }

    #endregion

    #region LessonType

	public enum G__LessonClass : int
	{
		Practice = 1,
		SpeedTest = 2,
		Assesment = 3
	}

    public enum G__AnswerState : int
    {
        Correct = 1,
        InCorrect = 2,
        PartCorrect = 3,
        Empty = 4,
        FreeForm = 5,
        ReadOnly = 6
    }

    public enum G__SolveAttempted : int
    {
        Attempted = 1,
        UnAttempted = 2
    }

    public enum G__WorkNumletType : int
    {
        Equation = 1,
        Method = 2,
        Result = 3,
        Solve = 4
    }

    public enum G__LessonState : int
    {
        Ready = 1,
        Started = 2,
        Paused = 3,
        Finished = 4
    }

    public enum G__ContainerType : int
    {
        Number = 1,
        Fraction = 2,
        Operator = 3,
        Container = 4,
        NumberLabel = 5,
        Decimal = 6,
        NumberText = 7,
        SolveButton = 8,
        Brace = 9
    }

    #endregion

    #region UI

    public enum G__PrimaryColor : int
    {
        Red = 1,
        Orange = 2,
        Yellow = 3,
        Green = 4,
        Blue = 5,
        Purple = 6
    }

    public enum G__DisplaySizeLevels : int
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7,
        Level8 = 8,
        Level9 = 9,
        Level10 = 10
    }

    public static class G__DisplaySizeLevel
    {
        public const int Level1 = 1;
        public const int Level2 = 2;
        public const int Level3 = 3;
        public const int Level4 = 4;
        public const int Level5 = 5;
        public const int Level6 = 6;
        public const int Level7 = 7;
        public const int Level8 = 8;
        public const int Level9 = 9;
        public const int Level10 = 10;


        static readonly Dictionary<int, float> _dictScale = new Dictionary<int, float>
        {
            {G__DisplaySizeLevel.Level1, 0.1f},
            {G__DisplaySizeLevel.Level2, 0.3f},
            {G__DisplaySizeLevel.Level3, 0.5f},
            {G__DisplaySizeLevel.Level4, 0.8f},
            {G__DisplaySizeLevel.Level5, 1.0f},
            {G__DisplaySizeLevel.Level6, 1.5f},
            {G__DisplaySizeLevel.Level7, 2.0f},
            {G__DisplaySizeLevel.Level8, 2.5f},
            {G__DisplaySizeLevel.Level9, 3.0f},
            {G__DisplaySizeLevel.Level10, 4.0f},
        };

        static readonly Dictionary<int, float> _dictFont = new Dictionary<int, float>
        {
            {G__DisplaySizeLevel.Level1, 6.0f},
            {G__DisplaySizeLevel.Level2, 8.0f},
            {G__DisplaySizeLevel.Level3, 12.0f},
            {G__DisplaySizeLevel.Level4, 16.0f},
            {G__DisplaySizeLevel.Level5, 22.0f},
            {G__DisplaySizeLevel.Level6, 26.0f},
            {G__DisplaySizeLevel.Level7, 30.0f},
            {G__DisplaySizeLevel.Level8, 36.0f},
            {G__DisplaySizeLevel.Level9, 40.0f},
            {G__DisplaySizeLevel.Level10, 44.0f},
        };

        public static float GetDisplaySizeScale(int x)
        {
            // Try to get the result in the static Dictionary
            float result;
            if (_dictScale.TryGetValue(x, out result))
            {
                return result;
            }
            else
            {
                return 1.0f;
            }
        }

        public static float GetDisplayFontSize(int x)
        {
            // Try to get the result in the static Dictionary
            float result;
            if (_dictFont.TryGetValue(x, out result))
            {
                return result;
            }
            else
            {
                // Basic size Level 5
                return 22.0f;
            }
        }

        public static float GetDisplaySizeScale(G__DisplaySizeLevels x)
        {
            // Try to get the result in the static Dictionary
            float result;
            if (_dictScale.TryGetValue((int)x, out result))
            {
                return result;
            }
            else
            {
                return 1.0f;
            }
        }

        public static float GetDisplayFontSize(G__DisplaySizeLevels x)
        {
            // Try to get the result in the static Dictionary
            float result;
            if (_dictFont.TryGetValue((int)x, out result))
            {
                return result;
            }
            else
            {
                return 22.0f;
            }
        }

        //public static int GetDisplaySizeSize(G__DisplaySizeLevels x)
        //{
        //    // Try to get the result in the static Dictionary
        //    float result;
        //    float y;

        //    if (_dict.TryGetValue((int)x, out result))
        //    {
        //         y = result;
        //    }
        //    else
        //    {
        //        y = 1.0f;
        //    }
        //    // TODO: Do I need an enumeration of all graphic height widths?
        //    // All Math drawings are 30 x 30 but this may not always the case.
        //    //
        //    int z = (int)Math.Round((30.0f * y), 0);

        //    return z;
        //}
    }

    public struct G__Color
    {
        // Statics
        public static readonly G__Color Empty;

        // Privates
        float _r;
        float _g;
        float _b;
        float _alpha;

        // Constructors
        public G__Color(float r, float g, float b, float alpha)
        {
            this = default(G__Color);
            this.Red = r;
            this.Green = g;
            this.Blue = b;
            this.Alpha = alpha;
        }

        public G__Color(string r, string g, string b, string alpha)
        {
            this = default(G__Color);
            this.Red = float.Parse(r, CultureInfo.InvariantCulture.NumberFormat);
            this.Green = float.Parse(g, CultureInfo.InvariantCulture.NumberFormat);
            this.Blue = float.Parse(b, CultureInfo.InvariantCulture.NumberFormat);
            this.Alpha = float.Parse(alpha, CultureInfo.InvariantCulture.NumberFormat);
        }

        public G__Color(string _elements)
        {
            this = default(G__Color);
            var x = _elements.Split(new[] { ',' }, 4);
            this.Red = float.Parse(x[0], CultureInfo.InvariantCulture.NumberFormat);
            this.Green = float.Parse(x[1], CultureInfo.InvariantCulture.NumberFormat);
            this.Blue = float.Parse(x[2], CultureInfo.InvariantCulture.NumberFormat);
            this.Alpha = float.Parse(x[3], CultureInfo.InvariantCulture.NumberFormat);
        }

        // Public Getters/Setters
        public float Red
        {
            get
            {
                return this._r;
            }
            set
            {
                this._r = value;
            }
        }

        public float Green
        {
            get
            {
                return this._g;
            }
            set
            {
                this._g = value;
            }
        }

        public float Blue
        {
            get
            {
                return this._b;
            }
            set
            {
                this._b = value;
            }
        }

        public float Alpha
        {
            get
            {
                return this._alpha;
            }
            set
            {
                this._alpha = value;
            }
        }

        // RGB values returned as RGB floats for UIColor UIColor only accepts 0-1 values. eg 0.335, 0.0 , 1.0 ,0.999
        public float RedRGB
        {
            get
            {
                return (this._r / 255.0f);
            }
            set
            {
                this._r = value;
            }
        }

        public float GreenRGB
        {
            get
            {
                return (this._g / 255.0f);
            }
            set
            {
                this._g = value;
            }
        }

        public float BlueRGB
        {
            get
            {
                return (this._b / 255.0f);
            }
            set
            {
                this._b = value;
            }
        }

        public float AlphaRGB
        {
            get
            {
                return (this._alpha / 255.0f);
            }
            set
            {
                this._alpha = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", new object[]
            {
                this._r,
                this._g,
                this._b,
                this._alpha
            });
        }

    }

    #endregion

    #region NumberField Attributes

    public enum G__NumberDisplayPositionY : int
    {
        Top = 1,
        Center = 2,
        Bottom = 3,
        TopPadded = 4, 
        BottomPadded = 5
    }

    public enum G__NumberDisplayPositionX : int
    {
        Right = 1,
        Center = 2,
        Left = 3,
        MiddleRight = 4,
        MIddleLeft = 5
    }

    public enum G__NumberPickerPosition : int
    {
        Top = 1,
        Center = 2,
        Bottom = 3
    }

    public enum G__UnitPlacement : int
    {
        ones = 1,
        tens = 2,
        hundreds = 3,
        thousands = 4,
        tenthousands = 5,
        hundredthousands = 6,
        millions = 7
    }

    public enum G__Significance : int
    {
        Significant = 1,
        InSignificant = 2
    }

    public enum G__NumberEditMode
    {
        EditScroll = 1,
        EditUpDown = 2,
        EditNumPad = 3       
    }

    public enum G__TimeDisplay
    {
        Seconds = 1,
        Time = 2
    }

    #endregion

	#region Filtering

	public enum G__ExpressionType 
	{
		// ** Note
		// Almost identical to ExpressionType enum

		Equal,
		GreaterThan,
		LessThan,
		GreaterThanOrEqual,
		LessThanOrEqual,
		// The following values are not in ExpressionType
		Contains,
		StartsWith,
		EndsWith
	}

	#endregion

	#region Operator Logic 

	public enum G__MathOperator
	{
        All = 0,
		Addition = 1,
		Subtraction = 2,
		Division = 3,
		Multiplication = 4,
		AddSub = 5,
		DivMulti = 6,
        Equals = 7,
        Value = 8,
        BraceRoundLeft = 9,
        BraceRoundRight = 10,
        Fraction = 11,
        Decimal = 12,
        Method = 13,
        Answer = 14,
        Empty = 15
	}

	public static class G__MathOperators
	{
		static Dictionary<G__MathOperator, string> _dict = new Dictionary<G__MathOperator, string>
		{
            {G__MathOperator.All, "All"},
			{G__MathOperator.Addition, "+"},
			{G__MathOperator.Subtraction, "-"},
			{G__MathOperator.Division, "÷"},
			{G__MathOperator.Multiplication, "x"},
			{G__MathOperator.AddSub, "+ –"},
			{G__MathOperator.DivMulti, "÷ x"},
            {G__MathOperator.BraceRoundLeft, "("},
            {G__MathOperator.BraceRoundRight, ")"},
            {G__MathOperator.Fraction, "F"},
            {G__MathOperator.Decimal, "."},
            {G__MathOperator.Equals, "="},
            {G__MathOperator.Method, "M"},
            {G__MathOperator.Answer, "A"},
            {G__MathOperator.Empty, "E"}
		};

        static Dictionary<string, G__MathOperator> _dictrev = new Dictionary<string, G__MathOperator>
        {
            {"All", G__MathOperator.All},
            {"+", G__MathOperator.Addition},
            {"-", G__MathOperator.Subtraction},
            {"÷", G__MathOperator.Division},
            {"x", G__MathOperator.Multiplication},
            {"+ –", G__MathOperator.AddSub},
            {"÷ x", G__MathOperator.DivMulti},
            {"(", G__MathOperator.BraceRoundLeft},
            {")", G__MathOperator.BraceRoundRight},
            {"F", G__MathOperator.Fraction},
            {".", G__MathOperator.Decimal},
            {"=", G__MathOperator.Equals},
            {"M", G__MathOperator.Method},
            {"A", G__MathOperator.Answer},
            {"E", G__MathOperator.Empty}
        };
			
		public static string GetOp(G__MathOperator x)
		{
			// Try to get the result in the static Dictionary
			string result;
			if (_dict.TryGetValue(x, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
		}

        public static G__MathOperator GetOp(string x)
        {
            // Try to get the result in the static Dictionary
            G__MathOperator result;
            if (_dictrev.TryGetValue(x, out result))
            {
                return result;
            }
            else
            {
                return G__MathOperator.Value;
            }
        }
	}

	#endregion

	#region Expression Type

	public enum G__MathType
	{
        All = 0,
		Basic = 1,
		Grouped = 2,
		Fraction = 3,
		Mixed = 4
	}

	public static class G__MathTypes
	{
		static Dictionary<G__MathType, string> _dict = new Dictionary<G__MathType, string>
		{
            {G__MathType.All, "All" },
			{G__MathType.Basic, "Basic"},
			{G__MathType.Fraction, "Fraction"},
			{G__MathType.Grouped, "Grouped"},
			{G__MathType.Mixed, "Mixed"}
		};

		public static string GetType(G__MathType x)
		{
			// Try to get the result in the static Dictionary
			string result;
			if (_dict.TryGetValue(x, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
		}
	}

	#endregion

	#region LevelType

	public enum G__MathLevel : int
	{
        All = 0,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
		Level4 = 4,
		Level5 = 5,
		Level6 = 6,
		Level7 = 7,
		Level8 = 8,
		Level9 = 9,
		Level10 = 10
	}

	public static class G__MathLevels
	{
		static Dictionary<G__MathLevel, string> _dict = new Dictionary<G__MathLevel, string>
		{
            {G__MathLevel.All, "All"},
			{G__MathLevel.Level1, "Level1"},
			{G__MathLevel.Level2, "Level2"},
			{G__MathLevel.Level3, "Level3"},
			{G__MathLevel.Level4, "Level4"},
			{G__MathLevel.Level5, "Level5"},
			{G__MathLevel.Level6, "Level6"},
			{G__MathLevel.Level7, "Level7"},
			{G__MathLevel.Level8, "Level8"},
			{G__MathLevel.Level9, "Level9"},
			{G__MathLevel.Level10, "Level10"}
		};

		public static string GetLevel(G__MathLevel x)
		{
			// Try to get the result in the static Dictionary
			string result;
			if (_dict.TryGetValue(x, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
		}
	}

	#endregion

    #region MathCharacters

    //public enum G__MathChar : int
    //{
    //    Value = 0,
    //    BraceRoundLeft = 1,
    //    BraceRoundRight = 2,
    //    Fraction = 3,
    //    Division = 4,
    //    Addition = 5,
    //    Subtraction = 6,
    //    Multiplication = 7,
    //    Decimal = 8,
    //    Equals = 9,
    //    Method = 10,
    //    Answer = 11
    //}

    //public static class G__MathChars
    //{
    //    static Dictionary<string, G__MathChar> _dict = new Dictionary<string, G__MathChar>
    //    {
    //        {"(", G__MathChar.BraceRoundLeft},
    //        {")", G__MathChar.BraceRoundRight},
    //        {"F", G__MathChar.Fraction},
    //        {".", G__MathChar.Decimal},
    //        {"=", G__MathChar.Equals},
    //        {"M", G__MathChar.Method},
    //        {"A", G__MathChar.Answer}
    //    };

    //    public static G__MathChar GetCharType(string x)
    //    {
    //        // Try to get the result in the static Dictionary
    //        G__MathChar result;
    //        if (_dict.TryGetValue(x, out result))
    //        {
    //            return result;
    //        }
    //        else
    //        {
    //            return G__MathChar.Value;
    //        }
    //    }
    //}

    #endregion
}

