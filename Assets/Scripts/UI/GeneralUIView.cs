using TMPro;
using UnityEngine;

public class GeneralUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinCountView;
    [SerializeField] private TextMeshProUGUI _gemCountView;
    [SerializeField] private PlayerInfo _player;

    private void OnEnable()
    {
        _player.Stats.OnItemsCountUpdate += UpdateItems;
    }

    private void OnDisable()
    {
        _player.Stats.OnItemsCountUpdate -= UpdateItems;
    }

    public void UpdateItems()
    {
        _coinCountView.text = $"x{_player.Stats.CoinCount}";
        _gemCountView.text = $"x{_player.Stats.GemCount}";
    }
}
