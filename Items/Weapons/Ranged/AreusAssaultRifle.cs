using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusAssaultRifle : ModItem
    {
        private int fireMode;

        public override void SetStaticDefaults()
        {
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 28;
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 6, 16f);

            Item.damage = 96;
            Item.knockBack = 4f;
            Item.crit = 5;

            Item.UseSound = SoundID.Item11;

            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = Item.sellPrice(0, 4, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 15)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ItemID.FragmentVortex, 10)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (fireMode == 1)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
            }
            if (fireMode == 2)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            }
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            if (fireMode == 1)
                return !(player.itemAnimation < Item.useAnimation - 2) || Main.rand.NextFloat() >= .66f;
            return Main.rand.NextFloat() >= .66f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 6;
                Item.useAnimation = 6;
                Item.reuseDelay = 20;
                Item.autoReuse = false;
                Item.shoot = ItemID.None;
                Item.UseSound = SoundID.Unlock;

                fireMode += 1;
                if (fireMode == 3)
                    fireMode = 0;

                if (fireMode == 0)
                    CombatText.NewText(player.Hitbox, Color.White, ShardsHelpers.LocalizeCommon("FiringMode1"));
                if (fireMode == 1)
                    CombatText.NewText(player.Hitbox, Color.White, ShardsHelpers.LocalizeCommon("FiringMode2"));
                if (fireMode == 2)
                    CombatText.NewText(player.Hitbox, Color.White, ShardsHelpers.LocalizeCommon("FiringMode3"));
            }
            else
            {
                Item.shoot = ProjectileID.PurificationPowder;
                if (fireMode == 0)
                {
                    Item.UseSound = SoundID.Item11;
                    Item.useTime = 6;
                    Item.useAnimation = 6;
                    Item.reuseDelay = 0;
                    Item.autoReuse = false;
                }
                else if (fireMode == 1)
                {
                    Item.UseSound = SoundID.Item31;
                    Item.useTime = 4;
                    Item.useAnimation = 12;
                    Item.reuseDelay = 18;
                    Item.autoReuse = true;
                }
                else if (fireMode == 2)
                {
                    Item.UseSound = SoundID.Item11;
                    Item.useTime = 8;
                    Item.useAnimation = 8;
                    Item.reuseDelay = 0;
                    Item.autoReuse = true;
                }
            }
            return base.CanUseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            switch (fireMode)
            {
                case 0:
                    tooltips.Add(new TooltipLine(Mod, "Fire mode", ShardsHelpers.LocalizeCommon("FiringMode1")));
                    break;
                case 1:
                    tooltips.Add(new TooltipLine(Mod, "Fire mode", ShardsHelpers.LocalizeCommon("FiringMode2")));
                    break;
                case 2:
                    tooltips.Add(new TooltipLine(Mod, "Fire mode", ShardsHelpers.LocalizeCommon("FiringMode3")));
                    break;
            }
            base.ModifyTooltips(tooltips);
        }
    }
}