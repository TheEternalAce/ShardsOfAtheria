using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic.SpectralGleam;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class SpellTag_Gleam : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(9);
            Item.AddRedemptionElement(5);
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 36;
            Item.maxStack = 9999;

            Item.damage = 12;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.ArmorPenetration = 15;

            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            //Item.UseSound = SoundID.NPCDeath7;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.consumable = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<GleamShot>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(20)
                .AddIngredient(ItemID.Book)
                .AddIngredient(ItemID.Diamond)
                .AddIngredient(ItemID.ManaCrystal)
                .AddTile(TileID.Bookcases)
                .Register();
        }

        public override bool ConsumeItem(Player player)
        {
            Item.stack--;
            if (Item.stack <= 0) Item.TurnToAir();
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 5; i++)
            {
                var pageOffset = Vector2.Normalize(velocity) * 20f;
                var pageVelocity = Main.rand.NextVector2Circular(10f, 10f);
                //if (pageVelocity.Y > 0) pageVelocity.Y *= -1;
                Gore.NewGorePerfect(source, player.Center + pageOffset, pageVelocity, GoreID.PageScrap);

                var adjustedPosition = position - Vector2.UnitX.RotatedBy(MathHelper.Lerp(0, MathHelper.Pi, i / 4f)) * 150f;
                var projectileVelocity = adjustedPosition.DirectionTo(position) * Item.shootSpeed;
                Projectile.NewProjectile(source, adjustedPosition, projectileVelocity, type, damage, knockback);
            }
            return false;
        }
    }
}