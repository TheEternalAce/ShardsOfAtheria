using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public class GluttonySoul : SevenSouls
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
            player.AddBuff(ModContent.BuffType<GluttonyBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class GluttonyPlayer : ModPlayer
    {
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Player.HasBuff(ModContent.BuffType<GluttonyBuff>()))
            {
                if (target.life > 0 && crit)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 0f);
                }
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 1f);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Player.HasBuff(ModContent.BuffType<GluttonyBuff>()))
            {
                if (target.lifeMax > 5 && target.life > 0 && crit)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 0f);
                }
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 1f);
                }
            }
        }
    }

    public class GluttonyBuff : SevenSoulsBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.GluttonySoul"));
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed = 2;
            player.GetDamage(DamageClass.Melee) += .15f;
            player.statDefense -= 15;
            player.starving = true;
            player.buffImmune[BuffID.WellFed] = true;
            player.buffImmune[BuffID.WellFed2] = true;
            player.buffImmune[BuffID.WellFed3] = true;
            base.Update(player, ref buffIndex);
        }
    }
}
