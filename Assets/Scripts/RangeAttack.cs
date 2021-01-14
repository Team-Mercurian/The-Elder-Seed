using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : DamageBase
{

    public override bool DoDamage(GameObject gameObject, Knockback knockback, int dmg)
    {
        if (Vector3.Distance(gameObject.transform.position, m_parent.position) > GetWeapon().GetRange()) return false;

        gameObject.GetComponent<EntityHealth>().GetDamage(dmg, knockback);
        return true;
    }

}
