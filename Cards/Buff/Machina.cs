using ClassesManagerReborn.Util;
using ModsPlus;
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
using UnstableCards.MonoBehaviours;
using WillsWackyManagers.MonoBehaviours;

namespace UnstableCards.Cards.Buff
{
    class Machina : CustomCard
    {
        private CustomHealthBar chargeBar;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = BuffClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.useCharge = true;
            gun.chargeNumberOfProjectilesTo += 0;
            gun.chargeSpeedTo = 3f;
            gun.dontAllowAutoFire = true;
            gun.chargeDamageMultiplier *= 2.5f;

            GunChargePatch.Extensions.GunExtensions.GetAdditionalData(gun).chargeTime = 2f;

            Transform parent = player.GetComponentInChildren<PlayerWobblePosition>().transform;
            GameObject obj = new GameObject("Machina Charge Bar");
            obj.transform.SetParent(parent);
            chargeBar = obj.AddComponent<CustomHealthBar>();
            chargeBar.transform.localPosition = Vector3.down * 0.25f;
            chargeBar.transform.localScale = Vector3.one;
            chargeBar.SetColor(Color.yellow);
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.chargeDamageMultiplier /= 2.5f;
        }

        protected override string GetTitle()
        {
            return "Machina";
        }
        protected override string GetDescription()
        {
            return "Charge up to exert extreme amounts of damage and at 100% charge gain the ability to penetrate walls!";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage at max charge",
                    amount = "+250%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ignore walls at max charge",
                    amount = "true",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Gravity",
                    amount = "none",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };

        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }
}
