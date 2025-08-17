using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Magic.MagicalGems;
using ShardsOfAtheria.ShardsUI.MagicStonesSelection;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class MagicStones : ModItem
    {
        public int[] selectedGems = [0, 0, 0];
        public int gemIndex = 0;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(1);
            Item.AddRedemptionElement(5);
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;

            Item.damage = 24;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 8f;
            Item.mana = 8;

            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.reuseDelay = 14;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemDefaults.RarityPreBoss;
            Item.value = 50000;
            Item.shoot = ModContent.ProjectileType<MagicStone>();
            Item.shootSpeed = 6;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                ModContent.GetInstance<StonesSelectionUI>().ToggleVisualSettings(Item);
                return false;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int level = 1;
            var gem = player.Gem();
            if (gem.gemCore) level++;
            if (gem.greaterGemCore) level++;
            if (gem.superGemCore) level++;
            if (gem.megaGemCore) level++;
            var tierLock = ToggleableTool.GetInstance<TierLock>(player);
            if (tierLock != null && tierLock.Active) level = tierLock.mode;
            damage *= level;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (selectedGems[gemIndex] != 0) type = selectedGems[gemIndex];
            gemIndex++;
            if (gemIndex > 2) gemIndex = 0;
            if (type == ModContent.ProjectileType<RubyStone>()) damage = (int)(damage * 1.5f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item1, position);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 20)
                .AddIngredient(ItemID.Amber, 2)
                .AddIngredient(ItemID.Amethyst, 2)
                .AddIngredient(ItemID.Diamond, 2)
                .AddIngredient(ItemID.Emerald, 2)
                .AddIngredient(ItemID.Ruby, 2)
                .AddIngredient(ItemID.Sapphire, 2)
                .AddIngredient(ItemID.Topaz, 2)
                .AddIngredient<Jade>(2)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}