using Microsoft.Xna.Framework;
using SagesMania.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
    public class OmegaDrive : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants effects of Living Metal and Rush Drive, always\n" +
                "'The sheer power with no drawbacks pulls you to insanity'");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<LivingMetal>());
            recipe.AddIngredient(ModContent.ItemType<RushDrive>());
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(ModContent.TileType<CobaltWorkbench>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SMPlayer>().omegaDrive = true;
            player.jumpSpeedBoost += 4.8f;
            player.extraFall += 45;
            player.allDamage += 0.25f;
            player.statDefense += 20;
            player.moveSpeed *= 2;
            player.statLifeMax2 += 100;
            player.statManaMax2 += 40;
            player.noKnockback = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.spikedBoots++;

            OmegaMetalDashPlayer mp = player.GetModPlayer<OmegaMetalDashPlayer>();
            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            //This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
            //Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
            //Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == OmegaMetalDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if ((mp.DashDir == OmegaMetalDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == OmegaMetalDashPlayer.DashRight && player.velocity.X < mp.DashVelocity))
                {
                    //X-velocity is set here
                    int dashDirection = mp.DashDir == OmegaMetalDashPlayer.DashRight ? 1 : -1;
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
                mp.DashDelay = OmegaMetalDashPlayer.MAX_DASH_DELAY;
                mp.DashTimer = OmegaMetalDashPlayer.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }
    }

    public class OmegaMetalDashPlayer : ModPlayer
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
        public static readonly int MAX_DASH_DELAY = 0;
        public static readonly int MAX_DASH_TIMER = 35;

        public override void ResetEffects()
        {
            //ResetEffects() is called not long after player.doubleTapCardinalTimer's values have been set

            //Check if the ExampleDashAccessory is equipped and also check against this priority:
            // If the Shield of Cthulhu, Master Ninja Gear, Tabi and/or Solar Armour set is equipped, prevent this accessory from doing its dash effect
            //The priority is used to prevent undesirable effects.
            //Without it, the player is able to use the ExampleDashAccessory's dash as well as the vanilla ones
            bool dashAccessoryEquipped = false;

            //This is the loop used in vanilla to update/check the not-vanity accessories
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];

                //Set the flag for the ExampleDashAccessory being equipped if we have it equipped OR immediately return if any of the accessories are
                // one of the higher-priority ones
                if (item.type == ModContent.ItemType<OmegaDrive>())
                    dashAccessoryEquipped = true;
                else if (item.type == ItemID.EoCShield || item.type == ItemID.MasterNinjaGear || item.type == ItemID.Tabi)
                    return;
            }

            //If we don't have the ExampleDashAccessory equipped or the player has the Solor armor set equipped, return immediately
            //Also return if the player is currently on a mount, since dashes on a mount look weird, or if the dash was already activated
            if (!dashAccessoryEquipped || player.setSolar || player.mount.Active || DashActive)
                return;

            //When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
            //If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
            if (player.controlRight && player.releaseRight && player.doubleTapCardinalTimer[DashRight] < 15)
                DashDir = DashRight;
            else if (player.controlLeft && player.releaseLeft && player.doubleTapCardinalTimer[DashLeft] < 15)
                DashDir = DashLeft;
            else
                return;  //No dash was activated, return

            DashActive = true;

            //Here you'd be able to set an effect that happens when the dash first activates
            //Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
        }
    }
}