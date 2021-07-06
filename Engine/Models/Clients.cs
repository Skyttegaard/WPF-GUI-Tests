using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Clients : BaseNotificationClass
    {
        public string ClientName { get; }
        public string IPAddress { get;}
        public Clients(string clientName, string ipAddress)
        {
            ClientName = clientName;
            IPAddress = ipAddress;
        }
    }
}
