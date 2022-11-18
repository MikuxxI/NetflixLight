package fr.formation.filmsservice.messaging;

import java.util.function.Consumer;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import fr.formation.filmsservice.model.Film;
import fr.formation.filmsservice.repo.IFilmRepository;
import lombok.AllArgsConstructor;

@Configuration
@AllArgsConstructor
public class FilmEventHandler {
	private final IFilmRepository repoFilm;
	
	@Bean
	public Consumer<FilmCreatedEvent> filmCreated(){
		return evt -> {
			Film film = Film.builder()
					.id(evt.getFilmId())
					.titre(evt.getTitre())
					.description(evt.getDescription())
					.prix(evt.getPrix())
					.categories(evt.getCategorie())
					.build();
			this.repoFilm.save(film);
		};
	}
	
	
	
}
