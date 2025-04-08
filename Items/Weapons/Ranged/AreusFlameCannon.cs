using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Ranged.FireCannon;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusFlameCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddAreus();
            Item.AddDamageType(3, 5);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 34;

            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 5f;
            Item.crit = 4;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = Item.sellPrice(0, 2, 75);
            Item.shoot = ModContent.ProjectileType<FlameCannon>();
            Item.useAmmo = AmmoID.Gel;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                return base.CanConsumeAmmo(ammo, player);
            }
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<FlameCannon>();
        }
    }
}