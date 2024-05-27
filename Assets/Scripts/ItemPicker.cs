using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Collectable item))
            Collect(item);
    }

    private void Collect(Collectable item)
    {
        _audioSource.clip = item.PickUpSound;
        Destroy(item.gameObject);
        _audioSource.Play();
    }
}
