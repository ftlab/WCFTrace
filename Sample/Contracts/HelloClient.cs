﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class HelloClient : ClientBase<IHelloService>, IHelloService
    {
        public void Hello() => Channel.Hello();
    }
}
