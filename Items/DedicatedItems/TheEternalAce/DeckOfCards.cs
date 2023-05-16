using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.DeckOfCards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    public class DeckOfCards : ModItem
    {
        int choiceCard = 0;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddFireDefault();
            Item.AddAquaDefault();
            Item.AddElecDefault();
            Item.AddWoodDefault();
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 22;

            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;
            Item.crit = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 15;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 2, 25);
            Item.shoot = ModContent.ProjectileType<AceOfSpades>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (++choiceCard >= 4)
                {
                    choiceCard = 0;
                }
                Item.shoot = ChooseCard(player);
            }
            return base.CanUseItem(player);
        }

        int ChooseCard(Player player)
        {
            string text;
            int type;
            switch (choiceCard)
            {
                default:
                    text = "Spades";
                    type = ModContent.ProjectileType<AceOfSpades>();
                    break;
                case 1:
                    text = "Hearts";
                    type = ModContent.ProjectileType<AceOfHearts>();
                    break;
                case 2:
                    text = "Clubs";
                    type = ModContent.ProjectileType<AceOfClubs>();
                    break;
                case 3:
                    text = "Diamonds";
                    type = ModContent.ProjectileType<AceOfDiamonds>();
                    break;
            }
            CombatText.NewText(player.getRect(), Color.Cyan, text);
            return type;
        }
    }
}
