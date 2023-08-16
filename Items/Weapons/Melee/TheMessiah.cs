using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Melee.OmegaSword;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMessiah : ModItem
    {
        public int charge;
        public bool theMessiah = false;

        public override void SaveData(TagCompound tag)
        {
            tag["theMessiah"] = theMessiah;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("theMessiah"))
            {
                theMessiah = tag.GetBool("theMessiah");
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.Eraser.Add(Type);
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
            Item.rare = ItemRarityID.Red;
            Item.value = 5000000;
            Item.shoot = ModContent.ProjectileType<MessiahRanbu>();
        }

        public override void UpdateInventory(Player player)
        {
            if (!theMessiah && Main.myPlayer == player.whoAmI)
            {
                theMessiah = true;
                SoundEngine.PlaySound(SoA.TheMessiah);
            }
            player.Shards().overdriveTimeCurrent = player.Shards().overdriveTimeMax;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.Shards().Overdrive && (player.controlUp || player.controlDown))
            {
                velocity = new(0, 4f);
                type = ModContent.ProjectileType<Messiah>();
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<Messiah>()] == 0;
        }
    }
}