using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NathansWay.Numeracy.Shared
{
    public class ServiceContainer 
    {
        static object locker = new object ();
        static ServiceContainer instance;

		private Dictionary<Type, Lazy<object>> Services { get; set; }

		ServiceContainer ()
        {
            Services = new Dictionary<Type, Lazy<object>> ();
        }      

        private static ServiceContainer Instance
		{
			get
			{
				lock (locker)
				{
					if (instance == null)
					{
						instance = new ServiceContainer ();
					}
                    return instance;
                }
            }
        }

		// Overload 1 - Takes an object of any type.
        public static void Register<T> (T service)
        {
            Instance.Services [typeof (T)] = new Lazy<object> (() => service);
        }

        public static void Register<T> () where T : new ()
        {
            Instance.Services [typeof (T)] = new Lazy<object> (() => new T ());
        }

        public static void Register<T> (Func<object> function)
        {
            Instance.Services [typeof (T)] = new Lazy<object> (function);
        }

        public static T Resolve<T> ()
        {
            Lazy<object> service;

            if (Instance.Services.TryGetValue (typeof (T), out service)) 
            {
                return (T)service.Value;
            } 
            else 
            {
                throw new KeyNotFoundException (string.Format ("Service not found for type '{0}'", typeof (T)));
            }
        }

		#region MonoGame Implemintation
		//		namespace Microsoft.Xna.Framework
		//		{
		//			public class GameServiceContainer : IServiceProvider
		//			{
		//				Dictionary<Type, object> services;
		//
		//				public GameServiceContainer()
		//				{
		//					services = new Dictionary<Type, object>();
		//				}
		//
		//				public void AddService(Type type, object provider)
		//				{
		//					if (type == null)
		//						throw new ArgumentNullException("type");
		//					if (provider == null)
		//						throw new ArgumentNullException("provider");
		//					#if WINRT
		//					if (!type.GetTypeInfo().IsAssignableFrom(provider.GetType().GetTypeInfo()))
		//					#else
		//					if (!type.IsAssignableFrom(provider.GetType()))
		//						#endif
		//						throw new ArgumentException("The provider does not match the specified service type!");
		//
		//					services.Add(type, provider);
		//				}
		//
		//				public object GetService(Type type)
		//				{
		//					if (type == null)
		//						throw new ArgumentNullException("type");
		//
		//					object service;
		//					if (services.TryGetValue(type, out service))
		//						return service;
		//
		//					return null;
		//				}
		//
		//				public void RemoveService(Type type)
		//				{
		//					if (type == null)
		//						throw new ArgumentNullException("type");
		//
		//					services.Remove(type);
		//				}
		//			}
		//		}
		#endregion

    }
}