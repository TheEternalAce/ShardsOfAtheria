using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Dusts
{
    public class PlagueDust : ModDust
    {
        public override bool Update(Dust dust)
        {
            if (!dust.noLight)
            {
                Lighting.AddLight(dust.position, TorchID.Green);
            }
            return base.Update(dust);
        }
    }
}
