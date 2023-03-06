using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Throwing.DeckOfCards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    public class AceOfSpades : ModItem
    {
        int choiceCard = 0;

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Fire.Add(Type);
            WeaponElements.Ice.Add(Type);
            WeaponElements.Electric.Add(Type);
            WeaponElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 22;

            Item.damage = 70;
            Item.DamageType = DamageClass.Throwing;
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
            Item.shoot = ModContent.ProjectileType<AceOfSpadesProj>();
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

        public override void UpdateInventory(Player player)
        {
            if (choiceCard > 0)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile card = Main.projectile[i];
                    if (card.active && card.owner == player.whoAmI)
                    {
                        if (card.type == ModContent.ProjectileType<AceOfSpadesProj>())
                        {
                            card.Kill();
                        }
                    }
                }
            }
        }

        int ChooseCard(Player player)
        {
            string text;
            int type;
            switch (choiceCard)
            {
                default:
                    text = "Spades";
                    type = ModContent.ProjectileType<AceOfSpadesProj>();
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
