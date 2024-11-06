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
using UnstableCards.Cards.Special;
using UnstableCards.Cards.NameClasses;
using ModdingUtils;

namespace UnstableCards.Cards.Totem
{
    class TotemOfRebirth : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = TotemClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block) { }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Audio Logic
            var audioSource = new GameObject("audioSource").gameObject.GetOrAddComponent<AudioSource>();
            audioSource.gameObject.GetOrAddComponent<RemoveAfterSeconds>();
            var timer = audioSource.GetComponent<RemoveAfterSeconds>();
            timer.seconds = 5;
            audioSource.PlayOneShot(Assets.totemOfTheForgottenAudio, 1.2f);


            // Card Removing Logic
            List<int> cardIndicesToRemove = new List<int>();
            for (int i = 0; i < player.data.currentCards.Count(); i++)
            {
                cardIndicesToRemove.Add(i);
            }
            ModdingUtils.Utils.Cards.instance.RemoveCardsFromPlayer(player, cardIndicesToRemove.ToArray());

            //Stat modifiers
            string cardName = "Rebirthed Soul";
            CardInfo cardInfo = UnstableCards.GetCardInfoByName(cardName);
            if (cardInfo != null)
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, cardInfo, reassign: false, twoLetterCode: "", forceDisplay: 0f, forceDisplayDelay: 0f);
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Totem Of Rebirth";
        }
        protected override string GetDescription()
        {
            return "Empower your soul with a new life. Start afresh when your deck is too far gone...";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.TotemOfTheForgottenArt;
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
                    stat = "Cards Removed",
                    amount = "All",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Lives",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+100%",
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
