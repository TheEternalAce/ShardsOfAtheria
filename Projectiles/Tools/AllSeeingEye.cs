using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class AllSeeingEye : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("All Seeing Eye");
            Main.projFrames[Projectile.type] = 3;
        }

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
            float maxDetectRadius = 5f;
            Player owner = Main.player[Projectile.owner];


            if (!CheckActive(owner))
            {
                return;
            }

            if (Main.myPlayer == owner.whoAmI)
            {
                Projectile.Center = Main.MouseWorld;
                Lighting.AddLight(Main.MouseWorld, TorchID.White);
            }

            if (Main.hardMode)
            {
                if (owner.GetModPlayer<SlayerPlayer>().TwinSoul)
                {
                    maxDetectRadius = 200f;
                    Projectile.frame = 1;
                }
                if (owner.GetModPlayer<SlayerPlayer>().LordSoul)
                {
                    maxDetectRadius = 400f;
                    Projectile.frame = 2;
                }
            }
            // This code is required either way, used for finding a target
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.CanBeChasedBy())
                {
                    if (owner.GetModPlayer<SlayerPlayer>().TwinSoul || owner.GetModPlayer<SlayerPlayer>().LordSoul && Main.hardMode)
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool inRange = between < maxDetectRadius;

                        if (inRange)
                        {
                            if (owner.GetModPlayer<SynergyPlayer>().eyeTwinSynergy)
                            {
                                npc.AddBuff(ModContent.BuffType<MarkedII>(), 600);
                                if (++Projectile.ai[0] >= 60)
                                {
                                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Normalize(npc.Center - Projectile.Center) * 16, ProjectileID.MiniRetinaLaser, 60, 0, owner.whoAmI);
                                    Projectile.ai[0] = 0;
                                }
                            }
                            if (owner.GetModPlayer<SynergyPlayer>().eyeLordSynergy)
                            {
                                npc.AddBuff(ModContent.BuffType<MarkedIII>(), 600);
                                if (++Projectile.ai[0] >= 60)
                                {
                                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Normalize(npc.Center - Projectile.Center) * 16, ModContent.ProjectileType<PhantasmalEye>(), 60, 0, owner.whoAmI);
                                    Projectile.ai[0] = 0;
                                }
                            }
                        }
                    }
                    else if (Projectile.Hitbox.Intersects(npc.getRect()))
                        npc.AddBuff(ModContent.BuffType<Marked>(), 600);
                }
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.GetModPlayer<SlayerPlayer>().EyeSoul)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }
    }
}
