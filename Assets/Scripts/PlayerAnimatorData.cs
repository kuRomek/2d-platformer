using static UnityEngine.Animator;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static int AnimState = StringToHash(nameof(AnimState));
        public static int AirSpeedY = StringToHash(nameof(AirSpeedY));
        public static int Grounded = StringToHash(nameof(Grounded));
        public static int Jump = StringToHash(nameof(Jump));
    }
}
