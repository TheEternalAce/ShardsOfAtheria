using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public abstract class SoulCrystal : SlayerItem
    {
        public int absorbSoulTimer = 300;
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
            var line2 = new TooltipLine(Mod, "Verbose:RemoveMe", "This tooltip won't show in-game");
            if (!ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
                line = new TooltipLine(Mod, "SoulCrystal", "Hold left click for 5 seconds to absorb the soul inside, this grants you this boss's powers")
                {
                    OverrideColor = Color.Purple
                };
            else line = new TooltipLine(Mod, "SoulCrystal", "Use to absorb the soul inside, this grants you this boss's powers")
                {
                    OverrideColor = Color.Purple
                };
            line2 = new TooltipLine(Mod, "Slayer Item", "Slayer Item")
            {
                OverrideColor = Color.Red
            };
            tooltips.Add(line);
            tooltips.Add(line2);
        }

        public override void UpdateInventory(Player Player)
        {
            if (!Main.mouseLeft)
                absorbSoulTimer = 300;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            if (Main.rand.NextBool(3))
                Dust.NewDustDirect(player.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, Scale: 1.2f);
            Lighting.AddLight(player.Center, TorchID.Yellow);
            if (absorbSoulTimer == 299 && !ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
                SoundEngine.PlaySound(SoundID.Item46);
            if (absorbSoulTimer == 240)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 180)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 120)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 60)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                if (ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (Main.rand.NextBool(3))
                            Dust.NewDustDirect(player.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, default, 1.2f);
                        Lighting.AddLight(player.Center, TorchID.Yellow);
                    }
                }
                Item.TurnToAir();
                SoundEngine.PlaySound(SoundID.Item4);
            }
            return true;
        }
    }

    public class SoulCrystalStatus
    {
        public const int None = 0;
        public const int Absorbed = 2;
    }
}
