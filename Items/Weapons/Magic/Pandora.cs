using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class Pandora : SpecialItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left Click to summon lightning, <right> to fire an ice bolt\n" +
                "'Destiny of destruction awaits'");
        }

        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.damage = 107;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.knockBack = 6;
            Item.shootSpeed = 15f;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0,  10);
            Item.rare = ItemRarityID.Red;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 7)
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
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.staff[Item.type] = true;
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.autoReuse = true;
                Item.UseSound = SoundID.Item28;
                Item.damage = 87;
                Item.mana = 6;
                Item.knockBack = 3;
                Item.shoot = ModContent.ProjectileType<IceBolt>();
                Item.shootSpeed = 15;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.useTime = 35;
                Item.useAnimation = 35;
                Item.autoReuse = false;
                Item.UseSound = SoundID.Item43;
                Item.damage = 107;
                Item.mana = 10;
                Item.knockBack = 0;
                Item.shoot = ModContent.ProjectileType<LightningBoltFriendly>();
                Item.shootSpeed = 100;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
                return true;
            else
            {
                Projectile.NewProjectile(source, Main.MouseWorld - new Vector2(0, 200), new Vector2(0, 10), type, damage, knockback, player.whoAmI);
                return false;
            }
        }
    }
}