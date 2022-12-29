using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalItem : GlobalItem
    {
        #region Item Categories
        public static List<int> SlayerItem = new();
        public static List<int> SinfulItem = new();
        public static List<int> Potions = new();
        public static List<int> UpgradeableItem = new();
        /// <summary>
        /// A list to let Conductive potion do it's work easily, automatically adds all items to ElectricWeapon list
        /// </summary>
        public static List<int> AreusWeapon = new();
        /// <summary>
        /// Same as AreusWeapon list, but doesn't add to ElectricWeapon list
        /// </summary>
        public static List<int> DarkAreusWeapon = new();
        /// <summary>
        /// A list of weapons that can erase projectiles
        /// </summary>
        public static List<int> Eraser = new List<int>();
        #endregion

        #region Ammo lists for Ammo Bags
        public static List<int> preHardmodeAmmo = new();
        public static List<int> hardmodeAmmo = new();
        public static List<int> postMoonLordAmmo = new();

        public static List<int> preHardmodeArrows = new();
        public static List<int> hardmodeArrows = new();
        public static List<int> postMoonLordArrows = new();

        public static List<int> preHardmodeBullets = new();
        public static List<int> hardmodeBullets = new();
        public static List<int> postMoonLordBullets = new();

        public static List<int> preHardmodeRockets = new();
        public static List<int> hardmodeRockets = new();
        public static List<int> postMoonLordRockets = new();
        #endregion

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            ShardsServerConfig serverConfig = ModContent.GetInstance<ShardsServerConfig>();
            switch (item.type)
            {
                case ItemID.SilverBullet:
                    // Why don't silbr bullets deal extra damage to werewolves???
                    // Add penetration and extra damage to werewolves
                    item.shoot = ModContent.ProjectileType<SilbrBullet>();
                    break;
                case ItemID.TungstenBullet:
                    // Add penetration and extra velocity
                    item.shoot = ModContent.ProjectileType<TungstenBullet>();
                    item.shootSpeed += 4f;
                    break;

                #region Buff Pearlwood gear, soon to be obsolete
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
                    item.damage = 45;
                    break;
                case ItemID.PearlwoodBow:
                    item.damage = 30;
                    item.autoReuse = true;
                    break;
                #endregion

                #region Make old 1.3 throwing weapons deal throwing damage if config is enabled
                case ItemID.ThrowingKnife:
                case ItemID.Shuriken:
                case ItemID.AleThrowingGlove:
                case ItemID.Snowball:
                case ItemID.RottenEgg:
                case ItemID.PoisonedKnife:
                case ItemID.StarAnise:
                case ItemID.Javelin:
                case ItemID.FrostDaggerfish:
                case ItemID.Bone:
                case ItemID.MolotovCocktail:
                case ItemID.BoneDagger:
                case ItemID.BoneJavelin:
                case ItemID.SpikyBall:
                    item.DamageType = DamageClass.Throwing;
                    break;
                #endregion

                #region Add new grenade ammo type
                case ItemID.Grenade:
                case ItemID.Beenade:
                case ItemID.StickyGrenade:
                case ItemID.BouncyGrenade:
                case ItemID.PartyGirlGrenade:
                    item.ammo = ItemID.Grenade;
                    item.DamageType = DamageClass.Throwing;
                    break;
                #endregion

                #region Make boss summons non-consumable if config is enabled
                case ItemID.SlimeCrown:
                case ItemID.SuspiciousLookingEye:
                case ItemID.BloodySpine:
                case ItemID.WormFood:
                case ItemID.Abeemination:
                case ItemID.DeerThing:
                case ItemID.QueenSlimeCrystal:
                case ItemID.MechanicalWorm:
                case ItemID.MechanicalSkull:
                case ItemID.MechanicalEye:
                case ItemID.LihzahrdPowerCell:
                case ItemID.TruffleWorm:
                case ItemID.EmpressButterfly:
                case ItemID.CelestialSigil:
                    if (serverConfig.nonConsumeBoss)
                    {
                        item.consumable = false;
                        item.maxStack = 1;
                        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[item.type] = 1;
                    }
                    break;
                #endregion

                #region Make small adjustments to Upgrade Items
                case ItemID.LifeCrystal:
                case ItemID.ManaCrystal:
                case ItemID.LifeFruit:
                    if (serverConfig.upgradeChange)
                    {
                        item.useTime = 15;
                        item.useAnimation = 15;
                        item.autoReuse = true;
                        item.useTurn = true;
                    }
                    break;
                    #endregion
            }
            if (ModContent.GetInstance<ShardsServerConfig>().betterWeapon && item.damage > 0)
            {
                item.useTurn = false;
            }
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            base.UpdateArmorSet(player, set);
            if (set == "Shards:Pearlwood")
            {
                player.setBonus = string.Format(Language.GetTextValue("Mods.ShardsOfAtheria.SetBonus.Pearlwood"),
                    ShardsOfAtheria.ArmorSetBonusActive.GetAssignedKeys().Count > 0 ? ShardsOfAtheria.ArmorSetBonusActive.GetAssignedKeys()[0] : "[Unbounded Hotkey]");
                player.statDefense += 5;
                player.statManaMax2 += 40;
                player.GetDamage(DamageClass.Generic) += .15f;
                player.GetCritChance(DamageClass.Generic) += 15;
                player.GetModPlayer<SoAPlayer>().pearlwoodSet = true;
            }
        }

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.PearlwoodHelmet && body.type == ItemID.PearlwoodBreastplate && legs.type == ItemID.PearlwoodGreaves)
            {
                return "Shards:Pearlwood";
            }
            return base.IsArmorSet(head, body, legs);
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(item, player, ref damage);
            if (player.HasBuff(ModContent.BuffType<Conductive>()) && (AreusWeapon.Contains(item.type) || DarkAreusWeapon.Contains(item.type)))
            {
                damage += .15f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
            if (SlayerItem.Contains(item.type))
            {
                var line = new TooltipLine(Mod, "SlayerItem", Language.GetTextValue("Mods.ShardsOfAtheria.Common.SlayerItem"))
                {
                    OverrideColor = Color.Red
                };
                tooltips.Add(line);
            }
            if (UpgradeableItem.Contains(item.type))
            {
                var line = new TooltipLine(Mod, "UpgradeItem", Language.GetTextValue("Mods.ShardsOfAtheria.Common.UpgradeableItem"));
                tooltips.Add(line);
            }
            if (Eraser.Contains(item.type))
            {
                var line = new TooltipLine(Mod, "Eraser", Language.GetTextValue("Mods.ShardsOfAtheria.Common.Eraser"));
                tooltips.Add(line);
            }
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.whoAmI == Main.myPlayer)
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
                SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
                if (slayer.soulCrystalProjectileCooldown == 0 && item.damage > 0)
                {
                    slayer.soulCrystalProjectileCooldown = 60;
                    if (slayer.soulCrystals.Contains(ModContent.ItemType<SkullSoulCrystal>()))
                    {
                        Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 3.5f, ProjectileID.BookOfSkullsSkull, 40, 3.5f, player.whoAmI);
                    }
                    if (slayer.soulCrystals.Contains(ModContent.ItemType<EaterSoulCrystal>()))
                    {
                        Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 16f, ModContent.ProjectileType<VileShot>(), 30, 1, player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Item17);
                    }
                    if (slayer.soulCrystals.Contains(ModContent.ItemType<ValkyrieSoulCrystal>()))
                    {
                        SoundEngine.PlaySound(SoundID.Item1);
                        for (int i = 0; i < 4; i++)
                        {
                            Vector2 projPos = Main.MouseWorld + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                            Projectile proj = Projectile.NewProjectileDirect(item.GetSource_FromThis(), projPos, Vector2.Normalize(Main.MouseWorld - projPos) * 16f, ModContent.ProjectileType<FeatherBladeFriendly>(), 18, 0f, Main.myPlayer);
                            proj.DamageType = DamageClass.Generic;
                        }
                    }
                    if (slayer.soulCrystals.Contains(ModContent.ItemType<BeeSoulCrystal>()))
                    {
                        Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 18f, ModContent.ProjectileType<Stinger>(), 5, 0f, player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Item17);
                    }
                    if (slayer.soulCrystals.Contains(ModContent.ItemType<PrimeSoulCrystal>()))
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
                    if (slayer.soulCrystals.Contains(ModContent.ItemType<PlantSoulCrystal>()))
                    {
                        Projectile.NewProjectile(item.GetSource_FromThis(), player.Center, vel * 16f, ModContent.ProjectileType<VenomSeed>(), 30, 1, player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Item17);
                    }
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.damage > 0 && player.HasBuff(ModContent.BuffType<CreeperShield>()))
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (ModContent.GetInstance<ShardsServerConfig>().betterWeapon && item.shoot == ProjectileID.None)
            {
                player.direction = player.Center.X < Main.MouseWorld.X ? 1 : -1;
            }
            return base.UseItem(item, player);
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (Potions.Contains(item.type) && !ModLoader.TryGetMod("Overhaul", out _))
            {
                Item.NewItem(item.GetSource_FromThis(), player.getRect(), ItemID.Bottle, 1);
            }

            GluttonyPlayer gluttonyPlayer = player.GetModPlayer<GluttonyPlayer>();
            if (gluttonyPlayer.gluttony)
            {
                if (item.buffType == BuffID.WellFed)
                {
                    player.Heal(25);
                    gluttonyPlayer.feed = 50;
                }
                if (item.buffType == BuffID.WellFed2)
                {
                    player.Heal(50);
                    gluttonyPlayer.feed = 75;
                }
                if (item.buffType == BuffID.WellFed3)
                {
                    player.Heal(75);
                    gluttonyPlayer.feed = 100;
                }
            }

            return base.ConsumeItem(item, player);
        }

        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(item, player, target, damage, knockBack, crit);
            if (AreusWeapon.Contains(item.type))
            {
                int buffTime = 600;
                if (player.HasBuff(ModContent.BuffType<Conductive>()))
                {
                    buffTime *= 2;
                }
                if (Main.hardMode)
                {
                    target.AddBuff(BuffID.Electrified, buffTime);
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), buffTime);
                }
            }
        }

        public override void OnHitPvp(Item item, Player player, Player target, int damage, bool crit)
        {
            base.OnHitPvp(item, player, target, damage, crit);
            if (AreusWeapon.Contains(item.type))
            {
                int buffTime = 600;
                if (player.HasBuff(ModContent.BuffType<Conductive>()))
                {
                    buffTime *= 2;
                }
                if (Main.hardMode)
                {
                    target.AddBuff(BuffID.Electrified, buffTime);
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), buffTime);
                }
            }
        }

        public override void UpdateInventory(Item item, Player player)
        {
            base.UpdateInventory(item, player);
            if (item.pick > 0 && item.axe > 0 && item.hammer > 0)
            {
                if (player.HasBuff(ModContent.BuffType<CreeperShield>()))
                {
                    item.damage = 0;
                }
                else
                {
                    item.damage = ContentSamples.ItemsByType[item.type].damage;
                }
            }
        }
    }
}
