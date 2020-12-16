using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutoMuteBot
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("config.json"))
            {
                Console.WriteLine("ERROR. No config file found. Creating one");
                string output = JsonConvert.SerializeObject(new JsonConfig());
                File.WriteAllText("config.json", output);
            }

            JsonConfig config = JsonConvert.DeserializeObject<JsonConfig>(File.ReadAllText("config.json"));
            Bot bot = new Bot();
            bot.RunAsync(config).GetAwaiter().GetResult();
        }
    }
}
