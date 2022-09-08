using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class DoubleBow : SlayerItem
    {
        private int cycleShot;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Cursed Arrows, Ichor arrows and lasers\n" +
                "'It's called 'Double Bow' and shoots 3 projectiles???'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 80;

            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;
            Item.crit = 5;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;

            Item.shootSpeed = 16;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = Item.sellPrice(0, 2, 75);
            Item.shoot = ItemID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(velocity) * 10f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (i)
                {
                    case 0:
                        type = ProjectileID.CursedArrow;
                        break;
                    case 1:
                        type = ProjectileID.MiniRetinaLaser;
                        break;
                    case 2:
                        type = ProjectileID.IchorArrow;
                        break;
                }

                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 2);
        }
    }
}