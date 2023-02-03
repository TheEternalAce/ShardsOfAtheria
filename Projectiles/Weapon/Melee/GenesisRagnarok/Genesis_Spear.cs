using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ReLogic.Content;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class Genesis_Spear : ModProjectile
    {
        // Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from this one.
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 240;
        public static Asset<Texture2D> glowmask;

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (upgrades < 5 && upgrades >= 3)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }
                else if (upgrades == 5)
                {
                    target.AddBuff(BuffID.Frostburn, 600);
                    //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<IceExplosion>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                    Projectile.Explode(Projectile.Center);
                }
            }
        }

        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
            ProjectileElements.Fire.Add(Type);
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            Projectile.ownerHitCheck = false;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

            float halfDuration = duration * 0.5f;
            float progress;

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.ToRadians(135f);
            }

            // Avoid spawning dusts on dedicated servers
            if (!Main.dedServ)
            {
                if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                {
                    if (Projectile.ai[1] == 0 && Main.myPlayer == player.whoAmI && upgrades == 5)
                    {
                        Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 5,
                            ModContent.ProjectileType<LightningBoltFriendly>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                        proj.DamageType = DamageClass.Melee;
                        Projectile.ai[1] = 1;
                    }

                    if (upgrades >= 3)
                    {
                        for (int num72 = 0; num72 < 2; num72++)
                        {
                            Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default,
                                upgrades < 5 ? 2f : .5f)];
                            obj4.noGravity = true;
                            obj4.velocity *= 2f;
                            obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                            obj4.fadeIn = 1.5f;
                        }
                    }
                }
            }
            Projectile.netUpdate = true;

            return false; // Don't execute vanilla AI.
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
