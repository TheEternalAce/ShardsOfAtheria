using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Accessories.SevenDeadlySouls;
using ShardsOfAtheria.Items.Weapons;
using ShardsOfAtheria.Projectiles;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
	public class SoAGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SlimeCrown || item.type == ItemID.SuspiciousLookingEye || item.type == ItemID.BloodySpine || item.type == ItemID.WormFood || item.type == ItemID.Abeemination
                || item.type == ItemID.ClothierVoodooDoll || item.type == ItemID.DeerThing || item.type == ItemID.GuideVoodooDoll || item.type == ItemID.QueenSlimeCrystal || item.type == ItemID.MechanicalWorm
                || item.type == ItemID.MechanicalSkull || item.type == ItemID.MechanicalEye || item.type == ItemID.LihzahrdPowerCell || item.type == ItemID.TruffleWorm || item.type == ItemID.EmpressButterfly
                || item.type == ItemID.CelestialSigil)
            {
                item.value = Item.buyPrice(0, 5);
            }
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vel = Vector2.Normalize(Main.MouseWorld - player.Center);
            if (!ModContent.GetInstance<SoAWorld>().slayerMode)
            {
                return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
            }
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (slayer.soulCrystalProjectileCooldown == 0)
            {
                slayer.soulCrystalProjectileCooldown = 60;
                if (slayer.SkullSoul && item.damage > 0)
                {
                    Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 3.5f, ProjectileID.BookOfSkullsSkull, 40, 3.5f, player.whoAmI);
                }
                if (slayer.EaterSoul && item.damage > 0)
                {
                    Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 16f, ModContent.ProjectileType<VileShot>(), 30, 1, player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item17);
                }
                if (slayer.ValkyrieSoul && item.damage > 0)
                {
                    SoundEngine.PlaySound(SoundID.Item1);
                    Projectile.NewProjectile(item.GetSource_FromThis(), Main.MouseWorld + new Vector2(125, 125), Vector2.Normalize(Main.MouseWorld - (Main.MouseWorld + new Vector2(125, 125))) * 7f, ModContent.ProjectileType<FeatherBladeFriendly>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(item.GetSource_FromThis(), Main.MouseWorld + new Vector2(150, -125), Vector2.Normalize(Main.MouseWorld - (Main.MouseWorld + new Vector2(150, -125))) * 7f, ModContent.ProjectileType<FeatherBladeFriendly>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(item.GetSource_FromThis(), Main.MouseWorld + new Vector2(-125, 125), Vector2.Normalize(Main.MouseWorld - (Main.MouseWorld + new Vector2(-125, 125))) * 7f, ModContent.ProjectileType<FeatherBladeFriendly>(), 18, 0f, Main.myPlayer);
                    Projectile.NewProjectile(item.GetSource_FromThis(), Main.MouseWorld + new Vector2(-125, -150), Vector2.Normalize(Main.MouseWorld - (Main.MouseWorld + new Vector2(-125, -150))) * 7f, ModContent.ProjectileType<FeatherBladeFriendly>(), 18, 0f, Main.myPlayer);
                }
                if (slayer.BeeSoul && item.damage > 0)
                {
                    Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 18f, ModContent.ProjectileType<Stinger>(), 5, 0f, player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item17);
                }
                if (slayer.PrimeSoul && item.damage > 0)
                {
                    Main.rand.Next(2);
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 10f, ProjectileID.MiniRetinaLaser, 40, 3.5f, player.whoAmI);
                            break;
                        case 1:
                            Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 8f, ProjectileID.RocketI, 40, 3.5f, player.whoAmI);
                            break;
                        case 2:
                            Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 8f, ProjectileID.Grenade, 40, 3.5f, player.whoAmI);
                            break;
                    }
                }
                if (slayer.PlantSoul && item.damage > 0)
                {
                    Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 16f, ModContent.ProjectileType<VenomSeed>(), 30, 1, player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item17);
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.HeldItem.damage > 0 && player.HasBuff(ModContent.BuffType<CreeperShield>()))
                return false;
            return base.CanUseItem(item, player);
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (player.GetModPlayer<GluttonyPlayer>().gluttonySoul)
            {
                if (item.buffType == BuffID.WellFed)
                {
                    player.HealEffect(25);
                    player.statLife += 25;
                }
                if (item.buffType == BuffID.WellFed2)
                {
                    player.HealEffect(50);
                    player.statLife += 50;
                }
                if (item.buffType == BuffID.WellFed3)
                {
                    player.HealEffect(75);
                    player.statLife += 75;
                }
            }

            return base.ConsumeItem(item, player);
        }

        public override void UpdateInventory(Item item, Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<CreeperShield>()) && item.pick > 0 && item.axe > 0 && item.hammer > 0)
            {
                Item copy = new(item.type);
                item.damage = copy.damage;
            }
        }
    }
}
