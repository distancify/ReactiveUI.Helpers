using System;
using System.Linq;
using System.Reflection;
using Splat;

namespace ReactiveUI.Helpers
{
    public static class SplatExtensions
    {
        /// <summary>        
        /// Registers a service of the type specified in <paramref name="serviceType"/> with an
        /// implementation type specified in <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="implementationType">The type of the implementation to use.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="contract">Can be used to isolate multiple <paramref name="implementationType"/> for 
        /// the same <paramref name="serviceType"/>.
        /// </param>
        public static void Register(this IMutableDependencyResolver resolver, Type implementationType, Type serviceType, string contract = null)
        {
            ClassInstanceFactory classInstanceFactory = null;

            resolver.Register(() =>
            {
                if (classInstanceFactory == null)
                {
                    classInstanceFactory = GetClassInstanceFactory(resolver, implementationType, contract);
                }

                return classInstanceFactory.Create();

            }, serviceType, contract);
        }

        /// <summary>
        /// Registers a service of the type specified in <typeparamref  name="TService"/> with an
        /// implementation type specified in <typeparamref  name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <typeparam name="TService">The type of the service to register.</typeparam>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="contract">Can be used to isolate multiple <typeparamref  name="TImplementation"/> for 
        /// the same <typeparamref  name="TService"/>.
        /// </param>
        public static void Register<TImplementation, TService>(this IMutableDependencyResolver resolver, string contract = null)
        {
            resolver.Register(typeof(TImplementation), typeof(TService), contract);
        }

        /// <summary>
        /// Registers a service of the type specified in <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="implementationType">The type of the implementation to use.</param>
        /// <param name="contract">Can be used to isolate multiple <paramref name="implementationType"/>.</param>
        public static void Register(this IMutableDependencyResolver resolver, Type implementationType, string contract = null)
        {
            resolver.Register(implementationType, implementationType, contract);
        }

        /// <summary>
        /// Registers a service of the type specified in <typeparamref  name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="contract">Can be used to isolate multiple <typeparamref  name="TImplementation"/>.</param>
        public static void Register<TImplementation>(this IMutableDependencyResolver resolver, string contract = null)
        {
            resolver.Register(typeof(TImplementation), typeof(TImplementation), contract);
        }

        /// <summary>        
        /// Registers a service of the type specified in <paramref name="serviceType"/> with an
        /// implementation type specified in <paramref name="implementationType"/> as a lazy singleton.
        /// </summary>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="implementationType">The type of the implementation to use.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="contract">Can be used to isolate multiple <paramref name="implementationType"/> for 
        /// the same <paramref name="serviceType"/>.
        /// </param>
        public static void RegisterLazySingleton(this IMutableDependencyResolver resolver, Type implementationType, Type serviceType, string contract = null)
        {
            ClassInstanceFactory classInstanceFactory = null;

            resolver.RegisterLazySingleton(() =>
            {
                if (classInstanceFactory == null)
                {
                    classInstanceFactory = GetClassInstanceFactory(resolver, implementationType, contract);
                }

                return classInstanceFactory.Create();

            }, serviceType, contract);
        }

        /// <summary>
        /// Registers a service of the type specified in <typeparamref  name="TService"/> with an
        /// implementation type specified in <typeparamref  name="TImplementation"/> as a lazy singleton.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <typeparam name="TService">The type of the service to register.</typeparam>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="contract">Can be used to isolate multiple <typeparamref  name="TImplementation"/> for 
        /// the same <typeparamref  name="TService"/>.
        /// </param>
        public static void RegisterLazySingleton<TImplementation, TService>(this IMutableDependencyResolver resolver, string contract = null)
        {
            resolver.RegisterLazySingleton(typeof(TImplementation), typeof(TService), contract);
        }

        /// <summary>
        /// Registers a service of the type specified in <paramref name="implementationType"/> as a lazy singleton.
        /// </summary>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="implementationType">The type of the implementation to use.</param>
        /// <param name="contract">Can be used to isolate multiple <paramref name="implementationType"/>.</param>
        public static void RegisterLazySingleton(this IMutableDependencyResolver resolver, Type implementationType, string contract = null)
        {
            resolver.RegisterLazySingleton(implementationType, implementationType, contract);
        }

        /// <summary>
        /// Registers a service of the type specified in <typeparamref  name="TImplementation"/> as a lazy singleton.
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="resolver">The <see cref="IMutableDependencyResolver"/> to register the service to.</param>
        /// <param name="contract">Can be used to isolate multiple <typeparamref  name="TImplementation"/>.</param>
        public static void RegisterLazySingleton<TImplementation>(this IMutableDependencyResolver resolver, string contract = null)
        {
            resolver.RegisterLazySingleton(typeof(TImplementation), typeof(TImplementation), contract);
        }

        private static ClassInstanceFactory GetClassInstanceFactory(IMutableDependencyResolver resolver, Type implementer, string contract = null)
        {
            var result = new ClassInstanceFactory();

            result.Constructor = implementer.GetTypeInfo().DeclaredConstructors.FirstOrDefault(c => c.IsPublic);
            var constructorParameters = result.Constructor.GetParameters();

            result.Parameters = new object[constructorParameters.Length];
            for (var i = 0; i < constructorParameters.Length; i++)
            {
                result.Parameters[i] = resolver.GetService(constructorParameters[i].ParameterType, contract);
                if (result.Parameters[i] == null)
                {
                    throw new ResolveException($"Unable to resolve {constructorParameters[i].Name} parameter for {implementer.Name}");
                }
            }

            return result;
        }

        private class ClassInstanceFactory
        {
            public ConstructorInfo Constructor { get; set; }
            public object[] Parameters { get; set; }

            public object Create()
            {
                return Constructor.Invoke(Parameters);
            }
        }
    }
}
