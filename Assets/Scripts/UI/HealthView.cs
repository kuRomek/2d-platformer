using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private HealthPoints _healthPoints;

    public HealthPoints HealthPoints => _healthPoints;

    private void OnEnable()
    {
        _healthPoints.OnValueChange += UpdateView;
    }

    private void OnDisable()
    {
        _healthPoints.OnValueChange -= UpdateView;
    }

    public abstract void UpdateView();
}
