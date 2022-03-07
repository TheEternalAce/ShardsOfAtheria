using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DecaEquipment
{
    public class DecaRifle : ModItem
    {
        private int shootingSoundsTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Stormer");
            Tooltip.SetDefault("Fires a storm of powerful luminite bullets\n" +
              "66% chance to not consume ammo\n" +
              "'Rifle of a godly machine'");
        }

        public override void SetDefaults()
        {
            Item.damage = 200000;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 6f;
            Item.useTime = 1;
            Item.useAnimation = 50;
            Item.rare = ItemRarityID.Red;

            Item.shoot = ItemID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;

            Item.noMelee = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.reuseDelay = 25;
            Item.width = 50;
            Item.height = 20;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        
		public override bool CanConsumeAmmo(Player player)
		{
			return !(player.itemAnimation < Item.useAnimation - 2) || Main.rand.NextFloat() >= .66f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
            shootingSoundsTimer++;
            if (shootingSoundsTimer == 1)
            {
                SoundEngine.PlaySound(Item.UseSound);
                shootingSoundsTimer = 0;
            }
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
            if (type == ProjectileID.Bullet)
                type = ProjectileID.MoonlordBullet;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Deca Gear", "[c/FF4100:Deca Equipment]"));
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<DecaPlayer>().modelDeca;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ModContent.ItemType<DecaShard>(), 10)
                .Register();
        }
    }
}