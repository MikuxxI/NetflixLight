package fr.formation.filmsservice.messaging;

import java.util.Optional;
import java.util.function.Consumer;

import org.springframework.cloud.stream.function.StreamBridge;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import fr.formation.filmsservice.model.Film;
import fr.formation.filmsservice.repo.IFilmRepository;
import lombok.AllArgsConstructor;
import lombok.Builder;

@Configuration
@AllArgsConstructor
@Builder
public class VisionnerDetailed {
	private final IFilmRepository repoFilm;
	private final StreamBridge streamBridge;
	
	@Bean
	public Consumer<VisionnerDetailedResponse> filmExist(){
		return evt -> {
			Optional<Film> film = this.repoFilm.findById(evt.getFilmId());	
//			if (film != null) {
//				this.streamBridge.send("userfilmdetail-out-0", VisionnerDetailed.builder());
//			}
//			else {
				this.streamBridge.send("film-error-out-0", VisionnerDetailError.builder().id(evt.getFilmId()).message("Le film n'existe pas").build());
			//}
		};
	}
}
