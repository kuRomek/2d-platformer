using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class ItemPicker : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private PlayerStats _stats;

    private void Start()
    {
        _stats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Collectable item))
            Collect(item);
    }

    private void Collect(Collectable item)
    {
        _audioSource.PlayOneShot(item.PickUpSound);

        if (item is Medicine medicine)
            _stats.HealthPoints.Heal(medicine.HealPoints);

        _stats.GetItem(item, 1);

        Destroy(item.gameObject);
    }
}
