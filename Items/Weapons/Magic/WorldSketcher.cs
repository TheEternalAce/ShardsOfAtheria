using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class WorldSketcher : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.AddElement(3);
            Item.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 36;

            Item.damage = 14;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 0f;
            Item.mana = 12;

            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.channel = true;

            Item.rare = ItemDefaults.RarityPreBoss;
            Item.value = 50000;
            Item.shoot = ModContent.ProjectileType<Pencil>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2) type = 0;
            position = Main.MouseWorld;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) foreach (Projectile projectile in Main.projectile)
                {
                    if (projectile.active && projectile.type == Item.shoot && projectile.owner == player.whoAmI) projectile.Kill();
                }
            return base.CanUseItem(player);
        }
    }
}