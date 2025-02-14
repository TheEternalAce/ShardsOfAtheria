using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic.Penmaster;
using ShardsOfAtheria.Projectiles.Melee.AreusSwordProjs;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class PenmastersInstrument : ModItem
    {
        public override string Texture => ModContent.GetInstance<WorldSketcher>().Texture;

        public override void SetStaticDefaults()
        {
            Item.AddElement(1);
            Item.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.damage = 90;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 0f;
            Item.mana = 12;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemDefaults.RarityPlantera;
            Item.value = 50000;
            Item.shoot = ModContent.ProjectileType<Pen>();
            Item.shootSpeed = 4f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<Pen>();
                Item.channel = true;
                Item.noUseGraphic = false;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<AreusSwordProj>();
                Item.channel = false;
                Item.noUseGraphic = true;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ModContent.ProjectileType<Pen>())
            {
                position = Main.MouseWorld;
                velocity *= 0;
            }
        }
    }
}