using System.Net;
using CoreRCON;
using System.Threading.Tasks;

namespace GenRconCORE
{
    class GenRCON
    {
        RCON rcon;
        
        public async void ResolveHostname(string address, int port, string password)
        {
            IPAddress destination;
            if (!(IPAddress.TryParse(address, out destination)))
            {
                IPAddress[] hostEntry = await Dns.GetHostAddressesAsync(address);
                destination = hostEntry[0];
            }
            Connect(destination, port, password);
        }

        public void Connect(IPAddress address, int port, string password)
        {
            rcon = new RCON(address, (ushort)port, password);
        }

        public async Task<string> Send(string message)
        {
            string reply = await rcon.SendCommandAsync(message);
            return reply;
        }
    }
}
