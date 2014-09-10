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
		private G__LessonTypes  _lessonType;

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
		public G__LessonTypes LessonType
		{ 
			get { return this._lessonType; } 
			set 
			{
				var tmp = value ; 

				this._lessonSeq = tmp;

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
