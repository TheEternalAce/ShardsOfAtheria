using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Players
{
    public partial class AreusArmorPlayer
    {
        public bool soldierSet;

        private void SoldierActive_Melee()
        {
            var center = Player.Center;
            float speed = 0f;
            float radius = 75f;
            float rotation = MathHelper.ToRadians(360 / 2);
            for (int i = 0; i < 2; i++)
            {
                Vector2 position = center + new Vector2(1, 0).RotatedBy(rotation * i) * radius;
                Vector2 velocity = Vector2.Normalize(center - position) * speed;
                Projectile.NewProjectileDirect(Player.GetSource_FromThis(), position,
                    velocity, ModContent.ProjectileType<ElectricBarrier>(), 0, 0f,
                    Player.whoAmI, i);
            }
            SoundEngine.PlaySound(SoundID.NPCDeath45, Player.Center);
        }
        private void SoldierActive_Ranged()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath45, Player.Center);
            Player.AddBuff<ElectricVeil>(600);
        }
        //private void SoldierActive_Magic()
        //{
        //}
        //private void SoldierActive_Summon()
        //{

        //}

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (MageSet && soldierSet)
            {
                if (Main.rand.NextBool(8))
                {
                    Vector2 teleport = Main.MouseWorld - Player.Center;
                    teleport.Normalize();
                    teleport *= 200;
                    teleport += Player.Center;
                    if (ShardsHelpers.CheckTileCollision(teleport, Player.Hitbox))
                    {
                        Player.Teleport(teleport, 1);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, teleport.X, teleport.Y, 1);
                    }
                    Player.AddBuff<ElectricBlink>(480);
                }
                return true;
            }
            return base.FreeDodge(info);
        }
    }
}
