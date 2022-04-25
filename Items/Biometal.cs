using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items
{
    public class Biometal : ModItem
    {
        public override void Load()
        {
            // The code below runs only if we're not loading on a server
            if (Main.netMode != NetmodeID.Server)
            {
                // Add equip textures
                Mod.AddEquipTexture(this, EquipType.Head, $"{Texture}_{EquipType.Head}");
                Mod.AddEquipTexture(this, EquipType.Body, $"{Texture}_{EquipType.Body}");
                Mod.AddEquipTexture(this, EquipType.Legs, $"{Texture}_{EquipType.Legs}");
            }
        }

        // Called in SetStaticDefaults
        private void SetupDrawing()
        {
            int equipSlotHead = Mod.GetEquipSlot(Name, EquipType.Head);
            int equipSlotBody = Mod.GetEquipSlot(Name, EquipType.Body);
            int equipSlotLegs = Mod.GetEquipSlot(Name, EquipType.Legs);

            ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
            ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Equipping transforms the user into a Mega Man\n" +
                "25% Increased damage\n" +
                "Increases movement speed by 10%\n" +
                "Increased life regen\n" +
                "Increased life by 100 and mana by 40\n" +
                "Grants dash, wall sliding and immunity to fall damage and certain debuffs");

            SetupDrawing();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var list = ShardsOfAtheria.OverdriveKey.GetAssignedKeys();
            string keyname = "Not bound";

            if (list.Count > 0)
            {
                keyname = list[0];
            }

            tooltips.Add(new TooltipLine(Mod, "Damage", $"Press '[i:{keyname}]' to activate or deactivate Overdrive\n" +
                "Overdrive doubles all damage\n" +
                "Overdrive lasts until you get hit or run out of Overdrive time"));
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = .7f;
            Item.accessory = true;
            Item.value = Item.sellPrice(0,  5);
            Item.rare = ItemRarityID.Blue;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
            Item.defense = 20;
        }

        public override void AddRecipes()
        {  
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 15)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                player.AddBuff(ModContent.BuffType<Megamerged>(), 60);
                if (player.Male)
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/MegamergeMale"));
                else SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/MegamergeMale"));
            }

            player.GetModPlayer<SoAPlayer>().Biometal = true;
            player.GetModPlayer<SoAPlayer>().BiometalHideVanity = hideVisual;

            player.extraFall += 45;
            player.GetDamage(DamageClass.Generic) += 0.25f;
            player.statLifeMax2 += 100;
            player.statManaMax2 += 40;
            player.noFallDmg = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.spikedBoots++;

            BiometalDashPlayer mp = player.GetModPlayer<BiometalDashPlayer>();
            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            //This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
            //Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
            //Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == BiometalDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if ((mp.DashDir == BiometalDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == BiometalDashPlayer.DashRight && player.velocity.X < mp.DashVelocity))
                {
                    //X-velocity is set here
                    int dashDirection = mp.DashDir == BiometalDashPlayer.DashRight ? 1 : -1;
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
                mp.DashDelay = BiometalDashPlayer.MAX_DASH_DELAY;
                mp.DashTimer = BiometalDashPlayer.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }

        public override void UpdateVanity(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                player.AddBuff(ModContent.BuffType<Megamerged>(), 60);
                if (player.Male)
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/MegamergeMale"));
                else SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/MegamergeMale"));
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                player.ClearBuff(ModContent.BuffType<Megamerged>());
                SoundEngine.PlaySound(SoundID.Item4);
            }
        }

        public override void HoldItem(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                player.ClearBuff(ModContent.BuffType<Megamerged>());
                SoundEngine.PlaySound(SoundID.Item4);
            }
        }

        // TODO: Fix this once new hook prior to FrameEffects added.
        // Required so UpdateVanitySet gets called in EquipTextures
        public override bool IsVanitySet(int head, int body, int legs) => true;
    }
}
