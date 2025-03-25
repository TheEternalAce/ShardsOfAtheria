using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class ScreamLantern : ModItem
    {
        int shockwave = 0;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(12);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 36;
            Item.master = true;

            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.crit = 6;
            Item.mana = 8;

            Item.useTime = 12;
            Item.useAnimation = 24;
            Item.reuseDelay = 36;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.holdStyle = ItemHoldStyleID.HoldLamp;

            Item.shootSpeed = 20f;
            Item.rare = ItemRarityID.Master;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<ScreamShockwave>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (shockwave >= 10)
            {
                shockwave = 0;
            }
            shockwave++;
            if (shockwave == 9 || shockwave == 10)
            {
                for (int i = 0; i < 5; i++)
                {
                    Projectile.NewProjectile(source, player.Center, new Vector2(0, -1).RotatedBy(MathHelper.ToRadians(72 * i)) * 16, type, damage, knockback, player.whoAmI);
                }
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}