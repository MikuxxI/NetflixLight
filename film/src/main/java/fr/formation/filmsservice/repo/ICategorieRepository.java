package fr.formation.filmsservice.repo;

// import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import fr.formation.filmsservice.model.Categorie;
// import fr.formation.filmsservice.model.Film;

public interface ICategorieRepository extends JpaRepository<Categorie, Integer> {
    public Categorie findById(int id);
}
