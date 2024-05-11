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

            Projectile.timeLeft *= 10;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.scale < 5) Projectile.scale += 0.1f;

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
                if (Projectile.Distance(projectile.Center) < radius) projectile.GetGlobalProjectile<SoAGlobalProjectile>().areusNullField = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Asset<Texture2D> circle = ModContent.Request<Texture2D>(SoA.Circle);
            Main.EntitySpriteDraw(circle.Value, Projectile.Center - Main.screenPosition, null, SoA.AreusColorA, 0, circle.Size() / 2, Projectile.scale, SpriteEffects.None);
            return base.PreDraw(ref lightColor);
        }
    }
}