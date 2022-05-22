using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    public class EntropySlicer : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Cut through the very fabric of life and escalate the heat death of the universe'\n" +
                "Dashing increases damage for a short time");
        }

        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.width = 44;
            Item.height = 54;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<EntropyBlade>();
            Item.shootSpeed = 15f;
            Item.knockBack = 4;
            Item.UseSound = SoundID.Item71;
            Item.crit = 8;
            Item.rare = ItemRarityID.Red;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<EntropyBlade>()] < 1;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (Main.hardMode)
            {
                damage += .05f;
            }
            if (NPC.downedQueenSlime)
            {
                damage += .05f;
            }
            if (NPC.downedMechBoss1)
            {
                damage += .1f;
            }
            if (NPC.downedMechBoss2)
            {
                damage += .1f;
            }
            if (NPC.downedMechBoss3)
            {
                damage += .1f;
            }
            if (NPC.downedPlantBoss)
            {
                damage += .15f;
            }
            if (NPC.downedGolemBoss)
            {
                damage += .15f;
            }
            if (NPC.downedFishron)
            {
                damage += .2f;
            }
            if (NPC.downedEmpressOfLight)
            {
                damage += .2f;
            }
            if (NPC.downedAncientCultist)
            {
                damage += .5f;
            }
            if (NPC.downedMoonlord)
            {
                damage += .5f;
            }
            if (player.timeSinceLastDashStarted < 30)
            {
                damage += .5f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentSolar, 32)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}