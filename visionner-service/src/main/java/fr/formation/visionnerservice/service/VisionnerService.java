package fr.formation.visionnerservice.service;

import java.util.List;

import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import fr.formation.visionnerservice.api.request.VisionnerRequest;
import fr.formation.visionnerservice.exception.EntityNotFoundException;
import fr.formation.visionnerservice.model.Visionner;
import fr.formation.visionnerservice.repo.IVisionnerRepository;

@Service
public class VisionnerService {
    @Autowired
    private IVisionnerRepository repoVisionner;

    
    // @Autowired
    // private StreamBridge streamBridge;


    public Visionner findById(int id) {
        return this.repoVisionner.findById(id).orElseThrow(EntityNotFoundException::new);
    }

    public Visionner findOneByUserId(int id) {
        return this.repoVisionner.findOneByUserId(id);
    }

    public List<Visionner> findAll() {
        return this.repoVisionner.findAll();
    }

    public int add(VisionnerRequest visionnerRequest){
        Visionner visionner = new Visionner();

        BeanUtils.copyProperties(visionnerRequest, visionner);

        this.repoVisionner.save(visionner);

        return visionner.getId();
    }

    public int update(int id, VisionnerRequest visionnerRequest){
        Visionner visionner = this.findById(id);

        BeanUtils.copyProperties(visionnerRequest, visionner);

        this.repoVisionner.save(visionner);

        return visionner.getId();
    }
}
