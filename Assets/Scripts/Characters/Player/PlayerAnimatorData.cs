using static UnityEngine.Animator;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int AnimState = StringToHash(nameof(AnimState));
        public static readonly int AirSpeedY = StringToHash(nameof(AirSpeedY));
        public static readonly int Grounded = StringToHash(nameof(Grounded));
        public static readonly int Jump = StringToHash(nameof(Jump));
        public static readonly int LightAttack = StringToHash(nameof(LightAttack));
        public static readonly int HeavyAttack = StringToHash(nameof(HeavyAttack));
        public static readonly int Hurt = StringToHash(nameof(Hurt));
        public static readonly int Die = StringToHash(nameof(Die));
    }
}
