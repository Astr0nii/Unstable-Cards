﻿using ClassesManagerReborn.Util;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    class GasGasGas : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = BuffClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.movementSpeed = 1.25f;
            gun.recoil = -50;
            block.forceToAdd = -10f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Gas Gas Gas";
        }
        protected override string GetDescription()
        {
            return "<b>OH LAWRD HE COMIN'</b>. Makes you dash forward when blocking and shooting";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.PlaceHolderArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-5%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
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
