# TP_JsonFile

Lecture et écriture de fichiers locaux au format JSON avec différents types C#.

Les fichiers, pour être disponibles dans le programme compilé, doivent avoir la propriété `Copier dans le répertoire de sortie` : `Toujours copier`.

Dans Visual Studio, les fichiers JSON de travail **ne sont pas** modifiés lorsque l'application est testée. En effet, puisque les fichiers sont copiés, alors VS pointe sur la copie (voir dans le dossier `Bin/Debug`). Ceci vaut lors du développement et non pas en production ; une fois l'application lancée en dehors de Visual Studio, les fichiers JSON sont modifiés.
