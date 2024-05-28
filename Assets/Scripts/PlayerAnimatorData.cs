using static UnityEngine.Animator;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int AnimState = StringToHash(nameof(AnimState));
        public static readonly int AirSpeedY = StringToHash(nameof(AirSpeedY));
        public static readonly int Grounded = StringToHash(nameof(Grounded));
        public static readonly int Jump = StringToHash(nameof(Jump));
    }
}
