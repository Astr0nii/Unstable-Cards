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
using System.Threading;
using System.Collections;
using Photon.Pun;

namespace UnstableCards.Cards.Totem
{
    class TotemOfTheBalanced : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = TotemClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Audio Logic
            var audioSource = new GameObject("audioSource").gameObject.GetOrAddComponent<AudioSource>();
            audioSource.gameObject.GetOrAddComponent<RemoveAfterSeconds>();
            var timer = audioSource.GetComponent<RemoveAfterSeconds>();
            timer.seconds = 5;
            audioSource.PlayOneShot(Assets.totemOfTheBalancedAudio, 1.2f);

            // Card Adding/Removing Logic
            UnityEngine.Debug.Log($"{player.data.currentCards}");
            Unbound.Instance.StartCoroutine(DoReplaceCards(player));
        }
        private static IEnumerator DoReplaceCards(Player player)
        {
            if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode) yield break;

            int[] cardIndeces = Enumerable.Range(0, player.data.currentCards.Count()).ToArray();
            var randomCard = cardIndeces[new System.Random().Next(0, cardIndeces.Length)];
            var choiceCard = player.data.currentCards[randomCard];
            CardInfo[] choiceCardList = Enumerable.Repeat(choiceCard, cardIndeces.Length).ToArray();
            yield return ModdingUtils.Utils.Cards.instance.ReplaceCards(player, cardIndeces, choiceCardList, null);
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Totem Of The Balanced";
        }
        protected override string GetDescription()
        {
            return "It cost everything. All of your cards are now perfectly balanced.";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.TotemOfTheBalancedArt;
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
                    stat = "Cards Converted",
                    amount = "All",
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
