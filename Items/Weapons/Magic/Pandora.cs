using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class Pandora : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Ice.Add(Type);
            WeaponElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.damage = 107;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.mana = 6;

            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.staff[Item.type] = true;

            Item.shootSpeed = 15f;
            Item.value = Item.sellPrice(0, 3, 25);
            Item.rare = ItemRarityID.Red;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 35;
                Item.useAnimation = 35;
                Item.autoReuse = false;
                Item.UseSound = SoundID.Item43;
                Item.damage = 107;
                Item.mana = 20;
                Item.knockBack = 0;
                Item.shoot = ModContent.ProjectileType<LightningBoltFriendly>();
                Item.shootSpeed = 2f;
            }
            else
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.autoReuse = true;
                Item.UseSound = SoundID.Item28;
                Item.damage = 87;
                Item.mana = 6;
                Item.knockBack = 3;
                Item.shoot = ModContent.ProjectileType<IceBolt>();
                Item.shootSpeed = 16f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 pos = Main.MouseWorld - new Vector2(0, 200);
                Projectile.NewProjectile(source, pos, Vector2.Normalize(Main.MouseWorld - pos) * 2f, type, damage, knockback, player.whoAmI, 0, 1);
                return false;
            }
            return true;
        }
    }
}