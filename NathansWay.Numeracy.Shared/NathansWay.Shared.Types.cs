using System;
using System.Globalization;


// NathansWay Type Library

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

	public enum G__LessonClass : int
	{
		Practice = 1,
		SpeedTest = 2,
		Assesment = 3
	}

	public enum G__Difficulty : int
	{
		Easy = 1,
		Level2 = 2,
		Level3 = 3,
		Level4 = 4,
		Level5 = 5,
		Level6 = 6,
		Level7 = 7,
		Level8 = 8,
		Level9 = 9,
		Hard = 10
	}

	public enum G__OperatorPlease
	{
		Addition = 1,
		Subtraction = 2,
		Division = 3,
		Multiplication = 4,
		Random = 5,
		Multi = 6
	}

	public enum G__Expression
	{
		Expression = 1,
		Grouped = 2,
		Fraction = 3,
		Random = 4,
		Multi = 5
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
			var x = _elements.Split (new[]{ ',' }, 4);
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
				return (this._r/255.0f);
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
				return (this._g/255.0f);
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
				return (this._b/255.0f);
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
				return (this._alpha/255.0f);
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
}

