using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public abstract class SevenSouls : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.useTime = Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SevenSoul", "Using this soul is permanent and only one can be active"));
            base.ModifyTooltips(tooltips);
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int newItem = Item.NewItem(Item.GetSource_DropAsItem(), player.getRect(), ModContent.ItemType<SinfulArmament>());
                Main.item[newItem].noGrabDelay = 0; // Set the new item to be able to be picked up instantly

                // Here we need to make sure the item is synced in multiplayer games.
                if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                }
            }
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed == 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SinfulSoul>())
                .Register();
        }
    }
    
    public class SevenSoulPlayer : ModPlayer
    {
        public int SevenSoulUsed;

        public override void Initialize()
        {
            SevenSoulUsed = 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["SevenSoulUsed"] = SevenSoulUsed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("SevenSoulUsed"))
            {
                SevenSoulUsed = tag.GetInt("SevenSoulUsed");
            }
        }

        public override void PostUpdate()
        {
            if (SevenSoulUsed == 1)
            {
                Player.AddBuff(ModContent.BuffType<EnvyBuff>(), 2);
            }
            if (SevenSoulUsed == 2)
            {
                Player.AddBuff(ModContent.BuffType<GluttonyBuff>(), 2);
            }
            if (SevenSoulUsed == 3)
            {
                Player.AddBuff(ModContent.BuffType<GreedBuff>(), 2);
            }
            if (SevenSoulUsed == 4)
            {
                Player.AddBuff(ModContent.BuffType<LustBuff>(), 2);
            }
            if (SevenSoulUsed == 5)
            {
                Player.AddBuff(ModContent.BuffType<PrideBuff>(), 2);
            }
            if (SevenSoulUsed == 6)
            {
                Player.AddBuff(ModContent.BuffType<SlothBuff>(), 2);
            }
            if (SevenSoulUsed == 7)
            {
                Player.AddBuff(ModContent.BuffType<WrathBuff>(), 2);
            }
        }
    }

    public abstract class SevenSoulsBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            base.Update(player, ref buffIndex);
        }
    }

    public class SinfulSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.maxStack = 30;
        }
    }
}
