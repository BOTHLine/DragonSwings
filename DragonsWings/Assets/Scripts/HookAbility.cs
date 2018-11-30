﻿using UnityEngine;

public class HookAbility : MonoBehaviour
{
    // Components
    public Hook _Hook;

    // References
    public FloatReference _HookRange;
    public FloatReference _HookSpeed;

    public FloatReference _PullSpeed;

    public FloatReference _Damage;

    public Vector2Reference _TargetPosition;

    public HookResponderVariable _AttachedHookResponder;

    // Events
    public GameEvent OnHookShoot;
    public GameEvent OnHookHit;
    public GameEvent OnHookReset;

    public GameEvent OnPullStart;
    public GameEvent OnPullFinish;

    public GameEvent OnObjectPickedUp;

    // Variables
    private bool _HookIsFlying;

    // Mono Behaviour
    private void Awake()
    {
        _Hook.Initialize(this, _HookSpeed);
    }

    private void FixedUpdate()
    {
        if (_HookIsFlying && ((Vector2)_Hook.transform.position).SquaredDistanceTo(transform.position) >= _HookRange * _HookRange)
        { _Hook.FlyBack(); }
    }

    // Methods
    public void ShootHook()
    {
        if (_HookIsFlying || _TargetPosition.Value.Equals(transform.position)) { return; }

        if (_AttachedHookResponder?.Value == null)
        {
            _HookIsFlying = true;
            _Hook.Shoot(_TargetPosition.Value);
            OnHookShoot.Raise();
        }
    }

    public void HookHitSomething(Collider2D collider)
    {
        HookResponder hookResponder = collider.GetComponentInSiblings<HookResponder>();
        if (hookResponder != null)
        {
            hookResponder.HitByHook();
            switch (hookResponder._Weight)
            {
                case Weight.None:
                    _Hook.ResetVelocity();
                    break;
                case Weight.Light:
                    _Hook.AttachHookResponder(hookResponder);
                    _Hook.FlyBack();
                    break;
                case Weight.Medium:
                    _PullSpeed.Value = _HookSpeed / 2.0f;
                    _Hook.AttachHookResponder(hookResponder);
                    OnPullStart.Raise();
                    _Hook.FlyBack();
                    break;
                case Weight.Heavy:
                    _PullSpeed.Value = _HookSpeed;
                    _Hook.StopHook();
                    OnPullStart.Raise();
                    break;
            }
        }
        else
        { _Hook.FlyBack(); }
        OnHookHit.Raise();
    }

    public void HookReachedPlayer()
    {
        OnPullFinish.Raise();
        PickUpHookResponder();
        ResetHook();
    }

    private void PickUpHookResponder()
    {
        HookResponder hookResponder = _Hook.DetachHookResponder();

        if (hookResponder?._Weight == Weight.Light)
        {
            hookResponder.AttachToObject(transform);
            _AttachedHookResponder.Value = hookResponder;
            OnObjectPickedUp.Raise();
        }
    }

    public void ResetHook()
    {
        _HookIsFlying = false;

        _Hook.Reset();
        OnHookReset.Raise();
    }
}