using ClassesManagerReborn.Util;
using RarityLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;
using ModdingUtils;
using System.Collections;
using ModdingUtils.Patches;
using UnboundLib.Utils;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace UnstableCards.Cards.Shrine
{
    class ShrineOfTheDamned : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = ShrineClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            for (int i = 5; i > 0; i--)
            {
                var choiceCard = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, (ci, p, g, ga, cd, hh, gr, b, csm) => ci.categories.Contains(UnstableCards.instance.debuffCategory));
                WaitFor.Frames(20);
                UnityEngine.Debug.Log($"{choiceCard}");
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, choiceCard, false, "", 0f, 0f, false);
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Shrine Of The Damned";
        }
        protected override string GetDescription()
        {
            return "The unfortunate place for a damned souls last hope.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Epic");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Debuff cards added",
                    amount = "Alot",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
        private static class WaitFor
        {
            public static IEnumerator Frames(int frameCount)
            {
                if (frameCount <= 0)
                {
                    throw new ArgumentOutOfRangeException("frameCount", "Cannot wait for less that 1 frame");
                }

                while (frameCount > 0)
                {
                    frameCount--;
                    yield return null;
                }
            }
        }
    }
}
