package fr.formation.filmsservice.messaging;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter @Setter
@AllArgsConstructor
@Builder
public class VisionnerDetailError {
	private int id;
	private String message;
}
