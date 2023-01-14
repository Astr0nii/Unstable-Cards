using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using WillsWackyManagers.MonoBehaviours;
using ClassesManagerReborn.Util;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Debuffs
{
    class Clumsy : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = DebuffClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.reloadTime = 1.4f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var misfire = player.gameObject.GetOrAddComponent<Misfire_Mono>();
            misfire.misfireChance += 25;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var misfire = player.gameObject.GetComponent<Misfire_Mono>();
            if (misfire)
            {
                misfire.misfireChance -= 25;
            }
        }

        protected override string GetTitle()
        {
            return "Clumsy";
        }
        protected override string GetDescription()
        {
            return "You are such a klutz, stop fumbling with the mag and reload it!";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Trinket");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Klutziness",
                    amount = "Accidents!",
                    simepleAmount = CardInfoStat.SimpleAmount.aHugeAmountOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Misfire chance",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }
}
