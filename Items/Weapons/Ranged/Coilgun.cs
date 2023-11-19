using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Coilgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 26;
            Item.master = true;

            Item.damage = 150;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7f;
            Item.crit = 5;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemRarityID.Master;
            Item.value = Item.sellPrice(0, 2, 75);
            Item.shoot = ModContent.ProjectileType<CoilgunProj>();
            Item.useAmmo = AmmoID.Dart;
            Item.ArmorPenetration = 20;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<CoilgunProj>();
        }
    }
}