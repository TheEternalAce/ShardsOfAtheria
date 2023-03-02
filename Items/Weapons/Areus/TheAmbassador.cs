using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Bases;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class TheAmbassador : SinfulItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
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
            if (player.whoAmI == Main.myPlayer)
            {
                type = Item.shoot;
                velocity = Vector2.Zero;
                position = Main.MouseWorld + new Vector2(-6, -6);
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}