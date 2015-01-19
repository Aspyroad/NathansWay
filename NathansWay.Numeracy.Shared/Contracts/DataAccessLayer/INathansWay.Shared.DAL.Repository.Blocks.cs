﻿// Core
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
using NathansWay.Shared.Utilities;
using NathansWay.Shared.DB;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.BUS;


namespace NathansWay.Shared.DAL.Repository
{
	public interface IRepoBlocks
	{
		RepoBlock<EntityBlock> repBlock { get; }
		RepoBlockDetail<EntityBlockDetail> repBlockDetail { get; } 
	}
}

