using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Regular
{
    public class AmethystCore : ModItem
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "ShardsOfAtheria/Items/Accessories/GemCores/AmethystMask", EquipType.Head, this, "AmethystMask");
            }
        }

        public void SetupDrawing()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                int equipSlotHead = EquipLoader.GetEquipSlot(Mod, "AmethystMask", EquipType.Head);
                ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
                ArmorIDs.Head.Sets.DrawFullHair[equipSlotHead] = true;
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;

            SetupDrawing();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AmethystCore_Lesser>())
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Shards().amethystMask = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().amethystMask = !hideVisual;

            AmethystDashPlayer mp = player.GetModPlayer<AmethystDashPlayer>();
            mp.DashVelocity = 13f;
            AmethystDashPlayer.MaxDashDelay = 50;
            AmethystDashPlayer.MaxDashTimer = 20;
            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == AmethystDashPlayer.MaxDashTimer)
            {
                Vector2 newVelocity = player.velocity;

                if (mp.DashDir == AmethystDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity || mp.DashDir == AmethystDashPlayer.DashRight && player.velocity.X < mp.DashVelocity)
                {
                    //X-velocity is set here
                    int dashDirection = mp.DashDir == AmethystDashPlayer.DashRight ? 1 : -1;
                    newVelocity.X = dashDirection * mp.DashVelocity;
                }

                player.velocity = newVelocity;
            }

            //Decrement the timers
            mp.DashTimer--;
            mp.DashDelay--;

            if (mp.DashDelay == 0)
            {
                //The dash has ended.  Reset the fields
                mp.DashDelay = AmethystDashPlayer.MaxDashDelay;
                mp.DashTimer = AmethystDashPlayer.MaxDashTimer;
                mp.DashActive = false;
            }
        }
    }
}