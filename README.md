# Template

## run single project with Dockerfile

```bash
cd ~/Template/Template.*

docker build -t angular-dashboard:dev .

docker run -it -p 15333:8081 angular-dashboard:dev
```

## run compose project with Dockerfile

```bash
cd ~/Template

docker-compose up
```