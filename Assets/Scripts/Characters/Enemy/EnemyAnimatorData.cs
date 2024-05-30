using static UnityEngine.Animator;

public static class EnemyAnimatorData
{
    public static class Params
    {
        public static readonly int AnimState = StringToHash(nameof(AnimState));
        public static readonly int Attack = StringToHash(nameof(Attack));
        public static readonly int Hurt = StringToHash(nameof(Hurt));
        public static readonly int Die = StringToHash(nameof(Die));
        public static readonly int Jump = StringToHash(nameof(Jump));
    }
}
