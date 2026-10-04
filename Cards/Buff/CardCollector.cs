using ClassesManagerReborn.Util;
using Photon.Realtime;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Buff
{
    class CardCollector : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = BuffClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int cardCount;
            if (player.data.currentCards.Count >= 10)
            {
                cardCount = 10;
            }
            else
            {
                cardCount = player.data.currentCards.Count;
            }
            
            gun.damage *= 1 + (cardCount * 0.04f);
            gun.projectileSpeed *= 1 + (cardCount * 0.04f);
            player.data.maxHealth *= 1 + (cardCount * 0.04f);
            gun.attackSpeed *= 1 - (cardCount * 0.04f);
            gunAmmo.reloadTimeMultiplier *= 1 - (cardCount * 0.04f);
            characterStats.movementSpeed *= 1 - (cardCount * 0.04f);
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Card Collector";
        }
        protected override string GetDescription()
        {
            return "Gain stats for every card you own. No way! Is that a real shiny block card?";
        }
        protected override GameObject GetCardArt()
        {
            return UnstableAssets.CardCollectorArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Exotic");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+4%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+4%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+4%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "-4%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Time",
                    amount = "-4%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-4%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }
}
