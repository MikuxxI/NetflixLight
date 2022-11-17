package fr.formation.filmsservice.model;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
// import javax.persistence.JoinColumn;
// import javax.persistence.ManyToOne;
import javax.persistence.Table;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Table(name = "categorie")
@Getter @Setter
@Builder @AllArgsConstructor @NoArgsConstructor
public class Categorie {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "cat_id")
    private int id;

    @Column(name = "cat_name", length = 500, nullable = false)
    private String nom;
}
