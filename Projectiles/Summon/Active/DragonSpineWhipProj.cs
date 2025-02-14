﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Active
{
    public class DragonSpineWhipProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // This makes the projectile use whip collision detection and allows flasks to be applied to it.
            ProjectileID.Sets.IsAWhip[Type] = true;
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            Projectile.DefaultToWhip();

            // use these to change from the vanilla defaults
            // Projectile.WhipSettings.Segments = 20;
            Projectile.WhipSettings.RangeMultiplier = 1.75f;
        }

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.75f);
            target.AddBuff(ModContent.BuffType<LoomingEntropy>(), 240);
            var player = Main.player[Projectile.owner];
            player.MinionAttackTargetNPC = target.whoAmI;

            if (Projectile.ai[1] == 0)
            {
                int fragments = 4 + Main.rand.Next(0, 7);
                for (int i = 0; i < fragments; i++)
                {
                    var vector = Vector2.One.RotatedByRandom(MathHelper.TwoPi);
                    vector *= 5f * Main.rand.NextFloat(0.8f, 1.1f);
                    Projectile.NewProjectile(player.GetSource_OnHit(target), target.Center, vector,
                        ModContent.ProjectileType<DragonBone>(), Projectile.damage / 5, 0, player.whoAmI);
                }
                Projectile.ai[1] = 1;
            }
        }

        // This method draws a line between all points of the whip, in case there's empty space between the sprites.
        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = new Color(90, 10, 120);
                Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                pos += diff;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            //Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                // These two values are set to suit this projectile's sprite, but won't necessarily work for your own.
                // You can change them if they don't!
                Rectangle frame = new(0, 0, 10, 26);
                Vector2 origin = new(5, 8);
                float scale = 1;

                // These statements determine what part of the spritesheet to draw for the current segment.
                // They can also be changed to suit your sprite.
                if (i == list.Count - 2)
                {
                    frame.Y = 74;
                    frame.Height = 18;

                    // For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                else if (i > 10)
                {
                    frame.Y = 58;
                    frame.Height = 16;
                }
                else if (i > 5)
                {
                    frame.Y = 42;
                    frame.Height = 16;
                }
                else if (i > 0)
                {
                    frame.Y = 26;
                    frame.Height = 16;
                }

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }
    }
}
