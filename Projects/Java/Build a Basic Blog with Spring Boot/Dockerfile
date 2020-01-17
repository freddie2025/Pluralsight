FROM maven:3.6-jdk-8-slim

WORKDIR /src/app/

# mvn needs a user.home in which to run as non-root
# https://hub.docker.com/_/maven , "Running as non-root"
# hence the mkdir and USER command later on
RUN ["mkdir", "/home/projects"]

RUN groupadd projects && useradd -g projects projects && \
  chown -R projects:projects /src/app && \
  chown -R projects:projects /home/projects

# needed for mvn, see above
USER projects

COPY --chown=projects:projects ./pom.xml .

RUN ["mvn", "clean"]

RUN ["mvn", "de.qaware.maven:go-offline-maven-plugin:resolve-dependencies"]

COPY --chown=projects:projects . .

ENTRYPOINT ["sh"]
