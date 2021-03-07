using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Emily
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commandserv;
        private IServiceProvider _serv;

        public async Task RunBotAsync()
        {
            string token = "your token";

            _client.Log += _client_Log;

            _client = new DiscordSocketClient();
            _commandserv = new CommandService();

            _serv = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commandserv)
                .BuildServiceProvider();

            await RegistCommandSync();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegistCommandSync()
        {
            _client.MessageReceived += HandleAdministrationSync;
            await _commandserv.AddModulesAsync(Assembly.GetEntryAssembly(), _serv);
        }

        private async Task HandleAdministrationSync(SocketMessage arg)
        {
            int argPos = 0;
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            if (message.HasStringPrefix("e?", ref argPos))
            {
                var result = await _commandserv.ExecuteAsync(context, argPos, _serv);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
