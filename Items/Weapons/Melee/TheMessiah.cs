using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee.OmegaSword;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMessiah : ModItem
    {
        public int charge;
        public bool soundPlayed = false;

        public override void SaveData(TagCompound tag)
        {
            tag["theMessiah"] = soundPlayed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("theMessiah"))
            {
                soundPlayed = tag.GetBool("theMessiah");
            }
        }

        public override void SetStaticDefaults()
        {
            Item.AddElement(0);
            Item.AddEraser();
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;

            Item.damage = 400;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 0;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 15;
            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = 5000000;
            Item.shoot = ModContent.ProjectileType<MessiahRanbu>();
        }

        public override void UpdateInventory(Player player)
        {
            if (!soundPlayed && Main.myPlayer == player.whoAmI)
            {
                soundPlayed = true;
                SoundEngine.PlaySound(SoA.TheMessiah);
            }
            player.Shards().overdriveTimeCurrent = ShardsPlayer.OVERDRIVE_TIME_MAX;
        }

        public override bool AltFunctionUse(Player player)
        {
            return player.Shards().Overdrive;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.Shards().Overdrive && player.altFunctionUse == 2)
            {
                velocity = new(0, 4f);
                type = ModContent.ProjectileType<Messiah>();
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<Messiah>()] == 0;
        }
    }
}