using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
    public class AmmoBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ammo Bag");
            Tooltip.SetDefault("Gives a stack of a random ammo");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 10;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            var ammoChooser = new WeightedRandom<int>();
            var source = player.GetSource_OpenItem(Type);
            //Arrow
            ammoChooser.Add(ItemID.BoneArrow);
            ammoChooser.Add(ItemID.ChlorophyteArrow);
            ammoChooser.Add(ItemID.CursedArrow);
            ammoChooser.Add(ItemID.FlamingArrow);
            ammoChooser.Add(ItemID.FrostburnArrow);
            ammoChooser.Add(ItemID.HellfireArrow);
            ammoChooser.Add(ItemID.HolyArrow);
            ammoChooser.Add(ItemID.IchorArrow);
            ammoChooser.Add(ItemID.JestersArrow);
            ammoChooser.Add(ItemID.MoonlordArrow);
            ammoChooser.Add(ItemID.UnholyArrow);
            ammoChooser.Add(ItemID.VenomArrow);
            ammoChooser.Add(ItemID.WoodenArrow);
            //Bullet
            ammoChooser.Add(ItemID.MusketBall);
            ammoChooser.Add(ItemID.MeteorShot);
            ammoChooser.Add(ItemID.SilverBullet);
            ammoChooser.Add(ItemID.CrystalBullet);
            ammoChooser.Add(ItemID.CursedBullet);
            ammoChooser.Add(ItemID.ChlorophyteBullet);
            ammoChooser.Add(ItemID.HighVelocityBullet);
            ammoChooser.Add(ItemID.IchorBullet);
            ammoChooser.Add(ItemID.VenomBullet);
            ammoChooser.Add(ItemID.PartyBullet);
            ammoChooser.Add(ItemID.NanoBullet);
            ammoChooser.Add(ItemID.ExplodingBullet);
            ammoChooser.Add(ItemID.GoldenBullet);
            ammoChooser.Add(ItemID.MoonlordBullet);
            ammoChooser.Add(ItemID.TungstenBullet);
            //Rocket
            ammoChooser.Add(ItemID.RocketI);
            ammoChooser.Add(ItemID.RocketII);
            ammoChooser.Add(ItemID.RocketIII);
            ammoChooser.Add(ItemID.RocketIV);
            ammoChooser.Add(ItemID.ClusterRocketI);
            ammoChooser.Add(ItemID.ClusterRocketII);
            ammoChooser.Add(ItemID.DryRocket);
            ammoChooser.Add(ItemID.WetRocket);
            ammoChooser.Add(ItemID.LavaRocket);
            ammoChooser.Add(ItemID.HoneyRocket);
            ammoChooser.Add(ItemID.MiniNukeI);
            ammoChooser.Add(ItemID.MiniNukeII);
            //Dart
            ammoChooser.Add(ItemID.PoisonDart);
            ammoChooser.Add(ItemID.CrystalDart);
            ammoChooser.Add(ItemID.CursedDart);
            ammoChooser.Add(ItemID.IchorDart);
            //Other
            ammoChooser.Add(ItemID.FallenStar);
            ammoChooser.Add(ItemID.Gel);
            ammoChooser.Add(ItemID.SandBlock);
            ammoChooser.Add(ItemID.CrimsandBlock);
            ammoChooser.Add(ItemID.EbonsandBlock);
            ammoChooser.Add(ItemID.PearlsandBlock);
            ammoChooser.Add(ItemID.Seed);
            ammoChooser.Add(ItemID.Bone);
            ammoChooser.Add(ItemID.StyngerBolt);
            ammoChooser.Add(ItemID.CandyCorn);
            ammoChooser.Add(ItemID.ExplosiveJackOLantern);
            ammoChooser.Add(ItemID.Stake);
            ammoChooser.Add(ItemID.Flare);
            ammoChooser.Add(ItemID.BlueFlare);
            ammoChooser.Add(ItemID.Snowball);
            ammoChooser.Add(ItemID.Nail);

            Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 999);
        }
    }
}