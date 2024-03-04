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
            
            gun.damage *= 1 + (cardCount * 0.05f);
            gun.projectileSpeed *= 1 + (cardCount * 0.05f);
            player.data.maxHealth *= 1 + (cardCount * 0.05f);
            gun.attackSpeed *= 1 - (cardCount * 0.05f);
            gunAmmo.reloadTimeMultiplier *= 1 - (cardCount * 0.05f);
            characterStats.movementSpeed *= 1 - (cardCount * 0.05f);
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
            return "No way dude is that a *insert absurdly rare card from a card game*. Isn't that worth like 100K? Per card gain (Capped at 10 cards):";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.CardCollectorArt;
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
                    amount = "+5%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+5%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+5%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "-5%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Time",
                    amount = "-5%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-5%",
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
