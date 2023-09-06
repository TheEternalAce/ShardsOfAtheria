using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusPistol : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 28;
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 24, 16f);

            Item.damage = 62;
            Item.knockBack = 2f;
            Item.crit = 0;

            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;

            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<AreusBounceShot>();
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}
