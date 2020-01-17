FROM maven:3.6-jdk-8-slim

WORKDIR /src/app/

COPY ./pom.xml .

RUN ["mkdir", "/home/projects"]

RUN groupadd projects && useradd -g projects projects && \
  chown -R projects:projects /src/app && \
  chown -R projects:projects /home/projects

USER projects

RUN ["mvn", "clean"]

RUN ["mvn", "de.qaware.maven:go-offline-maven-plugin:resolve-dependencies", "-P", "integration"]

COPY . .

ENTRYPOINT ["sh"]
