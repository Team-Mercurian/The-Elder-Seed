using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageBase : MonoBehaviour
{
    Weapon m_weapon;
    private string m_tag;
    protected Transform m_parent;

    public void Create(string tag, Transform parent, Weapon weapon, List<EntityBrain> brains)
    {
        m_tag = tag;
        m_parent = parent;
        m_weapon = weapon;

        Attack(brains);
    }
    public virtual void Attack(List<EntityBrain> brains)
    {
        EntityBrain closestBrain = null;
        float minDistance = 10000;
        foreach (EntityBrain brain in brains)
        {
            float distance = Vector2.Distance(brain.transform.position, m_parent.position);
            if (distance < minDistance)
            {
                closestBrain = brain;
                minDistance = distance;
            }
        }

        StartCoroutine(AttackCoroutine());
        if (closestBrain == null) return;
        transform.position = closestBrain.transform.position;
        
    }
    public abstract void DoDamage(GameObject gameObject, Knockback knockback, int dmg);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_tag))
        {
            Vector3 dir1 = (other.transform.position - m_parent.position).normalized;
            Vector2 dir2 = new Vector2(dir1.x, dir1.z);
            Knockback m_knockback = new Knockback(dir2, m_weapon.GetKnockbackForce(), m_weapon.GetKnockbackTime());
            DoDamage(other.gameObject, m_knockback, m_weapon.GetCalculatedDamage(m_weapon.GetUses()));
        }
    }

    protected Weapon GetWeapon()
    {
        return m_weapon;
    }

    protected IEnumerator AttackCoroutine()
    {

        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
