using System.Net;
using CoreRCON;
using System.Threading.Tasks;

namespace GenRconCORE
{
    class GenRCON
    {
        RCON rcon;
        
        public async Task<bool> Connect(string address, int port, string password)
        {
            IPAddress destination;

            try
            {
                if (!(IPAddress.TryParse(address, out destination)))
                {
                    IPAddress[] hostEntry = await Dns.GetHostAddressesAsync(address);
                    destination = hostEntry[0];
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            
            rcon = new RCON(destination, (ushort)port, password);
            return true;
        }

        public async Task<string> Send(string message)
        {
            int timeout = 1000;
            var task = rcon.SendCommandAsync(message);

            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                return await rcon.SendCommandAsync(message);
            }
            else
            {
                return "Server took too long to reply. Invalid Command?";
            }
        }

    }
}
