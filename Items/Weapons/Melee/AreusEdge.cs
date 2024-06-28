using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee.AreusJustitia;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusEdge : LobCorpLight
    {
        private int PreviouslyHitNPC;

        public override void SetStaticDefaults()
        {
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 54;

            Item.damage = 32;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 3;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 15;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = 150000;
            Item.shoot = ModContent.ProjectileType<AreusJustitia>();
            PreviouslyHitNPC = -1;
        }

        public override float UseSpeedMultiplier(Player player)
        {
            return 0.35f;
        }

        public override bool? UseItemAlt(Player player)
        {
            Item.noMelee = true;
            Item.noUseGraphic = true;
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.Shards().Overdrive)
            {
                PreviouslyHitNPC = target.whoAmI;
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (PreviouslyHitNPC >= 0 && player.Shards().Overdrive)
            {
                Main.npc[PreviouslyHitNPC].immune[player.whoAmI] = 3;
                player.attackCD = 1;
                PreviouslyHitNPC = -1;
            }
        }

        public override void UseStyleAlt(Player player, Rectangle heldItemFrame)
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            base.UseStyleAlt(player, heldItemFrame);
        }

        public override void UseItemHitboxAlt(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            noHitbox = false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity.Normalize();
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 0.5f;
            modifiers.ScalingArmorPenetration += 1f;
        }
    }
}