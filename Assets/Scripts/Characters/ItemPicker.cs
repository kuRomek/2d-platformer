using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private CharacterInfo _character;

    private void Start()
    {
        _character = GetComponent<CharacterInfo>();
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
            _character.BaseStats.AddHP(medicine.HealPoints);

        Destroy(item.gameObject);
    }
}
