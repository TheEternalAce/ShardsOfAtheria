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
        public bool lust;

        public override void ResetEffects()
        {
            lust = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (lust && Main.rand.NextBool(50))
                Item.NewItem(target.GetSource_OnHurt(item), target.getRect(), ItemID.Heart);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (lust)
            {
                if (proj.DamageType == DamageClass.Melee)
                {
                    if (Main.rand.NextBool(50))
                    {
                        Item.NewItem(target.GetSource_OnHurt(proj), target.getRect(), ItemID.Heart);
                    }
                }
                else if (Main.rand.NextBool(100))
                {
                    Item.NewItem(target.GetSource_OnHurt(proj), target.getRect(), ItemID.Heart);
                }
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (lust)
            {
                float critChance = 0.25f;
                if (Main.expertMode)
                {
                    critChance += 0.06f;
                }
                if (Main.masterMode)
                {
                    critChance += 0.12f;
                }
                if (Main.hardMode)
                {
                    critChance += 0.08f;
                }
                if (NPC.downedPlantBoss)
                {
                    critChance += 0.06f;
                }
                if (NPC.downedAncientCultist)
                {
                    critChance += 0.12f;
                }
                if (Main.rand.NextFloat() < critChance)
                {
                    damage *= 2;
                }
            }
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
            player.GetModPlayer<LustPlayer>().lust = true;
            base.Update(player, ref buffIndex);
        }
    }
}
