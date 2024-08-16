using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class NailPounder : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 26;

            Item.damage = 300;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 8f;

            Item.useTime = 90;
            Item.useAnimation = 90;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 12f;
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = Item.sellPrice(0, 2, 75);
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.NailFriendly;
        }

        public override void HoldItem(Player player)
        {
            int arm = ModContent.ProjectileType<PuncherArm>();
            if (player.ownedProjectileCounts[arm] == 0) Projectile.NewProjectile(player.GetSource_ItemUse_WithPotentialAmmo(Item, AmmoID.NailFriendly), player.Center,
                player.Center.DirectionTo(Main.MouseWorld), arm, 0, 0f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage = (int)(damage / 0.15f);
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}