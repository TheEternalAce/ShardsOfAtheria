using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.Super
{
    public class AmethystCore_Super : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityLunaticCultist;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AmethystCore_Greater>())
                .AddIngredient(ItemID.FragmentStardust, 5)
                .AddIngredient(ItemID.FragmentVortex, 5)
                .AddIngredient(ItemID.MasterNinjaGear)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateVanity(Player player)
        {
            player.Shards().amethystMask = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().amethystMask = !hideVisual;

            // Lava Waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 420;

            player.noKnockback = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.lavaImmune = true;
            player.fireWalk = true;

            AmethystDashPlayer mp = player.GetModPlayer<AmethystDashPlayer>();
            mp.DashVelocity = 16f;
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