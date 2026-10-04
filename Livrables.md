# Livrables — GestionProduits

## Réponse à la question 1

La clé étrangère de la table `Produits` est `CategorieId`. Elle référence la clé primaire `Id` de la table `Categories`.

La relation est obligatoire : chaque produit doit appartenir à une catégorie. Le comportement actuel est `Restrict` (`DeleteBehavior.Restrict`) : une catégorie qui contient encore des produits ne peut pas être supprimée. Il faut d'abord supprimer ou réaffecter ses produits.

## Réponse à la question 2

Le dossier `Migrations` contient les fichiers générés par Entity Framework Core pour faire évoluer le schéma de la base de données :

- la migration initiale `InitialCreate`, qui crée les tables `Categories` et `Produits` ;
- la migration `AjoutStockEtSuppressionCategorie`, qui ajoute la colonne `Stock` et modifie la suppression de la relation en `Restrict` ;
- un fichier `.Designer.cs` associé à chaque migration ;
- `AppDbContextModelSnapshot.cs`, qui représente l'état actuel du modèle EF Core.

Après la commande `dotnet ef database update`, la table `__EFMigrationsHistory` contient une ligne pour chaque migration appliquée. Elle permet à EF Core de savoir quelles migrations ont déjà été exécutées et d'appliquer uniquement les suivantes.



