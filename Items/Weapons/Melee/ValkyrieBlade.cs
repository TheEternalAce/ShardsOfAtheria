using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ValkyrieBlade : ModItem
    {
        int shoot = 0;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElec();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 2f;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 75);

            Item.shoot = ModContent.ProjectileType<HardlightSlash>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int type2 = ModContent.ProjectileType<HardlightBlade>();
            if (++shoot == 3)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    ShardsHelpers.ProjectileRing(source, Main.MouseWorld, 6, 120f, 16f,
                        type2, damage, knockback, player.whoAmI);
                }
                shoot = 0;
            }
            else
            {
                var velocity2 = Vector2.Normalize(velocity) * 16f;
                Projectile.NewProjectile(source, position, velocity2, type2, damage,
                    knockback, player.whoAmI);
            }
            Projectile.NewProjectile(source, position, velocity * 0, type, damage, knockback,
                player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
            return false;
        }
    }
}