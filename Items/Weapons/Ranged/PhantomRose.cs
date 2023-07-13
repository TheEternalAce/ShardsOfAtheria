using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.GunRose;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class PhantomRose : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAqua();
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.scale = .85f;

            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3.75f;
            Item.crit = 8;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item41;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shootSpeed = 13f;
            Item.rare = ItemRarityID.Green;
            Item.value = 42500;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6, -2);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HeroGun>())
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            return Main.rand.NextFloat() >= .48f;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ModContent.ProjectileType<WitheringSeed>();
            }
        }
    }
}