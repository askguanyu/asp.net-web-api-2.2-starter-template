﻿using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutofacSerilogIntegration;
using LegacyApplication.Database.Context;
using LegacyApplication.Database.Infrastructure;
using LegacyApplication.Repositories.Core;
using LegacyApplication.Repositories.HumanResources;
using LegacyApplication.Repositories.Work;
using LegacyApplication.Services.Core;
using LegacyApplication.Services.Work;

namespace LegacyStandalone.Web.MyConfigurations
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            // Serilog
            builder.RegisterLogger(autowireProperties: true);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // DbContext
            builder.RegisterType<CoreContext>().As<IUnitOfWork>().InstancePerRequest();

            // Services
            builder.RegisterType<CommonService>().As<ICommonService>().InstancePerRequest();
            builder.RegisterType<InternalMailService>().As<IInternalMailService>().InstancePerRequest();

            // Core
            builder.RegisterType<UploadedFileRepository>().As<IUploadedFileRepository>().InstancePerRequest();

            // Work
            builder.RegisterType<InternalMailRepository>().As<IInternalMailRepository>().InstancePerRequest();
            builder.RegisterType<InternalMailToRepository>().As<IInternalMailToRepository>().InstancePerRequest();
            builder.RegisterType<InternalMailAttachmentRepository>().As<IInternalMailAttachmentRepository>().InstancePerRequest();
            builder.RegisterType<TodoRepository>().As<ITodoRepository>().InstancePerRequest();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>().InstancePerRequest();

            // HR
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerRequest();
            builder.RegisterType<JobPostLevelRepository>().As<IJobPostLevelRepository>().InstancePerRequest();
            builder.RegisterType<JobPostRepository>().As<IJobPostRepository>().InstancePerRequest();
            builder.RegisterType<AdministrativeLevelRepository>().As<IAdministrativeLevelRepository>().InstancePerRequest();
            builder.RegisterType<TitleLevelRepository>().As<ITitleLevelRepository>().InstancePerRequest();
            builder.RegisterType<NationalityRepository>().As<INationalityRepository>().InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}