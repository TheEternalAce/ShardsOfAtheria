using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class StormSword : ModProjectile
    {
        public static Asset<Texture2D> glowmask;
        private double rotation;
        private int attacks = 4;

        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/ValkyrieBlade";

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture);
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            ProjectileElements.Metal.Add(Type);
            ProjectileElements.Electric.Add(Type);
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.damage = 37;

            Projectile.timeLeft = 121;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!NPC.AnyNPCs(ModContent.NPCType<NovaStellar>()))
            {
                Projectile.Kill();
            }

            if (attacks < 1)
            {
                Projectile.Kill();
            }

            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -40;
                DrawOriginOffsetX = 16;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -16;
            }

            if (Projectile.ai[1] == 0)
            {
                rotation += .05;
                if (rotation > MathHelper.TwoPi)
                {
                    rotation -= MathHelper.TwoPi;
                }
                Projectile.Center = player.Center + Vector2.One.RotatedBy(Projectile.ai[0] / 7f * MathHelper.TwoPi + rotation) * 225;
                Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + MathHelper.ToRadians(225);
                if (Projectile.timeLeft <= 40)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                    dust.noGravity = true;
                }
                if (Projectile.timeLeft == 1)
                {
                    Projectile.timeLeft = 121;
                    Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 6f;
                    Projectile.ai[1] = 1;
                }
            }
            if (Projectile.ai[1] == 1)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

                if (Projectile.timeLeft == 1)
                {
                    Projectile.ai[1] = 0;
                    Projectile.timeLeft = 121;
                    rotation += MathHelper.ToRadians(180);
                    attacks -= 1;
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 300);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new(227, 182, 245, 80);
            Projectile.DrawProjectilePrims(color, ProjectileHelper.DiamondX1, MathHelper.ToRadians(45f));
            lightColor = Color.White;
            return true;
        }
    }
}