using _EasyVenda.API.Configurations;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EasyVenda.UnitTests
{
    public static class AutoMapperHelper
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            return config.CreateMapper();
        }
    }
}
