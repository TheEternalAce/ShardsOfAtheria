using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Pantheon : SinfulItem
    {
        public override int RequiredSin => SinfulPlayer.Greed;

        public override void SetStaticDefaults()
        {
            Item.AddRedemptionElement(5);
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 70;

            Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;
            Item.crit = 6;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemDefaults.RaritySinful;
            Item.value = Item.sellPrice(0, 5);
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override bool CanUseItem(Player player)
        {
            player.AddBuff(BuffID.Midas, 3600);
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.BuyItem(10000))
            {
                type = ModContent.ProjectileType<PantheonsGreedArrow>();
                damage = (int)(damage * 1.5f);
            }
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Main.LocalPlayer.HeldItem.type == Type)
            {
                if (Main.LocalPlayer.mouseInterface || Main.gameMenu || !Main.PlayerLoaded)
                    return;

                var center = Main.MouseScreen;
                center.X += 10;
                center.Y += 40;
                int coins = 0;
                foreach (var item in Main.LocalPlayer.inventory)
                {
                    if (item.type == ItemID.GoldCoin) coins += item.stack;
                    else if (item.type == ItemID.PlatinumCoin) coins += item.stack * 100;
                }
                foreach (var item in Main.LocalPlayer.bank.item)
                {
                    if (item.type == ItemID.GoldCoin) coins += item.stack;
                    else if (item.type == ItemID.PlatinumCoin) coins += item.stack * 100;
                }
                //coins /= 10000;

                var color = Color.Gold;
                var font = FontAssets.MouseText.Value;
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, coins + "g", center + new Vector2(8f, -24f), color, 0f, Vector2.Zero, new Vector2(1f) * 0.8f, spread: Main.inventoryScale);
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -4);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GreedSoul>()
                .AddCondition(SoAConditions.TransformArmament)
                .Register();
        }
    }
}