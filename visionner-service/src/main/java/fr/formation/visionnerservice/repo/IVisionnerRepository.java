package fr.formation.visionnerservice.repo;

import org.springframework.data.jpa.repository.JpaRepository;

import fr.formation.visionnerservice.model.Visionner;

public interface IVisionnerRepository extends JpaRepository<Visionner, Integer> {
    public Visionner findOneByUserId(int id);
}
