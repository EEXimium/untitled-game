using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorch : Weapon
{

    protected override void Update()
    {
        if (this.transform.parent != null)
        {
            base.Update();
        }
    }
}
