using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
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

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 10);
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
            ammoChooser.Add(ItemID.WoodenArrow);
            ammoChooser.Add(ItemID.FlamingArrow);
            ammoChooser.Add(ItemID.FrostburnArrow);
            ammoChooser.Add(ItemID.BoneArrow);
            ammoChooser.Add(ItemID.JestersArrow);
            ammoChooser.Add(ItemID.UnholyArrow);

            if (NPC.downedBoss3)
            {
                ammoChooser.Add(ItemID.HellfireArrow);
            }

            if (Main.hardMode)
            {
                ammoChooser.Add(ItemID.ChlorophyteArrow);
                ammoChooser.Add(ItemID.CursedArrow);
                ammoChooser.Add(ItemID.HolyArrow);
                ammoChooser.Add(ItemID.IchorArrow);
                ammoChooser.Add(ItemID.VenomArrow);
            }

            if (NPC.downedMoonlord)
            {
                ammoChooser.Add(ItemID.MoonlordArrow);
            }

            //Bullet
            ammoChooser.Add(ItemID.MusketBall);
            ammoChooser.Add(ItemID.SilverBullet);
            ammoChooser.Add(ItemID.TungstenBullet);

            if (NPC.downedBoss2)
            {
                ammoChooser.Add(ItemID.MeteorShot);
            }

            if (Main.hardMode)
            {
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
            }
            if (NPC.downedMoonlord)
            {
                ammoChooser.Add(ItemID.MoonlordBullet);
            }

            //Rocket
            if (NPC.downedPlantBoss)
            {
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
            }

            //Dart
            ammoChooser.Add(ItemID.PoisonDart);

            if (Main.hardMode)
            {
                ammoChooser.Add(ItemID.CrystalDart);
                ammoChooser.Add(ItemID.CursedDart);
                ammoChooser.Add(ItemID.IchorDart);
            }

            //Other
            ammoChooser.Add(ItemID.FallenStar);
            ammoChooser.Add(ItemID.Gel);
            ammoChooser.Add(ItemID.SandBlock);
            ammoChooser.Add(ItemID.CrimsandBlock);
            ammoChooser.Add(ItemID.EbonsandBlock);
            ammoChooser.Add(ItemID.Seed);
            ammoChooser.Add(ItemID.Flare);
            ammoChooser.Add(ItemID.BlueFlare);
            ammoChooser.Add(ItemID.Snowball);

            if (NPC.downedBoss3)
            {
                ammoChooser.Add(ItemID.Bone);
            }

            if (Main.hardMode)
            {
                ammoChooser.Add(ItemID.PearlsandBlock);
            }

            if (NPC.downedPlantBoss)
            {
                ammoChooser.Add(ItemID.StyngerBolt);
                ammoChooser.Add(ItemID.CandyCorn);
                ammoChooser.Add(ItemID.ExplosiveJackOLantern);
                ammoChooser.Add(ItemID.Stake);
                ammoChooser.Add(ItemID.Nail);
            }

            Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 999);
        }
    }
}