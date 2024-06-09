using System;
using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public static int Used = Animator.StringToHash(nameof(Used));
    public static int Restore = Animator.StringToHash(nameof(Restore));

    [SerializeField] private float _cooldown;

    public float Cooldown => _cooldown;
    public bool IsReady { get; private set; } = true;
    private Coroutine _waitingCooldown;
    private Coroutine _executing;

    public event Action OnCooldownReset;

    public void Use()
    {
        _executing = StartCoroutine(Execute());
    }

    protected virtual IEnumerator Execute()
    {
        _waitingCooldown = StartCoroutine(WaitCooldown());

        yield return new WaitForSeconds(0f);
    }

    private IEnumerator WaitCooldown()
    {
        IsReady = false;
        yield return new WaitForSeconds(Cooldown);
        IsReady = true;
        OnCooldownReset?.Invoke();
    }

    public virtual void Interrupt()
    {
        if (_executing != null)
            StopCoroutine(_executing);
    }

    public void ResetCooldown()
    {
        if (_waitingCooldown != null)
        {
            StopCoroutine(_waitingCooldown);
            IsReady = true;
        }
    }
}
