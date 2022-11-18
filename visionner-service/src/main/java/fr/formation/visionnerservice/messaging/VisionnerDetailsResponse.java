package fr.formation.visionnerservice.messaging;

import lombok.AllArgsConstructor;
import lombok.NoArgsConstructor;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter @Setter @Builder
@NoArgsConstructor @AllArgsConstructor
public class VisionnerDetailsResponse {
    private int userId;
    private Integer[] listMovieId;
}