using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace SinafProduction.Windows.Types
{
	/// <summary>
	/// Affiche une fenêtre contenant une information.
	/// </summary>
	public class InfosWindow : Window
	{
		[SerializeField] private TextMeshProUGUI messageMeshPro;
		[SerializeField] private TextMeshProUGUI labelButton;
		[SerializeField] private Button button;

		/// <summary>
		/// Assigne le message.
		/// </summary>
		/// <param name="message">Message à afficher.</param>
		/// <param name="type">Type de message.</param>
		public void SetMessage(string message, InfosType type)
		{
			messageMeshPro.text = message;
			ChangeColorTitle(type switch
			{
				InfosType.Info    => Color.green,
				InfosType.Problem => Color.yellow,
				InfosType.Error   => Color.red,
				_                 => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			});
		}

		/// <summary>
		/// Fixe l'alignement du message.
		/// </summary>
		/// <param name="alignmentOptions">Type d'alignement.</param>
		public void SetMessageAlignement(TextAlignmentOptions alignmentOptions) => messageMeshPro.alignment = alignmentOptions;
		
		/// <summary>
		/// Assigne le bouton.
		/// </summary>
		/// <param name="text">Texte du bouton.</param>
		/// <param name="action">Action du bouton.</param>
		public void SetButton(string text, UnityAction action = null)
		{
			labelButton.text = text;
			button.onClick.AddListener(action ?? Close);
		}
	}
	
	public enum InfosType { Info, Problem, Error }
}