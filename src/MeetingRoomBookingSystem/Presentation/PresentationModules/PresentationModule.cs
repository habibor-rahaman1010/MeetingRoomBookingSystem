using Autofac;
using DataAccess.Data;
using DataAccess.UnitOfWork;
using Domain.UnitOfWorkContracts;

namespace Presentation.PresentationModules
{
    public class PresentationModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public PresentationModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MRBSDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssembly", _migrationAssembly)
               .InstancePerLifetimeScope();

            builder.RegisterType<MRBSUnitOfWork>()
                .As<IMRBSUnitOfWork>()
                .InstancePerLifetimeScope();

        }
    }
}
