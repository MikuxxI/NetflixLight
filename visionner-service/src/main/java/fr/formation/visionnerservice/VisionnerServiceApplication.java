package fr.formation.visionnerservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.EnableEurekaClient;

@SpringBootApplication
@EnableEurekaClient
public class VisionnerServiceApplication {
	public static void main(String[] args) {
		SpringApplication.run(VisionnerServiceApplication.class, args);
	}
}
