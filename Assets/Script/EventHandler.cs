using UnityEngine;

public delegate void OnPlayerHitDelegate(int damage);

public static class EventHandler
{
    public static event OnPlayerHitDelegate OnPlayerHitEvent;

    public static void CallOnPlayerHitEvent(int damage)
    {
        if (OnPlayerHitEvent != null)
            OnPlayerHitEvent(damage);
    }

}