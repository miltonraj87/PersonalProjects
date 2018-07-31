using CrossCutting.Utilities.Ioc;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagementSystem
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
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            // Add Extension
            container.AddNewExtension<ResolverUnityExtension>();

            container.AddNewExtension<DataUnityExtension>();

            // TODO: Register your types here
            container.RegisterTypes(AllClasses.FromLoadedAssemblies().Where(c =>
                (c.Namespace.StartsWith("CrossCutting")
                    || c.Namespace.StartsWith("SMS.Repository")
                    || c.Namespace.StartsWith("SMS.Services")
                ) && !c.Namespace.StartsWith("StoreManagementSystem.Controllers") && !c.Namespace.StartsWith("SMS.Repository.Context")),
                 WithMappings.FromAllInterfacesInSameAssembly,
                 type =>
                 {
                     if (type.Name.StartsWith("Mock")) return "Mock";
                     return WithName.Default(type);
                 },
                 WithLifetime.ContainerControlled
             );
        }
    }
}