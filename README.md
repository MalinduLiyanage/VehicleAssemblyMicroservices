### Vehicle Assembly Microservice API

This project is an extended version of Monolith Architectured version of the same project at <a href="https://github.com/MalinduLiyanage/VehicleAssembly_ASP.NET_API">```https://github.com/MalinduLiyanage/VehicleAssembly_ASP.NET_API```</a>

## Configuration via Kuburnetes
1. First install ```Minikube``` and ```kubectl``` on your local machine.
2. Start Minikube K8 cluster via ```minikube start```

## Config .yaml files

1.1 MySQL Deployment
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: mysql
spec:
  replicas: 1
  selector:
    matchLabels:
      app: mysql
  template:
    metadata:
      labels:
        app: mysql
    spec:
      containers:
      - name: mysql
        image: mysql:8.0
        env:
        - name: MYSQL_ROOT_PASSWORD
          value: "password"
        volumeMounts:
        - name: mysql-init-volume
          mountPath: /docker-entrypoint-initdb.d
      volumes:
      - name: mysql-init-volume
        configMap:
          name: mysql-init-script
---
apiVersion: v1
kind: Service
metadata:
  name: mysql-service
spec:
  selector:
    app: mysql
  ports:
    - protocol: TCP
      port: 3306
      targetPort: 3306
```

1.2 MySQL Configmap
```
apiVersion: v1
kind: ConfigMap
metadata:
  name: mysql-init-script
data:
  init.sql: |
    CREATE DATABASE IF NOT EXISTS vehicle_accounts_db;
    CREATE DATABASE IF NOT EXISTS vehicle_assembles_db;
    CREATE DATABASE IF NOT EXISTS vehicle_admin_db;
```

2.1 Accounts Microservice Deployment
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: accounts-service
spec:
  replicas: 1
  selector:
    matchLabels:
      app: accounts-service
  template:
    metadata:
      labels:
        app: accounts-service
    spec:
      containers:
      - name: accounts-service
        image: malinduliyanage/accountsservice:1.0
        ports:
        - containerPort: 8080
```

2.2 Accounts Microservice Service
```
apiVersion: v1
kind: Service
metadata:
  name: accounts-service
spec:
  selector:
    app: accounts-service
  ports:
    - protocol: TCP
      port: 8080
      targetPort: 8080
```

3.1 Admin Microservice Deployment
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: admin-service
spec:
  replicas: 1
  selector:
    matchLabels:
      app: admin-service
  template:
    metadata:
      labels:
        app: admin-service
    spec:
      containers:
      - name: admin-service
        image: malinduliyanage/adminservice:1.0
        ports:
        - containerPort: 8080
```

3.2 Admin Microservice Service
```
apiVersion: v1
kind: Service
metadata:
  name: admin-service
spec:
  selector:
    app: admin-service
  ports:
    - protocol: TCP
      port: 8080
      targetPort: 8080
```

4.1 Assmebly Microservice PVC
```
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: uploads-pvc
spec:
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 100Mi
```

4.2 Assmebly Microservice Deployment
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: assembly-service
spec:
  replicas: 1
  selector:
    matchLabels:
      app: assembly-service
  template:
    metadata:
      labels:
        app: assembly-service
    spec:
      containers:
      - name: assembly-service
        image: malinduliyanage/assemblyservice:1.0
        ports:
        - containerPort: 8080
        volumeMounts:
        - name: uploads-volume
          mountPath: /app/wwwroot/uploads
      volumes:
      - name: uploads-volume
        persistentVolumeClaim:
          claimName: uploads-pvc
```

4.3 Assmebly Microservice Service
```
apiVersion: v1
kind: Service
metadata:
  name: assembly-service
spec:
  selector:
    app: assembly-service
  ports:
    - protocol: TCP
      port: 8080
      targetPort: 8080
```

5.1 API Gateway Deployment
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: ocelot-api-gateway
spec:
  replicas: 1
  selector:
    matchLabels:
      app: ocelot-api-gateway
  template:
    metadata:
      labels:
        app: ocelot-api-gateway
    spec:
      containers:
      - name: ocelot-api-gateway
        image: malinduliyanage/apigateway:1.0
        ports:
        - containerPort: 8080
```
5.2 API Gateway Service
```
apiVersion: v1
kind: Service
metadata:
  name: ocelot-api-gateway
spec:
  type: NodePort
  selector:
    app: ocelot-api-gateway
  ports:
  - protocol: TCP
    port: 8080
    targetPort: 8080
    nodePort: 30000 
```
