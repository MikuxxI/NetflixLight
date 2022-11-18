package fr.formation.filmsservice.service;

import java.util.List;

import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import fr.formation.filmsservice.api.request.FilmRequest;
import fr.formation.filmsservice.model.Film;
import fr.formation.filmsservice.repo.IFilmRepository;
import fr.formation.filmsservice.exception.EntityNotFoundException;

@Service
public class FilmService {
    @Autowired
    private IFilmRepository repoFilm;

    public List<Film> findAll() {
        return this.repoFilm.findAll();
    }

    public Film findById(int id) {
        return this.repoFilm.findById(id).orElseThrow(EntityNotFoundException::new);
    }

    public int add(FilmRequest filmRequest) {
        Film film = new Film();
        BeanUtils.copyProperties(filmRequest, film);
        this.repoFilm.save(film);

        return film.getId();
    }

    public int edit(int id, FilmRequest produitRequest) {
        Film produit = this.findById(id);

        BeanUtils.copyProperties(produitRequest, produit);

        this.repoFilm.save(produit);

        return produit.getId();
    }

    public boolean deleteById(int id) {
        try {
            this.repoFilm.deleteById(id);
            return true;
        }

        catch (Exception e) {
            return false;
        }
    }
}
