using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : HealthView
{
    private Slider _bar;
    private float _smoothBarUpdateSpeed = 0.085f;
    private Coroutine _updatingBar;

    private void Start()
    {
        _bar = GetComponent<Slider>();

        _bar.minValue = 0f;
        _bar.maxValue = HealthPoints.Max;
        _bar.value = HealthPoints.Amount;
    }

    public override void UpdateView()
    {
        if (_updatingBar != null)
            StopCoroutine(_updatingBar);

        _updatingBar = StartCoroutine(UpdateBar());
    }

    private IEnumerator UpdateBar()
    {
        while (Mathf.Approximately(_bar.value, HealthPoints.Amount) == false)
        {
            _bar.value = Mathf.Lerp(_bar.value, HealthPoints.Amount, _smoothBarUpdateSpeed);

            yield return null;
        }

        _bar.value = HealthPoints.Amount;

        if (_bar.value == _bar.minValue)
            gameObject.SetActive(false);
    }
}
