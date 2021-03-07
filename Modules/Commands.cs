using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Emily.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("pat")]
        public async Task Pat(IGuildUser user = null)
        {
            Random rnd = new Random();
            int gifpicrnd = rnd.Next(0, 4);
            if (user == null)
            {
                await ReplyAsync("You need to mention a User for pat :c");
                return;
            }
            if (gifpicrnd == 0 || gifpicrnd == 1)
            {
                await ReplyAsync($"{user.Mention} got pat by {Context.User.Mention}");
                await ReplyAsync("https://i.imgur.com/XjsEMiK.gif");
            }
            if (gifpicrnd == 2)
            {
                await ReplyAsync($"{user.Mention} got pat by {Context.User.Mention}");
                await ReplyAsync("https://i.imgur.com/4wAtWay.gif");
            }
            if (gifpicrnd == 3 || gifpicrnd == 4)
            {
                await ReplyAsync($"{user.Mention} got pat by {Context.User.Mention}");
                await ReplyAsync("https://i.imgur.com/uFZITDD.gif");
            }
        }

        [Command("play")]
        public async Task Play(IGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync($"Wheres your Usermention");
                return;
            }
            else
            {
                // do smth
                await ReplyAsync($"test");
            }
        }

        [Command("simp")]
        public async Task simp(IGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync($"mention your dick");
                return;
            }
            else
            {
                await ReplyAsync($"i wanna simp for you");
            }
        }
        
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage ="You don't have the permission ``ban_member``!")]
        public async Task BanMember(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("You need to Mention a User!"); 
                return;
            }
            if (reason == null) reason = "nothing";

            await Context.Guild.AddBanAsync(user, 1, reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":User : {user.Mention} banned for \n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("Ban Log");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);

            ITextChannel logChannel = Context.Client.GetChannel(1234) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($"User : {user.Mention} banned for \n**Reason** {reason}\n**by** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("Ban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);

        }
    }
}
