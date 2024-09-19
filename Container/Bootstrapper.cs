using ApplicationService.Handler.Services;
using Autofac;
using Container.Modules;

namespace Container
{
    public class Bootstrapper
    {
        public static ILifetimeScope Container { get; private set; }
        public static void RegisterModules(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ValidatorModule());
            containerBuilder.RegisterModule(new MediatRModule());

            containerBuilder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<RabbitMQProducerService>().As<IRabbitMQProducerService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<RabbitMQConsumerService>().As<IRabbitMQConsumerService>().InstancePerLifetimeScope();


        }
        public static void SetContainer(ILifetimeScope autofacContainer)
        {
            Container = autofacContainer;
        }
    }
    
}
