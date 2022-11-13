using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Pets
{
    public class CreeperPetBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.vanityPet[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<SlayerPlayer>().creeperPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<PetCreeper>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Vector2 toOwner = Vector2.Normalize(player.Center + new Vector2(5, 0));
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center - new Vector2(5, 0), toOwner * 5f, ModContent.ProjectileType<PetCreeper>(), 0, 0f, player.whoAmI);
            }
        }
    }
}
