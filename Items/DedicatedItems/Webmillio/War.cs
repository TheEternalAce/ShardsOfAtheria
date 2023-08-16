using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.DedicatedItems.Webmillio
{
    public class War : ModItem
    {
        public bool upgraded = false;
        public static Asset<Texture2D> glowmask;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            }
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["upgraded"] = upgraded;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("upgraded"))
            {
                upgraded = tag.GetBool("upgraded");
            }
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.UpgradeableItem.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62;

            Item.damage = 90;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;
            Item.ArmorPenetration = 20;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shoot = ModContent.ProjectileType<Warframe>();
            Item.shootSpeed = 1;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 2, 50);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (upgraded)
            {
                damage += 0.12f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BreakerBlade)
                .AddIngredient(ItemID.BluePhasesaber)
                .AddIngredient(ItemID.BluePhasesaber)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddCondition(SoAConditions.Upgrade)
                .AddIngredient(Type)
                .AddIngredient(ItemID.HallowedBar, 20)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (upgraded)
            {
                type = ModContent.ProjectileType<Warframe_Upgrade>();
            }
            player.velocity += velocity * 8f;
            if (player.velocity.Length() > 16f)
            {
                player.velocity.Normalize();
                player.velocity *= 16f;
            }
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (upgraded)
            {
                spriteBatch.Draw(glowmask.Value, position, frame, drawColor, 0f, glowmask.Value.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (upgraded)
            {
                Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value, Color.White, rotation, 1f);
            }
        }
    }
}