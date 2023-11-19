using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Items.Accessories.GemCores.Lesser;
using ShardsOfAtheria.Items.Accessories.GemCores.Super;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
    public class MegaGemCore : ModItem
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
            Item.defense = 25;

            Item.rare = ItemDefaults.RarityLunarPillars;
            Item.value = ItemDefaults.ValueLunarPillars;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AmethystCore_Super>())
                .AddIngredient(ModContent.ItemType<DiamondCore_Super>())
                .AddIngredient(ModContent.ItemType<EmeraldCore_Super>())
                .AddIngredient(ModContent.ItemType<RubyCore_Super>())
                .AddIngredient(ModContent.ItemType<SapphireCore_Super>())
                .AddIngredient(ModContent.ItemType<TopazCore_Super>())
                .AddIngredient(ItemID.Amber, 24)
                .AddIngredient(ItemID.LunarBar, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AmethystDashPlayer mp = player.GetModPlayer<AmethystDashPlayer>();
            mp.DashVelocity = 20f;
            AmethystDashPlayer.MaxDashDelay = 50;
            AmethystDashPlayer.MaxDashTimer = 35;

            ShardsPlayer shards = player.Shards();
            if (!hideVisual)
            {
                shards.amethystMask = shards.megaGemCoreToggles[0];
                shards.diamondShield = shards.megaGemCoreToggles[1];
                shards.emeraldBoots = shards.megaGemCoreToggles[2];
                shards.rubyGauntlet = shards.megaGemCoreToggles[3];
                shards.sapphireSpirit = shards.megaGemCoreToggles[4];
                shards.topazNecklace = shards.megaGemCoreToggles[5];
            }

            //Bundle of Balloons
            player.GetJumpState(ExtraJump.CloudInABottle).Enable();
            player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
            player.GetJumpState(ExtraJump.SandstormInABottle).Enable();
            //player.hasJumpOption_Cloud = true;
            //player.hasJumpOption_Blizzard = true;
            //player.hasJumpOption_Sandstorm = true;
            player.jumpBoost = true;

            // Charm of Myths
            player.pStone = true;
            player.lifeRegen += 1;

            //Frostspark Boots
            player.accRunSpeed = 6.75f;
            player.rocketBoots = 3;
            player.iceSkate = true;

            //Lava Waders
            player.lavaImmune = true;
            player.waterWalk = true;
            player.fireWalk = true;

            //Fire Gauntlet
            player.GetAttackSpeed(DamageClass.Generic) += .20f;
            player.GetKnockback(DamageClass.Generic) += 2;
            player.meleeScaleGlove = true;

            //Other
            player.panic = true;
            player.accFlipper = true;

            player.AddBuff(BuffID.Thorns, 2);
            player.AddBuff(BuffID.Campfire, 2);
            player.AddBuff(BuffID.HeartLamp, 2);
            if (shards.megaGemCoreToggles[6])
            {
                player.AddBuff(BuffID.Gravitation, 2);
            }

            player.Shards().megaGemCore = true;
            player.Shards().superSapphireCore = true;

            player.GetDamage(DamageClass.Generic) += .2f;
            player.maxMinions += 8;
            player.statLifeMax2 += 100;
            player.Shards().superEmeraldCore = true;
            player.wingTimeMax += 25;

            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[ModContent.BuffType<HeartBreak>()] = true;

            // Ankh Shield
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.noKnockback = true;
            player.fireWalk = true;
            player.hasRaisableShield = true;

            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == AmethystDashPlayer.MaxDashTimer)
            {
                Vector2 newVelocity = player.velocity;

                if ((mp.DashDir == AmethystDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == AmethystDashPlayer.DashRight && player.velocity.X < mp.DashVelocity))
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