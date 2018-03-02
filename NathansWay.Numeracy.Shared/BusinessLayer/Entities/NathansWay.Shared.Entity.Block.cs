using SQLite.Net.Attributes;
// Shared
using NathansWay.Numeracy.Shared;
using NathansWay.Numeracy.Shared.BUS.Services;

namespace NathansWay.Numeracy.Shared.BUS.Entity
{
	public class EntityBlock : EntityBase
	{
		#region Private Variables

		private string _nameBlock;
		private string _description;
		private int _difficulty;

		#endregion

		#region Contructor

		public EntityBlock()
		{
			//Initialize();
		}

		#endregion

		#region Public Members 

		[Column("nameblock")]
		public string NameLesson
		{ 
			get { return this._nameBlock; } 
			set { this._nameBlock = value; }
		}
		[Column("description")]
		public string Description
		{ 
			get { return this._description; } 
			set { this._description = value; }
		}
		[Column("difficulty")]
		public int Difficulty
		{ 
			get { return this._difficulty; }
			set { this._difficulty = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        

	}
}
