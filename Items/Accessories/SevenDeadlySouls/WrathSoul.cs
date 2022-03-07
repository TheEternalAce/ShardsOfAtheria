using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.SevenDeadlySouls
{
    public class WrathSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Each time damage is taken damage is increased by 1%\n" +
                "Once damage bonus goes to 5% increase, crit chance starts to go up by 1 as well\n" +
                "Caps up to 15% damage increase and 20 bonus crit chance");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            tooltips.Add(new TooltipLine(Mod, "WrathSoulDamage", $"Current damage boost: {player.GetModPlayer<WrathPlayer>().anger}"));
            tooltips.Add(new TooltipLine(Mod, "WrathSoulCrit", $"Current crit boost: {player.GetModPlayer<WrathPlayer>().rage}"));
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.value = Item.buyPrice(gold: 2, silver: 10);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().anger;
            player.GetCritChance(DamageClass.Generic) += player.GetModPlayer<WrathPlayer>().rage;
            player.GetModPlayer<WrathPlayer>().wrathSoul = true;
        }
    }

    public class WrathPlayer : ModPlayer
    {
        public bool wrathSoul;

        public float anger;
        public int rage;

        public override void ResetEffects()
        {
            wrathSoul = false;
        }

        public override void PostUpdate()
        {
            if (anger > .15f)
                anger = .15f;
            if (rage > 20)
                rage = 20;
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (wrathSoul)
            {
                anger += .01f;
                if (anger > .05f)
                    rage += 1;
            }
        }

        //public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        //{
        //    if (target.life <= 0)
        //    {
        //        Player.GetModPlayer<WrathPlayer>().anger = 0;
        //        Player.GetModPlayer<WrathPlayer>().rage = 0;
        //    }
        //}
    }

    public class WrathNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            Player player = Main.LocalPlayer;
            if (npc.lifeMax > 5)
            {
                player.GetModPlayer<WrathPlayer>().anger = 0;
                player.GetModPlayer<WrathPlayer>().rage = 0;
            }

        }
    }
}
