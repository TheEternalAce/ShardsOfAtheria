using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee.BloodDagger;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class CrossDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(1);
            Item.AddElement(3);
            Item.AddRedemptionElement(12);
            Item.AddRedemptionElement(8);
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;

            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.shootSpeed = 12f;
            Item.value = Item.sellPrice(0, 15);
            Item.rare = ItemDefaults.RarityDemoniteCrimtane;
            Item.shoot = ModContent.ProjectileType<BloodBlade>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ShardsRecipes.Gold, 7)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.ManaCrystal, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            damage = (int)(damage * 0.75f);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.CanBeChasedBy())
            {
                int numOffenseBlood = Main.rand.Next(2, 5);
                int numHealBlood = Main.rand.Next(1, 3);
                int totalBlood = numHealBlood + numOffenseBlood;
                for (int i = 0; i < totalBlood; i++)
                {
                    var vector = Vector2.One * (1f + Main.rand.NextFloat(0f, 7f));
                    if (numHealBlood > 0)
                    {
                        vector = vector.RotatedByRandom(MathHelper.TwoPi);
                        int heal = damageDone / (4 + numHealBlood);
                        Projectile.NewProjectile(player.GetSource_OnHit(target), target.Center, vector,
                            ModContent.ProjectileType<HolyBloodHeal>(), heal, 0, player.whoAmI);
                        numHealBlood--;
                    }
                    if (numOffenseBlood > 0)
                    {
                        vector = vector.RotatedByRandom(MathHelper.TwoPi);
                        int damage = damageDone / numOffenseBlood;
                        Projectile.NewProjectile(player.GetSource_OnHit(target), target.Center, vector,
                            ModContent.ProjectileType<HolyBloodOffense>(), damage, 0, player.whoAmI);
                        numOffenseBlood--;
                    }
                }
            }
        }
    }
}
