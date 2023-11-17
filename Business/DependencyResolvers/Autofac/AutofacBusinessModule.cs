using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
namespace Business.DependencyResolvers.Autofac
{
	public class AutofacBusinessModule: Module
	{
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<CityDistrictManager>().As<ICityDistrictService>().SingleInstance();
            builder.RegisterType<EfCityDal>().As<ICityDal>().SingleInstance();
            builder.RegisterType<EfDistrictDal>().As<IDistrictDal>().SingleInstance();

            builder.RegisterType<EmployeeManager>().As<IEmployeeService>().SingleInstance();
            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>().SingleInstance();

            builder.RegisterType<EmployerManager>().As<IEmployerService>().SingleInstance();
            builder.RegisterType<EfEmployerDal>().As<IEmployerDal>().SingleInstance();

            builder.RegisterType<FavoriteManager>().As<IFavoriteService>().SingleInstance();
            builder.RegisterType<EfFavoriteDal>().As<IFavoriteDal>().SingleInstance();

            builder.RegisterType<InterestManager>().As<IInterestService>().SingleInstance();
            builder.RegisterType<EfInterestDal>().As<IInterestDal>().SingleInstance();

            builder.RegisterType<JobApplicationManager>().As<IJobApplicationService>().SingleInstance();
            builder.RegisterType<EfJobApplicationDal>().As<IJobApplicationDal>().SingleInstance();

            builder.RegisterType<JobCategoryManager>().As<IJobCategoryService>().SingleInstance();
            builder.RegisterType<EfJobCategoryDal>().As<IJobCategoryDal>().SingleInstance();

            builder.RegisterType<JobManager>().As<IJobService>().SingleInstance();
            builder.RegisterType<EfJobDal>().As<IJobDal>().SingleInstance();

            builder.RegisterType<JobTagManager>().As<IJobTagService>().SingleInstance();
            builder.RegisterType<EfJobTagDal>().As<IJobTagDal>().SingleInstance();

            builder.RegisterType<RatingManager>().As<IRatingService>().SingleInstance();
            builder.RegisterType<EfEmployeeRatingDal>().As<IEmployeeRatingDal>().SingleInstance();
            builder.RegisterType<EfEmployerRatingDal>().As<IEmployerRatingDal>().SingleInstance();

            builder.RegisterType<WorkExperienceManager>().As<IWorkExperienceService>().SingleInstance();
            builder.RegisterType<EfWorkExperienceDal>().As<IWorkExperienceDal>().SingleInstance();

            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();

            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(GetProxyGenerationOptions())
                .SingleInstance();
        }

        private ProxyGenerationOptions GetProxyGenerationOptions()
        {
            return new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            };
        }
    }
}

