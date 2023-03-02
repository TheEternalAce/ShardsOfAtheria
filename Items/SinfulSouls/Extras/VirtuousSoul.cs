using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Effects.ScreenShaking;

namespace ShardsOfAtheria.Items.SinfulSouls.Extras
{
    public class VirtuousSoul : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Sinful().SevenSoulUsed = Type;
            player.statDefense += 10;
            player.GetDamage(DamageClass.Generic) += .1f;
            player.thorns = 1;
        }
    }

    public class VirtuousPlayer : ModPlayer
    {
        public int purification;

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Player.HasBuff(ModContent.BuffType<VirtuousSoul>()))
            {
                purification++;
                if ((crit || target.life <= 0) && purification >= 35)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<HolyExplosion>(), item.damage * 3, item.knockBack, Player.whoAmI);
                    purification -= 35;
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Player.HasBuff(ModContent.BuffType<VirtuousSoul>()) && proj.type != ModContent.ProjectileType<HolyExplosion>())
            {
                purification++;
                if ((crit || target.life <= 0) && purification >= 35)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<HolyExplosion>(), proj.damage * 3, proj.knockBack, Player.whoAmI);
                    purification -= 35;
                }
            }
        }
    }
    public class HolyExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2000;
            Projectile.height = 1000;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10;

            DrawOffsetX = Projectile.width / 2 - 20;
            DrawOriginOffsetY = Projectile.height / 2 - 90;
        }

        public override void AI()
        {
            Projectile.Center = Main.player[Projectile.owner].Center;
            if (Projectile.ai[0] == 0)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.width = Main.screenWidth;
                    Projectile.height = Main.screenHeight;

                    DrawOffsetX = Projectile.width / 2 - 20;
                    DrawOriginOffsetY = Projectile.height / 2 - 90;
                }
                ScreenShake.ShakeScreen(6, 60);
                SoundEngine.PlaySound(SoundID.Item14);
                Projectile.ai[0] = 1;
            }

            for (int i = 0; i < 6; i++)
            {
                Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f)];
                obj4.noGravity = true;
                obj4.velocity *= 2f;
                obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                obj4.fadeIn = 1.5f;
            }
            for (int i = 0; i < 6; i++)
            {
                Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f)];
                obj4.noGravity = true;
                obj4.velocity *= 2f;
                obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                obj4.fadeIn = 1.5f;
            }
        }
    }
}
