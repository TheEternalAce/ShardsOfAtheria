using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Tools.Misc.Slayer;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class TrueEyeOfCthulhu : ModProjectile
    {
        public int fireTimer;

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            Projectile.Center = owner.Center - new Vector2(0, 90);

            if (!CheckActive(owner))
            {
                Projectile.Kill();
                return;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                if (owner.direction == 1)
                    Projectile.Center = owner.Center + new Vector2(45, -45);
                else Projectile.Center = owner.Center + new Vector2(-45, -45);

                Projectile.spriteDirection = Main.MouseWorld.X > Projectile.Center.X ? -1 : 1;

                if (Main.mouseLeft && !Main.LocalPlayer.mouseInterface && owner.HeldItem.type != ModContent.ItemType<SoulExtractingDagger>())
                {
                    Projectile.ai[1]++;
                    if (Projectile.ai[1] == 1)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 10, ModContent.ProjectileType<PhantasmalEye>(), 100, 2f, owner.whoAmI);
                    if (Projectile.ai[1] >= 60)
                        Projectile.ai[1] = 0;
                }
                else if (Projectile.ai[1] > 0)
                {
                    Projectile.ai[1]++;
                    if (Projectile.ai[1] >= 60)
                        Projectile.ai[1] = 0;
                }
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.Slayer().MoonLordSoul)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
