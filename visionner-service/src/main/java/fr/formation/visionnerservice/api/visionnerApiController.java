package fr.formation.visionnerservice.api;

import java.util.Arrays;
import java.util.List;

import org.springframework.cloud.stream.function.StreamBridge;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import fr.formation.visionnerservice.messaging.VisionnerDetailedCommand;
import fr.formation.visionnerservice.model.Visionner;
import fr.formation.visionnerservice.service.VisionnerService;
import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
public class visionnerApiController {
    private final VisionnerService srvVisionner;
    private final StreamBridge streamBridge;
    
    @GetMapping
    public List<Visionner> findAll() {
        return this.srvVisionner.findAll();
    }

    @GetMapping("/{userId}")
    public Visionner findOneByUserId(@PathVariable int userId) {
        return this.srvVisionner.findOneByUserId(userId);
    }

    @GetMapping("/visionner/{userId}/{movieId}")
    public boolean selectMovie(@PathVariable int userId, @PathVariable int movieId) {
        Visionner visionner = this.srvVisionner.findOneByUserId(userId);
            
        if(visionner != null && Arrays.asList(visionner.getMoviesId()).contains(movieId)) return true;

         return this.streamBridge.send("visionner-detailed-out-0", VisionnerDetailedCommand.builder()
                        .filmId(movieId)
                        .utilisateurId(userId)
                        .build());
    }

}
