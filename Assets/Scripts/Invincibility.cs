using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    protected override void PowerUp(Player player)
    {
        Debug.Log("invincible powerup!");
        player._isInvincible = true;
        player.ChangeColor(Color.white);
    }

    protected override void PowerDown(Player player)
    {
        Debug.Log("invincible powerdown!");
        player._isInvincible = false;
        player.RevertColor();
    }
}
