package fr.formation.filmsservice.repo;

import org.springframework.data.jpa.repository.JpaRepository;

import fr.formation.filmsservice.model.Film;

public interface IFilmRepository extends JpaRepository<Film, Integer> {
    
}
