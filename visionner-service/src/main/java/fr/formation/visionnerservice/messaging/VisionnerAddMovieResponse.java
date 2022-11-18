package fr.formation.visionnerservice.messaging;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter @Setter @Builder
public class VisionnerAddMovieResponse {
    private int userId;
    private int movieId;
}
