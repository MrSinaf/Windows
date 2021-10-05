using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace SinafProduction.Windows.Types
{
	/// <summary>
	/// Affiche une fenêtre posant une question avec deux réponses possibles.
	/// </summary>
	public class QuestionWindow : Window
	{
		[SerializeField] private TextMeshProUGUI questionMeshPro;
		[SerializeField] private TextMeshProUGUI reponseAMeshPro;
		[SerializeField] private TextMeshProUGUI reponseBMeshPro;

		[SerializeField] private Button reponseAButton;
		[SerializeField] private Button reponseBButton;

		/// <summary>
		/// Assigne la question.
		/// </summary>
		/// <param name="question">Question à afficher.</param>
		public void SetQuestion(string question) => questionMeshPro.text = question;

		/// <summary>
		/// Assigne le boutton de réponse A.
		/// </summary>
		/// <param name="reponse">Texte du bouton.</param>
		/// <param name="onClick">Action du bouton.</param>
		public void SetReponseA(string reponse, UnityAction onClick = null)
		{
			reponseAMeshPro.text = reponse;
			reponseAButton.onClick.AddListener(onClick ?? Close);
		}

		/// <summary>
		/// Assigne le boutton de réponse B.
		/// </summary>
		/// <param name="reponse">Texte du bouton.</param>
		/// <param name="onClick">Action du bouton.</param>
		public void SetReponseB(string reponse, UnityAction onClick = null)
		{
			reponseBMeshPro.text = reponse;
			reponseBButton.onClick.AddListener(onClick ?? Close);
		}
	}
}