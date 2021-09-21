using Autofac;
using Core.Helpers;
using System.Linq;
using Core.CQRS;

namespace Domain.IoC
{
    public class SharedIoCRegistry : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = new[] { GetType().Assembly, typeof(AssemblyHelpers).Assembly}
                .Distinct().ToArray();

            builder.RegisterAssemblyTypes(assemblies).Where(x => x.IsClass);

            builder.RegisterAssemblyTypes(assemblies).Where(x => x.IsClass).AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGenericDecorator(typeof(UnitOfWorkExecutor<>), typeof(ICommandExecutor<>));

            //builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            base.Load(builder);
        }
    }
}
