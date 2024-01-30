using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class HardlightBladeHitbox : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementElec();
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 70;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3;
        }

        int npcWhoAmI = -1;
        public override void AI()
        {
            // Find the first active and living instance of Nova.
            if (npcWhoAmI == -1)
            {
                foreach (var npc in Main.npc)
                {
                    if (npc.active)
                    {
                        if (npc.type == ModContent.NPCType<NovaStellar>())
                        {
                            npcWhoAmI = npc.whoAmI;
                            break;
                        }
                    }
                }
            }
            else
            {
                NPC npc = Main.npc[npcWhoAmI];
                Vector2 offset = new(21 * npc.spriteDirection, 7);
                Projectile.Center = npc.Center + offset;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 300);
        }
    }
}