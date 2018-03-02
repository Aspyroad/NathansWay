// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

// NathansWay
using NathansWay.Numeracy.Shared.DAL;
using NathansWay.Numeracy.Shared.BUS;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.DAL.Repository;
using NathansWay.Numeracy.Shared.BUS.ViewModel;
using NathansWay.Numeracy.Shared;


namespace NathansWay.Numeracy.Shared
{
    /// <summary>
    /// Class for registering services for the app
    /// </summary>
    public static class SharedServiceRegistrar 
    {
        
		public static void Startup ()
		{
			InitializeShared ();
		}
        
		private static void InitializeShared()
		{
			SharedServiceContainer.Register<IRepoLesson<EntityLesson>> (() => new RepoLesson<EntityLesson> ());
            SharedServiceContainer.Register<IRepoLessonDetail<EntityLessonDetail>>(() => new RepoLessonDetail<EntityLessonDetail>());
            SharedServiceContainer.Register<IRepoLessonResults<EntityLessonResults>>(() => new RepoLessonResults<EntityLessonResults>());
            SharedServiceContainer.Register<IRepoLessonDetailResults<EntityLessonDetailResults>>(() => new RepoLessonDetailResults<EntityLessonDetailResults>());


			#if !NETFX_CORE
				//Only do these on iOS or Android
				SharedServiceContainer.Register<LessonViewModel> ();
                SharedServiceContainer.Register<LessonResultsViewModel>();

			#endif		
		}
    }
}
