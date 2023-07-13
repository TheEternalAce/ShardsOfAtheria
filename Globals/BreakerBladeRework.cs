using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Melee.BreakerBlade;
using ShardsOfAtheria.ShardsUI.BBMateria;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class BreakerBladeRework : GlobalItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.BreakerBlade] = true;
        }
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (IsBreakerBlade(item))
            {
                var breakerPlayer = player.GetModPlayer<BreakerBladePlayer>();
                var materiaSlots = breakerPlayer.materiaSlots;

                if (materiaSlots.Contains("FiragaMateria"))
                {
                    return true;
                }
                if (materiaSlots.Contains("BlizzagaMateria"))
                {
                    return true;
                }
                if (materiaSlots.Contains("ThundagaMateria"))
                {
                    return true;
                }
                if (materiaSlots.Contains("BiogaMateria"))
                {
                    return true;
                }
                return base.AltFunctionUse(item, player);
            }
            return base.AltFunctionUse(item, player);
        }
        public override bool CanRightClick(Item item)
        {
            if (IsBreakerBlade(item))
            {
                return true;
            }
            return base.CanRightClick(item);
        }
        public override bool ConsumeItem(Item item, Player player)
        {
            if (IsBreakerBlade(item))
            {
                return false;
            }
            return base.ConsumeItem(item, player);
        }

        public override void RightClick(Item item, Player player)
        {
            if (IsBreakerBlade(item))
            {
                ModContent.GetInstance<BBMateriaUISystem>().ToggleUI();
            }
            base.RightClick(item, player);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (IsBreakerBlade(item))
            {
                var breakerPlayer = player.GetModPlayer<BreakerBladePlayer>();
                var materiaSlots = breakerPlayer.materiaSlots;

                item.shoot = ProjectileID.None;
                item.shootSpeed = 2;
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
                item.damage = 70;
                item.useTime = 35;
                item.mana = 0;
                if (player.altFunctionUse == 2)
                {
                    // Magic materia casts a spell with Alt-use
                    item.useStyle = ItemUseStyleID.Shoot;
                    Item.staff[item.type] = true;
                    item.shootSpeed = 16f;
                    item.noMelee = true;
                    if (materiaSlots.Contains("FiragaMateria"))
                    {
                        item.damage = 80;
                        item.shoot = ProjectileID.Flames;
                    }
                    if (materiaSlots.Contains("BlizzagaMateria"))
                    {
                        item.damage = 40;
                        item.useTime = 5;
                        item.shoot = ModContent.ProjectileType<IceSpike>();
                    }
                    if (materiaSlots.Contains("ThundagaMateria"))
                    {
                        item.damage = 80;
                        item.shoot = ModContent.ProjectileType<LightningBoltFriendly>();
                    }
                    if (materiaSlots.Contains("BiogaMateria"))
                    {
                        item.damage = 80;
                        item.shoot = ModContent.ProjectileType<VenomGlob>();
                    }
                    if (item.shoot != ProjectileID.None)
                    {
                        item.mana = 16;
                        item.UseSound = SoundID.Item43;
                    }
                }
                else
                {
                    // Support materia converts the slash to the respective element
                    item.useStyle = ItemUseStyleID.Swing;
                    Item.staff[item.type] = false;
                    if (materiaSlots.Contains("FireMateria"))
                    {
                        item.shoot = ModContent.ProjectileType<FireSlash>();
                    }
                    if (materiaSlots.Contains("AquaMateria"))
                    {
                        item.shoot = ModContent.ProjectileType<AquaSlash>();
                    }
                    if (materiaSlots.Contains("ElecMateria"))
                    {
                        item.shoot = ModContent.ProjectileType<ElecSlash>();
                    }
                    if (materiaSlots.Contains("WoodMateria"))
                    {
                        item.shoot = ModContent.ProjectileType<WoodSlash>();
                    }
                    if (item.shoot != ProjectileID.None)
                    {
                        item.noMelee = true;
                        return player.ownedProjectileCounts[item.shoot] <= 0;
                    }
                }
            }
            return base.CanUseItem(item, player);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (IsBreakerBlade(item))
            {
                if (type == ModContent.ProjectileType<IceSpike>())
                {
                    float rotation = MathHelper.ToRadians(8);
                    velocity = velocity.RotatedByRandom(rotation);
                }
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (IsBreakerBlade(item))
            {
                if (type == ModContent.ProjectileType<LightningBoltFriendly>())
                {
                    Projectile lightning = Projectile.NewProjectileDirect(source, position, velocity,
                        type, damage, knockback, player.whoAmI, 0, 1);
                    lightning.DamageType = DamageClass.Melee;
                    return false;
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        // Used for Summon Materia
        public override void HoldItem(Item item, Player player)
        {
            if (IsBreakerBlade(item))
            {
                var breakerPlayer = player.GetModPlayer<BreakerBladePlayer>();
                var materiaSlots = breakerPlayer.materiaSlots;

                if (materiaSlots.Contains("IfritMateria"))
                {
                    SpawnMinion(item, player, ModContent.ProjectileType<SapphireSpirit>());
                }
                if (materiaSlots.Contains("ShivaMateria"))
                {
                    SpawnMinion(item, player, ModContent.ProjectileType<SapphireSpirit>());
                }
                if (materiaSlots.Contains("RamuhMateria"))
                {
                    SpawnMinion(item, player, ModContent.ProjectileType<SapphireSpirit>());
                }
                if (materiaSlots.Contains("CactuarMateria"))
                {
                    SpawnMinion(item, player, ModContent.ProjectileType<SapphireSpirit>());
                }
            }
        }
        static int SpawnMinion(Item item, Player player, int type)
        {
            int whoAmI = -1;
            if (player.ownedProjectileCounts[type] == 0)
            {
                IEntitySource source = item.GetSource_FromThis();
                var position = player.Center;
                var velocity = Vector2.Zero;
                int damage = 70;
                int knockback = 8;
                int owner = player.whoAmI;
                whoAmI = Projectile.NewProjectile(source, position, velocity, type, damage,
                    knockback, owner);
            }
            return whoAmI;
        }

        static bool IsBreakerBlade(Item item)
        {
            return item.type == ItemID.BreakerBlade;
        }
    }
}
