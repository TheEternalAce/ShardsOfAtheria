using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class Genesis_Whip : ModProjectile
    {
        public int iceSickle = 3;

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (upgrades < 5 && upgrades >= 3)
                    target.AddBuff(BuffID.OnFire, 600);
                else if (upgrades == 5)
                {
                    target.AddBuff(BuffID.Frostburn, 600);
                    //Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<LightningBoltFriendly>(), Projectile.damage,
                    //Projectile.knockBack, player.whoAmI, 0, 1);
                    //proj.DamageType = DamageClass.Melee;
                    Projectile.Explode(target.Center);
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
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.aiStyle = ProjAIStyleID.HeldProjectile;
            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;
            player.itemAnimation = 10;
            player.itemTime = 10;

            // Behavior
            if (Main.netMode != NetmodeID.Server && Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item116, Projectile.Center);
            }
            if (Projectile.localAI[1] > 0f)
            {
                Projectile.localAI[1] -= 1f;
            }
            Projectile.alpha -= 42;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = Projectile.velocity.ToRotation();
            }
            float num42 = ((Projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : (-1));
            if (Projectile.ai[1] <= 0f)
            {
                num42 *= -1f;
            }
            Vector2 vector7 = (num42 * (Projectile.ai[0] / 30f * ((float)Math.PI * 2f) - (float)Math.PI / 2f)).ToRotationVector2();
            vector7.Y *= (float)Math.Sin(Projectile.ai[1]);
            if (Projectile.ai[1] <= 0f)
            {
                vector7.Y *= -1f;
            }
            vector7 = vector7.RotatedBy(Projectile.localAI[0]);
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] < 30f)
            {
                Projectile.velocity += 48f * vector7;
            }
            else
            {
                Projectile.Kill();
            }
            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (--iceSickle == 0 && upgrades == 5)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position + Projectile.velocity, Vector2.Zero, ProjectileID.IceSickle, Projectile.damage, Projectile.knockBack, player.whoAmI);
                    iceSickle = 3;
                }

                // Dust stuff
                if (upgrades >= 3)
                {
                    if (Projectile.alpha == 0)
                    {
                        for (int num72 = 0; num72 < 2; num72++)
                        {
                            Dust obj4 = Main.dust[Dust.NewDust(player.Center, Projectile.width, Projectile.height, upgrades < 5 ? DustID.Torch : DustID.Frost,
                                0f, 0f, 100, default, upgrades < 5 ? 2f : .5f)];
                            obj4.noGravity = true;
                            obj4.velocity *= 2f;
                            obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                            obj4.fadeIn = 1.5f;
                        }
                        for (int num74 = 0; (float)num74 < 18; num74++)
                        {
                            if (Main.rand.NextBool(4))
                            {
                                Vector2 position = Projectile.position + Projectile.velocity;
                                Dust obj5 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default,
                                    upgrades < 5 ? 2f : .5f)];
                                obj5.noGravity = true;
                                obj5.fadeIn = 0.5f;
                                obj5.velocity += Projectile.localAI[0].ToRotationVector2();
                                obj5.noLight = true;
                            }
                        }
                    }
                }
            }

            // Centering
            Vector2 value21 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                value21.X = (float)player.bodyFrame.Width - value21.X;
            }
            if (player.gravDir != 1f)
            {
                value21.Y = (float)player.bodyFrame.Height - value21.Y;
            }
            value21 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter - new Vector2(20f, 42f) / 2f + value21, reverseRotation: false, addGfxOffY: false) - Projectile.velocity;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint4 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity + Projectile.velocity.SafeNormalize(Vector2.Zero) * 48f, 16f * Projectile.scale, ref collisionPoint4))
            {
                return true;
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D value230 = TextureAssets.Projectile[Projectile.type].Value;
            Color alpha20 = Projectile.GetAlpha(Color.White);
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            float num257 = Projectile.velocity.Length() + 16f;
            bool flag30 = num257 < 100f;
            Vector2 value231 = Vector2.Normalize(Projectile.velocity);
            Rectangle rectangle14 = new Rectangle(0, 2, value230.Width, 40);
            Vector2 value232 = new Vector2(0f, Main.player[Projectile.owner].gfxOffY);
            float rotation25 = Projectile.rotation + (float)Math.PI;
            Main.EntitySpriteDraw(value230, Projectile.Center.Floor() - Main.screenPosition + value232, rectangle14, alpha20, rotation25, rectangle14.Size() / 2f - Vector2.UnitY * 4f, Projectile.scale, SpriteEffects.None, 0);
            num257 -= 40f * Projectile.scale;
            Vector2 vector41 = Projectile.Center.Floor();
            vector41 += value231 * Projectile.scale * 24f;
            rectangle14 = new Rectangle(0, 68, value230.Width, 18);
            if (num257 > 0f)
            {
                float num258 = 0f;
                while (num258 + 1f < num257)
                {
                    if (num257 - num258 < (float)rectangle14.Height)
                    {
                        rectangle14.Height = (int)(num257 - num258);
                    }
                    Main.EntitySpriteDraw(value230, vector41 - Main.screenPosition + value232, rectangle14, alpha20, rotation25, new Vector2(rectangle14.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0);
                    num258 += (float)rectangle14.Height * Projectile.scale;
                    vector41 += value231 * rectangle14.Height * Projectile.scale;
                }
            }
            Vector2 value233 = vector41;
            vector41 = Projectile.Center.Floor();
            vector41 += value231 * Projectile.scale * 24f;
            rectangle14 = new Rectangle(0, 46, value230.Width, 18);
            int num259 = 18;
            if (flag30)
            {
                num259 = 9;
            }
            float num260 = num257;
            if (num257 > 0f)
            {
                float num261 = 0f;
                float num262 = num260 / (float)num259;
                num261 += num262 * 0.25f;
                vector41 += value231 * num262 * 0.25f;
                for (int num263 = 0; num263 < num259; num263++)
                {
                    float num264 = num262;
                    if (num263 == 0)
                    {
                        num264 *= 0.75f;
                    }
                    Main.EntitySpriteDraw(value230, vector41 - Main.screenPosition + value232, rectangle14, alpha20, rotation25, new Vector2(rectangle14.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0);
                    num261 += num264;
                    vector41 += value231 * num264;
                }
            }
            Main.EntitySpriteDraw(sourceRectangle: new Rectangle(0, 90, value230.Width, 48), texture: value230, position: value233 - Main.screenPosition + value232, color: alpha20, rotation: rotation25, origin: value230.Frame().Top(), scale: Projectile.scale, effects: SpriteEffects.None, worthless: 0);
            return false;
        }
    }
}