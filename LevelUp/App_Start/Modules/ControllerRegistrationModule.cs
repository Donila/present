using System.Collections.Generic;
using System.Web.Configuration;
using Ninject.Modules;

namespace Present.WebMvc.App_Start.Modules
{
    public class ControllerRegistrationModule : NinjectModule
    {
        public override void Load()
        {
           

            // examples !!!!!!!!!!!!!!!!!!!!!

            //this.Bind<AvatarController>()
            //    .ToSelf()
            //    .WithConstructorArgument("avatarPath", WebConfigurationManager.AppSettings.Get("ProfileAvatarPath"));

            //Bind<CustomDataRequestController>().ToSelf()
            //    .WithConstructorArgument("uploadPath", WebConfigurationManager.AppSettings.Get("CustomRequestImagePath"))
            //    .WithConstructorArgument("filesPath", WebConfigurationManager.AppSettings.Get("DataResponseFilePath"));

        


        }
    }
}
