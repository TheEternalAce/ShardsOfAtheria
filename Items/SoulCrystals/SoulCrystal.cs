using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.NPCs.Misc;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Tools;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public abstract class SoulCrystal : ModItem
    {
        public int absorbSoulTimer = 300;

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
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
            Item.channel = true;

            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line;
            if (!ModContent.GetInstance<ShardsClientConfig>().instantAbsorb)
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
            if (absorbSoulTimer == 299 && !ModContent.GetInstance<ShardsClientConfig>().instantAbsorb)
                SoundEngine.PlaySound(SoundID.Item46);
            if (absorbSoulTimer == 240)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 180)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 120)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 60)
                SoundEngine.PlaySound(SoundID.Item43);
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ShardsClientConfig>().instantAbsorb)
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

    public class KingSoulCrystal : SoulCrystal
    {

    }

    public class EyeSoulCrystal : SoulCrystal
    {
        public override bool? UseItem(Player player)
        {
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ShardsClientConfig>().instantAbsorb)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<AllSeeingEye>()] <= 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
                }
            }
            return base.UseItem(player);
        }
    }

    public class BrainSoulCrystal : SoulCrystal
    {
        public override bool? UseItem(Player player)
        {
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ShardsClientConfig>().instantAbsorb)
            {
                if (!player.HasBuff(ModContent.BuffType<CreeperShield>()))
                {
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X + 20), (int)(player.Center.Y + 20), ModContent.NPCType<Creeper>());
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X - 20), (int)(player.Center.Y + 20), ModContent.NPCType<Creeper>());
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X + 20), (int)(player.Center.Y - 20), ModContent.NPCType<Creeper>());
                    NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)(player.Center.X - 20), (int)(player.Center.Y - 20), ModContent.NPCType<Creeper>());
                }
            }
            return base.UseItem(player);
        }
    }

    #region Create Soul Crystals
    public class EaterSoulCrystal : SoulCrystal
    {

    }

    public class BeeSoulCrystal : SoulCrystal
    {

    }

    public class SkullSoulCrystal : SoulCrystal
    {
    }

    public class ValkyrieSoulCrystal : SoulCrystal
    {
    }

    public class DeerclopsSoulCrystal : SoulCrystal
    {
    }

    public class WallSoulCrystal : SoulCrystal
    {
    }

    public class QueenSoulCrystal : SoulCrystal
    {
    }

    public class DestroyerSoulCrystal : SoulCrystal
    {
    }

    public class PrimeSoulCrystal : SoulCrystal
    {
    }

    public class TwinsSoulCrystal : SoulCrystal
    {
    }

    public class PlantSoulCrystal : SoulCrystal
    {
    }

    public class GolemSoulCrystal : SoulCrystal
    {
    }

    public class DukeSoulCrystal : SoulCrystal // This and LunaticSoulCrystal actualy have stuff in them lmao
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SoulTeleport", string.Format("Press {0} to teleport", ShardsOfAtheriaMod.SoulTeleport.GetAssignedKeys().Count > 0 ? ShardsOfAtheriaMod.SoulTeleport.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));

            base.ModifyTooltips(tooltips);
        }
    }

    public class EmpressSoulCrystal : SoulCrystal
    {
    }

    public class LunaticSoulCrystal : SoulCrystal
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SoulTeleport", string.Format("Press {0} to teleport", ShardsOfAtheriaMod.SoulTeleport.GetAssignedKeys().Count > 0 ? ShardsOfAtheriaMod.SoulTeleport.GetAssignedKeys()[0] : "[Unbounded Hotkey]")));

            base.ModifyTooltips(tooltips);
        }
    }

    public class LordSoulCrystal : SoulCrystal
    {
    }
    #endregion
}
