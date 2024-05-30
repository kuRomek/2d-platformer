using UnityEngine;

public class Medicine : Collectable
{
    [SerializeField] private float _healPoints;

    public float HealPoints => _healPoints;
}
