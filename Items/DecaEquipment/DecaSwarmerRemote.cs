using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaSwarmerRemote : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Swarmer Remote");
            Tooltip.SetDefault("'Drone controller of a godly machine\n'" +
              "'Death by triangle'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.summon = true;
            item.knockBack = 1f;
            item.mana = 12;
            item.useTime = 15;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Red;

            item.shoot = ModContent.ProjectileType<DecaSwarmer>();
            item.shootSpeed = 16f;

            item.noMelee = true;
            item.autoReuse = true;
            item.reuseDelay = 15;
            item.UseSound = SoundID.Item75;
            item.useStyle = ItemUseStyleID.HoldingOut;
            Item.staff[item.type] = true;
            item.width = 32;
            item.height = 32;
        }

        public override bool AltFunctionUse(Player player)
        {
            return false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(150, 0), new Vector2(-4, 0), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(-10, 0).ToRotation(), Main.rand.Next(100));
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(-150, 0), new Vector2(4, 0), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(10, 0).ToRotation(), Main.rand.Next(100));
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(0, 150), new Vector2(0, -4), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(0, -10).ToRotation(), Main.rand.Next(100));
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(0, -150), new Vector2(0, 4), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(0, 10).ToRotation(), Main.rand.Next(100));
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(110, 110), new Vector2(-3, -3), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(-10, -10).ToRotation());
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(110, -110), new Vector2(-3, 3), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(-10, 10).ToRotation());
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(-110, 110), new Vector2(3, -3), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(10, -10).ToRotation());
            Projectile.NewProjectile(Main.MouseWorld + new Vector2(-110, -110), new Vector2(3, 3), ModContent.ProjectileType<DecaSwarmer>(), item.damage, item.knockBack, Main.myPlayer, new Vector2(10, 10).ToRotation());
            return false;
        }
    }
}