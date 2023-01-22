# Groupe : 

BEN AZIZI Yassine, FOUCHER Olivier, MONANGE Marion, MONIN Pascal

# Project setup

Utilisation de Git, VSCode, Unity

## Git repository
Ecrivez la commande suivante dans un dossier
```
git clone https://github.com/epfmng/AIBaseProject.git
```

# Introduction au projet

Il existe deux versions du projet pour deux stratégies différentes :
- Une version simple sur la branche Simple
- Une version plus avancée sur la branche Advanced

# Réflexion stratégique et tactique :
Le premier élément qui a été considéré était de savoir s'il fallait mieux cibler en priorité les tourelles ou les drones.
Empiriquement, nous avons constaté rapidement que le ciblage des tourelles donnait de bien meilleurs résultats, et nous n'avons pas trouvé intéressant de dédier une partie des drones à cibler les autres drones. Donc le premier élément de notre tactique est que les drones ciblent tous les tourelles en premier, puis les drones.

Ensuite, nous avons constaté que, dans la stratégie de base, les drones changent de cible après chaque tir. Cela implique souvent un déplacement entre chaque tir, et ce déplacement peut être assez long car ils ciblent purement aléatoirement, donc la nouvelle cible peut être complètement de l'autre côté de la carte. On a donc rapidement implémenté le fait que les drones ne changent pas de cibles entre chaque tir, c'est à dire qu'ils vont continuer à courser puis tirer sur la même cible jusqu'à ce qu'elle meure. Cela a permis d'augmenter considérablement les dégâts infligés en réduisant le temps que passent les drones à se déplacer et en augmentant le nombre de tirs. La même logique a été appliquée aux tourelles : elles tirent sur la même cible jusqu'à sa mort, afin de limiter le temps passé en rotation entre chaque tir, même si ce temps perdu est beaucoup moins significatif sur les tourelles que sur les drones.

Naturellement, nous nous étions rendu compte qu'il n'est pas optimisé que les drones traversent la carte pour attaquer une nouvelle cible. On a donc implémenté des logiques de ciblage pour que, lorsque les drones cherchent une nouvelle cible, ils ciblent les ennemis les plus proches afin de réduire encore les temps de déplacement et augmenter le temps passer à infliger des dégâts.

A partir de ce point, nos deux stratégies se séparent : l'une est limitée à ce qui a été dit précédemment (on l'appellera stratégie "SIMPLE"), l'autre implémente d'autres éléments (on l'appellera "ADVANCED"). Il y a cependant une précision sur le ciblage des ennemis les plus proches : ADVANCED utilise un script qui choisit l'ennemi le plus proche, SIMPLE choisit aléatoirement dans une tranche de distance, qui augmente si aucune cible n'est trouvée. Cette différence a le résultat suivant : sous ADVANCED, les drones ont tendance à tous cibler les mêmes tourelles, alors que sous SIMPLE, les drones ont un peu plus tendance à se répartir. Cela a des avantages pour SIMPLE : les drones séparés sont moins vulnérables aux dégâts de zones des tourelles, et font perdre plus de temps aux drones verts qui changent de cible après chaque tir (si les drones rouges sont moins groupés, les drones verts perdent plus de temps). De plus, cette répartition des drones sur les tourelles ennemies réduit l'"overkill" par rapport à ADVANCED.

ADVANCED répond en plus à une autre problématique : les drones tirent, puis ils attendent une à deux secondes, puis ils se déplacent jusqu'à leur cible, puis ils tirent de nouveau. Nous avons considéré qu'il n'était pas contre les règles de l'exercice de permettre au drone de se déplacer pendant le cooldown de son arme, donc nous l'avons implémenté. Pour ce faire, nous avons rendu les logiques de déplacement et les logiques de tir complètement indépendantes.

ADVANCED comprend aussi un ciblage par distance qui priorise les tourelles les plus éloignées car les drones auront davantage de difficulté à les atteindre.

# Behavior Tree :
## Drones
### Version Simple
![Behavior tree Simple]

L'arbre complet

![Behavior tree Simple Left Part]

Zoom sur la partie gauche de l'arbre de décision

### Version Advanced
![Behavior tree Advanced]

L'arbre complet

![Behavior tree Advanced Left Part]

Zoom sur la partie gauche de l'arbre de décision

![Behavior tree Advanced Right Part]

Zoom sur la partie droite de l'arbre de décision

## Turrets
### Version Simple

![Behavior tree Turret Simple]

### Version Advanced

![Behavior tree Turret Advanced]

# Scripts C# :
### Version Advanced
GetClosestDrone

![GetClosestDrone]

GetClosestTurret

![GetClosestTurret]

GetFurthestTurret

![GetFurthestTurret]

myIsWithinDistanceDrone

![myIsWithinDistanceDrone]

myIsWithinDistanceTurret

![myIsWithinDistanceTurret]

myIsWithinAngle

![myIsWithinAngle]

# Remarques :
## Bugs connus :
Pour les deux stratégies, les drones ont tendance à se bloquer dans le relief du mesh. S'ils doivent se rendre sur un point en hauteur (par exemple s'ils ciblent une tourelle du fond), ils ont tendance à se déplacer pas par pas, ce qui les ralentit sensiblement.

## Voies d'amélioration :
Nous n'avons pas exploré gestion de comportements au niveau de l'armée qui pourrait notamment être utile pour réduire l'overkill des tourelles.

# Résultats :
Les fichiers suivants correspondent à un échantillon de 20 batailles pour un point de vue de la stratégie employée. 

[simple.tsv](simple.tsv)

[advanced.tsv](advanced.tsv)


[Behavior tree Simple]: .README/icons/Behavior_tree_simple_complete.png
[Behavior tree Simple Left Part]: .README/icons/Behavior_tree_simple_left.png
[Behavior tree Advanced]: .README/icons/Behavior_tree_advanced.png
[Behavior tree Advanced Left Part]: .README/icons/Behavior_tree_advanced_left.png
[Behavior tree Advanced Right Part]: .README/icons/Behavior_tree_advanced_right.png
[Behavior tree Turret Simple]: .README/icons/Behavior_tree_turret_simple_complete.png
[Behavior tree Turret Advanced]: .README/icons/Behavior_tree_turret_advanced_complete.png
[GetClosestDrone]: .README/icons/Get_closest_drone.png
[GetClosestTurret]: .README/icons/Get_closest_turret.png
[GetFurthestTurret]: .README/icons/Get_furthest_turret.png
[myIsWithinDistanceDrone]: .README/icons/My_is_within_distance_drone.png
[myIsWithinDistanceTurret]: .README/icons/My_is_within_distance_turret.png
[myIsWithinAngle]: .README/icons/My_is_within_angle.png

