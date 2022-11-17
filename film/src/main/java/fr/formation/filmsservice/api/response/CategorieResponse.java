package fr.formation.filmsservice.api.response;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter @Setter @Builder
public class CategorieResponse {
    private String nom;
}
