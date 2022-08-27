using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.SevenDeadlySouls;
using ShardsOfAtheria.Items.Weapons;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class SoAGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            switch (item.type)
            {
                case ItemID.Grenade:
                    item.ammo = ItemID.Grenade;
                    break;
                case ItemID.Beenade:
                    item.ammo = ItemID.Grenade;
                    break;
                case ItemID.StickyGrenade:
                    item.ammo = ItemID.Grenade;
                    break;
                case ItemID.BouncyGrenade:
                    item.ammo = ItemID.Grenade;
                    break;
                case ItemID.PartyGirlGrenade:
                    item.ammo = ItemID.Grenade;
                    break;

                case ItemID.PearlwoodHelmet:
                    item.defense = 8;
                    break;
                case ItemID.PearlwoodBreastplate:
                    item.defense = 8;
                    break;
                case ItemID.PearlwoodGreaves:
                    item.defense = 8;
                    break;
                case ItemID.PearlwoodSword:
                    item.defense = 8;
                    break;
                case ItemID.PearlwoodBow:
                    item.damage = 30;
                    item.autoReuse = true;
                    break;

                case ItemID.LifeCrystal:
                    if (ModContent.GetInstance<ConfigServerSide>().upgradeChange)
                    {
                        item.useTime = 15;
                        item.useAnimation = 15;
                        item.autoReuse = true;
                        item.useTurn = true;
                    }
                    break;
                case ItemID.ManaCrystal:
                    if (ModContent.GetInstance<ConfigServerSide>().upgradeChange)
                    {
                        item.useTime = 15;
                        item.useAnimation = 15;
                        item.autoReuse = true;
                        item.useTurn = true;
                    }
                    break;
                case ItemID.LifeFruit:
                    if (ModContent.GetInstance<ConfigServerSide>().upgradeChange)
                    {
                        item.useTime = 15;
                        item.useAnimation = 15;
                        item.autoReuse = true;
                        item.useTurn = true;
                    }
                    break;

                //case ItemID.RocketI:
                //    item.shoot = ProjectileID.RocketI;
                //    break;
                //case ItemID.RocketII:
                //    item.shoot = ProjectileID.RocketII;
                //    break;
                //case ItemID.RocketIII:
                //    item.shoot = ProjectileID.RocketIII;
                //    break;
                //case ItemID.RocketIV:
                //    item.shoot = ProjectileID.RocketIV;
                //    break;
                //case ItemID.DryRocket:
                //    item.shoot = ProjectileID.DryRocket;
                //    break;
                //case ItemID.WetRocket:
                //    item.shoot = ProjectileID.WetRocket;
                //    break;
                //case ItemID.LavaRocket:
                //    item.shoot = ProjectileID.LavaRocket;
                //    break;
                //case ItemID.HoneyRocket:
                //    item.shoot = ProjectileID.HoneyRocket;
                //    break;
                //case ItemID.ClusterRocketI:
                //    item.shoot = ProjectileID.ClusterRocketI;
                //    break;
                //case ItemID.ClusterRocketII:
                //    item.shoot = ProjectileID.ClusterRocketII;
                //    break;
                //case ItemID.MiniNukeI:
                //    item.shoot = ProjectileID.MiniNukeRocketI;
                //    break;
                //case ItemID.MiniNukeII:
                //    item.shoot = ProjectileID.MiniNukeRocketII;
                //    break;
            }
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            if (set == "SoA:Pearlwood")
            {
                player.setBonus = "6 defense\n" +
                    "Increases maximum mana by 40\n" +
                    "Increases damage and critical strike chance by 15%\n" +
                    string.Format("Press {0} to summon 10 Soul Daggers that orbit around you\n" +
                    "Press {0} again to send the daggers flying in the direction of your mouse", ShardsOfAtheria.ArmorSetBonusActive.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.ArmorSetBonusActive.GetAssignedKeys()[0] : "[Unbounded Hotkey]");
                player.statDefense += 5;
                player.statManaMax2 += 40;
                player.GetDamage(DamageClass.Generic) += .15f;
                player.GetCritChance(DamageClass.Generic) += 15;
                player.GetModPlayer<SoAPlayer>().pearlwoodSet = true;
            }
            base.UpdateArmorSet(player, set);
        }

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.PearlwoodHelmet && body.type == ItemID.PearlwoodBreastplate && legs.type == ItemID.PearlwoodGreaves)
            {
                return "SoA:Pearlwood";
            }
            return base.IsArmorSet(head, body, legs);
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type == ItemID.PearlwoodBow)
            {
                player.GetModPlayer<SoAPlayer>().pearlwoodBowShoot++;
                if (player.GetModPlayer<SoAPlayer>().pearlwoodBowShoot == 5)
                {
                    float numberProjectiles = 5;
                    float rotation = MathHelper.ToRadians(15);
                    position += Vector2.Normalize(velocity) * 10f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                        Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<SoulArrow>(), damage, knockback, player.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.Item78);
                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.PinkFairy);
                    }
                    player.GetModPlayer<SoAPlayer>().pearlwoodBowShoot = 0;
                }
            }

            Vector2 vel = Vector2.Normalize(Main.MouseWorld - player.Center);
            if (!player.GetModPlayer<SlayerPlayer>().slayerMode)
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
            if (player.HasBuff(ModContent.BuffType<GluttonyBuff>()))
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
