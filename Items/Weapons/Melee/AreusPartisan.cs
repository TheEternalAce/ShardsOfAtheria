using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee.AreusSpears;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusPartisan : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Spears[Type] = true;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.AddAreus(true);
            Item.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Item.width = 84;
            Item.height = 84;

            Item.damage = 120;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
            Item.shoot = ModContent.ProjectileType<AreusPartizan>();
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                type++;
                velocity *= 16;
            }
        }
    }
}