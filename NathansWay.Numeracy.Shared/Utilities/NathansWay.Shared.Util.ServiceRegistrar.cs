// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

// NathansWay
using NathansWay.MonoGame.DAL;
using NathansWay.MonoGame.BUS;
using NathansWay.MonoGame.BUS.Entity;
using NathansWay.MonoGame.DAL.Repository;
using NathansWay.MonoGame.BUS.ViewModel;
using NathansWay.MonoGame.Global;


namespace NathansWay.MonoGame.Global
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

			//            ServiceContainer.Register<IAssignmentService> (() => new SampleAssignmentService ());
			//

			#if !NETFX_CORE
				//Only do these on iOS or Android
				SharedServiceContainer.Register<LessonViewModel> ();
			//            ServiceContainer.Register<AssignmentViewModel>();
			//            ServiceContainer.Register<DocumentViewModel>();
			//            ServiceContainer.Register<ExpenseViewModel>();
			//            ServiceContainer.Register<HistoryViewModel>();
			//            ServiceContainer.Register<ItemViewModel>();
			//            ServiceContainer.Register<LaborViewModel>();
			//            ServiceContainer.Register<LoginViewModel>();
			//            ServiceContainer.Register<PhotoViewModel>();
			#endif		
		}
    }
}
