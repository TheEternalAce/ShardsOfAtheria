﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Melee.BalanceSwords
{
    public class LightBeam : HitscanBullet_Melee
    {
        public List<Vector2[]> electricPositions = [];
        public int electricPositionsIndex = 0;

        public Vector2 DirectionNormal => Vector2.Normalize(Projectile.velocity);

        public override Color HitscanColor => Color.White;
        public override int thickness => 4;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(10);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(8);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.timeLeft /= 5;
            Projectile.penetrate = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            electricPositions.Add([Projectile.Center, Projectile.Center, Projectile.Center]);
            SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
            SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
        }

        public override void AI()
        {
            base.AI();
            if (Projectile.velocity != Vector2.Zero && ++Projectile.ai[1] >= 16f - 16f / Projectile.extraUpdates)
            {
                Projectile.ai[1] = 0f;
                electricPositionsIndex++;
                electricPositions.Add([
                    Projectile.Center + DirectionNormal.RotatedBy(MathHelper.PiOver2) * Main.rand.NextFloat(-20f, 20f),
                    Projectile.Center + DirectionNormal.RotatedBy(MathHelper.PiOver2) * Main.rand.NextFloat(-20f, 20f),
                    Projectile.Center + DirectionNormal.RotatedBy(MathHelper.PiOver2) * Main.rand.NextFloat(-20f, 20f),
                    ]);
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width *= 12;
            hitbox.Height *= 12;
            hitbox.X -= hitbox.Width / 2 - Projectile.width / 2;
            hitbox.Y -= hitbox.Height / 2 - Projectile.height / 2;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active)
                {
                    if (proj.type == 514 && -proj.ai[0] - 1 == target.whoAmI) modifiers.FlatBonusDamage += 10;
                    if (proj.type == ModContent.ProjectileType<GoldenNail>() && -proj.ai[0] - 1 == target.whoAmI) modifiers.FlatBonusDamage += 5;
                    if (proj.type == ModContent.ProjectileType<StickingMagnetProj>() && proj.ai[1] == target.whoAmI) { modifiers.FlatBonusDamage += 30; proj.Kill(); }
                    if (proj.aiStyle == 93) { proj.ai[1] = 90; proj.damage *= 0; proj.friendly = false; }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.Gold;
            int thickness = 2;

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < electricPositions.Count - 1; i++)
                {
                    Main.spriteBatch.DrawLine(thickness, electricPositions[i][j] - Main.screenPosition, electricPositions[i + 1][j] - Main.screenPosition, lightColor);
                }
                if (electricPositions.Count > 0) Main.spriteBatch.DrawLine(thickness, electricPositions[electricPositions.Count - 1][j] - Main.screenPosition, Projectile.Center - Main.screenPosition, lightColor);
            }
            return base.PreDraw(ref lightColor);
        }
    }
}
