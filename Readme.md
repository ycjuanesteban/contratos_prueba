# Manejo de contratos

## Forma de ejecución
 
Lo primero es crear la base de datos, para esto es necesario ejecutar `docker-compose up -d` con esto, se creará los contenedores de PostgreSQL y de la Api.

Después de esto, es necesario ejecutar los dos proyectos en paralelo:

- Desde la carpeta `.\frontend\energy.app` se debe ejecutar el comando `npm install` y luego ejecutar `npm run dev`.
El proyecto se ejecutará en la dirección [app](http://localhost:5173/)


## Visualización del front

- Listado de contratos existentes
![Listado de contratos](./images/ContratosListado.png)

- Modal de nuevo contrato
![Modal nuevo contrato](./images/ContratosNuevo.png)

- Modal de edición de contrato
![Modal editar contrato](./images/ContratosEditar.png)

- Validación de campos en el modal de contratos
![Modal editar contrato](./images/ContratosNuevoValidacion.png)

- Listado de tarifas
![Modal editar contrato](./images/TarifasListado.png)

- Listado de usuarios
![Modal editar contrato](./images/UsuariosListado.png)

- Modal nuevo usuario con su validación
![Modal editar contrato](./images/UsuariosNuevo.png)