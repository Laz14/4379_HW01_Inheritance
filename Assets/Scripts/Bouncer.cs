using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    [SerializeField] float _bounceForce = 10f;

    protected override void PlayerImpact(Player player)
    {
        base.PlayerImpact(player);
        base.ImpactFeedback();

        player.Bounce(this.transform, _bounceForce);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
