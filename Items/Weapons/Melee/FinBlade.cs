using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SlayerItems;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class FinBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Getting hit by a fish has got to be humiliating'");

            SoAGlobalItem.SlayerItem.Add(Type);

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 58;

            Item.damage = 78;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.knockBack = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 3, 50);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Lang.GetNPCName(target.type) + " was hit by " + player.name + "'s fish"), Color.White);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(target.name + " was hit by " + player.name + "'s fish"), Color.White);
        }
    }
}