using UnboundLib.Cards;
using UnityEngine;
using System.Collections;
using UnboundLib;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Buff
{
    class HotPotato : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = BuffClass.name;
        }
        private float initialTimer = 10f;
        private float passedTimer = 5f;
        private bool hasPotatoActive = false;
        private Player potatoHolder;
        private Coroutine potatoCoroutine;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // Base stats when card is drawn
            statModifiers.health = 1.2f;
            statModifiers.movementSpeed = 1.1f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Start potato timer when card is added
            potatoHolder = player;
            hasPotatoActive = true;
            potatoCoroutine = StartCoroutine(PotatoTimer(initialTimer));
        }

        private IEnumerator PotatoTimer(float time)
        {
            float remainingTime = time;
            while (remainingTime > 0 && hasPotatoActive)
            {
                UnityEngine.Debug.Log(remainingTime);
                remainingTime -= Time.deltaTime;
                yield return null;
            }

            if (hasPotatoActive)
            {
                // Kill player if they didn't pass the potato
                potatoHolder.data.health = 0;
                hasPotatoActive = false;
                UnityEngine.Debug.Log("Player should be dead as hell.");
            }
        }

        public void PassPotato(Player target)
        {
            if (hasPotatoActive && potatoCoroutine != null)
            {
                StopCoroutine(potatoCoroutine);
                potatoHolder = target;
                potatoCoroutine = StartCoroutine(PotatoTimer(passedTimer));
            }
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (potatoCoroutine != null)
            {
                StopCoroutine(potatoCoroutine);
            }
            hasPotatoActive = false;
        }

        protected override string GetTitle()
        {
            return "Hot Potato";
        }

        protected override string GetDescription()
        {
            return "You have 10 seconds to pass this deadly potato! When passed, the new holder has 5 seconds! Don't let it explode!";
        }

        protected override GameObject GetCardArt()
        {
            return null; // Replace with actual potato art asset
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
                    stat = "Health",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        public override string GetModName()
        {
            return UnstableCards.ModInitials;
        }
    }
}