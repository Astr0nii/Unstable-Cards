using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnstableCards
{
    internal class Assets
    {
        private static readonly AssetBundle Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("assets", typeof(UnstableCards).Assembly);

        public static AudioClip shrineOfTheDamnedAudio = Bundle.LoadAsset<AudioClip>("ShrineOfTheDamned");
        public static AudioClip shrineOfTheBalancedAudio = Bundle.LoadAsset<AudioClip>("ShrineOfTheBalanced");
        public static AudioClip shrineOfTheForgottenAudio = Bundle.LoadAsset<AudioClip>("ShrineOfTheForgotten");
    }
}
