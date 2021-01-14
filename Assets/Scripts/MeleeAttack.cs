using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : DamageBase
{
    public float m_distanceToAttack;

    public override bool DoDamage(GameObject gO, Knockback knockback, int dmg)
    {
        if (Vector3.Distance(gO.transform.position, m_parent.position) > m_distanceToAttack) return false;

        gO.GetComponent<EntityHealth>().GetDamage(dmg, knockback);
        return true;

    }
}
