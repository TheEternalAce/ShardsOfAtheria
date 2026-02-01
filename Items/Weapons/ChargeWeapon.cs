using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    public abstract class ChargeWeapon : ModItem
    {
        public int Charge { get; private set; } = 0;
        public const int MaxCharge = 300;
        public bool smallChargeRing = false;

        public abstract DustInfo ChargeDustInfo { get; }

        public readonly struct DustInfo(int Type, int Alpha = 0, float Scale = 1f)
        {
            public int Type { get; } = Type;
            public float Scale { get; } = Scale;
            public int Alpha { get; } = Alpha;
        }

        public bool FullyCharged => Charge == MaxCharge;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new(Mod, "Charge", ShardsHelpers.LocalizeCommon("HoldToCharge", SoA.ChargeWeapons.GetAssignedKeys().FirstOrDefault()));
            tooltips.AddTooltip(line);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (Charge >= MaxCharge) damage += 1f;
        }

        public override void UpdateInventory(Player player)
        {
            bool cable = ToggleableTool.GetActive<BrokenCable>(player);
            bool charging = SoA.ChargeWeapons.GetAssignedKeys().Count > 0 && Main.keyState.IsKeyDown(Enum.Parse<Keys>(SoA.ChargeWeapons.GetAssignedKeys()[0]));
            if ((!player.ItemAnimationActive || player.HeldItem != Item) && !cable && charging)
            {
                if (Charge < MaxCharge)
                {
                    Charge++;
                    if (SoA.ClientConfig.chargeSound && Charge % 25 == 0) SoundEngine.PlaySound(SoA.ZeroCharge, player.Center);

                    int dustRingSize = smallChargeRing ? 25 : 50;
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(dustRingSize, dustRingSize);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, ChargeDustInfo.Type, 0, 0, ChargeDustInfo.Alpha, Scale: ChargeDustInfo.Scale);
                        dust.velocity = player.velocity;
                        if (Main.rand.NextBool(3)) dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                        dust.noGravity = true;
                    }
                }
                if (Charge == MaxCharge - 1) SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
            }
            else if (!FullyCharged) Charge = 0;
            else if (player.HeldItem == Item && player.ItemAnimationActive && player.ItemAnimationEndingOrEnded) Charge = 0;
        }

    }
}
