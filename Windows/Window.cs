using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

namespace SinafProduction.Windows
{
	public class Window : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
	{
		[SerializeField] private TextMeshProUGUI titleTMP;
		protected RectTransform rectTransform;
		
		protected bool openOnCreate = true;
		public UnityAction onClose;
		public UnityAction onOpen;

		public bool isActive { get; private set; }

		public void OnPointerDown(PointerEventData eventData) => Focus();
		public void OnPointerEnter(PointerEventData eventData) => WindowManager.cursorOnAWindow = true;
		public void OnPointerExit(PointerEventData eventData) => WindowManager.cursorOnAWindow = false;

		internal protected void OnCreate()
		{
			rectTransform = GetComponent<RectTransform>();
			
			onClose ??= () => Destroy(gameObject);
			onOpen ??= () => { };

			Centered();
			if (openOnCreate) Open();
			else onClose();
		}
		
		/// <summary>
		/// Change le titre de la fenêtre.
		/// </summary>
		/// <param name="title"></param>
		public void SetTitle(string title) => titleTMP.text = title;
		
		/// <summary>
		/// Change la couleur de la fenêtre.
		/// </summary>
		/// <param name="color"></param>
		public void ChangeColorTitle(Color color) => titleTMP.color = color;

		/// <summary>
		/// Ouvre la fenêtre.
		/// </summary>
		public void Open()
		{
			if (isActive) return;

			WindowManager.numberWindows++;
			isActive = true;

			transform.SetAsLastSibling();

			onOpen();
		}

		/// <summary>
		/// Ferme la fenêtre.
		/// </summary>
		public void Close()
		{
			if (!isActive) return;
			
			WindowManager.numberWindows--;
			WindowManager.cursorOnAWindow = false;
			isActive = false;
			
			onClose();
		}

		public void Focus() => transform.SetAsLastSibling();

		public void SetSize(int width, int height) => rectTransform.sizeDelta = new Vector2(width, height);

		public void AddPosition(Vector3 delta) => transform.position += delta * WindowManager.canvas.scaleFactor;

		public void Centered() => transform.position = new Vector3(Screen.width, Screen.height) / 2;
	}
}