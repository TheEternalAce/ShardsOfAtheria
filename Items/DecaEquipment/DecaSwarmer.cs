using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaSwarmer : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Swarmer");
            Tooltip.SetDefault("'Drone controller of a godly machine'\n" +
                "Controls the Deca Shards summoned from equipping Model Deca with left click\n" +
                "Right click turns one into item form, allowing you to make one of the other Deca Weapons");
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Red;
            Item.width = 32;
            Item.height = 32;

            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
        }

        public override bool AltFunctionUse(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<DecaShardProj>()] > 0;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.GetModPlayer<DecaPlayer>().decaShards -=1;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DecaShardProj>()] > 0)
                    Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<DecaShard>());
            }
            return player.GetModPlayer<DecaPlayer>().modelDeca;
        }

        public override bool? UseItem(Player player)
        {
            int newDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);

            Vector2 mountedCenter = player.MountedCenter;

            var drawPosition = Main.MouseWorld;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            int direction = -1;

            if (Main.MouseWorld.X < mountedCenter.X)
                direction = 1;

            player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);

            player.GetModPlayer<DecaPlayer>().swarming = true;
            return true;
        }

        public override void AddRecipes()
        {
            //return;
        }
    }
}