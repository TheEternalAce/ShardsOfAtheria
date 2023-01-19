using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Globals.Elements;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsHelpers // General class full of helper methods
    {
        public static int DefaultMaxStack = 9999;

        public struct UpgrageMaterial
        {
            public Item item;
            public int requiredStack;

            public UpgrageMaterial(Item item, int stack)
            {
                this.item = item;
                requiredStack = stack;
            }
        }

        public static void DefaultToPotion(this Item potion, int buff, int buffTime)
        {
            potion.useTime = 17;
            potion.useAnimation = 17;
            potion.useStyle = ItemUseStyleID.DrinkLiquid;
            potion.UseSound = SoundID.Item3;
            potion.consumable = true;
            potion.useTurn = true;

            potion.buffType = buff;
            potion.buffTime = buffTime;
            SoAGlobalItem.Potions.Add(potion.type);
        }

        /// <summary>
        /// Draws a basic single-frame glowmask for an item dropped in the world. Use in <see cref="Terraria.ModLoader.ModItem.PostDrawInWorld"/>
        /// </summary>
        public static void BasicInWorldGlowmask(this Item item, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, float rotation, float scale)
        {
            spriteBatch.Draw(
                glowTexture,
                new Vector2(
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - glowTexture.Height * 0.5f
                ),
                new Rectangle(0, 0, glowTexture.Width, glowTexture.Height),
                color,
                rotation,
                glowTexture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f);
        }

        public static void BasicInWorldGlowmask(this NPC npc, SpriteBatch spriteBatch, Texture2D glowTexture, Color color, Vector2 screenPos, SpriteEffects effects)
        {
            Vector2 drawOrigin = new Vector2(glowTexture.Width * 0.5f, npc.height * 0.5f);
            Vector2 drawPos = npc.Center - screenPos;
            spriteBatch.Draw(glowTexture, drawPos, npc.frame, color, npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
        }

        public static void SlayBoss(this NPC boss, Player player)
        {
            SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();
            if (slayer.slayerMode)
            {
                ShardsDownedSystem.slainBosses.Add(boss.type);
            }
        }

        public static void CallStorm(this Projectile projectile, int amount, int pierce = 1)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath56, projectile.Center);
            for (var i = 0; i < amount; i++)
            {
                Projectile p = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(),
                    new Vector2(projectile.Center.X + Main.rand.Next(-60 * amount, 60 * amount), projectile.Center.Y - 600), new Vector2(0, 5),
                    ModContent.ProjectileType<LightningBoltFriendly>(), (int)(projectile.damage * 0.66f), projectile.knockBack, Main.player[projectile.owner].whoAmI);
                p.penetrate = pierce;
                p.DamageType = projectile.DamageType;
            }
        }

        public static void Explode(this Projectile proj, Vector2 position, int explosionSize = 120)
        {
            Projectile explosion = Projectile.NewProjectileDirect(proj.GetSource_FromThis(), position, Vector2.Zero,
                ModContent.ProjectileType<ElementExplosion>(), proj.damage, proj.knockBack, proj.owner);
            explosion.DamageType = proj.DamageType;
            explosion.Size = new Vector2(explosionSize);
            ProjectileElements elementExplosion = explosion.GetGlobalProjectile<ProjectileElements>();
            int type = proj.type;
            if (ProjectileElements.Fire.Contains(type))
            {
                elementExplosion.tempFire = true;
            }
            if (ProjectileElements.Ice.Contains(type))
            {
                elementExplosion.tempIce = true;
            }
            if (ProjectileElements.Electric.Contains(type))
            {
                elementExplosion.tempElectric = true;
            }
            if (ProjectileElements.Metal.Contains(type))
            {
                elementExplosion.tempMetal = true;
            }
        }

        public static void TrackTarget(this Projectile projectile, NPC targetNPC, int speed = 16, int inertia = 16)
        {
            if (Vector2.Distance(projectile.Center, targetNPC.Center) > 40f)
            {
                // The immediate range around the target (so it doesn't latch onto it when close)
                Vector2 direction = targetNPC.Center - projectile.Center;
                direction.Normalize();
                direction *= speed;

                projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
            }
        }

        /// <summary>
        /// Sets NPC multipliers in the following order: Fire, Ice, Electric, Metal
        /// <para>This method should be called in the SetDefaults() override</para>
        /// </summary>
        public static void SetCustomElementMultipliers(this NPC npc, params double[] multipliers)
        {
            if (multipliers.Length > 4)
            {
                throw new ArgumentException("Too many arguments, please use only four (4) decimal values");
            }
            else if (multipliers.Length < 4)
            {
                throw new ArgumentException("Too few arguments, please provide only four (4) decimal values");
            }
            for (int i = 0; i < multipliers.Length; i++)
            {
                npc.GetGlobalNPC<NPCElements>().elementMultiplier[i] = multipliers[i];
            }
        }

        /// <summary>
        /// Sets NPC multipliers based on given Element
        /// <para>0 = Fire, 1 = Ice, 2 = Electric, 3 = Metal</para>
        /// <para>This method should be called in the SetDefaults() override</para>
        /// </summary>
        public static void SetElementMultipliersByElement(this NPC npc, int element)
        {
            switch (element)
            {
                case Element.Fire:
                    npc.SetElementMultiplier(Element.Electric, 2.0);
                    npc.SetElementMultiplier(Element.Ice, 0.5);
                    break;
                case Element.Ice:
                    npc.SetElementMultiplier(Element.Fire, 2.0);
                    npc.SetElementMultiplier(Element.Metal, 0.5);
                    break;
                case Element.Electric:
                    npc.SetElementMultiplier(Element.Metal, 2.0);
                    npc.SetElementMultiplier(Element.Fire, 0.5);
                    break;
                case Element.Metal:
                    npc.SetElementMultiplier(Element.Ice, 2.0);
                    npc.SetElementMultiplier(Element.Electric, 0.5);
                    break;
            }
        }

        public static void SetElementMultiplier(this NPC npc, int element, double multiplier)
        {
            npc.GetGlobalNPC<NPCElements>().elementMultiplier[element] = multiplier;
        }

        public static void SetElementMultiplier(this NPC npc, double[] multipliers)
        {
            npc.GetGlobalNPC<NPCElements>().elementMultiplier = multipliers;
        }

        /// <summary>
        /// Insert useful summary here
        /// </summary>
        /// <param name="player"> Player with the item to be upgraded</param>
        /// <param name="result"> Item to upgrade into</param>
        /// <param name="materials"> an array of ShardsHelpers.UpgradeMaterials</param>
        public static void UpgradeItem(this NPC npc, Player player, int result, params UpgrageMaterial[] materials)
        {
            SoAPlayer shardsPlayer = player.GetModPlayer<SoAPlayer>();

            Main.npcChatCornerItem = result;
            if (CanUpgradeItem(player, materials))
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    player.inventory[player.FindItem(materials[i].item.type)].stack -= materials[i].requiredStack;
                }
                if (materials[0].item.ModItem is GenesisAndRagnarok)
                {
                    shardsPlayer.genesisRagnarockUpgrades++;
                }
                SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound
                if (result > 0)
                {
                    if (result == ModContent.ItemType<GenesisAndRagnarok>())
                    {
                        string key = "";
                        switch (shardsPlayer.genesisRagnarockUpgrades)
                        {
                            case 1:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok1";
                                break;
                            case 2:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok2";
                                break;
                            case 3:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok3";
                                break;
                            case 4:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok4";
                                break;
                            case 5:
                                key = "Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeGenesisAndRagnarok5";
                                break;
                        }
                        Main.npcChatText = Language.GetTextValue(key);
                    }
                    else
                    {
                        Item.NewItem(npc.GetSource_FromThis(), npc.getRect(), result);
                        if (result == ModContent.ItemType<AreusSaber>() || result == ModContent.ItemType<TheMourningStar>())
                        {
                            Main.npcChatText = Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.UpgradeAreusWeapon");
                        }
                    }
                }
            }
            else
            {
                string insufficient = "I need the following items:\n";
                for (int i = 0; i < materials.Length; i++)
                {
                    Item item = null;
                    if (player.HasItem(materials[i].item.type))
                    {
                        item = player.inventory[player.FindItem(materials[i].item.type)];
                    }
                    insufficient += Language.GetTextValue("Mods.ShardsOfAtheria.NPCDialogue.Atherian.NotEnoughMaterial", i, materials[i].item.Name, materials[i].item.type,
                        materials[i].requiredStack, item == null ? 0 : item.stack) + "\n";
                    Main.npcChatText = insufficient;
                }
            }
        }

        public static bool CanUpgradeItem(Player player, UpgrageMaterial[] upgrageMaterials)
        {
            int requiredItems = upgrageMaterials.Length;
            int playerItems = 0;
            for (int i = 0; i < requiredItems; i++)
            {
                if (player.HasItem(upgrageMaterials[i].item.type))
                {
                    Item item = player.inventory[player.FindItem(upgrageMaterials[i].item.type)];
                    if (item.stack >= upgrageMaterials[i].requiredStack)
                    {
                        playerItems++;
                    }
                }
            }
            return playerItems >= requiredItems;
        }

        public static Color UseA(this Color color, int alpha) => new Color(color.R, color.G, color.B, alpha);
        public static Color UseA(this Color color, float alpha) => new Color(color.R, color.G, color.B, (int)(alpha * 255));

        public static SoAPlayer ShardsOfAtheria(this Player player)
        {
            return player.GetModPlayer<SoAPlayer>();
        }

        public static float CappedMeleeScale(this Player player)
        {
            var item = player.HeldItem;
            return Math.Clamp(player.GetAdjustedItemScale(item), 0.5f * item.scale, 2f * item.scale);
        }

        public static void CappedMeleeScale(Projectile proj)
        {
            float scale = Main.player[proj.owner].CappedMeleeScale();
            if (scale != 1f)
            {
                proj.scale *= scale;
                proj.width = (int)(proj.width * proj.scale);
                proj.height = (int)(proj.height * proj.scale);
            }
        }

        public static float Wave(float time, float minimum, float maximum)
        {
            return minimum + ((float)Math.Sin(time) + 1f) / 2f * (maximum - minimum);
        }

        internal static void Log(string str)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(str), Color.White);
            Console.WriteLine(str);
        }
    }

    public static class Element
    {
        public const int Null = -1;
        public const int Fire = 0;
        public const int Ice = 1;
        public const int Electric = 2;
        public const int Metal = 3;
        public const int Areus = 4;
    }
}
