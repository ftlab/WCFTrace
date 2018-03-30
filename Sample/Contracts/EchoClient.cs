using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class EchoClient : ClientBase<IEchoService>, IEchoService
    {
        public void Echo(string msg) => Channel.Echo(msg);
    }
}
