using UnityEngine.EventSystems;
using UnityEngine;

namespace SinafProduction.Windows
{
	public class DragWindow : MonoBehaviour, IDragHandler, IEndDragHandler
	{
		private RectTransform target;
		private Canvas canvas;

		private void Start()
		{
			target = transform.parent.GetComponent<RectTransform>();
			canvas = WindowManager.canvas;
		}

		public void OnDrag(PointerEventData eventData) => target.anchoredPosition += eventData.delta / canvas.scaleFactor;

		public void OnEndDrag(PointerEventData eventData)
		{
			var position = target.position;
			var screenWidth = Screen.width;
			var screenHeight = Screen.height;

			if (position.x < 0 || position.y < 0 || position.x > screenWidth || position.y + target.sizeDelta.y / 2 > screenHeight)
			{
				var window = target.GetComponent<Window>();
				window.Centered();
				window.Close();
			}
		}
	}
}