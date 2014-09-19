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
		private int _vcTag;
		private string _vcName;
		// UIButton
		private string _btnBGColor;
		private string _btnPressedBGImage;
		private string _btnNormalTitleColor;
		private string _btnPressedTitleColor;
		private string _btnNormalBGImage;
		// UIView
		private string _vwBGColor;
		private string _vwBGTint;
		// UILabel
		private string _lblTitleColor;
		// UITextViews
		private string _txtBGColor;
		private string _txtBGTint;
		private string _txtTextColor;

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
		// UIButton
		[Column("btnNormalBGColor")]
		public string ButtonBGColor
		{ 
			get { return this._btnBGColor; }
			set { this._btnBGColor = value; }

		}
		[Column("btnPressedBGImage")]
		public string ButtonPressedBGImage
		{ 
			get 
			{ 
				return this._btnPressedBGImage; 
			}
			set 
			{ 
				this._btnPressedBGImage = value; 
			}
		}
		[Column("btnNormalBGImage")]
		public string ButtonNormalBGImage
		{ 
			get 
			{ 
				return this._btnNormalBGImage; 
			}
			set 
			{ 
				this._btnNormalBGImage = value; 
			}
		}
		[Column("btnNormalTitleColor")]
		public string ButtonNormalTitleColor
		{ 
			get 
			{ 
				return this._btnNormalTitleColor; 
			} 
			set 
			{ 
				this._btnNormalTitleColor = value; 
			}
		}
		[Column("btnPressedTitleColor")]
		public string ButtonPressedTitleColor
		{ 
			get 
			{ 
				return this._btnPressedTitleColor; 
			}
			set 
			{ 
				this._btnPressedTitleColor = value; 
			}
		}
		// UIView
		[Column("vwBGColor")]
		public string ViewBackGroundColor
		{
			get { return this._vwBGColor; }
			set { this._vwBGColor = value; }            
		}
		[Column("vwBGTint")]
		public string ViewBackGroundTint
		{
			get 
			{ 
				return this._vwBGTint; 
			}
			set 
			{ 
				this._vwBGTint = value; 
			}
		}
		// UILabel
		[Column("lblTitleColor")]
		public string LabelTitleColor
		{ 
			get { return this._lblTitleColor; } 
			set { this._lblTitleColor = value; }
		}
		// UIText
		[Column("txtBGColor")]
		public string TextBackGroundColor
		{ 
			get { return this._txtBGColor; } 
			set { this._txtBGColor = value; }
		}
		[Column("txtBGTint")]
		public string TextBackGroundTint
		{ 
			get { return this._txtBGTint; } 
			set { this._txtBGTint = value; }
		}
		[Column("txtTextColor")]
		public string TextTextColor
		{ 
			get { return this._txtTextColor; } 
			set { this._txtTextColor = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


