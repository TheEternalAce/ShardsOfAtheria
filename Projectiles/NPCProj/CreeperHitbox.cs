using ShardsOfAtheria.NPCs.Misc;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj
{
    public class CreeperHitbox : ModProjectile
    {
        int LinkedNPCWhoAmI => (int)Projectile.ai[0];
        NPC LinkedNPC => Main.npc[LinkedNPCWhoAmI];

        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(1);
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;

            Projectile.timeLeft = 2;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.usesIDStaticNPCImmunity = true;

            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }


        public override void AI()
        {
            if (!LinkedNPC.active || LinkedNPC.type != ModContent.NPCType<Creeper>()) Projectile.Kill();
            Projectile.velocity = LinkedNPC.velocity;
            Projectile.timeLeft = 2;
        }
    }
}