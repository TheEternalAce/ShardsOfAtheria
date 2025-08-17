using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.EntropyCatalyst
{
    public class CatalystShockwave : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(12);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(100);
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 30;
            Projectile.tileCollide = false;
            Projectile.penetrate = 5;
            Projectile.friendly = true;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.extraUpdates = 2;
            Projectile.ownerHitCheck = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            Projectile.velocity /= 2;
            for (int i = 0; i < 10; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
                dust.velocity = Projectile.velocity;
            }
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (++Projectile.ai[0] > 20) Projectile.velocity *= 0.9f;
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.owner == Projectile.owner && proj.type == Projectile.type - 1 && Projectile.Hitbox.Intersects(proj.Hitbox) && proj.ai[0] == 0)
                {
                    proj.ai[0] = 1;
                    proj.extraUpdates = 20;
                    proj.damage = (int)(proj.damage * 1.25f);
                    if (Projectile.owner == Main.myPlayer) proj.velocity = proj.DirectionTo(Main.MouseWorld);
                    if (Main.hardMode)
                    {
                        player.statMana += 8;
                        player.ManaEffect(8);
                        proj.velocity *= 2f;
                    }
                    if (player.Pride().soulActive && player.InCombat()) player.Pride().attacks--;
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : -1;
        }
    }
}
