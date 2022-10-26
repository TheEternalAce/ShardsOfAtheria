using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public abstract class SoulCrystal : ModItem
    {
        public int absorbSoulTimer = 300;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SoAGlobalItem.SlayerItem.Add(Type);
        }

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
            TooltipLine line;
            if (!ModContent.GetInstance<ShardsConfigClientSide>().instantAbsorb)
                line = new TooltipLine(Mod, "SoulCrystal", "Hold left click for 5 seconds to absorb the soul inside, this grants you this boss's powers")
                {
                    OverrideColor = Color.Purple
                };
            else line = new TooltipLine(Mod, "SoulCrystal", "Use to absorb the soul inside, this grants you this boss's powers")
            {
                OverrideColor = Color.Purple
            };
            tooltips.Add(line);
        }

        public override void UpdateInventory(Player player)
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
            absorbSoulTimer--;
            if (Main.rand.NextBool(3))
                Dust.NewDustDirect(player.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, Scale: 1.2f);
            Lighting.AddLight(player.Center, TorchID.Yellow);
            if (absorbSoulTimer == 299 && !ModContent.GetInstance<ShardsConfigClientSide>().instantAbsorb)
                SoundEngine.PlaySound(SoundID.Item46);
            if (absorbSoulTimer == 240)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 180)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 120)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 60)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ShardsConfigClientSide>().instantAbsorb)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (Main.rand.NextBool(3))
                        Dust.NewDustDirect(player.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, default, 1.2f);
                    Lighting.AddLight(player.Center, TorchID.Yellow);
                }
                if (!player.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(Type))
                {
                    player.GetModPlayer<SlayerPlayer>().soulCrystals.Add(Type);
                    Item.TurnToAir();
                    SoundEngine.PlaySound(SoundID.Item4);
                }
            }
            return true;
        }
    }
}
