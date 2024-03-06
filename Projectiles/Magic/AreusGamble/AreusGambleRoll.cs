using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Magic.AreusGamble
{
    public class AreusGambleRoll : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 6;
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            var player = Projectile.GetPlayerOwner();
            if (player.Shards().Overdrive)
            {
                Projectile.timeLeft = 30;
                int result = Main.rand.Next(6) + 1;
                Projectile.frame = result - 1;
                Projectile.ai[0] = result;
            }
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            player.manaRegenDelay = 60;
            Projectile.Center = player.Center + new Vector2(0, -70);

            if (Projectile.timeLeft > 30)
            {
                if (Projectile.timeLeft % 10 == 0)
                {
                    int result = Main.rand.Next(6) + 1;
                    Projectile.frame = result - 1;
                    Projectile.ai[0] = result;
                }
            }
            if (Projectile.timeLeft == 30)
            {
                if (player.HasItem(ModContent.ItemType<WeightedDie>()))
                {
                    int inventoryIndex = player.FindItem(ModContent.ItemType<WeightedDie>());
                    var glove = player.inventory[inventoryIndex].ModItem as WeightedDie;
                    if (glove.cheatSide > 0)
                    {
                        int result = glove.cheatSide;
                        Projectile.frame = result - 1;
                        Projectile.ai[0] = result;
                    }
                }
            }
            if (Projectile.timeLeft == 15)
            {
                CombatText.NewText(Projectile.Hitbox, Color.Cyan, (int)Projectile.ai[0]);
            }
            foreach (var proj in Main.projectile)
            {
                if (proj.friendly &&
                    proj.owner == Projectile.owner &&
                    (proj.aiStyle == 1 || proj.aiStyle == 0) &&
                    proj.type != ModContent.ProjectileType<AreusGambleDagger>() &&
                    proj.damage > 0 &&
                    proj.active)
                {
                    if (proj.Distance(Projectile.Center) <= 30)
                    {
                        Projectile.Kill();
                        proj.Kill();
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            var player = Projectile.GetPlayerOwner();
            switch (Projectile.ai[0])
            {
                case 1f:
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                        ModContent.ProjectileType<AreusGambleBadExplosion>(), 90, 0, Projectile.owner);
                    break;
                case 2f:
                    if (player.IsLocal())
                    {
                        var velocity = Main.MouseWorld - Projectile.Center;
                        velocity.Normalize();
                        velocity *= 16f;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center - velocity * 8, velocity,
                            ModContent.ProjectileType<AreusGambleDagger>(), (int)(Projectile.damage * 0.66f), 0, Projectile.owner);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
                            ModContent.ProjectileType<AreusGambleDagger>(), (int)(Projectile.damage * 0.66f), 0, Projectile.owner);
                    }
                    break;
                case 3f:
                    ShardsHelpers.ProjectileRing(Projectile.GetSource_FromThis(), Projectile.Center, 2, 1, 12f,
                       ProjectileID.ThunderStaffShot, (int)(Projectile.damage * 0.66f), 0, rotationAddition: -MathHelper.PiOver4);
                    if (player.IsLocal())
                    {
                        var velocity = Main.MouseWorld - Projectile.Center;
                        velocity.Normalize();
                        velocity *= 16f;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
                            ProjectileID.ThunderStaffShot, (int)(Projectile.damage * 0.66f), 0, Projectile.owner);
                    }
                    break;
                case 4f:
                    if (player.IsLocal())
                    {
                        ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Main.MouseWorld, 4, 26, 0,
                            DamageClass.Magic, Projectile.owner);
                    }
                    break;
                case 5f:
                    ShardsHelpers.ProjectileRing(Projectile.GetSource_FromThis(), Projectile.Center, 5, 1, 16f,
                       ModContent.ProjectileType<AreusGambleThrow>(), (int)(Projectile.damage * 0.5f), 0,
                       rotationAddition: MathHelper.ToRadians(-25));
                    break;
                case 6f:
                    for (var i = 0; i < 50; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(16f, 16f);
                        Dust d = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<AreusDust>(), speed);
                        d.fadeIn = 1.3f;
                        d.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.NPCHit53, Projectile.Center);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                        ModContent.ProjectileType<AreusGambleGoodExplosion>(), Projectile.damage, 0, Projectile.owner);
                    break;
            }
        }
    }
}
