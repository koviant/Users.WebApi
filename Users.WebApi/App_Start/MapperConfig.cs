using System;
using System.Linq;

using AutoMapper;

namespace Users.WebApi.App_Start
{
    public static class MapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("Users")).ToArray();
                    cfg.AddProfiles(assemblies);
                });
        }
    }
}