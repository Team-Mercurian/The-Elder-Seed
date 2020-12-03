using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedSelector_SeedUI : MonoBehaviour {

    //Establecer variables.
    [Header("References")]
    [SerializeField] private Image m_image = null;
    [SerializeField] private TextMeshProUGUI m_textMesh = null;

    private bool m_selected;

    //Funciones publicas.
    public void SetData(Sprite icon, int count) {

        m_image.sprite = icon;
        m_textMesh.text = count.ToString();
        }
    public void Select() => SelectionAnimation(true);
    public void UnSelect() => SelectionAnimation(false);
        

    //Funciones privadas.
    private void SelectionAnimation(bool select) {

        if (m_selected == select) return;
        m_selected = select;
        
        m_image.transform.localScale = Vector3.one * (select ? 1.2f : 1f);
        }
    }
