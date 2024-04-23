using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
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
            if (Main.netMode == NetmodeID.Server)
                return;
            glowmask = ModContent.Request<Texture2D>(Texture);
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
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
                Projectile.rotation = (Projectile.Center - player.Center).ToRotation() + MathHelper.ToRadians(225);
                if (!SoA.Eternity())
                {
                    if (Projectile.timeLeft <= 40)
                    {
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                        dust.noGravity = true;
                    }
                }
                if (Projectile.timeLeft == 1)
                {
                    Projectile.timeLeft = 121;
                    Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 6f;
                    SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
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
                    rotation += MathHelper.Pi;
                    attacks -= 1;
                }
                if (SoA.Eternity())
                {
                    if (++Projectile.ai[2] >= 8)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Photosphere>(), Projectile.damage, 0f);
                        Projectile.ai[2] = 0;
                    }
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<ElectricShock>(600);
            if (SoA.Eternity() && Main.rand.NextBool(5))
            {
                target.AddBuff(ModContent.Find<ModBuff>("FargowiltasSouls", "ClippedWingsBuff").Type, 600);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return true;
        }
    }
}