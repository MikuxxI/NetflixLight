package fr.formation.filmsservice.api;

import java.util.List;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

import fr.formation.filmsservice.api.request.FilmRequest;
import fr.formation.filmsservice.api.response.FilmDetailsResponse;
import fr.formation.filmsservice.api.response.FilmResponse;
import fr.formation.filmsservice.model.Film;
import fr.formation.filmsservice.service.FilmService;
import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
public class FilmApiController {
    private final FilmService srvFilm;
    private final RestTemplate restTemplate;

    @GetMapping
    public List<FilmResponse> findAll() {
        return this.srvFilm
            .findAll()
            .stream()
            .map(p -> {
            	try {
            		@SuppressWarnings("unused")
					String categorie = this.restTemplate.getForObject("lb://categorie-service/" + p.getCategories(), String.class);
				} catch (Exception e) {
					
				}
                
                return FilmResponse.builder()
                    .id(p.getId())
                    .titre(p.getTitre())
                    .description(p.getDescription())
                    .prix(p.getPrix())
                    .build();
            })
            .toList();
    }

    @GetMapping("/{id}")
    public FilmDetailsResponse findById(@PathVariable int id) {
        Film film = this.srvFilm.findById(id);
        String categories;
        try {
            categories = this.restTemplate.getForObject("lb://categorie-service/" + film.getCategories(), String.class);
		} catch (Exception e) {
			categories = "";
		}
        
        return FilmDetailsResponse.builder()
            .id(film.getId())
            .titre(film.getTitre())
            .description(film.getDescription())
            .prix(film.getPrix())
            .categories((String) categories)
            .build();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public int add(@RequestBody FilmRequest filmRequest) {
        return this.srvFilm.add(filmRequest);
    }

    @PutMapping("/{id}")
    public int edit(@PathVariable int id, @RequestBody FilmRequest filmRequest) {
        return this.srvFilm.edit(id, filmRequest);
    }

    @DeleteMapping("/{id}")
    public boolean deleteById(@PathVariable int id) {        
        return this.srvFilm.deleteById(id);

    }
}
