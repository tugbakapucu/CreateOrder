using ApiContract.Validations;
using Autofac;

namespace Container.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ValidatorBase<>).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ValidatorBase<>))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            base.Load(builder);
        }
    }
}
