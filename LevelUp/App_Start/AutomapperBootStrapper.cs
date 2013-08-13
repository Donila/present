using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Present.WebMvc.App_Start.ObjectMapping.Users;

namespace Present.WebMvc.App_Start
{
    public static class AutomapperBootStrapper
    {
        /// <summary>
        /// The configuration method
        /// </summary>
        public static void Configure()
        {
          

            Mapper.Configuration
                .MapUsers();

        }
    }
}

