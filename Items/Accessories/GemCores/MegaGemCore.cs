﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Minions;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Players;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
    [AutoloadEquip(EquipType.Wings)]
	public class MegaGemCore : ModItem
	{
        bool gravitation = true;

        public override void OnCreate(ItemCreationContext context)
        {
            gravitation = true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["gravitation"] = gravitation;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("gravitation"))
            {
                gravitation = tag.GetBool("gravitation");
            }
        }

        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Counts as wings\n" +
                "Increases max life by 100\n" +
                "Increases damage, movement speed and attack speed by 20%\n" +
                "Increases knockback and melee size\n" +
                "+8 extra minion slots\n" +
                "20% chance to dodge attacks\n" +
                "Gives a super dash to the wearer\n" +
                "Attacks inflict Daybroken and Betsy's Curse\n" +
                "Melee weapons autoswing\n" +
                "Immunity to damage dealing, damage and defense reducing, anti-healing and cold debuffs and Chaos State\n" +
                "Grants Ironskin and Endurance when dealing damage and Wrath, Rage and Inferno when taking damage\n" +
                "Effects of Ankh Shield, Bundle of Ballons, Frostspark Boots, Lava Waders, Shiny Stone and Soaring insignia\n" +
                "Permanent Thorns, Regeneration, Honey, Heart Lantern, Cozy Campfire, Heartreach and Gravitation buffs\n" +
                "Use to disable Gravitation\n" +
                "Grants infinite flight and slow fall");

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(100, 9f, 2.5f, true, 1f, 1f);

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var list = ShardsOfAtheria.EmeraldTeleportKey.GetAssignedKeys();
            string keyname = "Not bound";

            if (list.Count > 0)
            {
                keyname = list[0];
            }

            tooltips.Add(new TooltipLine(Mod, "Teleport", $"Allows teleportation on press of '[i:{keyname}]'"));
        }

        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
            Item.accessory = true;

            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 10;
            Item.useAnimation = 10;

            Item.defense = 50;

            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 3);
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
                .AddIngredient(ItemID.Amber, 5)
                .AddIngredient(ItemID.LunarBar, 5)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            gravitation = !gravitation;
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AmethystDashPlayerII mp = player.GetModPlayer<AmethystDashPlayerII>();
            mp.DashVelocity = 20f;
            AmethystDashPlayerII.MAX_DASH_DELAY = 50;
            AmethystDashPlayerII.MAX_DASH_TIMER = 35;

            //Bundle of Balloons
            player.hasJumpOption_Cloud = true;
            player.hasJumpOption_Blizzard = true;
            player.hasJumpOption_Sandstorm = true;
            player.jumpBoost = true;

            //Frostspark Boots
            player.accRunSpeed = 6.75f;
            player.rocketBoots = 3;
            player.iceSkate = true;

            //Lava Waders
            player.lavaImmune = true;
            player.waterWalk = true;
            player.fireWalk = true;

            //Fire Gauntlet
            player.autoReuseGlove = true;
            player.GetAttackSpeed(DamageClass.Generic) += .20f;
            player.GetKnockback(DamageClass.Generic) += 2;
            player.meleeScaleGlove = true;

            //Other
            player.panic = true;
            player.accFlipper = true;

            player.shinyStone = true;

            player.empressBrooch = true;

            player.AddBuff(BuffID.Thorns, 2);
            player.AddBuff(BuffID.Regeneration, 2);
            player.AddBuff(BuffID.Honey, 2);
            player.AddBuff(BuffID.Campfire, 2);
            player.AddBuff(BuffID.HeartLamp, 2);
            if (gravitation)
            {
                player.AddBuff(BuffID.Gravitation, 2);
            }

            player.GetModPlayer<SoAPlayer>().megaGemCore = true;

            player.GetDamage(DamageClass.Generic) += .2f;
            player.maxMinions += 8;
            player.statLifeMax2 += 100;
            player.GetModPlayer<SoAPlayer>().superEmeraldCore = true;
            player.GetModPlayer<SoAPlayer>().megaGemCore = true;

            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[BuffID.PotionSickness] = true;
            player.buffImmune[ModContent.BuffType<HeartBreak>()] = true;

            //Royal Gel
            player.npcTypeNoAggro[1] = true;
            player.npcTypeNoAggro[16] = true;
            player.npcTypeNoAggro[59] = true;
            player.npcTypeNoAggro[71] = true;
            player.npcTypeNoAggro[81] = true;
            player.npcTypeNoAggro[138] = true;
            player.npcTypeNoAggro[121] = true;
            player.npcTypeNoAggro[122] = true;
            player.npcTypeNoAggro[141] = true;
            player.npcTypeNoAggro[147] = true;
            player.npcTypeNoAggro[183] = true;
            player.npcTypeNoAggro[184] = true;
            player.npcTypeNoAggro[204] = true;
            player.npcTypeNoAggro[225] = true;
            player.npcTypeNoAggro[244] = true;
            player.npcTypeNoAggro[302] = true;
            player.npcTypeNoAggro[333] = true;
            player.npcTypeNoAggro[335] = true;
            player.npcTypeNoAggro[334] = true;
            player.npcTypeNoAggro[336] = true;
            player.npcTypeNoAggro[537] = true;

            //Ankh Shield
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

            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            //This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
            //Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
            //Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == AmethystDashPlayerII.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if ((mp.DashDir == AmethystDashPlayerII.DashUp && player.velocity.Y > -mp.DashVelocity) || (mp.DashDir == AmethystDashPlayerII.DashDown && player.velocity.Y < mp.DashVelocity))
                {
                    //Y-velocity is set here
                    //If the direction requested was DashUp, then we adjust the velocity to make the dash appear "faster" due to gravity being immediately in effect
                    //This adjustment is roughly 1.3x the intended dash velocity
                    float dashDirection = mp.DashDir == AmethystDashPlayerII.DashDown ? 1 : -1.3f;
                    newVelocity.Y = dashDirection * mp.DashVelocity;
                }
                else if ((mp.DashDir == AmethystDashPlayerII.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == AmethystDashPlayerII.DashRight && player.velocity.X < mp.DashVelocity))
                {
                    //X-velocity is set here
                    int dashDirection = mp.DashDir == AmethystDashPlayerII.DashRight ? 1 : -1;
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
                mp.DashDelay = AmethystDashPlayerII.MAX_DASH_DELAY;
                mp.DashTimer = AmethystDashPlayerII.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 9f;
            acceleration *= 2.5f;
        }
    }
}