using ApiContract.Request;
using ApplicationService.Handler;
using Autofac;
using Container.Decorator;
using MediatR;
using System.Reflection;
using Module = Autofac.Module;

namespace Container.Modules
{
    public class MediatRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();


            builder
                .RegisterAssemblyTypes(typeof(RequestBase<>).Assembly)
                .AsClosedTypesOf(typeof(IRequest<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(typeof(RequestHandlerBase<,>).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterGeneric(typeof(LoggingHandler<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder
                .RegisterGeneric(typeof(ExceptionHandler<,>))
                .As(typeof(IPipelineBehavior<,>));

            base.Load(builder);
        }

    }
}
