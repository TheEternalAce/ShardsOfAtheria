using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaSwarmer : ModItem
    {
        public override string Texture => "ShardsOfAtheria/Items/DecaEquipment/DecaShard";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Swarmer");
            Tooltip.SetDefault("'Drone controller of a godly machine'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 1f;
            Item.mana = 12;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Red;

            Item.shoot = ModContent.ProjectileType<DecaSwarmerProj>();
            Item.shootSpeed = 16f;

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.reuseDelay = 15;
            Item.UseSound = SoundID.Item75;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 32;
            Item.height = 32;
        }

        public override bool AltFunctionUse(Player player)
        {
            return false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(150, 0), new Vector2(-4, 0), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(-150, 0), new Vector2(4, 0), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(0, 150), new Vector2(0, -4), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(0, -150), new Vector2(0, 4), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(110, 110), new Vector2(-3, -3), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(110, -110), new Vector2(-3, 3), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(-110, 110), new Vector2(3, -3), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld + new Vector2(-110, -110), new Vector2(3, 3), ModContent.ProjectileType<DecaSwarmerProj>(), Item.damage, Item.knockBack, Main.myPlayer);
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Deca Gear", "[c/FF4100:Deca Equipment]"));
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<DecaPlayer>().modelDeca;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 10)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddIngredient(ModContent.ItemType<SoulOfStarlight>(), 10)
                .AddIngredient(ModContent.ItemType<DecaShard>())
                .Register();
        }
    }
}