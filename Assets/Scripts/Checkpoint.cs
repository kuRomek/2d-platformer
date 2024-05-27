using UnityEngine;

public class Checkpoint : MonoBehaviour 
{
    private void Awake()
    {
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
            spriteRenderer.enabled = false;
    }
}
