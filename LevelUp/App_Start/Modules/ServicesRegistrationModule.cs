using AutoMapper;
using Ninject.Modules;
using Ninject.Web.Common;
using Present.Infrastructure.Services.Users;

namespace Present.WebMvc.App_Start.Modules
{
    public class ServicesRegistrationModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IMappingEngine>()
                .ToConstant(Mapper.Engine)
                .InSingletonScope();

            Bind<IAuthorizationControllerService>()
            .To<AuthorizationControllerService>()
           .InRequestScope();
        }
    }
}