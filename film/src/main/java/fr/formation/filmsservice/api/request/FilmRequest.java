package fr.formation.filmsservice.api.request;

import lombok.Getter;
import lombok.Setter;

@Getter @Setter
public class FilmRequest {
    private String titre;
    private String description;
    private Double prix;
    private int categorie;
}
