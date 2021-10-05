using SinafProduction.Essentials;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using TMPro;
using System;

namespace SinafProduction.Windows.Types
{
	[Obsolete("Ne pas utiliser tant que WebManager est obsolète.")]
	public class InstalleWindow : Window
	{
		[SerializeField] private GameObject buttons;
		[SerializeField] private GameObject sliderObj;

		[SerializeField] private TextMeshProUGUI message;
		[SerializeField] private TextMeshProUGUI noneMessage;
		[SerializeField] private TextMeshProUGUI cancelMessage;
		[SerializeField] private TextMeshProUGUI installeMessage;
		[SerializeField] private TextMeshProUGUI onProgressMessage;
		[SerializeField] private TextMeshProUGUI sliderMessage;
		
		[SerializeField] private Button installeButton;
		[SerializeField] private Button cancelButton;
		[SerializeField] private Button noneButton;

		[SerializeField] private Slider slider;
		
		private Canvas canvas;
		private WebClient client;
		private Action onCompleted;
		
		private void Awake()
		{
			canvas = GetComponent<Canvas>();
			
			onClose = () => canvas.enabled = false;
			onOpen = () => canvas.enabled = true;
			
			sliderObj.SetActive(false);
			buttons.SetActive(true);
		}

		public void SetMessage(string message) => this.message.text = message;

		public void SetButtonSlider(string sliderLabel, string cancelLabel, UnityAction onClick = null)
		{
			sliderMessage.text = sliderLabel + ":";
			cancelMessage.text = cancelLabel;
			cancelButton.onClick.AddListener(onClick ?? Close);
			cancelButton.onClick.AddListener(() => client.CancelAsync());
		}

		public void SetButtonNone(string label, UnityAction onClick = null)
		{
			noneMessage.text = label;
			noneButton.onClick.AddListener(onClick ?? Close);
		}

		public void SetButtonInstalle(string message, string label, string url, string path, Action onCompleted = null)
		{
			this.onCompleted = onCompleted;
			installeMessage.text = label;
			installeButton.onClick.AddListener(() =>
			{
				this.message.text = message;
				client = WebManager.DownloadFile(url, path, Progress, Completed);
				
				sliderObj.SetActive(true);
				buttons.SetActive(false);
			});
		}

		private void Progress(DownloadProgressChangedEventArgs downloadProgress)
		{
			slider.value = downloadProgress.ProgressPercentage;
			onProgressMessage.text = $" {downloadProgress.BytesReceived / 1000000f:0.00}/" +
									 $"{downloadProgress.TotalBytesToReceive / 1000000f:0.00}mo";
		}

		private void Completed(AsyncCompletedEventArgs downloadCompleted)
		{
			//var window = Windows.CreateWindow<MessageWindow>("DownloadFinish", )
			if (gameObject == null) return;

			if (isActive) Close();
			onCompleted();
			Destroy(gameObject);
		}

		private void OnDestroy() => client?.CancelAsync();
	}
}