// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Xamarin
using Xamarin.Forms;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
	public class EntityUISettings : EntityBase
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

		public EntityUISettings()
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
		[Column("backcolor")]
		public Color BackColor
		{ 
			get 
			{ 
				return this._backColor; 
			}
			set 
			{ 
				this._backColor = value; 
			}
		}
		[Column("forecolor")]
		public Color ForeColor
		{ 
			get 
			{ 
				return this._foreColor; 
			} 
			set 
			{ 
				this._foreColor = value; 
			}
		}
		[Column("fontsize")]
		public int FontSize
		{ 
			get 
			{ 
				return this._fontSize; 
			}
			set 
			{ 
				this._fontSize = value; 
			}
		}
		[Column("fontname")]
		public string FontName
		{
			get { return this._fontName; }
			set { this._fontName = value; }            
		}
		[Column("bordercolor")]
		public string BorderColor
		{
			get 
			{ 
				return this._borderColor; 
			}
			set 
			{ 
				this._borderColor = value; 
			}
		}
		[Column("bordersize")]
		public int BorderSize
		{ 
			get { return this._borderSize; } 
			set { this._borderSize = value; }
		}
		[Column("hasborder")]
		public bool HasBorder
		{ 
			get { return this._hasBorder; } 
			set { this._hasBorder = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


