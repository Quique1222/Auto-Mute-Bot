using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMuteBot
{
    public class Bot
    {

        public static JsonConfig Config;
        public static DiscordClient Client;
        public async Task RunAsync(JsonConfig config)
        {

            var bconfig = new DiscordConfiguration
            {
                Token = config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Information


            };



            Client = new DiscordClient(bconfig);
            Client.Ready += Client_Ready;
            Client.VoiceStateUpdated += Client_VoiceStateUpdated;
            Config = config;
            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task Client_VoiceStateUpdated(DiscordClient sender, DSharpPlus.EventArgs.VoiceStateUpdateEventArgs e)
        {
            VoiceStateUpdated(e);
            return Task.CompletedTask;
        }

        private Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            Console.WriteLine("Bot ready");
            return Task.CompletedTask;
        }

        public async Task VoiceStateUpdated(DSharpPlus.EventArgs.VoiceStateUpdateEventArgs e)
        {
            if (e.After.Channel == null)
                return;

            if (e.Before?.Channel == e.After?.Channel)
                return;

            DiscordMember member = await e.Guild.GetMemberAsync(e.User.Id);
            try
            {
                if (Config.BypassRoles != null)
                {
                    foreach (var item in member.Roles)
                    {
                        if (Config.BypassRoles.Contains(item.Id))
                        {
                            return;
                        }
                    }
                }

                if (Config.MuteWhenChannelEmpty == false)
                {
                    if (e.After.Channel.Users.ToList().Count == 0)
                        return;
                }


                if (Config.Channels != null)
                {
                    if (Config.Channels.Contains(e.After.Channel.Id))
                    {
                        await member.SetMuteAsync(true);
                        await Task.Delay((int)(Config.MuteSeconds*1000));
                        await member.SetMuteAsync(false);

                    }
                }
                else
                {
                    await member.SetMuteAsync(true);
                    await Task.Delay((int)(Config.MuteSeconds * 1000));
                    await member.SetMuteAsync(false);
                }

            }
            catch (Exception er)
            {
                Console.WriteLine("An error ocurred. " + er.ToString());
            }
        }
    }
}