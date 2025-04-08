using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using WebCom.Extensions;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusGhostLantern : ModItem
    {
        public int poes = 0;
        int poeSpawnTimer = 0;

        public override void SetStaticDefaults()
        {
            Item.AddAreus(true, true);
            Item.AddDamageType(3, 5, 6);
            Item.AddElement(0);
            Item.AddRedemptionElement(1);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            poes = 20;
            Item.width = 26;
            Item.height = 40;
            Item.scale = 0.6f;

            Item.damage = 42;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3f;
            Item.mana = 12;

            Item.useTime = 5;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.holdStyle = ItemHoldStyleID.HoldLamp;

            Item.shootSpeed = 7f;
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<ElectricFlame>();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Main.playerInventory || Main.gameMenu || !Main.PlayerLoaded)
                return;

            var center = position;
            center.X -= TextureAssets.InventoryBack.Value.Width / 2f * Main.inventoryScale;
            center.Y += TextureAssets.InventoryBack.Value.Height / 2f * Main.inventoryScale;
            string ghosts = poes.ToString();

            var color = SoA.ElectricColor;
            var font = FontAssets.MouseText.Value;
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, ghosts, center + new Vector2(8f, -24f) * Main.inventoryScale, color, 0f, Vector2.Zero, new Vector2(1f) * Main.inventoryScale * 0.8f, spread: Main.inventoryScale);
        }

        public override void UpdateInventory(Player player)
        {
            string key = this.GetLocalizationKey("DisplayName");
            string name = Language.GetTextValue(key) + " (" + poes + ")";
            Item.SetNameOverride(name);

            if (player.IsLocal())
            {
                int activePoes = player.ownedProjectileCounts[ModContent.ProjectileType<ElectricPoe>()];
                if (poes > 100) poes = 100;
                if (player.shimmering || player.CCed || player.sleeping.isSleeping || poes + activePoes >= 100) return;
                if (++poeSpawnTimer >= 240 + Main.rand.Next(240) - player.Shards().combatTimer / 3)
                {
                    bool validPosition = false;
                    var position = player.Center + Main.rand.NextVector2Circular(10, 10) * 50;
                    while (!validPosition)
                    {
                        position = player.Center + Main.rand.NextVector2Circular(10, 10) * 50;
                        validPosition = Collision.CanHit(player.position, player.width, player.height, position, 20, 20);
                    }
                    Projectile.NewProjectile(Item.GetSource_FromThis(), position, Vector2.Zero, ModContent.ProjectileType<ElectricPoe>(), player.GetWeaponDamage(Item), 0);
                    poeSpawnTimer = 0;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            return poes > 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(20)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ItemID.SoulofNight, 15)
                .AddRecipeGroup(ShardsRecipes.Tombstone, 7)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.itemAnimation == player.itemAnimationMax)
            {
                if (!player.Overdrive() || !Main.rand.NextBool(5)) poes--;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}