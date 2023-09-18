using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class Biometal : ModItem
    {
        private static SoundStyle MegamergeMale;
        private static SoundStyle MegamergeFemale;

        public override void Load()
        {
            // Since the equipment textures weren't loaded on the server, we can't have this code running server-side
            if (Main.netMode == NetmodeID.Server)
                return;
            // Add equip textures
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Head}", EquipType.Head, this);
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Body}", EquipType.Body, this);
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Legs}", EquipType.Legs, this);

            MegamergeMale = new SoundStyle("ShardsOfAtheria/Sounds/Item/MegamergeMale");
            MegamergeFemale = new SoundStyle("ShardsOfAtheria/Sounds/Item/MegamergeFemale");
        }

        // Called in SetStaticDefaults
        private void SetupDrawing()
        {
            // Since the equipment textures weren't loaded on the server, we can't have this code running server-side
            if (Main.netMode == NetmodeID.Server)
                return;
            int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);

            ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = true;
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;

            SetupDrawing();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var tooltip = string.Format(Language.GetTextValue("Mods.ShardsOfAtheria.Common.OverdriveInfo"),
                    SoA.OverdriveKey.GetAssignedKeys().Count > 0 ? SoA.OverdriveKey.GetAssignedKeys()[0] : "[Unbounded Hotkey]");
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Overdrive", tooltip));
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.defense = 20;

            Item.rare = ItemDefaults.RarityPreMechs;
            Item.value = ItemDefaults.ValueEarlyHardmode;
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
            ShardsPlayer shardsPlayer = player.Shards();
            BiometalSound(player);

            shardsPlayer.Biometal = true;
            shardsPlayer.BiometalHideVanity = hideVisual;

            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.statManaMax2 += 40;
            player.noFallDmg = true;
            player.spikedBoots = 1;

            var bar = ModContent.GetInstance<OverdriveEnergyBarSystem>();
            if (!bar.BarShowing)
            {
                bar.ShowBar();
            }

            BiometalDashPlayer mp = player.GetModPlayer<BiometalDashPlayer>();
            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            //This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
            //Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
            //Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == BiometalDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if (mp.DashDir == BiometalDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity || mp.DashDir == BiometalDashPlayer.DashRight && player.velocity.X < mp.DashVelocity)
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
            BiometalSound(player);
        }

        public void BiometalSound(Player player)
        {
            ShardsPlayer shardsPlayer = player.Shards();
            if (SoA.ClientConfig.biometalSound)
            {
                if (!shardsPlayer.BiometalSound)
                {
                    if (player.Male)
                        SoundEngine.PlaySound(MegamergeMale);
                    else SoundEngine.PlaySound(MegamergeFemale);
                    shardsPlayer.BiometalSound = true;
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            UnMegaMerge(player);
        }

        public override void HoldItem(Player player)
        {
            UnMegaMerge(player);
        }

        public static void UnMegaMerge(Player player)
        {
            ShardsPlayer shardsPlayer = player.Shards();
            if (shardsPlayer.BiometalSound)
            {
                SoundEngine.PlaySound(SoundID.Item4);
                shardsPlayer.BiometalSound = false;
            }
        }

        // TODO: Fix this once new hook prior to FrameEffects added.
        // Required so UpdateVanitySet gets called in EquipTextures
        public override bool IsVanitySet(int head, int body, int legs) => true;
    }

    public class BiometalDashPlayer : ModPlayer
    {
        //These indicate what direction is what in the timer arrays used
        public static readonly int DashRight = 2;
        public static readonly int DashLeft = 3;

        //The direction the player is currently dashing towards.  Defaults to -1 if no dash is ocurring.
        public int DashDir = -1;

        //The fields related to the dash accessory
        public bool DashActive = false;
        public int DashDelay = MAX_DASH_DELAY;
        public int DashTimer = MAX_DASH_TIMER;
        //The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
        public readonly float DashVelocity = 10f;
        //These two fields are the max values for the delay between dashes and the length of the dash in that order
        //The time is measured in frames
        public static readonly int MAX_DASH_DELAY = 50;
        public static readonly int MAX_DASH_TIMER = 35;

        public override void ResetEffects()
        {
            //ResetEffects() is called not long after player.doubleTapCardinalTimer's values have been set

            //Check if the ExampleDashAccessory is equipped and also check against this priority:
            // If the Shield of Cthulhu, Master Ninja Gear, Tabi and/or Solar Armour set is equipped, prevent this accessory from doing its dash effect
            //The priority is used to prevent undesirable effects.
            //Without it, the player is able to use the ExampleDashAccessory's dash as well as the vanilla ones
            bool dashBuffActive = false;

            //This is the loop used in vanilla to update/check the not-vanity accessories
            for (int i = 3; i < 8 + Player.extraAccessorySlots; i++)
            {
                Item item = Player.armor[i];

                //Set the flag for the ExampleDashAccessory being equipped if we have it equipped OR immediately return if any of the accessories are
                // one of the higher-priority ones
                if (item.type == ModContent.ItemType<Biometal>())
                    dashBuffActive = true;
                else if (item.type == ItemID.EoCShield || item.type == ItemID.MasterNinjaGear || item.type == ItemID.Tabi)
                    return;
            }

            //If we don't have the ExampleDashAccessory equipped or the player has the Solor armor set equipped, return immediately
            //Also return if the player is currently on a mount, since dashes on a mount look weird, or if the dash was already activated
            if (!dashBuffActive || Player.setSolar || Player.mount.Active || DashActive)
                return;

            //When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
            //If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
            if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
                DashDir = DashRight;
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
                DashDir = DashLeft;
            else
                return;  //No dash was activated, return

            DashActive = true;

            //Here you'd be able to set an effect that happens when the dash first activates
            //Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
            Dust dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Smoke, Player.velocity.X, Player.velocity.Y, 0, default, 1f);
            dust.noGravity = true;
        }
    }
}
