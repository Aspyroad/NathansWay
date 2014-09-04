// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
	public class Settings : EntityBase
	{
		#region Private Variables

		private int _studentSeq;
		private int _teacherSeq;
		private string _frameSize;
		private int _vcTag;
		private string _vcName;
		private string _backColor;
		private string _foreColor;
		private int _fontSize;
		private string _fontName;
		private string _borderColor;
		private bool _hasBorder;
		private int _borderSize;

		#endregion

		#region Contructor

		public Settings()
		{
			//Initialize();
		}

		#endregion

		#region Public Members 

		[Column("student_seq")]
		public int StudentSeq
		{ 
			get { return this._studentSeq; } 
			set { this._studentSeq = value; }
		}
		[Column("teacher_seq")]
		public int TeacherSeq
		{ 
			get { return this._teacherSeq; } 
			set { this._teacherSeq = value; }
		}
		[Column("framesize")]
		public RectangleF FrameSize
		{ 
			get 
			{ 
				// x, y, width, height
				var strArray = _frameSize.Split (',');
				RectangleF tmpRect = 
					new RectangleF (
						float.Parse (strArray [0]),
						float.Parse (strArray [1]),
						float.Parse (strArray [2]),
						float.Parse (strArray [3])
					);
				 return tmpRect; 
			}
			set 
			{ 
				// Convert a RectF to Text (csv)
				string tmpStrRect = 
					value.X.ToString () + ',' +
					value.Y.ToString () + ',' +
					value.Width.ToString () + ',' +
					value.Height.ToString ();
				this._frameSize = tmpStrRect;
			}
		}
		[Column("vctag")]
		public int VcTag
		{ 
			get { return this._vcTag; }
			set { this._vcTag = value; }
		}
		[Column("vcname")]
		public string VcName
		{ 
			get { return this._vcName; }
			set { this._vcName = value; }
		}
		[Column("datetmcomplete")]
		public DateTime EndTime
		{ 
			get { return this._backColor; }
			set { this._backColor = value; }
		}
		[Column("operator")]
		public string Operator
		{ 
			get { return this._operator; } 
			set { this._operator = value; }
		}
		[Column("equation")]
		public string Equation
		{ 
			get { return this._equation; }
			set { this._equation = value; }
		}
		[Column("method")]
		public string Method
		{
			get { return this._method; }
			set { this._method = value; }            
		}
		[Column("result")]
		public string Result
		{
			get { return this._result; }
			set { this._result = value; }            
		}
		[Column("correctresult")]
		public int ScoreResult
		{ 
			get { return this._scoreResult; } 
			set { this._scoreResult = value; }
		}
		[Column("correctmethod")]
		public int ScoreMethod
		{ 
			get { return this._scoreMethod; } 
			set { this._scoreMethod = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


