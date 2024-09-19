using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Module = Autofac.Module;

namespace Container.Modules
{
    public class RepositoryModule : Module
    {
        private static string _connectionString;

        public static void AddDbContext(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _connectionString = configuration["DbConnString"];
            //todo
            serviceCollection.AddEntityFrameworkSqlServer().AddDbContext<CaseAktifDbContext>(options => options.UseSqlServer(_connectionString));
        }
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyType = typeof(GenericRepository<>).GetTypeInfo();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .Where(x => x != typeof(CaseAktifDbContext))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
