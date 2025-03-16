using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
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
        readonly List<Vector2> recordedPositions = [];

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed && Main.player[Projectile.owner].statMana > 0;

        public override string Texture => ModContent.GetInstance<WorldSketcher>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(11);
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
            Projectile.penetrate = 10;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 60;

            DrawOriginOffsetY -= 30;
        }

        public override void OnSpawn(IEntitySource source)
        {
            recordedPositions.Add(Projectile.Center);
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (Projectile.ai[0] == 0f)
            {
                if (BeingHeld)
                {
                    Projectile.timeLeft = 3600;
                    player.SetDummyItemTime(10);
                    player.manaRegenDelay = 10;
                    player.manaRegen = 0;
                    if (++Projectile.ai[1] >= 2) { player.CheckMana(1, true); Projectile.ai[1] = 0; }
                    if (player.IsLocal()) { Projectile.Center = Main.MouseWorld; Projectile.netUpdate = true; }

                    if (!Projectile.Hitbox.Contains(recordedPositions[^1].ToPoint()))
                        recordedPositions.Add(Projectile.Center);
                }
                else Projectile.ai[0] = 1f;
            }
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
            Color markColor = new(36, 35, 34);
            int thickness = 6;
            for (int i = 0; i < recordedPositions.Count - 1; i++)
            {
                Main.spriteBatch.DrawLine(thickness, recordedPositions[i] - Main.screenPosition, recordedPositions[i + 1] - Main.screenPosition, markColor);
            }
            if (Projectile.ai[0] == 1) return false;
            return base.PreDraw(ref lightColor);
        }
    }
}
