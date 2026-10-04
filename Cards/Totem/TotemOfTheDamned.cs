using ClassesManagerReborn.Util;
using System.Linq;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Totem
{
    class TotemOfTheDamned : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = TotemClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileColor = Color.green;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Audio Logic
            var audioSource = new GameObject("audioSource").gameObject.GetOrAddComponent<AudioSource>();
            audioSource.gameObject.GetOrAddComponent<RemoveAfterSeconds>();
            var timer = audioSource.GetComponent<RemoveAfterSeconds>();
            timer.seconds = 5;
            audioSource.PlayOneShot(UnstableAssets.totemOfTheDamnedAudio, 1.2f);

            // Card Adding Logic
            UnstableCards.damnedCards.Shuffle();
            var cardsToAdd = UnstableCards.damnedCards.Take(3).ToArray();
            ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cardsToAdd, reassign: false, addToCardBar: true);
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Totem Of The Damned";
        }
        protected override string GetDescription()
        {
            return "The unfortunate place for a damned souls last hope.";
        }
        protected override GameObject GetCardArt()
        {
            return UnstableAssets.TotemOfTheDamnedArt;
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
                    stat = "Damned cards added",
                    amount = "+3",
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
    }
}
