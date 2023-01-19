using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
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
            SacrificeTotal = 1;
            WeaponElements.Metal.Add(Type);
            WeaponElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 36;
            Item.scale = .75f;

            Item.damage = 40;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.crit = 6;

            Item.useTime = 12;
            Item.useAnimation = 24;
            Item.reuseDelay = 36;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.holdStyle = ItemHoldStyleID.HoldLamp;

            Item.shootSpeed = 20f;
            Item.rare = ItemRarityID.Yellow;
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