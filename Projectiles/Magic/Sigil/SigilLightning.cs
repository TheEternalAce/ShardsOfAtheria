using ShardsOfAtheria.Utilities;
using Terraria;

namespace ShardsOfAtheria.Projectiles.Magic.Sigil
{
    public class SigilLightning : LightningBoltFriendly
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void AI()
        {
            base.AI();

            DustTimer += 2;

            var player = Projectile.GetPlayerOwner();
            var shards = player.Shards();
            if (shards.Overdrive)
            {
                int npcWhoAmI = Projectile.FindTargetWithLineOfSight();
                if (npcWhoAmI >= 0)
                {
                    var npc = Main.npc[npcWhoAmI];
                    Projectile.Track(npc);

                    if (Projectile.Distance(npc.Center) < 100)
                    {
                        Projectile.velocity = Projectile.DirectionTo(npc.Center);
                    }
                }
            }
        }
    }
}
