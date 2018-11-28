﻿using EC.DataAccess.Repositories.Implementation;
using EC.DataAccess.Repositories.Interfaces;
using StructureMap;

namespace EC.DataAccess.Dependency
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IPhoneRepository>().Use<PhoneRepository>();
        }
    }
}
