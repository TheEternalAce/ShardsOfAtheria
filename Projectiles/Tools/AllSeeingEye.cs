using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class AllSeeingEye : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.frame = 0;
            Player owner = Main.player[Projectile.owner];


            if (!CheckActive(owner))
            {
                return;
            }

            if (Main.myPlayer == owner.whoAmI)
            {
                Projectile.netUpdate = true;
                Projectile.Center = Main.MouseWorld;
                Lighting.AddLight(Main.MouseWorld, TorchID.White);

                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy() && Projectile.Hitbox.Intersects(npc.getRect()))
                    {
                        npc.AddBuff(ModContent.BuffType<Marked>(), 600);
                    }
                }
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.GetModPlayer<SlayerPlayer>().soulCrystals.Contains(ModContent.ItemType<EyeSoulCrystal>()))
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }
    }
}
