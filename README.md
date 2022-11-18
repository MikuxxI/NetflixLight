# NetflixLight
Microservices

## Visionner

|   Service   | Port |              Description                        |
|   :------   | :--- |              :-------------------------         |
| `Visionner` |  80  | Sauvegarde la liste des films pour un utilisateur |

|Route|        Description           |
| :-- |   :---------------------     |
| `/` |  Récupérer toute la table visionner |
| `/{userId}` |  Récupère la liste des films d'un utilisateur |
| `/visionner/{userId}/{movieId}` |  Regarde si le film est en possession ou l'achète s'il possède assez de crédit |

![Java](https://img.shields.io/badge/java-%23ED8B00.png?style=for-the-badge&logo=java&logoColor=white)
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)
![Visual Studio Code](https://img.shields.io/badge/Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white)

# Architecture
![architecture](https://github.com/MikuxxI/NetflixLight/blob/main/architecture.drawio.png)

lien badge : https://github.com/Ileriayo/markdown-badges
