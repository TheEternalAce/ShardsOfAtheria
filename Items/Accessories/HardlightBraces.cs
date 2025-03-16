using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class HardlightBraces : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(7);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 22;
            Item.accessory = true;
            Item.defense = 5;

            Item.damage = 13;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4;
            Item.crit = 2;

            Item.shoot = ModContent.ProjectileType<HardlightFeatherMagic>();
            Item.shootSpeed = 16;

            Item.rare = ItemDefaults.RarityNova;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().hardlightBraces = true;
            player.wingTimeMax += 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HardlightPrism>(), 10)
                .AddTile(TileID.SkyMill)
                .Register();
        }

        public static void OnHitEffect(Player player, NPC target)
        {
            var shards = player.Shards();
            if (shards.hardlightBraces && shards.hardlightBracesCooldown == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Item braces = ModContent.GetInstance<HardlightBraces>().Item;
                    Vector2 point = target.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(90 * i)) * 120f;
                    Vector2 velocity = Vector2.Normalize(target.Center - point) * braces.shootSpeed;
                    Projectile.NewProjectileDirect(player.GetSource_Accessory(braces), point, velocity, braces.shoot,
                        player.GetWeaponDamage(braces), player.GetWeaponKnockback(braces), player.whoAmI);
                }
                shards.hardlightBracesCooldown = shards.hardlightBracesCooldownMax;
            }
        }
    }
}