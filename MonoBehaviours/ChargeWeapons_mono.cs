using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnboundLib;
using Photon.Pun;
using System.Collections;
using ModsPlus;

namespace UnstableCards.MonoBehaviours
{
    [DisallowMultipleComponent]
    public class ChargeWeapons_mono : MonoBehaviour
    {
        private Player player;
        private CharacterData data;
        private Gun gun;
        private PhotonView view;
        private Coroutine syncRoutine;
        private CustomHealthBar chargeBar;

        private void Start()
        {
            
        }

        private void Update()
        {
            player = data.player;
            view = player.data.view;
            chargeBar.SetValues(gun.currentCharge, 1);
        }
    }
}
