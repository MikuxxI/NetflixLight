package fr.formation.visionnerservice.api.request;


import lombok.Getter;
import lombok.Setter;

@Getter @Setter
public class VisionnerRequest {
    private int userId;
    private Integer[] listMovieId;
}
