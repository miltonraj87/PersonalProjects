using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossCutting.Utilities.Ioc
{
    public class ResolverUnityExtension: UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.AddNew<AutowireResolverStrategy>(UnityBuildStage.Initialization);
        }
    }
}