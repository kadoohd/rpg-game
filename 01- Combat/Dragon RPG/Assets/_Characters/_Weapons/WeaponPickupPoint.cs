﻿using RPG.Characters;
using System;
using UnityEngine;

namespace RPG.Characters
{
    [ExecuteInEditMode]
    public class WeaponPickupPoint : MonoBehaviour
    {
        [SerializeField] WeaponConfig weaponConfig;
        [SerializeField] AudioClip pickupSFX;
        AudioSource audioSource;

        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!Application.isPlaying) // if the game is not playing
            {
                DestroyChildren();
                InstantiateWeapon();
            }
        }

        void DestroyChildren()
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        void InstantiateWeapon()
        {
            var weapon = weaponConfig.GetWeaponPrefab();
            weapon.transform.position = Vector3.zero;
            Instantiate(weapon, gameObject.transform);
        }

        void OnTriggerEnter()
        {
            // pick up weapon only if it's player
            FindObjectOfType<PlayerControl>().GetComponent<WeaponSystem>().PutWeaponInHand(weaponConfig);
            audioSource.PlayOneShot(pickupSFX);
        }
    }
}