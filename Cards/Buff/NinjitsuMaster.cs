﻿using ClassesManagerReborn.Util;
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

namespace UnstableCards.Cards.Buff
{
    class NinjitsuMaster : CustomCard
    {
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = BuffClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.additionalBlocks = +2;
            block.forceToAdd = -15f;
            block.cdMultiplier = 0.55f;
            statModifiers.health = 0.85f;
            statModifiers.movementSpeed = 1.3f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gunAmmo.maxAmmo = 0;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle()
        {
            return "Ninjitsu Master";
        }
        protected override string GetDescription()
        {
            return "Dash backwards and unleash the true art of ninjitsu!";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.NinjitsuMasterArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Addtional Blocks",
                    amount = "+2",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "-45%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "health",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.lower
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Ammo",
                    amount = "resets",
                    simepleAmount = CardInfoStat.SimpleAmount.aLotLower
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
