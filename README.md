 # Présentation
Créer des fenêtres internes ou personnalisés de la meilleur manière possible.
Gérer vos fenêtres facilement, grâce à la class *WindowManager* qui vous permet principalement de :
* Ouvrir des fenêtres internes et personnalisées;
* Fermer et/ou détruire automatiquement toutes les fenêtres;
* Récupérer des fenêtres précises;
* Récupérer le nombre de fenêtre ouverte;

Sans oublier que plus de paramètre vous attends dans la class *Window*.
## Comment ça marche ?
L'objectif à été de simplifier au maximum la création des fenêtres quelle soit interne ou externe.
*WindowManager* vous permet de créer vos fenêtres, grâce à:
```csharp
WindowManager.CreateWindow<Type>(<name>, <action>, <title>, out <window>)
// ou:
WindowManager.CreateCustomWindow<Type>(<name>, <action>, <prefabPath>, out <window>)
```

Grâce à la sortie de la fenêtre via le dernier paramètre des deux méthodes, vous pouvez ensuite configurer à votre souhait votre fenêtre.
Voici un exemple avec la fenêtre interne *QuestionWindow*:
> ```csharp
> WindowManager.CreateWindow<QuestionWindow>("WantQuit", WindowExist.Get, "Quitter l'application", out var window);
> window.SetQuestion("Voulez-vous vraiment quitter l'application ?");
> window.SetReponseA("Oui", () => Application.Quit());
> window.SetReponseB("Non");
> ```

Les fenêtres customisés doivent dériver de la class *Window* et doivent avoir une prefab, la création de ce genre de fenêtre est similaire au fenêtre interne.
Sauf qu'a la place du titre, vous devez préciser le chemin de la prefab.

## Dépendances
1. UnityEngine
2. TextMeshPro

## Copyright
L'utilisation, la modification, le partage de ce plugin est autorisé à condition de préciser distinctement le créateur original en soit la Sinaf Production, comme suit:<br/>
©SinafProduction - 2021, All rights reserved
