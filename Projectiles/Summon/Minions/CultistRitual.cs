using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Tools.Misc.Slayer;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class CultistRitual : ModProjectile
    {
        public int fireTimer;
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 180;
            Projectile.timeLeft = 360000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];


            if (!CheckActive(owner))
                Projectile.Kill();

            Projectile.Center = owner.Center;
            if (Main.myPlayer == Projectile.owner)
            {
                if (Main.mouseLeft && !Main.LocalPlayer.mouseInterface && owner.HeldItem.type != ModContent.ItemType<SoulExtractingDagger>())
                {
                    Projectile.ai[1]++;
                    if (Projectile.ai[1] == 1)
                    {
                        SoundEngine.PlaySound(SoundID.Item120.WithVolumeScale(0.5f));
                        if (owner.Slayer().lunaticCircleFragments > 1)
                        {
                            float numberProjectiles = owner.Slayer().lunaticCircleFragments;
                            float rotation = MathHelper.ToRadians(20);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = Vector2.Normalize(Main.MouseWorld - Projectile.Center).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, perturbedSpeed * 16, ModContent.ProjectileType<IceFragment>(), 60, 1, owner.whoAmI);
                            }
                        }
                        else Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 16, ModContent.ProjectileType<IceFragment>(), 90, 1, owner.whoAmI);
                    }
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
            if (owner.dead || !owner.active || !owner.Slayer().CultistSoul)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}