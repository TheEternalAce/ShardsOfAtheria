using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.VergilFlamethrower
{
    public class ApproachingStorm : ModProjectile
    {
        public static Asset<Texture2D> glowmask;

        int fireTimer = 0;
        int fireTimerMax = 4;

        int consumeAmmoTimer = 0;
        int consumeAmmoTimerMax = 20;

        public override void Load()
        {
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true; //aiStyle 20 assigns heldProj
            Projectile.AddElec();
        }

        public override void AI()
        {
            ModifiedDrillAI();
            Player player = Main.player[Projectile.owner];
            int projToShoot = ProjectileID.Flames;
            float flameSpeed = 8f;
            bool canShoot = player.channel && player.HasAmmo(player.inventory[player.selectedItem]) && !player.noItems && !player.CCed && ++fireTimer == fireTimerMax;
            bool consumeAmmo = ++consumeAmmoTimer == consumeAmmoTimerMax;
            player.heldProj = Projectile.whoAmI;
            if (canShoot)
            {

                player.PickAmmo(player.inventory[player.selectedItem], out _, out _, out int Damage, out float KnockBack, out var usedAmmoItemId);
                Item gel = player.inventory[player.FindItem(usedAmmoItemId)];
                if (consumeAmmo)
                {
                    consumeAmmoTimer = 0;
                    if (gel.consumable && consumeAmmo)
                    {
                        gel.stack--;
                    }
                }
                IEntitySource source = player.GetSource_ItemUse_WithPotentialAmmo(player.inventory[player.selectedItem], usedAmmoItemId);
                Vector2 velocity = Projectile.velocity;
                velocity.Normalize();

                //if (player.InfernalPlayer().nozzleEquipped)
                //{
                //    flameSpeed *= 1.3f;
                //}
                //if (player.InfernalPlayer().metalBacktankEquipped > BacktankTiers.None && player.InfernalPlayer().nozzleEquipped)
                //{
                //    velocity *= flameSpeed * 1.2f;
                //    int numProjectiles = 2;
                //    float rotation = MathHelper.ToRadians(5);
                //    for (int i = 0; i < numProjectiles; i++)
                //    {
                //        Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                //        Projectile.NewProjectile(source, Projectile.Center, perturbedSpeed, projToShoot, Damage,
                //            KnockBack, player.whoAmI);
                //    }
                //}
                //else
                {
                    Projectile.NewProjectileDirect(source, Projectile.Center, velocity * flameSpeed, projToShoot,
                        Damage, KnockBack, player.whoAmI);
                }

                Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float ceilingLimit = target.Y;
                if (ceilingLimit > player.Center.Y - 200f)
                {
                    ceilingLimit = player.Center.Y - 200f;
                }
                Vector2 position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= 16f;
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, ModContent.ProjectileType<AreusShardProjRanged>(), Damage,
                    KnockBack, player.whoAmI, 0f, ceilingLimit);
                fireTimer = 0;
            }
        }

        void ModifiedDrillAI()
        {
            Projectile.timeLeft = 60;
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(in SoundID.Item34, Projectile.position);
                Projectile.soundDelay = 20;
            }
            Vector2 vector13 = Main.player[Projectile.owner].RotatedRelativePoint(Main.player[Projectile.owner].MountedCenter);
            if (Main.myPlayer == Projectile.owner)
            {
                if (Main.player[Projectile.owner].channel)
                {
                    float num127 = Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].shootSpeed * Projectile.scale;
                    Vector2 vector14 = vector13;
                    float num128 = Main.mouseX + Main.screenPosition.X - vector14.X;
                    float num130 = Main.mouseY + Main.screenPosition.Y - vector14.Y;
                    if (Main.player[Projectile.owner].gravDir == -1f)
                    {
                        num130 = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector14.Y;
                    }
                    float num131 = (float)Math.Sqrt(num128 * num128 + num130 * num130);
                    num131 = (float)Math.Sqrt(num128 * num128 + num130 * num130);
                    num131 = num127 / num131;
                    num128 *= num131;
                    num130 *= num131;
                    if (num128 != Projectile.velocity.X || num130 != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity.X = num128;
                    Projectile.velocity.Y = num130;
                }
                else
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.velocity.X > 0f)
            {
                Main.player[Projectile.owner].ChangeDir(1);
            }
            else if (Projectile.velocity.X < 0f)
            {
                Main.player[Projectile.owner].ChangeDir(-1);
            }
            Projectile.spriteDirection = Projectile.direction;
            Main.player[Projectile.owner].ChangeDir(Projectile.direction);
            Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
            Main.player[Projectile.owner].SetDummyItemTime(2);
            Projectile.position.X = vector13.X - Projectile.width / 2;
            Projectile.position.Y = vector13.Y - Projectile.height / 2;
            Projectile.rotation = (float)(Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.5700000524520874);
            if (Main.player[Projectile.owner].direction == 1)
            {
                Main.player[Projectile.owner].itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
            }
            else
            {
                Main.player[Projectile.owner].itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Projectile.hide = true;
            overPlayers.Add(index);
        }

        public override void PostDraw(Color lightColor)
        {
            //TODO Generic glowmask draw, maybe generalize method
            Player player = Main.player[Projectile.owner];

            int offsetY = 0;
            int offsetX = 0;
            Texture2D glowmaskTexture = glowmask.Value;
            float originX = (glowmaskTexture.Width - Projectile.width) * 0.5f + Projectile.width * 0.5f;
            ProjectileLoader.DrawOffset(Projectile, ref offsetX, ref offsetY, ref originX);

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (Projectile.ownerHitCheck && player.gravDir == -1f)
            {
                if (player.direction == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                else if (player.direction == -1)
                {
                    spriteEffects = SpriteEffects.None;
                }
            }

            Vector2 drawPos = new Vector2(Projectile.position.X - Main.screenPosition.X + originX + offsetX, Projectile.position.Y - Main.screenPosition.Y + Projectile.height / 2 + Projectile.gfxOffY);
            Rectangle sourceRect = glowmaskTexture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Color glowColor = new Color(255, 255, 255, 255) * 0.7f * Projectile.Opacity;
            Vector2 drawOrigin = new Vector2(originX, Projectile.height / 2 + offsetY);
            Main.EntitySpriteDraw(glowmaskTexture, drawPos, sourceRect, glowColor, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
        }
    }
}