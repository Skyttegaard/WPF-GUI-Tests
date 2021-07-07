namespace Engine.Models
{
    public class Clients : BaseNotificationClass
    {
        public string ClientName { get; }
        public string IPAddress { get; }
        public Clients(string clientName, string ipAddress)
        {
            ClientName = clientName;
            IPAddress = ipAddress;
        }
    }
}
