using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Magnus : SinfulItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 28;
            Item.scale = .85f;

            Item.damage = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 5;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item41;
            Item.noMelee = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 10, 25);
            Item.shoot = ModContent.ProjectileType<AmbassadorShot>();
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = Item.shoot;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.IsLocal())
            {
                var position1 = Main.MouseWorld + new Vector2(-6, -6);
                Projectile.NewProjectile(source, position1, Vector2.Zero, type, damage, knockback);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}