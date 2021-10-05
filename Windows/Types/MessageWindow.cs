using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace SinafProduction.Windows.Types
{
	/// <summary>
	/// Affiche une fenêtre contenant un message.
	/// </summary>
	public class MessageWindow : Window
	{
		[SerializeField] private TextMeshProUGUI messageMeshPro;
		[SerializeField] private TextMeshProUGUI labelButton;
		[SerializeField] private Button button;

		/// <summary>
		/// Assigne le message.
		/// </summary>
		/// <param name="message">Message à afficher.</param>
		public void SetMessage(string message) => messageMeshPro.text = message;

		/// <summary>
		/// Assigne le bouton.
		/// </summary>
		/// <param name="label">Texte du bouton.</param>
		/// <param name="onClick">Action du bouton quand on clique.</param>
		public void SetButton(string label, UnityAction onClick = null)
		{
			labelButton.text = label;
			button.onClick.AddListener(onClick ?? Close);
		}
	}
}