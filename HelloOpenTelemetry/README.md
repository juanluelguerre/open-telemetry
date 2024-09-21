# Project Name

Sample .Net proyect to test Open Telemetry with Prometheus and Grafana in a simple API Rest.

## Requirements

- Docker
- Docker Compose

## Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/juanluelguerre/open-telemetry.git
   cd your-repository
   ```

2. Build and start the containers using Docker Compose:
   ```sh
   docker-compose up --build
   ```

## Usage

Access the application in your web browser at `http://localhost:port`.

## Useful Commands

- To stop the containers:

  ```sh
  docker-compose down
  ```

- To rebuild the containers without using the cache:

  ```sh
  docker-compose build --no-cache
  ```

- To view the logs of the containers:
  ```sh
  docker-compose logs
  ```

## How to Configure your project

- Set `<DockerComposeProjectName>helloopentelemetry</DockerComposeProjectName>` at the root of your `docker-compose.dcproj` file.
-

## Contributing

1. Fork the project.
2. Create a new branch (`git checkout -b feature/new-feature`).
3. Make your changes and commit them (`git commit -am 'Add new feature'`).
4. Push your changes (`git push origin feature/new-feature`).
5. Open a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
