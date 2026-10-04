using System;
using UnboundLib;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    class HeavyWeaponsGuy : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // Rebalanced: minigun used to be a zero-cost laser beam of death.
            gun.attackSpeed = 0.2f;
            gun.reflects = -999;
            gun.spread = 0.6f;
            gun.reloadTime = 0.8f;
            gun.projectileSpeed = 2.0f;
            gun.damage = 0.1f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var audioSource = new GameObject("audioSource").gameObject.GetOrAddComponent<AudioSource>();
            audioSource.gameObject.GetOrAddComponent<RemoveAfterSeconds>();
            var timer = audioSource.GetComponent<RemoveAfterSeconds>();
            timer.seconds = 5;
            audioSource.PlayOneShot(UnstableAssets.HeavyWeaponsGuyAudio, 1.5f);

            gunAmmo.maxAmmo += 75;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Heavy weapons guy";
        }
        protected override string GetDescription()
        {
            return "A custom tool cartridge 10000rpm Max ammo bullet machine of death named 'Sasha'.";
        }
        protected override GameObject GetCardArt()
        {
            return UnstableAssets.HeavyWeaponsGuyArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Mythical");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "0.8s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "0.2s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "DMG",
                    amount = "-90%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bullet Bounces",
                    amount = "Disables",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+200%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
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
