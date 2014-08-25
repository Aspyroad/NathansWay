// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
	public class EntityToolz : EntityBase
	{
		#region Private Variables

//		private string _;
//		private string _nameLast;
//		private int _school;

		#endregion

		#region Contructor

		public EntityToolz()
		{
			//Initialize();
		}

		#endregion

		#region Public Members 

		[PrimaryKey, AutoIncrement]
		public override int SEQ 
		{ 
			get { return this._seq; }
			set { this._seq = value; }
		}
//		public string nameFirst 
//		{ 
//			get { return this._nameFirst; } 
//			set { this._nameFirst = value; }
//		}
//		public string nameLast 
//		{ 
//			get { return this._nameLast; } 
//			set { this._nameLast = value; }
//		}
//		public int school 
//		{ 
//			get { return this.school; }
//			set { this._school = value; }
//		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


