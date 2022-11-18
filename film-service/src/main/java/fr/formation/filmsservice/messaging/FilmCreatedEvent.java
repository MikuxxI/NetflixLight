package fr.formation.filmsservice.messaging;

import lombok.Getter;
import lombok.Setter;

@Getter @Setter
public class FilmCreatedEvent {
	private int filmId;
	private String titre;
	private String description;
	private Double prix;
	private int[] categorie;
}
