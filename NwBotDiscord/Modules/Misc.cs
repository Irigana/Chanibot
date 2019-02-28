using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NwBotDiscord.Modules
{
    public class Misc:ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message de "+ Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(0,255,0));
            await Context.Channel.SendMessageAsync("", false,embed);
        }

        [Command("pick")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Random r = new Random();//r devient un nombre aléatoire
            string selection = options[r.Next(0, options.Length)];//choisis un item dans la plage de 0 a la longueur de options
            var embed = new EmbedBuilder();// défini une nouvelle itération de réponse à l'utilisateur
            embed.WithTitle("Choice for" + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(0, 255, 0));
            embed.WithThumbnailUrl("https://muju.deviantart.com/art/Mercy-622238414");
           await Context.Channel.SendMessageAsync("", false, embed);
        }
        [Command("SECRET")]
        public async Task RevealSecret([Remainder] string arg="")
        {
            if (!UserIsSecretOwner((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync(":x: you need the SecretOwner permission to do that" + Context.User.Mention);
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }
        /*
         //Enregistrer des données dans un fichier texte via classe Datastorage.CS
         //A voir dans le futur si applicable
         // voir les possibilités en détail/9*
        [Command("Data")]
        public async Task Getdata()
        {
            await Context.Channel.SendMessageAsync("Data has " + DataStorage.GetPairCount ()+ " pairs");
            DataStorage.pairs.Add("Peter", "Prof");
        }
        */

        [Command("iziel")]
        public async Task iziel([Remainder]string arg="")
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("@Iziel notre grand maitre et créateur, AMEN");
            embed.WithDescription("Oh mon maiiiitre");
            embed.WithColor(new Color(0, 255, 0));
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        private bool UserIsSecretOwner(SocketGuildUser user)
        {
            String TargetRoleName = "SecretOwner";
            var result = from r in user.Guild.Roles
                         where r.Name==TargetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);

        }
    }
}
