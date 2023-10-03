using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Mounts
{
    // This mount is a car with wheels which behaves simillarly to the unicorn mount. The car has 3 baloons attached to the back.
    public class LifeCycle : ModMount
    {
        public override void SetStaticDefaults()
        {
            // Movement
            MountData.jumpHeight = 50;
            MountData.acceleration = 0.19f;
            MountData.jumpSpeed = 12f;
            MountData.blockExtraJumps = false;
            MountData.constantJump = true;
            MountData.heightBoost = 20;
            MountData.fallDamage = 0f;
            MountData.runSpeed = 11f;
            MountData.dashSpeed = 8f;
            MountData.flightTimeMax = 0;

            // Misc
            MountData.fatigueMax = 0;
            MountData.buff = ModContent.BuffType<LifeCycleBuff>();

            // Effects
            MountData.spawnDust = DustID.Silver;

            // Frame data and player offsets
            MountData.totalFrames = 4;
            MountData.playerYOffsets = Enumerable.Repeat(20, MountData.totalFrames).ToArray();
            MountData.xOffset = 10;
            MountData.yOffset = -10;
            MountData.playerHeadOffset = 22;
            MountData.bodyFrame = 3;
            // Standing
            MountData.standingFrameCount = 4;
            MountData.standingFrameDelay = 6;
            MountData.standingFrameStart = 0;
            // Running
            MountData.runningFrameCount = 4;
            MountData.runningFrameDelay = 12;
            MountData.runningFrameStart = 0;
            // Flying
            MountData.flyingFrameCount = 0;
            MountData.flyingFrameDelay = 0;
            MountData.flyingFrameStart = 0;
            // In-air
            MountData.inAirFrameCount = 4;
            MountData.inAirFrameDelay = 12;
            MountData.inAirFrameStart = 0;
            // Idle
            MountData.idleFrameCount = 4;
            MountData.idleFrameDelay = 6;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = true;
            // Swim
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;

            if (!Main.dedServ)
            {
                MountData.textureWidth = MountData.backTexture.Width() + 20;
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }

        public override void UpdateEffects(Player player)
        {
            // This code spawns some dust if we are moving fast enough.
            if (Math.Abs(player.velocity.X) > 4f)
            {
                Rectangle rect = player.getRect();

                Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, DustID.Blood);
            }
        }

        public override void SetMount(Player player, ref bool skipDust)
        {
            if (!Main.dedServ)
            {
                for (int i = 0; i < 16; i++)
                {
                    Dust.NewDustPerfect(player.Center + new Vector2(80, 0).RotatedBy(i * Math.PI * 2 / 16f), MountData.spawnDust);
                }

                skipDust = true;
            }
        }

        public override void Dismount(Player player, ref bool skipDust)
        {
            if (!Main.dedServ)
            {
                for (int i = 0; i < 16; i++)
                {
                    Dust.NewDustPerfect(player.Center + new Vector2(80, 0).RotatedBy(i * Math.PI * 2 / 16f), MountData.spawnDust);
                }

                skipDust = true;
            }
        }
    }
}
