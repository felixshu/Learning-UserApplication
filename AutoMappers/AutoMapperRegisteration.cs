using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoMappers
{
    public class AutoMapperRegisteration
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                //Adding Profiles Here
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
