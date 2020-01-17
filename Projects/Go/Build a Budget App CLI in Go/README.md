## Golang Personal Budget CLI

Demo app for the Golang Personal Budget CLI Project.

## Running tests

A proper Go environment is required in order to run this project.
Once setup, tests can be run with the following command:

`go test -v ./module1/ ./module2/`

### Running with Docker

To build the image from the Dockerfile, run:

`docker build -t project-budget-app .`

To start an interactive shell, run:

`docker run -it --rm --name run-budget project-budget-app`

From inside the shell, run the tests with:

`go test -v ./module1/ ./module2/`

