﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Tiles.Crafting;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Hooks
{
    public class AreusTether : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1; // Amount of this item needed to research and become available in Journey mode's duplication menu. Amount based on vanilla hooks' amount needed
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<AreusTetherHook>();

            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 16)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ItemID.LunarBar, 8)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }

    internal class AreusTetherHook : ModProjectile
    {
        private static Asset<Texture2D> chainTexture;
        private const int hookAmount = 4;

        public override void Load()
        {
            // This is called once on mod (re)load when this piece of content is being loaded.
            // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            if (Main.netMode != NetmodeID.Server)
            {
                chainTexture = ModContent.Request<Texture2D>("ShardsOfAtheria/Items/Tools/Hooks/AreusTether_Chain");
            }
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            chainTexture = null;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst); // Copies the attributes of the Amethyst hook's projectile.
        }

        // Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook.
        public override bool? CanUseGrapple(Player player)
        {
            int hooksOut = 0;
            for (int l = 0; l < 1000; l++)
            {
                if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type)
                {
                    hooksOut++;
                }
            }

            return hooksOut <= hookAmount;
        }

        // Return true if it is like: Hook, CandyCaneHook, BatHook, GemHooks
        // public override bool? SingleGrappleHook(Player player)
        // {
        //	return true;
        // }

        // Use this to kill oldest hook. For hooks that kill the oldest when shot, not when the newest latches on: Like SkeletronHand
        // You can also change the projectile like: Dual Hook, Lunar Hook
        public override void UseGrapple(Player player, ref int type)
        {
            int hooksOut = 0;
            int oldestHookIndex = -1;
            int oldestHookTimeLeft = 100000;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.whoAmI && Main.projectile[i].type == Projectile.type)
                {
                    hooksOut++;
                    if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
                    {
                        oldestHookIndex = i;
                        oldestHookTimeLeft = Main.projectile[i].timeLeft;
                    }
                }
            }
            if (hooksOut > 3)
            {
                Main.projectile[oldestHookIndex].Kill();
            }
        }

        // Amethyst Hook is 300, Static Hook is 600.
        public override float GrappleRange()
        {
            return 500f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = hookAmount; // The amount of hooks that can be shot out
        }

        // default is 11, Lunar is 24
        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            speed = 20f; // How fast the grapple returns to you after meeting its max shoot distance
        }

        public override void GrapplePullSpeed(Player player, ref float speed)
        {
            speed = 30; // How fast you get pulled to the grappling hook projectile's landing position
        }

        // Draws the grappling hook's chain.
        public override bool PreDrawExtras()
        {
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - Projectile.Center;
            float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
            float distanceToPlayer = directionToPlayer.Length();

            while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
            {
                directionToPlayer /= distanceToPlayer; // get unit vector
                directionToPlayer *= chainTexture.Height(); // multiply by chain link length

                center += directionToPlayer; // update draw position
                directionToPlayer = playerCenter - center; // update distance
                distanceToPlayer = directionToPlayer.Length();

                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

                // Draw chain
                Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
                    chainTexture.Value.Bounds, drawColor, chainRotation,
                    chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }
            // Stop vanilla from drawing the default chain.
            return false;
        }
    }
}