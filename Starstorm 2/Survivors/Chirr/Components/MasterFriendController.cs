﻿using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace Starstorm2Unofficial.Survivors.Chirr.Components
{
    [RequireComponent(typeof(CharacterMaster))]
    public class MasterFriendController : MonoBehaviour
    {
        public uint masterNetID = NetworkInstanceId.Invalid.Value;
        public int[] masterItemStacks;
    }
}
