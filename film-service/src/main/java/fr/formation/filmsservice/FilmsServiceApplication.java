package fr.formation.filmsservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.EnableEurekaClient;

@SpringBootApplication
@EnableEurekaClient
public class FilmsServiceApplication {
	public static void main(String[] args) {
		SpringApplication.run(FilmsServiceApplication.class, args);
	}
}
