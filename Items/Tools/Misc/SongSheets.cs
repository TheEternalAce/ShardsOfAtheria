using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public abstract class SongSheets : ModItem
    {
        public override string Texture => "ShardsOfAtheria/Items/PlaceholderSprite";

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.consumable = true;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.rare = ItemRarityID.Blue;
            Item.value = 525;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().songsLearned++;
            return true;
        }
    }

    public class SongOfTimeSheet : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().timeSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }

    public class SongOfHealing : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().healingSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }

    public class SongOfWeather : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().weatherSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }

    public class SongOfHealingAlt : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().healingSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }

    public class SongOfUnhealing : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().unhealingSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }

    public class SongOfDoubleTime : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().doubleTimeSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }

    public class SongOfNature : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().natureSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }

    public class SongOfSoaring : SongSheets
    {
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OcarinaPlayer>().soaringSongLearned = true;
            base.UseItem(player);
            return true;
        }
    }
}