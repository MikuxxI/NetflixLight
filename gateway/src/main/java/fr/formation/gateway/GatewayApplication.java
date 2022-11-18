package fr.formation.gateway;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.gateway.route.RouteLocator;
import org.springframework.cloud.gateway.route.builder.RouteLocatorBuilder;
import org.springframework.cloud.netflix.eureka.EnableEurekaClient;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
@EnableEurekaClient
public class GatewayApplication {
	public static void main(String[] args) {
		SpringApplication.run(GatewayApplication.class, args);
	}

	@Bean
	public RouteLocator customRouteLocator(RouteLocatorBuilder builder) {
		return builder.routes()
			.route(r ->
				r.path("/api/visionner/query/**")
				.filters(f ->
					f.rewritePath("/api/visionner/query", "/visionner")
				)
				.uri("lb://query-service")
			)
			.route(r ->
				r.path("/api/visionner/**")
				.filters(f ->
					f.stripPrefix(2)
				)
				.uri("lb://visionner-service")
			)
			.route(r ->
				r.path("/api/film/query/**")
				.filters(f ->
					f.rewritePath("/api/film/query", "/film")
				)
				.uri("lb://query-service")
			)
			.route(r ->
				r.path("/api/film/**")
				.filters(f ->
					f.stripPrefix(2)
				)
				.uri("lb://film-service")
			)
			.route(r ->
				r.path("/api/payment/query/**")
				.filters(f ->
					f.rewritePath("/api/payment/query", "/payment")
				)
				.uri("lb://query-service")
			)
			.route(r ->
				r.path("/api/payment/**")
				.filters(f ->
					f.stripPrefix(2)
				)
				.uri("lb://payment-service")
			)
			.build();
	}
}
