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

		private string _nameTool;
		private string _description;
		//private int _school;

		#endregion

		#region Contructor

		public EntityToolz()
		{
			//Initialize();
		}

		#endregion

		#region Public Members 

		//		[PrimaryKey, AutoIncrement]
		//		public override int SEQ 
		//		{ 
		//			get { return this._seq; }
		//			set { this._seq = value; }
		//		}
		public string nameTool
		{ 
			get { return this._nameTool; } 
			set { this._nameTool = value; }
		}
		public string description
		{ 
			get { return this._description; } 
			set { this._description = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


