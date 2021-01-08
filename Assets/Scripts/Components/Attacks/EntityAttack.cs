using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{

    //Establecer variables.

    //Establecer estructuras.

    //Establecer enumeradores.

    //Establecer variables estaticas.

    //Publicas.

    //Privadas

    //Establecer variables.

    //Publicas.
    [Header("References")]

    [SerializeField] private Transform m_parent = null;
    [SerializeField] private GameObject m_meleeAttack = null;
    [SerializeField] private GameObject m_rangeAttack = null;
    [SerializeField] protected Weapon m_weapon = null;
    [SerializeField] private string m_tagToCompare = null;

    //Privadas.


    //Funciones

    //Funciones de MonoBehaviour.

    //Funciones privadas.

    //Funciones publicas.
    protected Transform GetParentTransform() => m_parent;
    public virtual void SetWeapon(Weapon weapon) => m_weapon = weapon;
        

    //Funciones heredadas.

    //Funciones ha heredar.
    public virtual void Attack()
    {
        GameObject attack = null;

        if (m_weapon.GetWeaponType() == Weapon.WeaponType.Melee)
        {
            attack = Instantiate(m_meleeAttack);
        }
        else
        {
            attack = Instantiate(m_rangeAttack);
        }

        List<EntityBrain> m_brains = new List<EntityBrain>();

        foreach (EntityBrain brain in FindObjectsOfType<EntityBrain>())
        {
            if (brain.gameObject.tag == m_tagToCompare)
            {
                m_brains.Add(brain);
            }
        }

        attack.GetComponent<DamageBase>().Create(m_tagToCompare, m_parent, m_weapon, m_brains);

    }

}
