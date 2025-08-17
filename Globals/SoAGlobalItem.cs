using BattleNetworkElements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Consumable;
using ShardsOfAtheria.Items.Placeable.Furniture;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.NPCs.Town.TheArchivist;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
using ShardsOfAtheria.NPCs.Variant.Harpy;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Projectiles.Summon;
using ShardsOfAtheria.Projectiles.Tools;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalItem : GlobalItem
    {
        #region Item Categories
        public static readonly List<int> Potions = [];
        public static readonly List<int> UpgradeableItem = [];
        // Potentially use this in place of the above List<int>
        //public static readonly Dictionary<int, int> UpgradeableItems = new();
        /// <summary>
        /// A list to let Conductive potion do it's work easily
        /// Automatically adds the electric element to the item if it isn't dark
        /// </summary>
        public static readonly Dictionary<int, bool> AreusItem = [];
        /// <summary>
        /// A list of weapons that can erase projectiles or spawn projectiles that can erase other projectiles
        /// </summary>
        public static readonly List<int> Eraser = [];
        #endregion

        int transformTimer = 0;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            if (item.type == ItemID.Feather)
                item.color = new Color(101, 187, 236);
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(item, player, ref damage);
            if (item.IsAreus(true))
            {
                AreusArmorPlayer armorPlayer = player.Areus();
                damage += armorPlayer.areusDamage;
            }

            if (SoA.BNEEnabled)
            {
                ConductiveEffects(item, player, ref damage);
            }
        }

        [JITWhenModsEnabled("BattleNetworkElements")]
        private static void ConductiveEffects(Item item, Player player, ref StatModifier damage)
        {
            if (player.Shards().conductive && item.IsElec())
            {
                damage += .15f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
            if (item.ModItem != null)
            {
                string overdriveKey = item.ModItem.GetLocalizationKey("Overdrive");
                if (Language.Exists(overdriveKey))
                {
                    if (Main.LocalPlayer.Overdrive())
                    {
                        tooltips[0].Text += ShardsHelpers.LocalizeCommon("OverdriveTag");
                        var line = new TooltipLine(Mod, "OverdriveHeader",
                            ShardsHelpers.LocalizeCommon("OverdriveTooltip"));
                        tooltips.Insert(ShardsHelpers.GetIndex(tooltips, "OneDropLogo"), line);
                        line = new TooltipLine(Mod, "OverdriveEffect",
                            Language.GetTextValue(overdriveKey));
                        tooltips.Insert(ShardsHelpers.GetIndex(tooltips, "OneDropLogo"), line);
                    }
                }
                string devKey = item.ModItem.GetLocalizationKey("DeveloperKeyChange");
                if (Language.Exists(devKey))
                {
                    var devCard = ToggleableTool.GetInstance<DevelopersKeyCard>(Main.LocalPlayer);
                    bool cardActive = devCard != null && devCard.Active;
                    if (cardActive)
                    {
                        var line = new TooltipLine(Mod, "DevKeyHeader",
                            ShardsHelpers.LocalizeCommon("DeveloperKeyHeader"));
                        tooltips.Insert(ShardsHelpers.GetIndex(tooltips, "OneDropLogo"), line);
                        line = new TooltipLine(Mod, "DevKeyChange",
                            Language.GetTextValue(devKey));
                        tooltips.Insert(ShardsHelpers.GetIndex(tooltips, "OneDropLogo"), line);
                    }
                }
            }
            if (UpgradeableItem.Contains(item.type))
            {
                var line = new TooltipLine(Mod, "UpgradeItem",
                    ShardsHelpers.LocalizeCommon("UpgradeableItem"));
                tooltips.Insert(ShardsHelpers.GetIndex(tooltips, "OneDropLogo"), line);
            }
            if (Eraser.Contains(item.type))
            {
                var line = new TooltipLine(Mod, "Eraser",
                    ShardsHelpers.LocalizeCommon("Eraser"));
                tooltips.Insert(ShardsHelpers.GetIndex(tooltips, "OneDropLogo"), line);
            }
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.damage > 0)
            {
                SlayerPlayer slayer = player.Slayer();
                var shards = player.Shards();
                if (player.whoAmI == Main.myPlayer)
                {
                    Vector2 directionToMouse = player.Center.DirectionTo(Main.MouseWorld);
                    if (slayer.soulCrystalProjectileCooldown == 0)
                    {
                        slayer.soulCrystalProjectileCooldown = 60;
                        if (slayer.SkeletronSoul)
                        {
                            Projectile.NewProjectile(source, player.Center, directionToMouse * 3.5f, ProjectileID.BookOfSkullsSkull, 40, 3.5f, player.whoAmI);
                        }
                        if (slayer.EoWSoul)
                        {
                            Projectile.NewProjectile(source, player.Center, directionToMouse * 16f, ModContent.ProjectileType<VileShot>(), 30, 1, player.whoAmI);
                            SoundEngine.PlaySound(SoundID.Item17, position);
                        }
                        if (slayer.NovaSoul && item.type != ModContent.ItemType<ValkyrieBlade>())
                        {
                            SoundEngine.PlaySound(SoundID.Item1, position);
                            int projtype = ModContent.ProjectileType<HardlightBlade>();
                            ShardsHelpers.ProjectileRing(source,
                                Main.MouseWorld, 4, 150f, 16f, projtype, 18, 0f, player.whoAmI, 1);
                        }
                        if (slayer.BeeSoul)
                        {
                            Projectile.NewProjectile(source, player.Center, directionToMouse * 18f, ModContent.ProjectileType<Stinger>(), 5, 0f, player.whoAmI);
                            SoundEngine.PlaySound(SoundID.Item17, position);
                        }
                        if (slayer.SkeletronSoul)
                        {
                            Main.rand.Next(2);
                            switch (Main.rand.Next(3))
                            {
                                case 0:
                                    Projectile.NewProjectile(source, player.Center, directionToMouse * 10f, ProjectileID.MiniRetinaLaser, 40, 3.5f, player.whoAmI);
                                    break;
                                case 1:
                                    Projectile.NewProjectile(source, player.Center, directionToMouse * 8f, ProjectileID.RocketI, 40, 3.5f, player.whoAmI);
                                    break;
                                case 2:
                                    Projectile.NewProjectile(source, player.Center, directionToMouse * 8f, ProjectileID.Grenade, 40, 3.5f, player.whoAmI);
                                    break;
                            }
                        }
                        if (slayer.PlanteraSoul)
                        {
                            Projectile.NewProjectile(source, player.Center, directionToMouse * 16f, ModContent.ProjectileType<VenomSeed>(), 30, 1, player.whoAmI);
                            SoundEngine.PlaySound(SoundID.Item17, position);
                        }
                        if (slayer.DeathSoul && player.bleed)
                        {
                            int bloodProjectile = 0;
                            float amount = 1f;
                            float rotation = MathHelper.ToRadians(15);
                            bool evenSpread = true;
                            float speedVariation = 0.33f;
                            if (item.DamageType.CountsAsClass(DamageClass.Melee))
                            {
                                bloodProjectile = ModContent.ProjectileType<BloodKris>();
                                amount = 3;
                                speedVariation = 0f;
                            }
                            else if (item.DamageType.CountsAsClass(DamageClass.Ranged)) bloodProjectile = ModContent.ProjectileType<BloodShuriken>();
                            else if (item.DamageType.CountsAsClass(DamageClass.Magic))
                            {
                                bloodProjectile = ModContent.ProjectileType<BloodGlob>();
                                amount = 5;
                                evenSpread = false;
                            }
                            else if (item.DamageType.CountsAsClass(DamageClass.Summon)) bloodProjectile = ModContent.ProjectileType<BloodDart>();
                            if (bloodProjectile > 0) ShardsHelpers.ProjectileSpread(source, player.Center, directionToMouse * 16f, amount, rotation, evenSpread, bloodProjectile, 200 / (int)amount, 7f, player.whoAmI, speedVariation: speedVariation);
                        }
                    }
                }
            }
            if (ShardsHelpers.TryGetModContent("GMR", "OvercooledNailgun", out ModItem nailgun) && item.type == nailgun.Type && player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity *= 2.75f, ModContent.ProjectileType<StickingMagnetProj>(), 1, knockback);
                return false;
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            if (ShardsHelpers.TryGetModContent("GMR", "OvercooledNailgun", out ModItem nailgun) && item.type == nailgun.Type)
                return player.ownedProjectileCounts[ModContent.ProjectileType<StickingMagnetProj>()] < 3;
            return base.AltFunctionUse(item, player);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.damage > 0 && player.HasBuff(ModContent.BuffType<CreeperShield>()))
            {
                return false;
            }
            if (ShardsHelpers.TryGetModContent("GMR", "OvercooledNailgun", out ModItem nailgun) && item.type == nailgun.Type)
            {
                var defaultNailgun = new Item(nailgun.Type);
                if (player.altFunctionUse == 2)
                {
                    item.reuseDelay = 3;
                    item.noUseGraphic = false;
                }
                else
                {
                    item.reuseDelay = defaultNailgun.reuseDelay;
                    item.noUseGraphic = true;
                }
            }
            return true;
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.DamageType.CountsAsClass(DamageClass.Magic)) player.ClearBuff<SpareChange>();
            if (item.type == ItemID.BugNet ||
                item.type == ItemID.FireproofBugNet ||
                item.type == ItemID.GoldenBugNet)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (SoA.ServerConfig.catchableNPC)
                    {
                        if (npc.type == ModContent.NPCType<Atherian>())
                        {
                            Main.npcCatchable[npc.type] = true;
                            npc.catchItem = ModContent.ItemType<AtherianSummonItem>();
                        }
                        if (npc.type == ModContent.NPCType<Archivist>())
                        {
                            Main.npcCatchable[npc.type] = true;
                            npc.catchItem = ModContent.ItemType<ArchivistSummonItem>();
                        }
                    }
                    else
                    {
                        if (npc.type == ModContent.NPCType<Atherian>() ||
                            npc.type == ModContent.NPCType<Archivist>())
                        {
                            Main.npcCatchable[npc.type] = false;
                            npc.catchItem = -1;
                        }
                    }
                }
            }
            var shards = player.Shards();
            if (item.damage > 0)
            {
                if (player == Main.LocalPlayer)
                {
                    var directionToMouse = player.Center.DirectionTo(Main.MouseWorld);

                    if (shards.spoonBender && shards.spoonBenderCD == 0)
                    {
                        Rectangle rect = new((int)player.Center.X - 50, (int)player.Center.Y - 50, 100, 100);
                        var spoon = ModContent.GetInstance<TwistedUtensil>().Item;
                        int tearDamage = player.GetWeaponDamage(spoon);
                        float tearKB = player.GetWeaponKnockback(spoon);
                        Projectile.NewProjectile(player.GetSource_FromThis(), Main.rand.NextVector2FromRectangle(rect), directionToMouse * 8f, spoon.shoot, tearDamage, tearKB);
                        shards.spoonBenderCD = 25;
                    }
                }
                if (shards.prototypeBand && shards.prototypeBandCooldown == 0)
                {
                    if (player.HasItemEquipped<PrototypeAreusBand>(out var modItem))
                    {
                        var band = modItem as PrototypeAreusBand;
                        band.UseEffect(player);
                    }
                    shards.prototypeBandCooldown = shards.prototypeBandCooldownMax;
                }
                var areus = player.Areus();
                if (areus.imperialSet && areus.WarriorSetChip && player.HasBuff<ShadeState>())
                {
                    var spawnpos = Main.MouseWorld + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 130f;
                    var vector = Vector2.Normalize(Main.MouseWorld - spawnpos) * 32;
                    var meleeDamage = player.GetTotalDamage(DamageClass.Melee);
                    Projectile.NewProjectile(player.GetSource_FromThis(), spawnpos, vector, ModContent.ProjectileType<VoidSlash>(),
                        (int)meleeDamage.ApplyTo(120 + areus.imperialVoid), 8f);
                }
                if (player.Slayer().DeathSoul)
                {
                    if (player.bleed)
                    {
                        if (item.DamageType.CountsAsClass(DamageClass.Magic))
                        {

                        }
                        if (item.DamageType.CountsAsClass(DamageClass.Melee))
                        {

                        }
                        if (item.DamageType.CountsAsClass(DamageClass.Ranged))
                        {

                        }
                        if (item.DamageType.CountsAsClass(DamageClass.Summon))
                        {

                        }
                        if (item.DamageType.CountsAsClass(DamageClass.Throwing))
                        {

                        }
                        if (item.DamageType.CountsAsClass(DamageClass.Generic))
                        {

                        }
                    }
                }
            }
            if (player.ItemAnimationJustStarted)
            {
                if (item.pick > 0)
                {
                    var gem = player.Gem();
                    if (gem.greaterAmethystCore && gem.amethystBomb)
                    {
                        var spawnpos = player.Center;
                        var vector = Vector2.Normalize(Main.MouseWorld - spawnpos) * 16f;
                        Projectile.NewProjectile(player.GetSource_FromThis(), spawnpos, vector, ModContent.ProjectileType<AmethystBomb>(), 120, 8f);
                    }
                }
            }
            return null;
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.FloatingIslandFishingCrateHard)
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<OmegaKey>(), 14));
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            var areusPlayer = player.Areus();
            if (areusPlayer.bannerResourceManagement && item.IsWeapon() && Main.rand.NextBool(3)) return false;

            if (Potions.Contains(item.type) && !ModLoader.TryGetMod("Overhaul", out _))
                Item.NewItem(item.GetSource_FromThis(), player.getRect(), ItemID.Bottle, 1);

            var gluttonyPlayer = player.Gluttony();
            if (gluttonyPlayer.soulActive)
            {
                int gluttonyHealing = item.buffTime;
                if (item.buffType == BuffID.WellFed) gluttonyHealing /= 1800;
                else if (item.buffType == BuffID.WellFed2) gluttonyHealing /= 1200;
                else if (item.buffType == BuffID.WellFed3) gluttonyHealing /= 900;
                else gluttonyHealing = 0;
                if (gluttonyHealing > 0)
                {
                    player.Heal(gluttonyHealing);
                    gluttonyPlayer.hunger += gluttonyHealing * 2;
                }
            }

            return base.ConsumeItem(item, player);
        }

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (item.type == ItemID.Katana)
            {
                if (item.lavaWet)
                {
                    var player = item.Center.FindClosestPlayer();
                    if (player.ZoneUnderworldHeight)
                    {
                        if (++transformTimer >= 30)
                        {
                            item.TurnToAir();
                            var shards = player.Shards();
                            shards.sacrificedKatana = true;
                            SoundEngine.PlaySound(SoundID.Item74, item.Center);
                        }
                    }
                }
            }
        }

        public override void UpdateInventory(Item item, Player player)
        {
            base.UpdateInventory(item, player);
            // Allow tools to be usable when Shield Creepers are alive
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
            if (SoA.BNEEnabled)
            {
                ChangeElements(item, player);
            }
        }

        [JITWhenModsEnabled("BattleNetworkElements")]
        private static void ChangeElements(Item item, Player player)
        {
            var shards = player.Shards();
            var elementItem = item.Elements();
            if (shards.areusProcessorPrevious)
            {
                if (item.damage > 0)
                {
                    elementItem.isFire = false;
                    elementItem.isAqua = false;
                    elementItem.isElec = false;
                    elementItem.isWood = false;
                    switch (player.Shards().processorElement)
                    {
                        case Element.Fire:
                            elementItem.isFire = true;
                            break;
                        case Element.Aqua:
                            elementItem.isAqua = true;
                            break;
                        case Element.Elec:
                            elementItem.isElec = true;
                            break;
                        case Element.Wood:
                            elementItem.isWood = true;
                            break;
                    }
                }
            }
        }

        public override void OnSpawn(Item item, IEntitySource source)
        {
            if (item.type == ItemID.Feather)
            {
                if (source is EntitySource_Loot parentSource && parentSource.Entity is NPC npc)
                {
                    if (npc.type == ModContent.NPCType<CaveHarpy>()) item.color = Color.Gray;
                    if (npc.type == ModContent.NPCType<CorruptHarpy>()) item.color = Color.Purple;
                    if (npc.type == ModContent.NPCType<CrimsonHarpy>()) item.color = Color.Red;
                    if (npc.type == ModContent.NPCType<DesertHarpy>()) item.color = Color.Yellow;
                    if (npc.type == ModContent.NPCType<ForestHarpy>()) item.color = Color.GreenYellow;
                    if (npc.type == ModContent.NPCType<HallowedHarpy>()) item.color = Color.Pink;
                    if (npc.type == NPCID.Harpy) item.color = default;
                    if (npc.type == ModContent.NPCType<SnowHarpy>()) item.color = Color.LightBlue;
                    if (npc.type == ModContent.NPCType<OceanHarpy>()) item.color = Color.Cyan;
                    if (npc.type == ModContent.NPCType<VoidHarpy>()) item.color = Color.DarkGray;
                }
            }
        }

        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (item.type == ItemID.Feather)
            {
                Asset<Texture2D> feather = AltFeatherSpriteItem(item);
                spriteBatch.Draw(feather.Value, position, frame, drawColor, 0f, frame.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                return false;
            }
            return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (item.type == ItemID.Feather)
            {
                Asset<Texture2D> feather = AltFeatherSpriteItem(item);
                item.BasicInWorldGlowmask(spriteBatch, feather.Value, lightColor, rotation, scale);
                return false;
            }
            return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }

        private static Asset<Texture2D> AltFeatherSpriteItem(Item item)
        {
            Asset<Texture2D> feather;
            string path = "ShardsOfAtheria/Items/Materials/Feathers/";
            string texture = AltFeather(item);
            if (texture == "Default") feather = TextureAssets.Item[ItemID.Feather];
            else feather = ModContent.Request<Texture2D>(path + texture);
            return feather;
        }
        private static string AltFeather(Item item)
        {
            if (item.color == Color.Gray) return "Cave";
            if (item.color == Color.Purple) return "Corrupt";
            if (item.color == Color.Red) return "Crimson";
            if (item.color == Color.Yellow) return "Dessert";
            if (item.color == Color.GreenYellow) return "Forest";
            if (item.color == Color.Pink) return "Hallowed";
            if (item.color == Color.LightBlue) return "Snow";
            if (item.color == Color.Cyan) return "Ocean";
            if (item.color == Color.DarkGray) return "Void";
            return "Default";
        }
    }
}
