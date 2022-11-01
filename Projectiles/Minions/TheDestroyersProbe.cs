using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Tools.Misc;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions
{
    public class TheDestroyersProbe : ModProjectile
    {
        public int shootTimer;
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.netUpdate = true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!CheckActive(player))
            {
                return;
            }
            if (Main.myPlayer == Projectile.owner)
            {
                var direction = Main.MouseWorld - Projectile.Center;
                if (Projectile.spriteDirection == 1)
                    Projectile.rotation = direction.ToRotation() + MathHelper.ToRadians(180);
                else Projectile.rotation = direction.ToRotation();

                if (player.direction == 1)
                    Projectile.Center = player.Center + new Vector2(-45, -45);
                else Projectile.Center = player.Center + new Vector2(45, -45);

                Projectile.spriteDirection = Main.MouseWorld.X > Projectile.Center.X ? -1 : 1;

                if (Main.mouseLeft && !Main.LocalPlayer.mouseInterface && player.HeldItem.type != ModContent.ItemType<SoulExtractingDagger>())
                {
                    shootTimer++;
                    if (shootTimer == 1)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 10, ProjectileID.MiniRetinaLaser, 50, 2f, player.whoAmI);
                    if (shootTimer >= 60)
                        shootTimer = 0;
                }
                else if (shootTimer > 0)
                {
                    shootTimer++;
                    if (shootTimer >= 60)
                        shootTimer = 0;
                }
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(ModContent.ItemType<DestroyerSoulCrystal>()))
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }
    }
}
