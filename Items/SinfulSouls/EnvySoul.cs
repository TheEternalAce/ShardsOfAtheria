using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class EnvySoul : SinfulSouls
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<EnvyBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class EnvyPlayer : ModPlayer
    {
        public bool targetFound;
        public int focusDamage;

        public override void ResetEffects()
        {
            targetFound = false;
        }

        public override void PostUpdate()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].HasBuff(ModContent.BuffType<EnvyTarget>()))
                {
                    targetFound = true;
                }
            }

            base.PostUpdate();
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life <= 0 && target.HasBuff(ModContent.BuffType<EnvyTarget>()))
            {
                targetFound = false;
                focusDamage = 0;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.life <= 0 && target.HasBuff(ModContent.BuffType<EnvyTarget>()))
            {
                targetFound = false;
                focusDamage = 0;
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Player.HasBuff(ModContent.BuffType<EnvyBuff>()))
            {
                if (!target.HasBuff(ModContent.BuffType<EnvyTarget>()) && !targetFound)
                {
                    target.AddBuff(ModContent.BuffType<EnvyTarget>(), 300);
                }
                else if (target.HasBuff(ModContent.BuffType<EnvyTarget>()))
                {
                    damage += focusDamage;
                    focusDamage++;
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Player.HasBuff(ModContent.BuffType<EnvyBuff>()))
            {
                if (!target.HasBuff(ModContent.BuffType<EnvyTarget>()) && !targetFound)
                {
                    target.AddBuff(ModContent.BuffType<EnvyTarget>(), 300);
                }
                else if (target.HasBuff(ModContent.BuffType<EnvyTarget>()))
                {
                    damage += focusDamage;
                    focusDamage++;
                }
            }
        }
    }

    public class EnvyTarget : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Items/SinfulSouls/EnvyBuff";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }

    public class EnvyBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.EnvySoul"));
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinfulPlayer>().SevenSoulUsed = 1;
            player.AddBuff(BuffID.Darkness, 2);
            base.Update(player, ref buffIndex);
        }
    }
}
