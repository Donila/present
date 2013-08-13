using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Ninject.Modules;
using Ninject.Web.Common;
using Present.Core.Domain;
using Present.Core.Domain.Generic;
using Present.Data;
using Present.Domain.Users;

namespace Present.WebMvc.App_Start.Modules
{
    public class DataAccessRegistrationModule : NinjectModule
    {
        /// <summary>
        /// The overriden load method.
        /// </summary>
        public override void Load()
        {
            Bind<DbContext>().To<PresentDbContext>().InRequestScope().WithConstructorArgument(
                "connString", WebConfigurationManager.ConnectionStrings["Present"].ConnectionString).
                WithConstructorArgument(
                    "commandTimeout", int.Parse(WebConfigurationManager.AppSettings.Get("CommandTimeout")));

            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            #region ReadOnly repositories

            Bind<IReadOnlyRepositoryGeneric<User, int>>()
               .To<ReadOnlyRepositoryGeneric<User, int>>()
               .InRequestScope();
            #endregion


            #region Complex repositories
            //Bind<IComplexKeyRepositoryGeneric<PlanDetailDigitalSummaryFilterConfig>>().To
            //    <ComplexKeyRepositoryGeneric<PlanDetailDigitalSummaryFilterConfig>>().InRequestScope();

            #endregion
            #region Repository repositories
            //Bind<IRepositoryGeneric<Document, int>>()
            //   .To<RepositoryGeneric<Document, int>>()
            //   .InRequestScope()
            //   .WithConstructorArgument("keyName", "Id");
            #endregion
        }
    }
}
