﻿using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class GluttonySoul : SinfulSouls
    {
        public override int SoulType => ModContent.BuffType<GluttonyBuff>();
        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.NeutralHunger, 18000);
            return base.UseItem(player);
        }
    }

    public class GluttonyPlayer : ModPlayer
    {
        public bool gluttony = false;
        public int feed = 100;
        public int feedTimer = 60;
        public int foodCoolDown = 0;
        const int foodCoolDownMax = 300;

        public override void ResetEffects()
        {
            gluttony = false;
            if (!(Player.HasBuff(BuffID.WellFed) || Player.HasBuff(BuffID.WellFed2) || Player.HasBuff(BuffID.WellFed3)))
            {
                if (--feedTimer <= 0)
                {
                    feed--;
                    feedTimer = 30;
                }
            }
            if (foodCoolDown < 0)
            {
                foodCoolDown = 0;
            }
            else if (foodCoolDown > 0)
            {
                foodCoolDown--;
            }
        }

        public override void OnRespawn()
        {
            feed = 100;
            feedTimer = 60;
        }

        public override void OnEnterWorld()
        {
            feed = 100;
            feedTimer = 60;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (gluttony && foodCoolDown <= 0)
            {
                if (target.lifeMax > 5 && target.life > 0 && hit.Crit)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 0f);
                    foodCoolDown = foodCoolDownMax;
                }
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 1f);
                    foodCoolDown = foodCoolDownMax;
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (gluttony && foodCoolDown <= 0)
            {
                if (target.lifeMax > 5 && target.life > 0 && hit.Crit)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 0f);
                    foodCoolDown = foodCoolDownMax;
                }
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 1f);
                    foodCoolDown = foodCoolDownMax;
                }
            }
        }
    }

    public class GluttonyBuff : SinfulSoulBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            GluttonyPlayer gluttonyPlayer = player.Gluttony();

            gluttonyPlayer.gluttony = true;
            player.GetDamage(DamageClass.Melee) += .15f;
            player.statDefense -= 7;

            if (gluttonyPlayer.feed <= 0 && !(player.HasBuff(BuffID.WellFed) || player.HasBuff(BuffID.WellFed2) || player.HasBuff(BuffID.WellFed3)))
            {
                player.starving = true;
            }
            base.Update(player, ref buffIndex);
        }
    }
}
