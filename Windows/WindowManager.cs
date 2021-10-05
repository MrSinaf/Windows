using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace SinafProduction.Windows
{
	public class WindowManager : MonoBehaviour
	{
		internal static int numberWindows = 0;
		public static bool cursorOnAWindow { get; internal set; }
		internal static Canvas canvas { get; private set; }

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			canvas = Instantiate(Resources.Load<Canvas>("Windows"));
			canvas.name = "[Windows]";
			DontDestroyOnLoad(canvas);

			SceneManager.sceneLoaded += (scene, mode) => CloseAllWindows();
		}

		/// <summary>
		/// Crée une fenêtre officiel.
		/// </summary>
		/// <param name="name">Nom de la fenêtre.</param>
		/// <param name="action">Si la fenêtre existe déjà.</param>
		/// <param name="title">Titre de la fenêtre.</param>
		/// <param name="window">Retourn la fenêtre créée.</param>
		/// <typeparam name="T">Type de fenêtre (dérivant de la class Window).</typeparam>
		/// <returns>Retourne si la fenêtre à été créée.</returns>
		public static bool CreateWindow<T>(string name, WindowExist action, string title, out T window) where T : Window
		{
			if (!WindowExistAction(name, action, out window)) return false;
			
			window = Instantiate(Resources.Load<T>(typeof(T).Name), canvas.transform);
			window.name = name;
			window.SetTitle(title);
			window.OnCreate();

			return true;
		}

		/// <summary>
		/// Crée une fenêtre customisée.
		/// </summary>
		/// <param name="name">Nom de la fenêtre.</param>
		/// <param name="action">Si la fenêtre existe déjà.</param>
		/// <param name="prefabPath">Chemin du préfabriqué.</param>
		/// <param name="window">Retourn la fenêtre créée.</param>
		/// <typeparam name="T">Type de fenêtre (dérivant de la class Window).</typeparam>
		/// <returns>Retourne si la fenêtre à été créée.</returns>
		public static bool CreateCustomWindow<T>(string name, WindowExist action, string prefabPath, out T window) where T : Window
		{
			if (!WindowExistAction(name, action, out window)) return false;

			window = Instantiate(Resources.Load<T>(prefabPath), canvas.transform);
			window.name = name;
			window.OnCreate();

			return true;
		}
		
		/// <summary>
		/// Récupère la fenêtre demandé.
		/// </summary>
		/// <param name="name">Nom de la fenêtre à récupérer.</param>
		/// <param name="currentWindow">Sortie de la fenêtre trouvée.</param>
		/// <returns>Retourne si la fenêtre à été trouvée.</returns>
		public static bool GetWindow<T>(string name, out T currentWindow) where T : Window
		{
			for (var i = 0; i < canvas.transform.childCount; i++)
			{
				var window = canvas.transform.GetChild(i);
				if (window.name == name)
				{
					currentWindow = window.GetComponent<T>();
					return true;
				}
			}

			currentWindow = null;
			return false;
		}

		/// <summary>
		/// Récupère la dernière fenêtre active.
		/// </summary>
		/// <param name="currentWindow">Sortie de la fenêtre trouvée.</param>
		/// <returns>Retourne si une fenêtre active à été trouvée.</returns>
		public static bool GetFocusWindow(out Window currentWindow)
		{
			var windows = canvas.GetComponentsInChildren<Window>();
			for (var i = windows.Length - 1; i >= 0; i--)
				if (windows[i].isActive)
				{
					currentWindow = windows[i];
					return true;
				}

			currentWindow = null;
			return false;
		}

		/// <summary>
		/// Vérifie si la fenêtre demandée existe.
		/// </summary>
		/// <param name="name">Nom de la fenêtre à vérifier.</param>
		/// <returns>Retourne si elle existe.</returns>
		public static bool WindowIsActif(string name)
		{
			for (var i = 0; i < canvas.transform.childCount; i++)
			{
				var window = canvas.transform.GetChild(i);
				if (window.name == name)
					return window.GetComponent<Window>().isActive;
			}
			
			return false;
		}

		/// <summary>
		/// Vérifie si au moins une fenêtre est active.
		/// </summary>
		/// <returns>Retourne si au moins une fenêtre est active.</returns>
		public static bool WindowIsActif() => numberWindows != 0;

		/// <summary>
		/// Ferme une fenêtre donnée.
		/// </summary>
		/// <param name="name">Nom de la fenêtre.</param>
		public static void CloseWindow(string name)
		{
			for (var i = 0; i < canvas.transform.childCount; i++)
			{
				var window = canvas.transform.GetChild(i);
				if (window.name == name)
				{
					window.GetComponent<Window>().Close();
					return;
				}
			}
		}

		/// <summary>
		/// Ferme toute les fenêtres actives.
		/// </summary>
		public static void CloseAllWindows()
		{
			var windows = canvas.GetComponentsInChildren<Window>();
			foreach (var window in windows)
				if (window.isActive)
					window.Close();
		}

		/// <summary>
		/// Détruit une fenêtre donnée.
		/// </summary>
		/// <param name="name">Nom de la fenêtre à détruire.</param>
		public static void DestroyWindow(string name)
		{
			// Evite une erreur classique:
			if (!canvas) return;

			for (var i = 0; i < canvas.transform.childCount; i++)
			{
				var window = canvas.transform.GetChild(i);
				if (window.name == name)
				{
					Destroy(window.gameObject);
					return;
				}
			}
		}

		/// <summary>
		/// Détruit toute les fenêtes actives.
		/// </summary>
		public static void DestroyAllWindows()
		{
			var windows = canvas.GetComponentsInChildren<Window>();
			foreach (var window in windows)
				if (window.isActive)
					Destroy(window);
		}

		private static bool WindowExistAction<T>(string name, WindowExist action, out T window) where T : Window
		{
			window = null;
			switch (action)
			{
				case WindowExist.Continue:
					return true;
				case WindowExist.Delete:
					DestroyWindow(name);
					return true;
				case WindowExist.Get:
					if (GetWindow(name, out window))
					{
						window.Focus();
						return false;
					}

					return true;
				default:
					throw new ArgumentOutOfRangeException(nameof(action), action, null);
			}
		}
	}
}