# Notes

## JSON files

Le fichier json, pour être disponible dans le programme compilé, doit avoir la propriété `Copier dans le répertoire de sortie` : `Toujours copier`.

Dans Visual Studio, le fichier json de travail **n'est pas** modifié lorsque je teste l'application. En effet, puisque le fichier json est copié, alors VS pointe sur la copie (voir dans le dossier Bin/Debug par exemple). Ceci vaut lors du développement et non pas en usage courant de l'application ; lorsque je lance l'application en dehors de Visual Studio, alors je constate qu'elle traite bien le fichier json qui l'accompagne et les données sont persistantes.