﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Helpers.Tests.Fakes
{
    public class ClassWithDependencies : IService
    {
        public ClassWithDependencies(Dependency service)
        {

        }
    }
}
