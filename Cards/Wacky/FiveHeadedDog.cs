using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnstableCards.Cards.NameClasses;

namespace UnstableCards.Cards.Wacky
{
    /// <summary>
    /// Good boy x5. This dog has five heads, which means five times the aiming ability
    /// (they all aim at different things) and absolutely zero extra loyalty.
    /// Fires a spread of homing "little doggies" that boop enemies around the room.
    /// Also: he licks his wounds. Slowly. It's very undignified but it works.
    /// </summary>
    class FiveHeadedDog : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = WackyClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // Five heads, five directions, one very confused dog.
            gun.numberOfProjectiles += 5;
            gun.spread = 0.45f;                                  // each head aims where it pleases
            gun.damage = 0.5f;                                   // -50%: it's a puppy, not a war crime
            gun.projectileSpeed = 1.1f;                          // zoomies
            gun.projectileColor = new Color(0.85f, 0.6f, 0.35f); // golden retriever, obviously
            gun.knockback = 1.0f;                                // friendly boop
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // He has five heads, therefore he can lick five times as effectively.
            // (Same pattern as GoldenApple: apply on add, no manual undo on remove.)
            player.data.healthHandler.regeneration += 1;

            // But he also eats five times as much. Vet bills were not in the budget.
            characterStats.movementSpeed *= 0.95f;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Five Headed Dog";
        }
        protected override string GetDescription()
        {
            return "He's a good boy. Five times over. The heads disagree on literally everything, including where to aim.";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoMod.Brief GetBrief()
        {
            return new CardInfoMod.Brief(CardInfoMod.Brief.Direction.None);
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullets",
                    amount = "+5",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Life Regeneration",
                    amount = "+1hp/s",
                    simepleAmount = CardInfoStat.SimpleAmount.aLittleBitOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-50%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Spread",
                    amount = "+45%",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotOf
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-5%",
                    simepleAmount = CardInfoStat.SimpleAmount.slightlyLower
                }
            };
        }
        protected override CardCategory GetCategory()
        {
            return null;
        }
        protected override string GetModdableText()
        {
            return "of the five headed dog";
        }
    }
}
