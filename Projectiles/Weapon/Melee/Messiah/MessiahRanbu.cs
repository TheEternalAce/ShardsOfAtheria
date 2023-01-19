using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using MMZeroElements;
using ShardsOfAtheria.Items.Weapons.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.Messiah
{
    public abstract class Ranbu : ModProjectile
    {
        public int num1;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileElements.Fire.Add(Type);
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.timeLeft = 30;
        }

        public override void AI()
        {
            UpdateVisuals();

            Player player = Main.player[Projectile.owner];
            RanbuPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<RanbuPlayer>();

            player.position = player.oldPosition;

            if (Main.myPlayer == Projectile.owner)
            {
                if (player.HeldItem.type == ModContent.ItemType<TheMessiah>())
                {
                    (player.HeldItem.ModItem as TheMessiah).charge = 0;
                }
            }
            Projectile.Center = player.Center;

            Projectile.rotation = 0;
            modPlayer.ranbu = true;
            if (++num1 >= 10)
            {
                Projectile.timeLeft--;
            }
            Projectile.netUpdate = true;
        }

        public void UpdateVisuals()
        {
            if (++Projectile.frameCounter == 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame == Main.projFrames[Type])
                {
                    Projectile.Kill();
                }
            }
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1);
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.velocity.X = 15f * player.direction;
                }
                Projectile.velocity.Y = 0;
                Projectile.ai[1] = 1;
            }
            return base.PreAI();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<StunLock>(), 10);
            target.AddBuff(BuffID.OnFire3, 600);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<StunLock>(), 10);
            target.AddBuff(BuffID.OnFire3, 600);
            base.OnHitPlayer(target, damage, crit);
        }
    }

    public class MessiahRanbu1 : Ranbu
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 88;
            Projectile.height = 50;
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<MessiahRanbu2>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
            }

            base.Kill(timeLeft);
        }
    }

    public class MessiahRanbu2 : Ranbu
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 94;
            Projectile.height = 58;
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
            {
                if (Projectile.ai[0] == 0)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<MessiahRanbu3>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
                if (Projectile.ai[0] == 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<MessiahRanbu4>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 1);
                }
            }
            base.Kill(timeLeft);
        }
    }

    public class MessiahRanbu3 : Ranbu
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 86;
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
            {
                if (Projectile.ai[0] == 0)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<MessiahRanbu2>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 1);
                }
                if (Projectile.ai[0] == 1)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<MessiahRanbu5>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
            }
            base.Kill(timeLeft);
        }
    }

    public class MessiahRanbu4 : Ranbu
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 138;
            Projectile.height = 98;
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, new Vector2(1, 0) * player.direction, ModContent.ProjectileType<MessiahRanbu3>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 1);

            base.Kill(timeLeft);
        }
    }

    public class MessiahRanbu5 : Ranbu
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 78;
            base.SetDefaults();
        }

        public override void AI()
        {
            UpdateVisuals();

            Player player = Main.player[Projectile.owner];
            RanbuPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<RanbuPlayer>();
            (player.HeldItem.ModItem as TheMessiah).charge = 0;

            Projectile.Center = player.Center + new Vector2(8 * player.direction, -5);

            Projectile.rotation = 0;
            modPlayer.ranbu = true;
            if (++num1 >= 10)
            {
                Projectile.timeLeft--;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                player.velocity = new Vector2(1 * player.direction, -2) * 4f;
            }
            Projectile.netUpdate = true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
        }
    }

    public class RanbuPlayer : ModPlayer
    {
        public bool ranbu;

        public override void ResetEffects()
        {
            ranbu = false;
        }

        public override void SetControls()
        {
            if (ranbu)
            {
                Player.controlUp = false;
                Player.controlDown = false;
                Player.controlLeft = false;
                Player.controlRight = false;
                Player.controlJump = false;
            }
        }
    }
}