public static class ElementBehaviorUtil
{
    public static void DealDamageToEverything(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, null);
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, Player.Instance);
        Player.Instance.Minion.Stats.TakeDamage(value, damageType, null);
        ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Large);
    }

    //  This needs to be addressed when theres more minion slot and enemy slots, you need to come up with a way to know which enemy class is which enemy
    //  Damage needs to be done passing in a "source" character
    public static void DealDamageToEverythingExceptEnemy(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, null);
        Player.Instance.Minion.Stats.TakeDamage(value, damageType, null);
        ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Large);
    }
}
