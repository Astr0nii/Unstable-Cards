using ClassesManagerReborn.Util;
using System.Linq;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.Special;
using UnstableCards.Cards.NameClasses;

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
            audioSource.PlayOneShot(UnstableAssets.totemOfTheForgottenAudio, 1.2f);


            // Card Removing Logic
            // The Totem itself occupies the last slot, so only remove the cards that come before it.
            int totemIndex = player.data.currentCards.Count - 1;
            if (totemIndex > 0)
            {
                int[] cardIndicesToRemove = Enumerable.Range(0, totemIndex).ToArray();
                ModdingUtils.Utils.Cards.instance.RemoveCardsFromPlayer(player, cardIndicesToRemove);
            }

            // Add new card
            CardInfo cardInfo = UnstableCards.GetCardInfoByName("Rebirthed Soul");
            if (cardInfo != null)
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, cardInfo, reassign: false, twoLetterCode: "", forceDisplay: 0f, forceDisplayDelay: 0f);
            }
            else
            {
                Debug.LogWarning("[UC] Totem Of Rebirth could not find the 'Rebirthed Soul' card.");
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
            return UnstableAssets.TotemOfTheForgottenArt;
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
