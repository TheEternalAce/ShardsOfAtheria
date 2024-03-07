using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged.DeckOfCards;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    public class DeckOfCards : ModItem
    {
        int choiceCard = 0;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElement(0);
            Item.AddElement(1);
            Item.AddElement(2);
            Item.AddElement(3);
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
            Item.rare = ItemDefaults.RarityDevSet;
            Item.value = ItemDefaults.ValueEarlyHardmode;
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
            int type;
            Item.crit = 4;
            switch (choiceCard)
            {
                default:
                    type = ModContent.ProjectileType<AceOfSpades>();
                    break;
                case 1:
                    type = ModContent.ProjectileType<AceOfHearts>();
                    break;
                case 2:
                    type = ModContent.ProjectileType<AceOfClubs>();
                    break;
                case 3:
                    type = ModContent.ProjectileType<AceOfDiamonds>();
                    Item.crit = 12;
                    break;
            }
            string key = this.GetLocalizationKey("CardSuit" + choiceCard);
            CombatText.NewText(player.getRect(), Color.Cyan, Language.GetTextValue(key));
            return type;
        }

        public override void UpdateInventory(Player player)
        {
            string key = this.GetLocalizationKey("CardSuit" + choiceCard);
            string key2 = this.GetLocalizationKey("DisplayName");
            string name = Language.GetTextValue(key2) + " (" + Language.GetTextValue(key) + ")";
            Item.SetNameOverride(name);
        }
    }
}
