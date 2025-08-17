using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.MagicalGems
{
    public class MagicStone : ModProjectile
    {
        public int dustID = DustID.Stone;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(1);
            Projectile.AddRedemptionElement(5);

            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 8;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 4;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.light = 0.4f;
            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            //if (++Projectile.ai[0] >= 6)
            //{
            //    Dust d = Dust.NewDustPerfect(Projectile.Center, dustID, Vector2.Zero);
            //    d.velocity *= 0;
            //    d.noGravity = true;
            //}
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawAfterImage(lightColor);
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            return base.OnTileCollide(oldVelocity);
        }
    }

    public class AmberStone : MagicStone
    {
        public override void OnKill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer && Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<AmberFly2>()] == 0)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<AmberFly2>(), 15, 0f);
        }
    }

    public class AmethystStone : MagicStone
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustID = DustID.GemAmethyst;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);

            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X * 1.05f;
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 1.05f;
            }

            float speed = Projectile.velocity.Length();
            if (speed > 20f)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 20f;
                speed = 20f;
            }

            var npc = Projectile.FindClosestNPC(null, 100);
            if (npc != null)
            {
                var velocity = npc.Center - Projectile.Center;
                velocity.Normalize();
                Projectile.velocity = velocity *= speed;
            }
            return false;
        }
    }

    public class DiamondStone : MagicStone
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = 5;
            dustID = DustID.GemDiamond;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.8f);
        }
    }

    public class EmeraldStone : MagicStone
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.extraUpdates = 9;
            dustID = DustID.GemEmerald;
        }
    }

    public class RubyStone : MagicStone
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustID = DustID.GemRuby;
        }
    }

    public class SapphireStone : MagicStone
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustID = DustID.GemSapphire;
        }

        public override void AI()
        {
            base.AI();
            var target = Projectile.FindClosestNPC(null, 200f);
            if (target != null) Projectile.Track(target, 6f, 20f);
        }
    }

    public class TopazStone : MagicStone
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            dustID = DustID.GemTopaz;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Projectile.GetPlayerOwner();
            for (int i = 0; i < player.buffType.Length - 1; i++)
            {
                if (player.buffTime[i] > 0 && Main.debuff[player.buffType[i]])
                {
                    player.buffTime[i] -= 30;
                    break;
                }
            }
        }
    }
}
