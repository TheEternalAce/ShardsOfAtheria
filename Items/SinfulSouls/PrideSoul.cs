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
        public int prideTimer = 0;
        public float pride = 0;
        public int shotsFired = 0;
        public int shotsLanded = 0;
        public int shotsTimer = 0;
        public bool prideful = false;

        public override void PreUpdate()
        {
            if (!Player.HasBuff(ModContent.BuffType<PrideBuff>()) && pride > 0)
            {
                pride = 0;
                prideTimer = 0;
                shotsFired = 0;
                shotsLanded = 0;
            }
            else
            {
                if (Player.ShardsOfAtheria().combatTimer == 0 && pride > 0)
                {
                    prideTimer--;
                    if (prideTimer <= -60)
                    {
                        pride -= .02f;
                        prideTimer = 0;
                    }
                }

                if (shotsTimer > 0)
                {
                    if (--shotsTimer == 0)
                    {
                        if (shotsLanded < shotsFired)
                        {
                            Player.AddBuff(BuffID.Slow, 300);
                            Player.AddBuff(BuffID.WitheredArmor, 300);
                            Player.AddBuff(BuffID.WitheredWeapon, 300);
                            shotsFired = 0;
                            shotsLanded = 0;
                            prideful = false;
                        }
                        else
                        {
                            prideful = true;
                        }
                        shotsTimer = -1;
                    }
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            shotsLanded++;
            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            shotsLanded++;
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            pride = 0;
            prideTimer = 0;
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            pride = 0;
            prideTimer = 0;
        }
    }

    public class PrideWeapon : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.HasBuff(ModContent.BuffType<PrideBuff>()))
            {
                PridePlayer pridePlayer = player.GetModPlayer<PridePlayer>();
                if (item.damage > 0 && item.DamageType != DamageClass.Summon && type > ProjectileID.None)
                {
                    pridePlayer.shotsFired++;

                    if (pridePlayer.shotsTimer <= 0)
                    {
                        pridePlayer.shotsTimer = 300;
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
            PridePlayer pridePlayer = Main.LocalPlayer.GetModPlayer<PridePlayer>();
            tip = Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.PrideSoul", pridePlayer.shotsFired, pridePlayer.shotsLanded);
            base.ModifyBuffTip(ref tip, ref rare);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Sinful().SevenSoulUsed = 5;
            if (player.ShardsOfAtheria().inCombat)
            {
                player.GetModPlayer<PridePlayer>().prideTimer++;
            }
            if (player.GetModPlayer<PridePlayer>().prideTimer >= 600)
            {
                player.GetModPlayer<PridePlayer>().pride += .02f;
                player.GetModPlayer<PridePlayer>().prideTimer = 0;
            }
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<PridePlayer>().pride;
            if (player.GetModPlayer<PridePlayer>().prideful)
            {
                player.GetDamage(DamageClass.Generic) += .05f;
            }
            base.Update(player, ref buffIndex);
        }
    }
}
