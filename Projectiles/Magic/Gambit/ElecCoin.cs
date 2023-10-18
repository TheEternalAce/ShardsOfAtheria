using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Gambit
{
    public class ElecCoin : ModProjectile
    {
        int gravityTimer = 0;

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 6; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
        }

        public override void AI()
        {
            var player = Main.player[Projectile.owner];
            Projectile.rotation += MathHelper.ToRadians(32) * Projectile.direction;
            gravityTimer++;
            if (gravityTimer >= 16)
            {
                if (++Projectile.velocity.Y > 16)
                {
                    Projectile.velocity.Y = 16;
                }
                gravityTimer = 16;
                Projectile.friendly = true;
                if (Projectile.Center.Y > player.Center.Y)
                {
                    Projectile.tileCollide = true;
                }
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                    var coin = player.HeldItem.ModItem as AreusGambit;
                    coin?.SetAttack(player);
                }
            }
            foreach (var proj in Main.projectile)
            {
                if (proj.friendly &&
                    proj.owner == Projectile.owner &&
                    (proj.aiStyle == 1 || proj.aiStyle == 0) &&
                    proj.damage > 0 &&
                    proj.active)
                {
                    if (ModLoader.TryGetMod("TerrariaMayQuake", out var mod))
                    {
                        if (mod.TryFind("FeedbackerPunch", out ModProjectile arm))
                        {
                            if (proj.type == arm.Type)
                            {
                                if (arm.Colliding(proj.Hitbox, Projectile.Hitbox) == null)
                                {
                                    gravityTimer = 0;
                                }
                                return;
                            }
                        }
                    }
                    if (proj.Distance(Projectile.Center) <= 30)
                    {
                        float speed = proj.velocity.Length();
                        NPC npc = Projectile.FindClosestNPC(-1);
                        if (npc != null)
                        {
                            if (npc.CanBeChasedBy())
                            {
                                proj.velocity = npc.Center - proj.Center;
                                proj.velocity.Normalize();
                                proj.velocity *= speed + 0.5f;
                                proj.damage = (int)(proj.damage * 1.5f);
                            }
                        }
                        Projectile.Kill();
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }
    }
}
