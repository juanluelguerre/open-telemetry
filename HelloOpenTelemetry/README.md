# Hellow Open Telemetry

This project is a sample application that demonstrates how to integrate a .NET API REst, OpenTelemetry, Prometheus, and Grafana using Visual Studio. It showcases the end-to-end flow of collecting and visualizing application metrics.

![Prometheus and Grafana](assets/Prometheus_And_Graphana.png)

## Requirements

- Docker
- Docker Compose

## Installation

1. Clone the repository:

   ```
   git clone https://github.com/juanluelguerre/open-telemetry.git
   cd your-repository
   ```

2. Build and start the containers using Docker Compose:
   ```
   docker-compose up --build -d
   ```

## Usage

Access the application in your web browser at `http://localhost:5000`.

## Useful Commands

- To stop the containers:

  ```
  docker-compose down
  ```

- To rebuild the containers without using the cache:

  ```
  docker-compose build --no-cache
  ```

- To view the logs of the containers:
  ```
  docker-compose logs
  ```

## How to Configure your project

- Set `<DockerComposeProjectName>helloopentelemetry</DockerComposeProjectName>` at the root of your `docker-compose.dcproj` file, to name your project.
- Be sure deploy folder is configured correctly and also referenced in the `docker-compose.yml` file.

## Contributing

1. Fork the project.
2. Create a new branch (`git checkout -b feature/new-feature`).
3. Make your changes and commit them (`git commit -am 'Add new feature'`).
4. Push your changes (`git push origin feature/new-feature`).
5. Open a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
