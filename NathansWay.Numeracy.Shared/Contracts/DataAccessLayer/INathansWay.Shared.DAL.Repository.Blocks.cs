// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// SQLite
using SQLite.Net.Interop;

// NathansWay
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.DB;
using NathansWay.MonoGame.BUS.Entity;
using NathansWay.MonoGame.BUS;


namespace NathansWay.MonoGame.DAL.Repository
{
	public interface IRepoBlocks
	{
		RepoBlock<EntityBlock> repBlock { get; }
		RepoBlockDetail<EntityBlockDetail> repBlockDetail { get; } 
	}
}

