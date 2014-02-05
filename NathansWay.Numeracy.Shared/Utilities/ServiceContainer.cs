using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

<<<<<<< HEAD
namespace NathansWay.Numeracy.Shared.Utilities 
{
    public class ServiceContainer 
    {
=======
namespace NathansWay.Numeracy.Shared.Utilities {
    public class ServiceContainer {
>>>>>>> 605b7e83dbe2071b617d342041348c66efb91a04
        static object locker = new object ();
        static ServiceContainer instance;

        private ServiceContainer ()
        {
            Services = new Dictionary<Type, Lazy<object>> ();
        }

        private Dictionary<Type, Lazy<object>> Services { get; set; }

        private static ServiceContainer Instance
<<<<<<< HEAD
		{
			get
			{
				lock (locker)
				{
					if (instance == null)
					{
						instance = new ServiceContainer ();
					}
=======
        {
            get
            {
                lock (locker) {
                    if (instance == null)
                        instance = new ServiceContainer ();
>>>>>>> 605b7e83dbe2071b617d342041348c66efb91a04
                    return instance;
                }
            }
        }

        public static void Register<T> (T service)
        {
            Instance.Services [typeof (T)] = new Lazy<object> (() => service);
        }

<<<<<<< HEAD
        public static void Register<T> () where T : new ()
=======
        public static void Register<T> ()
            where T : new ()
>>>>>>> 605b7e83dbe2071b617d342041348c66efb91a04
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
<<<<<<< HEAD
            if (Instance.Services.TryGetValue (typeof (T), out service)) 
            {
                return (T)service.Value;
            } 
            else 
            {
=======
            if (Instance.Services.TryGetValue (typeof (T), out service)) {
                return (T)service.Value;
            } else {
>>>>>>> 605b7e83dbe2071b617d342041348c66efb91a04
                throw new KeyNotFoundException (string.Format ("Service not found for type '{0}'", typeof (T)));
            }
        }
    }
}