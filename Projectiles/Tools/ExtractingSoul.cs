using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class ExtractingSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Extracting Dagger");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;

            DrawOffsetX = -21;
            DrawOriginOffsetX = 15;
        }

        public override void AI()
        {
            Player owner = Main.LocalPlayer;
            Projectile.velocity = Vector2.Normalize(owner.Center - Projectile.Center) * 10;
            if (owner.direction == 1)
                Projectile.rotation = MathHelper.ToRadians(225);
            else Projectile.rotation = MathHelper.ToRadians(45);
            if (Projectile.getRect().Intersects(owner.getRect()))
            {
                SoundEngine.PlaySound(SoundID.Item4);
                Projectile.Kill();
                ExtractSoul();
                for (int i = 0; i < 20; i++)
                {
                    if (Main.rand.NextBool(3))
                        Dust.NewDustDirect(owner.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, Color.Purple, 1.2f);
                    Lighting.AddLight(owner.Center, TorchID.Purple);
                }
            }
        }

        public void ExtractSoul()
        {
            Player player = Main.LocalPlayer;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King)
            {
                player.GetModPlayer<SlayerPlayer>().KingSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<KingSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye)
            {
                player.GetModPlayer<SlayerPlayer>().EyeSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<EyeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain)
            {
                player.GetModPlayer<SlayerPlayer>().BrainSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<BrainSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater)
            {
                player.GetModPlayer<SlayerPlayer>().EaterSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<EaterSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie)
            {
                player.GetModPlayer<SlayerPlayer>().ValkyrieSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<ValkyrieSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee)
            {
                player.GetModPlayer<SlayerPlayer>().BeeSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<BeeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull)
            {
                player.GetModPlayer<SlayerPlayer>().SkullSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<SkullSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Deerclops)
            {
                player.GetModPlayer<SlayerPlayer>().DeerclopsSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<DeerclopsSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall)
            {
                player.GetModPlayer<SlayerPlayer>().WallSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<WallSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Queen)
            {
                player.GetModPlayer<SlayerPlayer>().QueenSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<QueenSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer)
            {
                player.GetModPlayer<SlayerPlayer>().DestroyerSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<DestroyerSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins)
            {
                player.GetModPlayer<SlayerPlayer>().TwinSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<TwinsSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime)
            {
                player.GetModPlayer<SlayerPlayer>().PrimeSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<PrimeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant)
            {
                player.GetModPlayer<SlayerPlayer>().PlantSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<PlantSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem)
            {
                player.GetModPlayer<SlayerPlayer>().GolemSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<GolemSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke)
            {
                player.GetModPlayer<SlayerPlayer>().DukeSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<DukeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress)
            {
                player.GetModPlayer<SlayerPlayer>().EmpressSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<EmpressSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord)
            {
                player.GetModPlayer<SlayerPlayer>().LordSoul = SoulCrystalStatus.None;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<LordSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra)
            {
                player.GetModPlayer<SlayerPlayer>().LandSoul = SoulCrystalStatus.None;
                //Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<SenterrraSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis)
            {
                player.GetModPlayer<SlayerPlayer>().TimeSoul = SoulCrystalStatus.None;
                //Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<GenesisSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death)
            {
                player.GetModPlayer<SlayerPlayer>().DeathSoul = SoulCrystalStatus.None;
                //Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<SoulExtractingDagger>()), player.getRect(), ModContent.ItemType<DeathSoulCrystal>());
            }
        }
    }
}
