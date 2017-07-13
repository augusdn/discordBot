using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        string[] freshestMemes;

        public MyBot()
        {
            rand = new Random();

            freshestMemes = new string[]
            {
                "memes/meme1.jpg",
                "memes/meme2.jpg",
                "memes/meme3.jpg",
                "memes/meme4.jpg"
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterMemeCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzM0OTg0ODEyNjM5MDI3MjEw.DEjWvw.19MwXHtk9b7vlcyprzZQCnmFwH0", TokenType.Bot);
            });
        }

        private void RegisterMemeCommand()
        {
            commands.CreateCommand("meme")
                .Do(async (e) =>
                {
                    int randomMemeIndex = rand.Next(freshestMemes.Length);
                    string memeToPost = freshestMemes[randomMemeIndex];
                    await e.Channel.SendFile(memeToPost);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
