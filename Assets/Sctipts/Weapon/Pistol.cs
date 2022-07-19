using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Soot(Transform sootPoint)
    {
        Instantiate(Bullet, sootPoint.position, Quaternion.identity);
    }
}
