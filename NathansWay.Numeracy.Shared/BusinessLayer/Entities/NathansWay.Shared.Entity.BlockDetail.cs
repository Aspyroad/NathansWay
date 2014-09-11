using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;
using NathansWay.Shared.BUS.Services;

namespace NathansWay.Shared.BUS.Entity
{
	public class EntityBlockDetail : EntityBase
	{
		#region Private Variables

		private int _blockSeq;
		private int _lessonSeq;
		private int _lessonType;

		#endregion

		#region Contructor

		public EntityBlockDetail()
		{
			//Initialize();
		}

		#endregion

		#region Public Members 

		[Column("block_seq")]
		public int BlockSeq
		{ 
			get { return this._blockSeq; } 
			set { this._blockSeq = value; }
		}
		[Column("lesson_seq")]
		public int LessonSeq
		{ 
			get { return this._lessonSeq; } 
			set { this._lessonSeq = value; }
		}
		[Column("lessontype")]
		public int LessonType
		{ 
			get 
			{ 
				return this._lessonType; 
			} 
			set 
			{
				this._lessonSeq = value;
			}
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        

	}
}
