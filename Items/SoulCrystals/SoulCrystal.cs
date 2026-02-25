using Humanizer;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.NPCs.Misc;
using ShardsOfAtheria.Projectiles.Tools;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public abstract class SoulCrystal : ModItem
    {
        const int ABSORB_SOUL_TIMER_MAX = 180;
        public int absorbSoulTimer = ABSORB_SOUL_TIMER_MAX;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
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

            Item.rare = ItemDefaults.RaritySlayer;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line;
            if (!SoA.ClientConfig.instantAbsorb)
                line = new TooltipLine(Mod, "SoulCrystal", "Hold use for 5 seconds to absorb the soul inside, this grants you this boss's powers")
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
            if (!player.ItemAnimationActive)
                absorbSoulTimer = ABSORB_SOUL_TIMER_MAX;
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
            if (SoA.ClientConfig.instantAbsorb) absorbSoulTimer = 0;
            else
            {
                switch (absorbSoulTimer)
                {
                    case 179:
                        SoundEngine.PlaySound(SoundID.Item46, player.Center);
                        break;
                    case 120:
                    case 60:
                        SoundEngine.PlaySound(SoundID.Item43, player.Center);
                        break;
                }
            }
            if (absorbSoulTimer == 0)
            {
                var slayer = player.Slayer();
                for (int i = 0; i < 20; i++)
                {
                    if (Main.rand.NextBool(3))
                        Dust.NewDustDirect(player.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, default, 1.2f);
                    Lighting.AddLight(player.Center, TorchID.Yellow);
                }
                if (!slayer.soulCrystalNames.Contains(Name))
                {
                    slayer.soulCrystalNames.Add(Name);
                    Item.TurnToAir();
                    SoundEngine.PlaySound(SoundID.Item4, player.Center);
                    SoA.LogInfo(slayer.soulCrystalNames, "Soul crystals: ");
                    CompleteAbsorption(player);
                }
            }
            return true;
        }

        public virtual void CompleteAbsorption(Player player) { }
    }

    public class EyeSoulCrystal : SoulCrystal
    {
        public override void CompleteAbsorption(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<AllSeeingEye>()] <= 0)
                Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero,
                    ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
        }
    }

    public class BrainSoulCrystal : SoulCrystal
    {
        public override void CompleteAbsorption(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<CreeperShield>()))
            {
                var source = NPC.GetBossSpawnSource(player.whoAmI);
                int type = ModContent.NPCType<Creeper>();
                int halfSize = 13;
                for (int i = 0; i < 8; i++)
                {
                    float rotation = MathHelper.TwoPi / 8f * i;
                    Vector2 position = player.Center + Vector2.One.RotatedBy(rotation) * 50f;
                    NPC.NewNPC(source, (int)position.X - halfSize, (int)position.Y - halfSize, type);
                }
            }
        }
    }

    public class DukeSoulCrystal : SoulCrystal
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ModContent.GetInstance<LunaticSoulCrystal>().ModifyTooltips(tooltips);
        }
    }

    public class LunaticSoulCrystal : SoulCrystal
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string lineText = this.GetLocalization("Teleport").Value.FormatWith(SoA.SoulTeleport.GetAssignedKeys().FirstOrDefault());
            TooltipLine line = new(Mod, "SoulCrystalTeleport", lineText);
            tooltips.AddTooltip(line);
        }

    }

    #region Create Soul Crystals
    public class KingSoulCrystal : SoulCrystal
    {

    }

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

    public class EmpressSoulCrystal : SoulCrystal
    {
    }

    public class LordSoulCrystal : SoulCrystal
    {
    }

    public class DeathSoulCrystal : SoulCrystal
    {
    }
    #endregion
}
