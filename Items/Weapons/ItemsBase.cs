using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    /// <summary>
    /// Use useStyle = 15 to use the light animation :)
    /// </summary>
    public abstract class LobCorpLight : ModItem
    {
        public sealed override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (Item.useStyle == 15)
            {
                float rotation = ItemRotation(player);
                PseudoUseStyleSwing(player, heldItemFrame, rotation);
            }
            UseStyleAlt(player, heldItemFrame);
            base.UseStyle(player, heldItemFrame);
        }

        /// <summary>
        /// Just to use as a base for other weapons for a smoother time, Use Degrees for rotation just to maintain my SP
        /// </summary>
        /// <param name="player"></param>
        /// <param name="heldItemFrame"></param>
        /// <param name="rotation"></param>
        public static void PseudoUseStyleSwing(Player player, Rectangle heldItemFrame, float rotation)
        {
            player.itemLocation = LobItemLocation(player, heldItemFrame, rotation - 90);

            player.itemRotation = MathHelper.ToRadians(rotation - 45) * player.direction;
        }

        /// <summary>
        /// No need for reversed rotation, if using reversed rotation, change Direction
        /// </summary>
        /// <param name="player"></param>
        /// <param name="heldItemFrame"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static Vector2 LobItemLocation(Player player, Rectangle heldItemFrame, float rotation, int direction = 1, int Xoffset = 0)
        {
            rotation = Math.Clamp(rotation, -180, 180);
            rotation = MathHelper.ToRadians(rotation);
            float x = (float)Math.Cos(rotation) * direction;
            //Main.NewText(x);
            float y = (float)Math.Sin(rotation);

            Vector2 location = new Vector2();
            if (y < 0)
            {
                if (x < 0)
                {
                    float num33 = 6f;
                    if (heldItemFrame.Width > 32)
                    {
                        num33 = 14f;
                    }
                    if (heldItemFrame.Width >= 48)
                    {
                        num33 = 18f;
                    }
                    if (heldItemFrame.Width >= 52)
                    {
                        num33 = 24f;
                    }
                    if (heldItemFrame.Width >= 64)
                    {
                        num33 = 28f;
                    }
                    if (heldItemFrame.Width >= 92)
                    {
                        num33 = 38f;
                    }
                    location.X = player.position.X + player.width * 0.5f - (heldItemFrame.Width * 0.5f - (num33 + Xoffset)) * player.direction;
                    num33 = 10f;
                    if (heldItemFrame.Height > 32)
                    {
                        num33 = 10f;
                    }
                    if (heldItemFrame.Height > 52)
                    {
                        num33 = 12f;
                    }
                    if (heldItemFrame.Height > 64)
                    {
                        num33 = 14f;
                    }
                    location.Y = player.position.Y + num33 + player.HeightOffsetHitboxCenter;
                }
                else
                {
                    float num32 = 10f;
                    if (heldItemFrame.Width > 32)
                    {
                        num32 = 18f;
                    }
                    if (heldItemFrame.Width >= 52)
                    {
                        num32 = 24f;
                    }
                    if (heldItemFrame.Width >= 64)
                    {
                        num32 = 28f;
                    }
                    if (heldItemFrame.Width >= 92)
                    {
                        num32 = 38f;
                    }
                    location.X = player.position.X + player.width * 0.5f + (heldItemFrame.Width * 0.5f - (num32 + Xoffset)) * player.direction;
                    num32 = 10f;
                    if (heldItemFrame.Height > 32)
                    {
                        num32 = 8f;
                    }
                    if (heldItemFrame.Height > 52)
                    {
                        num32 = 12f;
                    }
                    if (heldItemFrame.Height > 64)
                    {
                        num32 = 14f;
                    }
                    location.Y = player.position.Y + num32 + player.HeightOffsetHitboxCenter;
                }
            }
            else
            {
                if (x > 0)
                {
                    float num31 = 14f;
                    if (heldItemFrame.Width > 32)
                    {
                        num31 = 18f;
                    }
                    if (heldItemFrame.Width >= 52)
                    {
                        num31 = 28f;
                    }
                    if (heldItemFrame.Width >= 64)
                    {
                        num31 = 32f;
                    }
                    if (heldItemFrame.Width >= 92)
                    {
                        num31 = 42f;
                    }
                    location.X = player.position.X + player.width * 0.5f + (heldItemFrame.Width * 0.5f - (num31 + Xoffset)) * player.direction;
                    location.Y = player.position.Y + 26f + player.HeightOffsetHitboxCenter;
                }
                else
                {
                    float num33 = 6f;
                    if (heldItemFrame.Width > 32)
                    {
                        num33 = 14f;
                    }
                    if (heldItemFrame.Width >= 48)
                    {
                        num33 = 18f;
                    }
                    if (heldItemFrame.Width >= 52)
                    {
                        num33 = 24f;
                    }
                    if (heldItemFrame.Width >= 64)
                    {
                        num33 = 28f;
                    }
                    if (heldItemFrame.Width >= 92)
                    {
                        num33 = 38f;
                    }
                    location.X = player.position.X + player.width * 0.5f - (heldItemFrame.Width * 0.5f - (num33 + Xoffset)) * player.direction;
                    location.Y = player.position.Y + 24f + player.HeightOffsetHitboxCenter;
                }
            }
            return location;
        }

        public virtual void UseStyleAlt(Player player, Rectangle heldItemFrame)
        {
        }

        public sealed override void UseItemFrame(Player player)
        {
            if (Item.useStyle == 15)
            {
                float rotation = ItemRotation(player);
                LobItemFrame(player, rotation - 90);
            }
            base.UseItemFrame(player);
        }

        /// <summary>
        /// [OLDER VERSION]Just to use as a base for other weapons for a smoother time, Use Degrees for rotation just to maintain my SP
        /// </summary>
        /// <param name="player"></param>
        /// <param name="rotation"></param>
        public static void PseudoUseItemFrame(Player player, float rotation)
        {
            if (player.direction == 1 && rotation < 0 ||
                    player.direction == -1 && rotation > 0)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 1;
            }
            else if (player.direction == 1 && rotation < 90 ||
                     player.direction == -1 && rotation > -90)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 2;
            }
            else if (player.direction == 1 && rotation < 180 ||
                     player.direction == -1 && rotation > -180)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 4;
            }
            else
            {
                player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, MathHelper.ToRadians(45 - (player.direction < 0 ? 90 : 0)));
            }
        }

        /// <summary>
        /// Base for other weapons to use, Use Degrees for rotation just to maintain my SP, if using reversed rotation, change Direction
        /// </summary>
        /// <param name="player"></param>
        /// <param name="rotation"></param>
        public static void LobItemFrame(Player player, float rotation, int direction = 1)
        {
            rotation = Math.Clamp(rotation, -180, 180);
            rotation = MathHelper.ToRadians(rotation);
            float x = (float)Math.Cos(rotation) * direction;
            //Main.NewText(x);
            float y = (float)Math.Sin(rotation);

            if (y < 0)
            {
                if (x < 0)
                {
                    player.bodyFrame.Y = player.bodyFrame.Height * 1;
                }
                else
                {
                    player.bodyFrame.Y = player.bodyFrame.Height * 2;
                }
            }
            else
            {
                if (x > 0)
                {
                    player.bodyFrame.Y = player.bodyFrame.Height * 4;
                }
                else
                {
                    player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, MathHelper.ToRadians(45 - (player.direction < 0 ? 90 : 0)));
                }
            }
        }

        public sealed override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            if (Item.useStyle == 15)
            {
                hitbox = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, 32, 32);
                if (!Main.dedServ)
                {
                    Rectangle hitboxSize = Item.GetDrawHitbox(Item.type, player);
                    hitbox = new Rectangle((int)player.itemLocation.X, (int)player.itemLocation.Y, hitboxSize.Width, hitboxSize.Height);
                }
                float adjustedItemScale = player.GetAdjustedItemScale(Item);
                hitbox.Width = (int)(hitbox.Width * adjustedItemScale);
                hitbox.Height = (int)(hitbox.Height * adjustedItemScale);
                if (player.direction == -1)
                {
                    hitbox.X -= hitbox.Width;
                }
                if (player.gravDir == 1f)
                {
                    hitbox.Y -= hitbox.Height;
                }

                float prog = 1f - player.itemAnimation / (float)player.itemAnimationMax;
                if (prog < .2f)
                {
                    if (player.direction == 1)
                    {
                        hitbox.X -= hitbox.Width * 1;
                    }
                    hitbox.Width *= 2;
                    hitbox.Y -= (int)((hitbox.Height * 1.4 - hitbox.Height) * player.gravDir);
                    hitbox.Height = (int)(hitbox.Height * 1.4);
                }
                else if (prog < .4f)
                {
                    if (player.direction == -1)
                    {
                        hitbox.X -= (int)(hitbox.Width * 1.4 - hitbox.Width);
                    }
                    hitbox.Width = (int)(hitbox.Width * 1.4);
                    hitbox.Y += (int)(hitbox.Height * 0.5 * player.gravDir);
                    hitbox.Height = (int)(hitbox.Height * 1.4);
                }
                else
                    noHitbox = true;
            }
            UseItemHitboxAlt(player, ref hitbox, ref noHitbox);
            base.UseItemHitbox(player, ref hitbox, ref noHitbox);
        }

        public virtual void UseItemHitboxAlt(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
        }

        public override bool? UseItem(Player player)
        {
            if (Item.useStyle == 15)
            {
                //This is a jank way of changing item's attack cooldown, thought it would better fit at onhit same as immune but I guess not since that ones before they change the immune time and attackCD
                ResetPlayerAttackCooldown(player);
            }
            return UseItemAlt(player);
        }

        /// <summary>
        /// Use when overriding UseItem since its there :(, shouldn't really be used... unless
        /// Put it on UseStyleAlt
        /// Im really angry they dont let you change this when you actually hit the fucking doods
        /// </summary>
        /// <param name="player"></param>
        public static void ResetPlayerAttackCooldown(Player player, double percent = 0.1)
        {
            int cooldown = Math.Max(1, (int)(player.itemAnimationMax * percent));
            if (player.attackCD > cooldown)
                player.attackCD = cooldown;
        }

        /// <summary>
        /// Changes IFrames, also changes AttackCooldown
        /// Put it on UseStyleAlt or HoldItem maybe...
        /// Jank, might cause problems
        /// Im really angry they dont let you change this when you actually hit the fucking doods
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        /// <param name=""></param>
        public static void ResetPlayerImmuneHit(Player player, ref int target, int immuneLimit, double percent = 0.1)
        {
            NPC npc = Main.npc[target];
            if (player.itemAnimation > immuneLimit)
            {
                player.SetMeleeHitCooldown(target, 0);// player.itemAnimation - immuneLimit);
                npc.immune[player.whoAmI] = player.itemAnimation - immuneLimit;
            }

            target = -1;
            ResetPlayerAttackCooldown(player, percent);
        }

        /// <summary>
        /// ResetPlayerMeleeCooldown, but as a number
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        /// <param name="immuneLimit"></param>
        /// <param name="percent"></param>
        public static void SetPlayerMeleeCooldown(Player player, ref int target, int immuneTime, double percent = 0.1)
        {
            NPC npc = Main.npc[target];
            if (player.itemAnimation > immuneTime)
            {
                player.SetMeleeHitCooldown(target, 0);// player.itemAnimation - immuneLimit);
                npc.immune[player.whoAmI] = immuneTime;
            }

            target = -1;
            ResetPlayerAttackCooldown(player, percent);
        }

        public virtual bool? UseItemAlt(Player player)
        {
            return null;
        }

        /// <summary>
        /// Gives raw rotation in form of degrees, flip when nescessary 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static float ItemRotation(Player player)
        {
            float prog = 1f - player.itemAnimation / (float)player.itemAnimationMax;
            float rotation = 0;

            if (prog < 0.4f)
            {
                prog = prog / 0.4f;
                rotation = -60 + 200 * (float)Math.Sin(1.57f * prog);// * player.direction;
            }
            else if (prog < 0.5f)
            {
                rotation = 140;// * player.direction;
            }
            else
            {
                prog = (prog - 0.5f) / 0.5f;
                rotation = 140 - 45 * prog;// * player.direction;
            }
            return rotation;
        }

        public override void ModifyItemScale(Player player, ref float scale)
        {

            base.ModifyItemScale(player, ref scale);
        }
    }
}