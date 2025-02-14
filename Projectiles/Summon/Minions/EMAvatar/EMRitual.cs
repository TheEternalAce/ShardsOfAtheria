using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar
{
    public class EMRitual : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 0.25f;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (Projectile.localAI[0] == 0)
            {
                var target = ShardsHelpers.FindClosestProjectile(Projectile.Center, 3000, ModContent.ProjectileType<EMAvatar>(), Projectile.owner);
                Projectile.localAI[0] = target.whoAmI;
            }
            var avatar = Main.projectile[(int)Projectile.localAI[0]];

            if (!CheckActive(owner) || avatar == null)
                Projectile.Kill();

            if (Projectile.scale < 1f)
            {
                Projectile.scale += 0.05f;
            }

            Projectile.Center = avatar.Center;
            Projectile.netUpdate = true;
            Projectile.rotation += MathHelper.ToRadians(1);
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.Areus().royalSet || !owner.Areus().MageSetChip)
                return false;
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            if (timeLeft == 0)
            {
                if (!Projectile.GetPlayerOwner().IsLocal()) return;
                SoundEngine.PlaySound(SoundID.NPCDeath7, Projectile.Center);
                foreach (var player in Main.player)
                {
                    if (Projectile.Distance(player.Center) < 500)
                    {
                        player.Heal(player.statLifeMax2 / 10);
                        player.ManaEffect(player.statManaMax2 / 3);
                        player.statMana += player.statManaMax2 / 3;
                    }
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var texure = ModContent.Request<Texture2D>(Texture).Value;
            Main.spriteBatch.Draw(texure, Projectile.Center - Main.screenPosition,
                null, SoA.ElectricColorA, Projectile.rotation, texure.Size() / 2, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}