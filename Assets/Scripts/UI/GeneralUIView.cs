using TMPro;
using UnityEngine;

public class GeneralUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinCountView;
    [SerializeField] private TextMeshProUGUI _gemCountView;
    [SerializeField] private Animator _vampirismDurationBar;
    [SerializeField] private Animator _vampirismCooldown;
    [SerializeField] private PlayerInfo _player;

    private void OnEnable()
    {
        _player.Stats.OnItemsCountUpdate += UpdateItems;
        _player.Stats.Vampirism.OnSkillUsed += UpdateUsedSkills;
        _player.Stats.Vampirism.OnSkillEnd += UpdateInteruptedSkills;
        _player.Stats.Vampirism.OnCooldownReset += UpdateSkillsCooldown;
    }

    private void OnDisable()
    {
        _player.Stats.OnItemsCountUpdate -= UpdateItems;
        _player.Stats.Vampirism.OnSkillUsed -= UpdateUsedSkills;
        _player.Stats.Vampirism.OnSkillEnd -= UpdateInteruptedSkills;
        _player.Stats.Vampirism.OnCooldownReset -= UpdateSkillsCooldown;
    }

    public void UpdateItems()
    {
        _coinCountView.text = $"x{_player.Stats.CoinCount}";
        _gemCountView.text = $"x{_player.Stats.GemCount}";
    }

    public void UpdateUsedSkills()
    {
        _vampirismCooldown.SetTrigger(Skill.Used);
        _vampirismCooldown.speed = 1f / _player.Stats.Vampirism.Cooldown;

        _vampirismDurationBar.SetTrigger(Skill.Used);
        _vampirismDurationBar.speed = 1f / _player.Stats.Vampirism.DurationSeconds;
    }

    public void UpdateInteruptedSkills()
    {
        _vampirismDurationBar.SetTrigger(Skill.Restore);
    }

    public void UpdateSkillsCooldown()
    {
        _vampirismCooldown.SetTrigger(Skill.Restore);
    }
}
