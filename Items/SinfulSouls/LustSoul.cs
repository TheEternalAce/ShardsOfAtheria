using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class LustSoul : SinfulSouls
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<LustBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class LustPlayer : ModPlayer
    {
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Player.HasBuff(ModContent.BuffType<LustBuff>()) && Main.rand.NextBool(50))
                Item.NewItem(target.GetSource_OnHurt(item), target.getRect(), ItemID.Heart);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.DamageType == DamageClass.Melee)
            {
                if (Player.HasBuff(ModContent.BuffType<LustBuff>()) && Main.rand.NextBool(50))
                    Item.NewItem(target.GetSource_OnHurt(proj), target.getRect(), ItemID.Heart);
            }
            else if (Player.HasBuff(ModContent.BuffType<LustBuff>()) && Main.rand.NextBool(100))
            {
                Item.NewItem(target.GetSource_OnHurt(proj), target.getRect(), ItemID.Heart);
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (Player.HasBuff(ModContent.BuffType<LustBuff>()) && Main.rand.NextBool(4))
                Player.AddBuff(ModContent.BuffType<StunLock>(), 60);
        }
    }

    public class LustBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.LustSoul"));
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinfulPlayer>().SevenSoulUsed = 4;
            player.maxMinions += 3;
            player.GetDamage(DamageClass.Generic) -= .2f;
            player.AddBuff(BuffID.Lovestruck, 2);
            base.Update(player, ref buffIndex);
        }
    }
}
