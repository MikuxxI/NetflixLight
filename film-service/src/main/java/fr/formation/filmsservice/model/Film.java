package fr.formation.filmsservice.model;

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
@Table(name = "film")
@Getter @Setter
@Builder @AllArgsConstructor @NoArgsConstructor
public class Film {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "film_id")
    private int id;

    @Column(name = "film_titre", length = 150, nullable = false)
    private String titre;
    
    @Column(name = "film_description", length = 150, nullable = false)
    private String description;
    
    @Column(name = "film_prix", nullable = false)
    private Double prix;
    
    @ElementCollection
    @OrderColumn
    @Column(name = "film_categories", nullable = false, columnDefinition = "Integer")
    private int[] categories;
}
