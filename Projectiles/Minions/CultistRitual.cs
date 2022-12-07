using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Tools.Misc;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions
{
    public class CultistRitual : ModProjectile
    {
        public static Asset<Texture2D> glowmask;
        public int fireTimer;
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
            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
        }

        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 180;
            Projectile.timeLeft = 360000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];


            if (!CheckActive(owner))
                Projectile.Kill();

            Projectile.Center = owner.Center;
            if (Main.myPlayer == Projectile.owner)
            {
                if (Main.mouseLeft && !Main.LocalPlayer.mouseInterface && owner.HeldItem.type != ModContent.ItemType<SoulExtractingDagger>())
                {
                    Projectile.ai[1]++;
                    if (Projectile.ai[1] == 1)
                    {
                        SoundEngine.PlaySound(SoundID.Item120.WithVolumeScale(0.5f));
                        if (owner.GetModPlayer<SlayerPlayer>().lunaticCircleFragments > 1)
                        {
                            float numberProjectiles = owner.GetModPlayer<SlayerPlayer>().lunaticCircleFragments;
                            float rotation = MathHelper.ToRadians(20);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = Vector2.Normalize(Main.MouseWorld - Projectile.Center).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, perturbedSpeed * 16, ModContent.ProjectileType<IceFragment>(), 60, 1, owner.whoAmI);
                            }
                        }
                        else Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 16, ModContent.ProjectileType<IceFragment>(), 90, 1, owner.whoAmI);
                    }
                    if (Projectile.ai[1] >= 60)
                        Projectile.ai[1] = 0;
                }
                else if (Projectile.ai[1] > 0)
                {
                    Projectile.ai[1]++;
                    if (Projectile.ai[1] >= 60)
                        Projectile.ai[1] = 0;
                }
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(ModContent.ItemType<LunaticSoulCrystal>()))
                return false;
            else Projectile.timeLeft = 2;
            return true;
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