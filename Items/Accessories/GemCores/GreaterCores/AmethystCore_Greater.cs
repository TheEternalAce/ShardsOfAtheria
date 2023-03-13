using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories.GemCores.Cores;
using ShardsOfAtheria.Items.Accessories.GemCores.LesserCores;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores.GreaterCores
{
    public class AmethystCore_Greater : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
               .AddIngredient(ModContent.ItemType<AmethystCore>())
                .AddIngredient(ItemID.HallowedBar, 5)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().amethystMask = !hideVisual;

            AmethystDashPlayer mp = player.GetModPlayer<AmethystDashPlayer>();
            mp.DashVelocity = 13f;
            AmethystDashPlayer.MAX_DASH_DELAY = 50;
            AmethystDashPlayer.MAX_DASH_TIMER = 35;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.CursedInferno] = true;

            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            //This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
            //Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
            //Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == AmethystDashPlayer.MAX_DASH_TIMER)
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
                mp.DashDelay = AmethystDashPlayer.MAX_DASH_DELAY;
                mp.DashTimer = AmethystDashPlayer.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }
    }
}