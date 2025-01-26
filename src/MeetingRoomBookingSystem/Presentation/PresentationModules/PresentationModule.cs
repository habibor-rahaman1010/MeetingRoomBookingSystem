using Autofac;
using DataAccess.Data;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Domain.RepositoryContracts;
using Domain.UnitOfWorkContracts;
using Service.Services;
using Service.ServicesContract;

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

            builder.RegisterType<DepartmentManagementService>()
                .As<IDepartmentManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DepartmentRepository>()
                .As<IDepartmentRepository>()
                .InstancePerLifetimeScope();

        }
    }
}
