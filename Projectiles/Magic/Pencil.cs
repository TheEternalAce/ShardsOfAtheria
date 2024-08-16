using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class Pencil : ModProjectile
    {
        List<Vector2> recordedPositions = [];

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            recordedPositions.Add(Projectile.Center);
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (BeingHeld && Projectile.ai[0] == 0f)
            {
                Projectile.timeLeft = 3600;
                player.SetDummyItemTime(10);
                player.manaRegenDelay = 10;
                if (player.IsLocal()) { Projectile.Center = Main.MouseWorld; Projectile.netUpdate = true; }

                if (!recordedPositions.Contains(Projectile.Center)) recordedPositions.Add(Projectile.Center);
            }
            else Projectile.ai[0] = 1f;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = 0; i < recordedPositions.Count - 1; i++)
            {
                if (ShardsHelpers.DeathrayHitbox(recordedPositions[i], recordedPositions[i + 1], targetHitbox, 6f)) return true;
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = new(36, 35, 34);
            int thickness = 6;
            for (int i = 0; i < recordedPositions.Count - 1; i++)
            {
                Main.spriteBatch.DrawLine(thickness, recordedPositions[i] - Main.screenPosition, recordedPositions[i + 1] - Main.screenPosition, lightColor);
            }
            Main.spriteBatch.DrawLine(thickness, recordedPositions[^1] - Main.screenPosition, Projectile.Center - Main.screenPosition, lightColor);
            if (Projectile.ai[0] == 1) return false;
            return base.PreDraw(ref lightColor);
        }
    }
}
