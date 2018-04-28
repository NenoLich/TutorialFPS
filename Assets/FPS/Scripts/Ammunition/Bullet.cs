using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS
{
    public class Bullet : Ammunition
    {
        [SerializeField]
        private float _trailTime = 0.05f;

        private TrailRenderer _trailRenderer;

        protected override float Damage
        {
            get
            {
                return _damage == 0 ? _damage = 20f : _damage;
            }
        }

        protected override float Mass
        {
            get
            {
                return _mass == 0 ? _mass = 0.01f : _mass;
            }
        }

        protected override float TimeToRelease
        {
            get
            {
                return _timeToRelease == 0 ? _timeToRelease = 5f : _timeToRelease;
            }
        }

        protected override float DamageReductionMultiplier
        {
            get
            {
                return _damageReductionMultiplier == 0 ? _damageReductionMultiplier = 4f : _damageReductionMultiplier;
            }
        }

        protected override void SetVisibility(Transform objTransform, bool visible)
        {
            base.SetVisibility(objTransform, visible);
            _trailRenderer.time = visible ? _trailTime : 0f;
        }

        protected override void Awake()
        {
            base.Awake();
            _trailRenderer = GetComponentInChildren<TrailRenderer>(true);
            IsVisible = false;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            Release();
        }
    }
}