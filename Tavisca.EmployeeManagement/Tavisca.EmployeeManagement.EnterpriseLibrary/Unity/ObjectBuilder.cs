using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Configuration;
using System.Reflection;

namespace Tavisca.EmployeeManagement.EnterpriseLibrary
{
    /// <summary>
    /// Static factory for building objects via the inbuilt DI infrastructure.
    /// </summary>
    public static class ObjectBuilder
    {
        public static object Resolve(Type type, string name = null, object args = null)
        {
            if (args == null)
                return GetContainer().Resolve(type, name);
            var dyn = args.GetType();
            var properties = dyn.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var parameters = Array.ConvertAll(properties, p =>
            {
                var value = p.GetValue(args, null);
                return new ParameterOverride(p.Name, value) as ResolverOverride;
            });
            return GetContainer().Resolve(type, name, parameters);
        }
        /// <summary>
        /// Resolve instance of the given type.
        /// </summary>
        /// <typeparam name="T">Type for which the instance is to be resolved.</typeparam>
        /// <param name="name">Name of the named instance. (optional)</param>
        /// <param  name="args" example="new { paramerter1Name = parameter1Value, paramerter2Name = parameter2Value ... }">()Arguments to be passed to the constructor of the instance. (Optional)</param>
        /// <returns></returns>
        public static T Resolve<T>(string name = null, object args = null)
            where T : class
        {
            return (T)Resolve(typeof(T), name, args);
        }

        public static IEnumerable ResolveAll(Type type)
        {
            return GetContainer().ResolveAll(type);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return GetContainer().ResolveAll<T>();
        }

        public static bool IsRegistered(Type type, string name = null)
        {
            return GetContainer().IsRegistered(type, name);
        }

        public static bool IsRegistered<T>(string name = null)
        {
            if (name == null)
                return GetContainer().IsRegistered<T>();
            else
                return GetContainer().IsRegistered<T>(name);
        }


        public static void SetConfiguration(string configurationFile)
        {
            lock (_locker)
            {
                _configurationFile = configurationFile;
                _rootContainer = null;
            }
        }

        private static string _configurationFile = null;

        private static IUnityContainer _rootContainer = null;
        private static readonly object _locker = new object();
        private static IUnityContainer GetContainer()
        {
            if (_rootContainer == null)
            {
                lock (_locker)
                {
                    if (_rootContainer == null)
                    {
                        IUnityContainer myContainer = new UnityContainer();
                        myContainer.LoadConfiguration();
                        _rootContainer = myContainer;
                    }
                }
            }
            return _rootContainer;
        }
    }

    public class ConstructorArg
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }
}
