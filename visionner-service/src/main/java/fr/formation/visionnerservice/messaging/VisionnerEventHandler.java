package fr.formation.visionnerservice.messaging;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.function.Consumer;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import fr.formation.visionnerservice.api.request.VisionnerRequest;
import fr.formation.visionnerservice.model.Visionner;
import fr.formation.visionnerservice.service.VisionnerService;
import lombok.AllArgsConstructor;

@Configuration
@AllArgsConstructor
public class VisionnerEventHandler {
    private final VisionnerService srvVisionner;
    
    @Bean
    public Consumer<VisionnerErrorResponse> visionnerFilmError() {
        return evt -> {
            VisionnerErrorResponse.builder()
                .id(evt.getId())
                .message(evt.getMessage())
                .build();
        };
    }

    @Bean
    public Consumer<VisionnerErrorResponse> visionnerUserError() {
        return evt -> {
            VisionnerErrorResponse.builder()
                .id(evt.getId())
                .message(evt.getMessage())
                .build();
        };
    }

    @Bean
    public Consumer<VisionnerAddMovieResponse> visionnerAddMovie() {
        return evt -> {
            Visionner visionner = this.srvVisionner.findOneByUserId(evt.getUserId());

            VisionnerRequest visionnerRequest = new VisionnerRequest();
            visionnerRequest.setUserId(evt.getUserId());

            if(visionner == null){
                List<Integer> ids = new ArrayList<>();
                ids.add(evt.getMovieId());

                Integer newIds2[] = new Integer[ids.size()];

                visionnerRequest.setListMovieId(ids.toArray(newIds2));
                this.srvVisionner.add(visionnerRequest);
            }else{
                Integer newIds[] = visionner.getMoviesId();
                List<Integer> ids = new ArrayList<>(Arrays.asList(newIds));
                
                ids.add(evt.getMovieId());
                Integer newIds2[] = new Integer[ids.size()];

                visionnerRequest.setListMovieId(ids.toArray(newIds2));
                this.srvVisionner.update(visionner.getId(), visionnerRequest);
            };


            VisionnerDetailsResponse.builder()
                .userId(visionnerRequest.getUserId())
                .listMovieId(visionnerRequest.getListMovieId())
                .build();
            
        };
    }
}
