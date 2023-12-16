﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class EmeraldTeleport : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.LargeEmerald;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.tileCollide = false;
            Projectile.alpha = 100;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Vector2 position = new(Projectile.ai[0], Projectile.ai[1]);
            Projectile.Track(position, 13f, 1f);
            if (Projectile.Distance(position) < 10)
            {
                Projectile.Kill();
            }

            Player owner = Main.player[Projectile.owner];
            owner.Center = Projectile.Center;
            owner.velocity = Vector2.Zero;
            owner.immuneNoBlink = true;
            owner.immuneTime = 2;
            owner.AddBuff(BuffID.Invisibility, 2);
            int cooldownTime = 360;
            if (owner.Gem().megaGemCore)
            {
                cooldownTime = 180;
            }
            owner.AddBuff<EmeraldTeleportCooldown>(cooldownTime);

            if (Projectile.ai[2] == 0)
            {
                SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.5f), Projectile.Center);
                //SoundEngine.PlaySound(SoundID.Item37.WithPitchOffset(-0.5f), Projectile.Center);
                //SoundEngine.PlaySound(SoundID.NPCHit4.WithPitchOffset(-0.5f), Projectile.Center);
                Projectile.ai[2] = 1;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink);
            ShardsHelpers.DustRing(Projectile.Center, 30, DustID.GemEmerald);
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Projectile.hide = true;
            overPlayers.Add(index);
        }
    }
}