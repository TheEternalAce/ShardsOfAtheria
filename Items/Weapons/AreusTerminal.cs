using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.ShardsUI.AreusComputer;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    public class AreusTerminal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.AddAreus();
            Item.AddRedemptionElement(13);
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 34;

            Item.damage = 77;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 6;
            Item.mana = 16;

            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.holdStyle = ItemHoldStyleID.HoldHeavy;

            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<CodeProjectile>();
            Item.value = ItemDefaults.ValueLunarPillars;
            Item.rare = ItemDefaults.RarityMoonLord;
        }

        public override void HoldItem(Player player)
        {
            UpdateStats(player);
        }

        public override void UpdateInventory(Player player)
        {
            UpdateStats(player);
        }

        private void UpdateStats(Player player)
        {
            bool cardActive = DevelopersKeyCard.CardActive(player, out var _);
            if (!cardActive)
            {
                int statTotal = (Item.damage + (60 / Item.useAnimation) + Item.crit + 4) / 5;
                Item.mana = statTotal;
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            var soundType = SoundID.Item91;
            bool playSound = true;
            if (player.altFunctionUse == 2)
            {
                type = 0;
                player.statMana += (int)(Item.mana * player.manaCost);
                player.manaRegenDelay = 0;
                var terminalSystem = ModContent.GetInstance<AreusTerminalSystem>();
                terminalSystem.ShowTerminal();
                soundType = SoA.KeyPress;
                playSound = false;
            }
            if (player.itemAnimation == player.itemAnimationMax || playSound)
            {
                SoundEngine.PlaySound(soundType, player.Center);
            }
        }
    }
}