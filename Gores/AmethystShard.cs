using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Gores
{
    public class AmethystShard : ModGore
    {
        public override bool Update(Gore gore)
        {
            if (gore.timeLeft >= 600)
            {
                gore.timeLeft = 0;
            }
            return base.Update(gore);
        }
    }
}
