using Autofac;
using PracticeTest.Core.Interfaces;
using PracticeTest.Core.Services;

namespace PracticeTest.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
