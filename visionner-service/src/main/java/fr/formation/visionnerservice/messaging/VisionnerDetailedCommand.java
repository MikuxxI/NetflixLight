package fr.formation.visionnerservice.messaging;

import lombok.Setter;
import lombok.Getter;
import lombok.Builder;

@Getter @Setter @Builder
public class VisionnerDetailedCommand {
    private int filmId;
    private int utilisateurId;
}