using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaShotgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Annihilator");
            Tooltip.SetDefault("'Shotgun of a godly machine'\n" +
              "'Makes things go boom'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 6f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.rare = ItemRarityID.Red;

            Item.shoot = ItemID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;

            Item.noMelee = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item38;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 50;
            Item.height = 20;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-18, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 8; // The number of projectiles that this gun will shoot.

            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a Projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ProjectileID.ExplosiveBullet;
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
                .AddIngredient(ModContent.ItemType<DecaShard>(), 10)
                .Register();
        }
    }
}