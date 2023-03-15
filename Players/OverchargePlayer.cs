using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Players
{
    public class OverchargePlayer : ModPlayer
    {
        public float overcharge = 0f;
        public float charge;
        public bool overcharged = false;

        public override void ResetEffects()
        {
            charge = 0f;
            overcharged = overcharge >= 1f;
        }
    }

    public class OverchargedProjectile : GlobalProjectile
    {
        public bool overcharged = false;

        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Main.player[projectile.owner].GetModPlayer<OverchargePlayer>().overcharged)
            {
                projectile.GetGlobalProjectile<OverchargedProjectile>().overcharged = true;
            }
            else if (source is EntitySource_Parent parentSourceProj && parentSourceProj.Entity is Projectile proj)
            {
                projectile.GetGlobalProjectile<OverchargedProjectile>().overcharged = proj.GetGlobalProjectile<OverchargedProjectile>().overcharged;
            }
            base.OnSpawn(projectile, source);
        }
    }

    public abstract class OverchargeWeapon : ModItem
    {
        public float chargeAmount = 0.1f;
        public float chargeVelocity = 16f;
        /// <summary>
        /// Prevents the weapon from firing projectiles when overcharged.
        /// </summary>
        public bool overchargeShoot = true;

        public override void SetDefaults()
        {
            chargeVelocity = Item.shootSpeed;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string key = "Mods.ShardsOfAtheria.Common.OverchargeCapable";
            var line = new TooltipLine(Mod, "OverchargeCapable", Language.GetTextValue(key, chargeAmount));
            tooltips.AddTooltip(line);
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                OverchargePlayer overchargePlayer = player.GetModPlayer<OverchargePlayer>();
                if (player.ShardsOfAtheria().inCombat)
                {
                    float chargeToAdd = chargeAmount + overchargePlayer.charge;
                    if (Item.useTime != Item.useAnimation)
                    {
                        if (!(player.itemAnimation < Item.useAnimation - 2))
                        {
                            overchargePlayer.overcharge += chargeToAdd;
                        }
                    }
                    else
                    {
                        overchargePlayer.overcharge += chargeToAdd;
                    }
                }
                if (overchargePlayer.overcharged)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath56.WithVolumeScale(0.5f), player.Center);
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * chargeVelocity;
                    DoOverchargeEffect(player, ModContent.ProjectileType<LightningBoltFriendly>(), 2f, velocity, 1f);
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Electric);
                        dust.velocity *= 8f;
                    }
                }
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<OverchargePlayer>().overcharged)
            {
                return overchargeShoot;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public virtual void DoOverchargeEffect(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 0)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                Projectile proj = Projectile.NewProjectileDirect(Item.GetSource_ItemUse(Item), player.Center, velocity, projType,
                    (int)(player.GetWeaponDamage(Item) * damageMultiplier), Item.knockBack, player.whoAmI, 0f, ai1);
                proj.GetGlobalProjectile<OverchargedProjectile>().overcharged = true;
                proj.DamageType = Item.DamageType;
                ConsumeOvercharge(player);
            }
        }

        public void ConsumeOvercharge(Player player)
        {
            player.GetModPlayer<OverchargePlayer>().overcharge = 0;
        }
    }
}
