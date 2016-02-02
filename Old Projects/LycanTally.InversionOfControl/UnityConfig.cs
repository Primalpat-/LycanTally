using LycanTally.Core.Contexts;
using LycanTally.Data.Models.Context;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace LycanTally.InversionOfControl
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer unityContainer)
        {
            RegisterByConvention(unityContainer);
            RegisterEntityFramework(unityContainer);
        }

        private static void RegisterByConvention(IUnityContainer unityContainer)
        {
            unityContainer.RegisterTypes(
                AllClasses.FromLoadedAssemblies().Where(t => t.Namespace.StartsWith("LycanTally")),
                WithMappings.FromMatchingInterface,
                WithName.Default);
        }

        private static void RegisterEntityFramework(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<ILycanTallyContext, LycanTallyContext>(new PerRequestLifetimeManager());
        }
    }
}
