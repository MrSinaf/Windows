using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace SinafProduction.Windows.Types
{
	/// <summary>
	/// Affiche une fenêtre avec un slider et deux boutons.
	/// </summary>
	public class SliderWindow : Window
	{
		[SerializeField] private Slider slider;
		[SerializeField] private Button buttonA;
		[SerializeField] private Button buttonB;
		[SerializeField] private TMP_InputField input;
		[SerializeField] private TextMeshProUGUI textButtonA;
		[SerializeField] private TextMeshProUGUI textButtonB;

		/// <summary>
		/// Assigne les valeurs du slider.
		/// </summary>
		/// <param name="currentValue">La valeur par default.</param>
		/// <param name="minValue">La valeur minimum.</param>
		/// <param name="maxValue">La valeur maximum.</param>
		public void SetSlider(int currentValue, int minValue, int maxValue)
		{
			slider.maxValue = maxValue;
			slider.minValue = minValue;
			slider.value = currentValue;
		}

		/// <summary>
		/// Assigne le bouton A.
		/// </summary>
		/// <param name="label">Texte du bouton.</param>
		/// <param name="onClick">Action du bouton quand on clique.</param>
		public void SetButtonA(string label, Action<int> onClick)
		{
			textButtonA.text = label;
			buttonA.onClick.AddListener(() => onClick((int) slider.value));
		}

		/// <summary>
		/// Assigne le bouton B.
		/// </summary>
		/// <param name="label">Texte du bouton.</param>
		/// <param name="onClick">Action du bouton quand on clique.</param>
		public void SetButtonB(string label, Action<int> onClick)
		{
			textButtonB.text = label;
			buttonB.onClick.AddListener(() => onClick((int) slider.value));
		}

		internal void OnValueChanged(float value) => input.text = $"{value}";

		internal void OnInputChanged(string value)
		{
			if (value != "")
				slider.value = float.Parse(value);
			input.text = $"{slider.value}";
		}
	}
}