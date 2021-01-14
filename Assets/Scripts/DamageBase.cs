using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageBase : MonoBehaviour
{
    Weapon m_weapon;
    private string m_tag;
    protected Transform m_parent;
    private bool m_used = false;
    private bool m_useWeapon = false;

    public void Create(string tag, Transform parent, Weapon weapon, List<EntityBrain> brains, bool useWeapon)
    {
        m_tag = tag;
        m_parent = parent;
        m_weapon = weapon;
        m_useWeapon = useWeapon;
        Attack(brains);
    }
    public virtual void Attack(List<EntityBrain> brains)
    {
        EntityBrain closestBrain = null;
        float minDistance = 10000;
        foreach (EntityBrain brain in brains)
        {
            float distance = Mathf.Abs(Vector3.Distance(brain.transform.position, m_parent.position));
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
    public abstract bool DoDamage(GameObject gameObject, Knockback knockback, int dmg);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_tag))
        {
            Vector3 dir1 = (other.transform.position - m_parent.position).normalized;
            Vector2 dir2 = new Vector2(dir1.x, dir1.z);
            Knockback m_knockback = new Knockback(dir2, m_weapon.GetKnockbackForce(), m_weapon.GetKnockbackTime());

            int m_uses = m_parent == PlayerBrain.GetSingleton().transform ? DataSystem.GetSingleton().GetDungeonData().GetActualWeapon().GetUses() : m_weapon.GetUses();

            if (DoDamage(other.gameObject, m_knockback, m_weapon.GetCalculatedDamage(m_uses, true))) 
            {
                UseWeapon();
            }
        }
    }

    protected void UseWeapon()
    {
        if (m_useWeapon && !m_used) {

            DungeonData m_ds = DataSystem.GetSingleton().GetDungeonData();
            m_ds.UseWeapon();
            SelectedWeaponUI.GetSingleton().SetData(DataSystem.GetSingleton().GetActualWeapon(), m_ds.GetActualWeapon().GetUses());
            m_used = true;
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
