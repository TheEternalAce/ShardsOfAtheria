using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class PrideSoul : SinfulSouls
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
            player.AddBuff(ModContent.BuffType<PrideBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class PridePlayer : ModPlayer
    {
        public bool pride;
        public int noHitTimer = 0;
        public float noHitTime = 0;
        public int shotsFired = 0;
        public int shotsLanded = 0;
        public int shotsTimer = 0;
        public bool prideful = false;

        public override void ResetEffects()
        {
            pride = false;
        }

        public override void PreUpdate()
        {
            if (!pride && noHitTimer > 0)
            {
                noHitTime = 0;
                noHitTimer = 0;
                shotsFired = 0;
                shotsLanded = 0;
            }
            else
            {
                if (Player.ShardsOfAtheria().combatTimer == 0 && noHitTime > 0)
                {
                    noHitTimer--;
                    if (noHitTimer <= -60)
                    {
                        noHitTime -= .02f;
                        noHitTimer = 0;
                    }
                }

                if (shotsTimer > 0)
                {
                    if (--shotsTimer == 0)
                    {
                        if (shotsLanded > shotsFired * 0.8)
                        {
                            prideful = true;
                        }
                        else if (shotsLanded < shotsFired * 0.5)
                        {
                            Player.AddBuff(BuffID.Slow, 300);
                            Player.AddBuff(BuffID.WitheredArmor, 300);
                            Player.AddBuff(BuffID.WitheredWeapon, 300);
                            shotsFired = 0;
                            shotsLanded = 0;
                            prideful = false;
                        }
                        shotsTimer = -1;
                    }
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (pride && proj.DamageType != DamageClass.Summon)
            {
                shotsLanded++;
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (pride)
            {
                noHitTime = 0;
                noHitTimer = 0;
            }
        }

        public override void UpdateDead()
        {
            if (pride)
            {
                prideful = false;
                noHitTime = 0;
                noHitTimer = 0;
                shotsFired = 0;
                shotsLanded = 0;
            }
        }
    }

    public class PrideWeapon : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Pride().pride)
            {
                if (player.ShardsOfAtheria().inCombat && item.DamageType != DamageClass.Summon)
                {
                    PridePlayer pridePlayer = player.Pride();
                    if (item.damage > 0 && item.DamageType != DamageClass.Summon && type > ProjectileID.None)
                    {
                        pridePlayer.shotsFired++;

                        if (pridePlayer.shotsTimer <= 0)
                        {
                            pridePlayer.shotsTimer = 300;
                        }
                    }
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }

    public class PrideBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.PrideSoul"));
            base.SetStaticDefaults();
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            PridePlayer pridePlayer = Main.LocalPlayer.Pride();
            tip = Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.PrideSoul", pridePlayer.shotsFired, pridePlayer.shotsLanded);
            base.ModifyBuffTip(ref tip, ref rare);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Pride().pride = true;
            if (player.ShardsOfAtheria().inCombat)
            {
                player.Pride().noHitTimer++;
            }
            if (player.Pride().noHitTimer >= 600)
            {
                player.Pride().noHitTime += .02f;
                player.Pride().noHitTimer = 0;
            }
            player.GetDamage(DamageClass.Generic) += player.Pride().noHitTime;
            if (player.Pride().prideful)
            {
                player.GetDamage(DamageClass.Generic) += .5f;
            }
            base.Update(player, ref buffIndex);
        }
    }
}
