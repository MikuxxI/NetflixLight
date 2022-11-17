package fr.formation.filmsservice.service;

import java.util.List;

import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import fr.formation.filmsservice.api.request.FilmRequest;
import fr.formation.filmsservice.exception.EntityNotFoundException;
import fr.formation.filmsservice.model.Categorie;
import fr.formation.filmsservice.model.Film;
import fr.formation.filmsservice.repo.ICategorieRepository;
import fr.formation.filmsservice.repo.IFilmRepository;

@Service
public class FilmService {
    @Autowired
    private IFilmRepository repoFilm;

    @Autowired
    private ICategorieRepository repoCategorie;

    public List<Film> findAll() {
        return this.repoFilm.findAll();
    }

    public Film findById(int id) {
        return this.repoFilm.findById(id).orElseThrow(EntityNotFoundException::new);
    }

    public String getCategorie(Film film) {
        return repoCategorie
                    .findById(film.getCategorie()).getNom();
    }

    public Categorie findCatById(int id) {
        return repoCategorie.findById(id);
    }

    public int add(FilmRequest filmRequest) {
        Film film = new Film();

        BeanUtils.copyProperties(filmRequest, film);

        this.repoFilm.save(film);

        return film.getId();
    }

    public int edit(int id, FilmRequest filmRequest) {
        Film film = this.findById(id);

        BeanUtils.copyProperties(filmRequest, film);

        this.repoFilm.save(film);

        return film.getId();
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
