public static class ElementBehaviorUtil
{
    public static void DealDamageToEverything(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, Player.Instance, Player.Instance);
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, GameManager.Instance.Enemy, Player.Instance);
        Player.Instance.Minion.Stats.TakeDamage(value, damageType, Player.Instance.Minion, Player.Instance);
        ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
    }

    //  This needs to be addressed when theres more minion slot and enemy slots, you need to come up with a way to know which enemy class is which enemy
    //  Damage needs to be done passing in a "source" character
    public static void DealDamageToEverythingExceptEnemy(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, Player.Instance, Player.Instance);
        Player.Instance.Minion.Stats.TakeDamage(value, damageType, Player.Instance.Minion, Player.Instance);
        ScreenShakeBehavior.Instance.StartShake(1.5f, 0.8f, 7.5f);
    }
}
