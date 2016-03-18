using DiscordSharp;
using DiscordSharp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordClient client = new DiscordClient();
            Console.Write("Username: ");
            client.ClientPrivateInformation.email = Console.ReadLine().Trim();
            Console.Write("Password: ");
            client.ClientPrivateInformation.password = Console.ReadLine().Trim();

            client.Connected += (sender, e) =>
            {
                Console.WriteLine($"Connected! User: {e.user.Email}");
            };
            try
            {
                client.SendLoginRequest();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            client.GetTextClientLogger.LogMessageReceived += delegate (object sender, LoggerMessageReceivedArgs e)
              {
                  Console.WriteLine(e.message.Message);
              };
            /*Thread t = new Thread(client.Connect);
            t.Start();*/
            client.Connect();
            client.MessageReceived += delegate (object sender, DiscordMessageEventArgs e)
            {
                Console.WriteLine(e.message_text);
                if (e.Channel.Name != "general" && e.Channel.Name != "music-shitpost")
                    return;
                if (e.message_text.Contains("<@126012419792306177>") && e.author.ID != "126012419792306177")
                {
                    client.SendMessageToChannel("Stop pinging NorbiPeti, <@" + e.author.ID + ">", e.Channel);
                }
            };
            Console.ReadLine();
        }
    }
}
