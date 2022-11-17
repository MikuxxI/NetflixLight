package fr.formation.filmsservice.api.response;

// import java.util.List;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter @Setter @Builder
public class FilmDetailsResponse {
    private int id;
    private String titre;
    private String description;
    private Double prix;
    private String categorie;
}
