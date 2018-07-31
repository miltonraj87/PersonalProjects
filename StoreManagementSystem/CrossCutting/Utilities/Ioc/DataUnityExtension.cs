using Microsoft.Practices.Unity;
using SMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Utilities.Ioc
{
    public class DataUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<StoreDbContext>(new ContainerControlledLifetimeManager());
        }
    }
}
