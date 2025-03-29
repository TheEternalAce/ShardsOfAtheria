using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Projectiles.Melee.AreusJustitia;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ZAreusEdge : LobCorpLight
    {
        private bool AlternateAttack;
        private int PreviouslyHitNPC;

        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5, 9);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(13);
        }

        public override void SetDefaults()
        {
            Item.width = 86;
            Item.height = 86;
            Item.scale = 1.3f;

            Item.damage = 245;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 4f;

            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = 15;
            Item.UseSound = SoA.Judgement0;
            Item.autoReuse = true;

            Item.shootSpeed = 28f;
            Item.rare = ItemRarityID.Purple;
            Item.value = 150000;
            Item.shoot = ModContent.ProjectileType<FlameSlashExtention>();
            PreviouslyHitNPC = -1;
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override float UseSpeedMultiplier(Player player)
        {
            if (AlternateAttack) return 0.35f;
            return base.UseSpeedMultiplier(player);
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.rand.NextBool(3))
            {
                AlternateAttack = true;
                Item.UseSound = null;
                Item.noMelee = true;
            }
            else
            {
                AlternateAttack = false;
                Item.UseSound = SoA.Judgement0;
                Item.noMelee = false;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItemAlt(Player player)
        {
            Item.noMelee = AlternateAttack;
            Item.noUseGraphic = AlternateAttack;
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            PreviouslyHitNPC = target.whoAmI;
        }

        public override void UpdateInventory(Player player)
        {
            if (PreviouslyHitNPC >= 0) SetPlayerMeleeCooldown(player, ref PreviouslyHitNPC, 3, 0.0f);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            float percent = ((float)player.itemAnimation / (float)player.itemAnimationMax);
            if (percent > 0.8f)
            {
                for (int i = 0; i < 6; i++)
                {
                    Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, ModContent.DustType<AreusDust>()).noGravity = true;
                }
            }
        }

        public override void UseStyleAlt(Player player, Rectangle heldItemFrame)
        {
            if (AlternateAttack)
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noUseGraphic = true;
            }
            else
            {
                Item.useStyle = 15;
                Item.noUseGraphic = false;
            }
            base.UseStyleAlt(player, heldItemFrame);
        }

        public override void UseItemHitboxAlt(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            noHitbox = false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (AlternateAttack)
            {
                type = ModContent.ProjectileType<ZAreusJustitia>();
                velocity.Normalize();
            }
            else
            {
                velocity.Y = 0;
                velocity.X = Item.shootSpeed * Math.Sign(velocity.X);
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.blind) damage += 0.2f;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 0.5f;
            modifiers.ScalingArmorPenetration += 1f;
        }
    }
}