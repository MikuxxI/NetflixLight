package fr.formation.visionnerservice.model;

import javax.persistence.Column;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OrderColumn;
import javax.persistence.Table;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Table(name = "visionner")
@Getter @Setter
@Builder @AllArgsConstructor @NoArgsConstructor
public class Visionner {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "vis_Id")
    private int id;

    @Column(name = "vis_UserId", nullable = false)
    private int userId;

    @ElementCollection
    @OrderColumn
    @Column(name = "vis_ListMovieId", nullable = false, columnDefinition = "Integer")
    private Integer[] MoviesId;
}
