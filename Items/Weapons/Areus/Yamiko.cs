using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Bases;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class Yamiko : SinfulItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 50;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.shootSpeed = 10;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<PrometheusFire>();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<YamikoDashCooldown>()))
            {
                player.velocity = Vector2.Normalize(Main.MouseWorld - player.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 16;
                string[] insult = { "How did you manage that? Dumbass.", "Good job idiot, you fatally cut yourself. ", "How could you be so stupid?" };
                int i = Main.rand.Next(insult.Length);
                string dying = $"{player.name} cut {(player.Male ? "himself" : " herself")}";
                string die = ModContent.GetInstance<ShardsServerConfig>().yamikoInsult ? insult[i] + " (" + dying + ")" : dying;
                player.Hurt(PlayerDeathReason.ByCustomReason(die), 100, 0);
                player.immune = true;
                player.immuneTime = 30;
            }
            return null;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<YamikoDashCooldown>()))
            {
                float numberProjectiles = 4;
                float rotation = MathHelper.ToRadians(10);
                position += Vector2.Normalize(velocity) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile proj = Projectile.NewProjectileDirect(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI, 2f);
                    proj.DamageType = DamageClass.Melee;
                }
                player.AddBuff(ModContent.BuffType<ElectricShock>(), 120);
                player.AddBuff(ModContent.BuffType<YamikoDashCooldown>(), 120);
            }
            return false;
        }
    }
}