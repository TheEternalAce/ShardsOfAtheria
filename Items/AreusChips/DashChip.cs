using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Armor.Areus.Guard;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class DashChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            slotType = SlotChest;
        }

        public override void ChipEffect(Player player)
        {
            var mp = player.GetModPlayer<AreusDashPlayer>();

            //If the dash is not active, immediately return so we don't do any of the logic for it
            if (!mp.DashActive)
                return;

            //If the dash has just started, apply the dash velocity in whatever direction we wanted to dash towards
            if (mp.DashTimer == AreusDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if (mp.DashDir == AreusDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity || mp.DashDir == AreusDashPlayer.DashRight && player.velocity.X < mp.DashVelocity)
                {
                    //X-velocity is set here
                    int dashDirection = mp.DashDir == AreusDashPlayer.DashRight ? 1 : -1;
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
                mp.DashDelay = AreusDashPlayer.MAX_DASH_DELAY;
                mp.DashTimer = AreusDashPlayer.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusArmorChip>()
                .AddIngredient(ItemID.SwiftnessPotion, 3)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }

    public class AreusDashPlayer : ModPlayer
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
        //The initial velocity. 10 velocity is about 37.5 tiles/second or 50 mph
        public float DashVelocity = 12f;
        //These two fields are the max values for the delay between dashes and the length of the dash in that order
        //The time is measured in frames
        public const int MAX_DASH_DELAY = 50;
        public const int MAX_DASH_TIMER = 15;

        public override void ResetEffects()
        {
            //ResetEffects() is called not long after player.doubleTapCardinalTimer's values have been set

            //Check if the ExampleDashAccessory is equipped and also check against this priority:
            // If the Shield of Cthulhu, Master Ninja Gear, Tabi and/or Solar Armour set is equipped, prevent this accessory from doing its dash effect
            //The priority is used to prevent undesirable effects.
            //Without it, the player is able to use the ExampleDashAccessory's dash as well as the vanilla ones
            bool dashAccessoryEquipped = false;

            //Set the flag for the ExampleDashAccessory being equipped if we have it equipped OR immediately return if any of the accessories are
            // one of the higher-priority ones
            for (int i = 0; i < Player.armor.Length / 2; i++)
            {
                var item = Player.armor[i];
                if (item.type == ModContent.ItemType<GuardMail>())
                {
                    dashAccessoryEquipped = Player.HasChipEquipped(ModContent.ItemType<DashChip>());
                }
                else if (item.type == ItemID.EoCShield || item.type == ItemID.MasterNinjaGear || item.type == ItemID.Tabi)
                {
                    return;
                }
            }

            //If we don't have the ExampleDashAccessory equipped or the player has the Solor armor set equipped, return immediately
            //Also return if the player is currently on a mount, since dashes on a mount look weird, or if the dash was already activated
            if (!dashAccessoryEquipped || Player.setSolar || Player.mount.Active || DashActive)
                return;

            if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
                DashDir = DashRight;
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
                DashDir = DashLeft;
            else
                return;  //No dash was activated, return

            DashActive = true;
            Player.timeSinceLastDashStarted = 0;

            //Here you'd be able to set an effect that happens when the dash first activates
            //Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi

            var vector = new Vector2(1, 0) * -Player.direction;
            for (int i = 0; i < 10; i++)
            {
                var dust = Dust.NewDustDirect(Player.position, Player.width, Player.height,
                    ModContent.DustType<AreusDust>());
                dust.velocity = vector.RotatedByRandom(MathHelper.ToRadians(15));
                dust.velocity *= Main.rand.NextFloat(4f, 8f);
            }
        }

        public override void FrameEffects()
        {
            if (DashActive)
            {
                Player.armorEffectDrawShadow = true;
            }
        }
    }
}