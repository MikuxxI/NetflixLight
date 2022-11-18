package fr.formation.visionnerservice.model;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Table(name="\"user\"")
@Getter @Setter
@Builder @AllArgsConstructor @NoArgsConstructor
public class Utilisateur {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "use_id")
    private int id;

    @Column(name = "use_firstname", length = 150)
    private String firstname;

    @Column(name = "use_lastname", length = 150)
    private String lastname;

    @Column(name = "use_username", length = 150)
    private String username;

    @Column(name = "use_password", length = 150)
    private String password;
    
    @Column(name = "use_sold")
    private Double Sold;
    
    @Column(name = "AdminRole")
    private boolean AdminRole;
}