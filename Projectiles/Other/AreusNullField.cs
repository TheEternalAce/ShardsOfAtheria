using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class AreusNullField : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;

            Projectile.scale = 0.1f;
            Projectile.timeLeft *= 10;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0 && Projectile.scale < 5) Projectile.scale += 0.1f;
            else if (Projectile.ai[0] == 0) Projectile.ai[0] = 1;
            else if (Projectile.ai[0] == 2)
            {
                Projectile.scale -= 0.1f;
                if (Projectile.scale <= 0f) Projectile.Kill();
            }

            float radius = 1070f / 5 * Projectile.scale;
            foreach (NPC npc in Main.npc)
            {
                if (Projectile.Distance(npc.Center) < radius) npc.GetGlobalNPC<SoAGlobalNPC>().areusNullField = true;
            }
            foreach (Player player in Main.player)
            {
                if (Projectile.Distance(player.Center) < radius) player.AddBuff<NullField>(2);
            }
            foreach (Projectile projectile in Main.projectile)
            {
                if (Projectile.Distance(projectile.Center) < radius + 20)
                {
                    if (SoAGlobalProjectile.ReflectAiList.Contains(projectile.aiStyle) && Projectile.Distance(projectile.Center) > radius - 20)
                    {
                        float speed = projectile.velocity.Length();
                        Vector2 vector = projectile.Center - Projectile.Center;
                        vector.Normalize();
                        vector *= speed;
                        var vector2 = vector + Projectile.Center;
                        if (!ShardsHelpers.MovingTowardPoint(vector2, projectile.velocity, Projectile.Center, 400)) vector *= -1;
                        projectile.velocity = vector.RotatedByRandom(MathHelper.ToRadians(15));
                        projectile.netUpdate = true;
                    };
                    projectile.GetGlobalProjectile<SoAGlobalProjectile>().areusNullField = true;
                    if (projectile.damage > 0) projectile.damage = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> circle = ModContent.Request<Texture2D>(SoA.Circle);
            Main.EntitySpriteDraw(circle.Value, Projectile.Center - Main.screenPosition, null, SoA.AreusColorA * 0.4f, 0, circle.Size() / 2, Projectile.scale, SpriteEffects.None);
            return base.PreDraw(ref lightColor);
        }
    }
}