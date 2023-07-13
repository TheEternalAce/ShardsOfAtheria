using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BreakerBladeMateria
{
    public abstract class Materia : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 50);
        }
    }
    public class FireMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Support";
    }
    public class AquaMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Support";
    }
    public class ElecMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Support";
    }
    public class WoodMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Support";
    }
    public class FiragaMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Magic";
    }
    public class BlizzagaMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Magic";
    }
    public class ThundagaMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Magic";
    }
    public class BiogaMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Magic";
    }
    public class IfritMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Summon";
    }
    public class ShivaMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Summon";
    }
    public class RamuhMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Summon";
    }
    public class CactuarMateria : Materia
    {
        public override string Texture => "ShardsOfAtheria/Items/BreakerBladeMateria/Summon";
    }
}