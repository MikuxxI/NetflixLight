package fr.formation.filmsservice.api.response;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter @Setter @Builder
public class FilmResponse {
    private int id;
    private String titre;
    private String description;
    private Double prix;
    private int[] categories;
}
