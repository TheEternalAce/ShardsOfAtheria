using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Projectiles.Ranged.PlagueRail;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class PlagueHandgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 28;
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 24, 16f);

            Item.damage = 15;
            Item.knockBack = 2f;

            Item.UseSound = SoundID.Item41;
            Item.autoReuse = true;

            Item.rare = ItemDefaults.RarityMechs;
            Item.value = Item.sellPrice(0, 4, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BionicBarItem>(15)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient<PlagueCell>(20)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            Item plagueRail = ModContent.GetInstance<PlagueRailgun>().Item;
            return !player.HasAmmo(plagueRail) && !player.Overdrive();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                Item plagueRail = ModContent.GetInstance<PlagueRailgun>().Item;
                if (player.HasAmmo(plagueRail))
                {
                    player.PickAmmo(plagueRail, out int _, out float _, out int _, out float _, out int _);
                    type = ModContent.ProjectileType<PlagueBullet>();
                    velocity.Normalize();
                    velocity *= 16;
                }
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(damage);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0);
        }
    }
}
