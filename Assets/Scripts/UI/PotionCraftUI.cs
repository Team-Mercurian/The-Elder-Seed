using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionCraftUI : PanelUI {
	
	//Singleton
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//Non Static
		[Header("References")]
		[SerializeField] private TextMeshProUGUI m_selectionText = null;

		[Space]
		[SerializeField] private TextMeshProUGUI m_countText = null;
        [SerializeField] private Button m_subtractButton = null;
        [SerializeField] private Button m_addButton = null;
		
		[Space]
		[SerializeField] private Button m_confirmButton = null;

		[Space]
		[SerializeField] private PotionCraftPlantUI m_plantCommon = null;
		[SerializeField] private PotionCraftPlantUI m_plantRare = null;
		[SerializeField] private PotionCraftPlantUI m_plantEpic = null;
		[SerializeField] private PotionCraftPlantUI m_plantLegendary = null;
	
		private PotionCraftPlantUI m_selectedPlant = null;

		private int m_count;
		private int m_plants;

		private string m_savedText;

    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
		public override void Open() {
			
			base.Open();
			m_selectedPlant = m_plantCommon;
            InputController.SetLookObject(null);
			GameSystem.SetUI(this);
			SetData();
			}
		public override void Close() {

			base.Close();
            InputController.SetLookObject(CameraController.GetSingleton());
			GameSystem.SetUI(null);
			}

		public void Add() {
			
			if (m_count + 1 > m_plants) return;
			ChangeCount(m_count + 1);
			}
		public void Subtract() {

			if (m_count - 1 < 0) return;
			ChangeCount(m_count - 1);
			}
		public void Cancel() {

			Close();
			}
		public void Confirm() {
			
			if (m_count == 0 || m_count > m_plants) return;
			
			Plant m_plant = m_selectedPlant.GetPlant();
			int m_potionID = DataSystem.GetSingleton().GetPotions().Find(c => c.GetRarity() == m_plant.GetRarity()).GetID();
			Potion m_potion = DataSystem.GetSingleton().GetPotion(m_potionID);

			DataSystem.GetSingleton().GetGameData().GetInventoryData().AddPotion(m_potionID, m_count);
			DataSystem.GetSingleton().GetGameData().GetInventoryData().AddPlant(m_plant.GetID(), -m_count);
			SaveSystem.Save();

			string m_a = "¡Has creado " + m_count + " " + (m_count == 1 ? "pocion" : "pociones") + " de curacion!";
			string m_b = "La " + (m_potion.GetName().ToLower()) + " regenera un " + m_potion.GetHealPercent() + "% de tu vida."; 
			string m_bRich = "<size=16><color=#ffffff75>" + m_b + "</size></color>";

			string m_str = m_a + "\n\n" + m_bRich;

			WarningPanelUI.GetSingleton().SetData(m_str, "Cerrar");
			WarningPanelUI.GetSingleton().Open();

            if (DataSystem.GetSingleton().GetGameData().GetTutorialIndex() < 10) 
                TutorialController.GetSingleton().SetTutorialText(10);
			SetData();
			}

		public void SelectPlant(PotionCraftPlantUI selectedPlant, int plantCount) {

			m_selectedPlant = selectedPlant;
			m_plants = plantCount;
			
			m_savedText = "Seleccionado: " + selectedPlant.GetPlant().GetName();
			m_selectionText.text = m_savedText;
			
			ChangeCount(m_count);

			if (m_plantCommon == selectedPlant) m_plantCommon.Select();
			else m_plantCommon.UnSelect();
			
			if (m_plantRare == selectedPlant) m_plantRare.Select();
			else m_plantRare.UnSelect();
			
			if (m_plantEpic == selectedPlant) m_plantEpic.Select();
			else m_plantEpic.UnSelect();
			
			if (m_plantLegendary == selectedPlant) m_plantLegendary.Select();
			else m_plantLegendary.UnSelect();
			}
        
		public void HoverPlant(string text) => m_selectionText.text = text; 
		public void UnHoverPlant() => m_selectionText.text = m_savedText; 

		//Private Functions
        private void ChangeCount(int count) {
			
			m_count = Mathf.Clamp(count, 0, m_plants);
			m_countText.text = count.ToString();

			m_addButton.interactable = m_count < m_plants;
			
			m_subtractButton.interactable = m_count > 0;
			m_confirmButton.interactable = m_count > 0;
			}
		private void SetData() {
			
			m_plantCommon.SetData();
			m_plantRare.SetData();
			m_plantEpic.SetData();
			m_plantLegendary.SetData();
			m_count = 0;

			if (m_selectedPlant.GetCount() == 0) m_selectedPlant = m_plantCommon;
			m_selectedPlant.SelectPlant();
			}

        
	//Coroutines
	
	}
