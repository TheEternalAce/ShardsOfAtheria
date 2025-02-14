using ShardsOfAtheria.Buffs.PlayerDebuff.SinDebuffs;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class GluttonySoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<GluttonyBuff>();
    }

    public class GluttonyPlayer : ModPlayer
    {
        public bool soulActive = false;
        public int hunger = 100;
        public const int HUNGER_MAX = 100;
        public int hungerTimer = 60;
        public int foodCoolDown = 0;
        public const int FOOD_COOLDOWN_MAX = 240;

        public override void ResetEffects()
        {
            soulActive = false;
            if (foodCoolDown < 0) foodCoolDown = 0;
            else if (foodCoolDown > 0) foodCoolDown--;
        }

        public override void OnRespawn()
        {
            hunger = 600;
        }

        public override void OnEnterWorld()
        {
            hunger = 100;
            hungerTimer = 60;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff<OverStuffed>()) Player.lifeRegen /= 3;
        }

        public override void PreUpdate()
        {
            if (soulActive)
            {
                if (hunger > 0 && --hungerTimer <= 0)
                {
                    hunger--;
                    if (!(Player.HasBuff(BuffID.WellFed) || Player.HasBuff(BuffID.WellFed2) || Player.HasBuff(BuffID.WellFed3))) hunger--;
                    hungerTimer = 30;
                }
                if (hunger > 600) Player.AddBuff<OverStuffed>(121);
                else if (hunger < 0) hunger = 0;
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (soulActive && hunger > 1)
            {
                int maxReduction = ShardsHelpers.ScaleByProggression(Player, 10);
                int reduction = Math.Min(hunger / 2, maxReduction);
                modifiers.FinalDamage.Flat -= maxReduction;
                hunger -= reduction * 2;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (soulActive && foodCoolDown <= 0)
            {
                float foodChance = 0.1f;
                if (hit.Crit || target.life <= 0) foodChance = 1f;
                if (target.lifeMax > 5 && Main.rand.NextFloat() < foodChance)
                {
                    float ai0 = 0;
                    if (target.life <= 0) ai0 = 1;
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, ai0);
                    foodCoolDown = FOOD_COOLDOWN_MAX;
                }
            }
        }
    }

    public class GluttonyBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.GluttonySoul.Tooltip");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            GluttonyPlayer gluttonyPlayer = player.Gluttony();

            gluttonyPlayer.soulActive = true;
            player.GetDamage(DamageClass.Generic) += .15f;
            player.statDefense -= 7;

            if (gluttonyPlayer.hunger <= 0 && !(player.HasBuff(BuffID.WellFed) || player.HasBuff(BuffID.WellFed2) || player.HasBuff(BuffID.WellFed3)))
            {
                player.starving = true;
            }
            base.Update(player, ref buffIndex);
        }
    }
}
