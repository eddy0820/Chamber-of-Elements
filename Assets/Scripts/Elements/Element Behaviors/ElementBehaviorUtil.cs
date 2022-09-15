public static class ElementBehaviorUtil
{
    public static void DealDamageToEverything(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, null);
        GameManager.Instance.Enemy.Stats.TakeDamage(value, damageType, Player.Instance);

        if(Player.Instance.MinionExists)
        {
            Player.Instance.Minion.Stats.TakeDamage(value, damageType, null);
        }
        
        ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Large);
    }

    //  This needs to be addressed when theres more minion slot and enemy slots, you need to come up with a way to know which enemy class is which enemy
    //  Damage needs to be done passing in a "source" character
    public static void DealDamageToEverythingExceptEnemy(AffinityTypes damageType, float value)
    {
        Player.Instance.Stats.TakeDamage(value, damageType, null);

        if(Player.Instance.MinionExists)
        {
            Player.Instance.Minion.Stats.TakeDamage(value, damageType, null);
        }
        
        ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Large);
    }

    public static Character ConvertCharacterEntry(CharacterEntry characterEntry)
    {
        switch(characterEntry)
        {
            case CharacterEntry.Player:
                return Player.Instance;
            
            case CharacterEntry.Enemy:
                return GameManager.Instance.Enemy;
            case CharacterEntry.Minion:
                return Player.Instance.Minion;
            default:
                return null;
        }
    }
}
