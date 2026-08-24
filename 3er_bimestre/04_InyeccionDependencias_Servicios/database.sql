CREATE DATABASE IF NOT EXISTS usuarios_app
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE usuarios_app;

START TRANSACTION;

CREATE TABLE IF NOT EXISTS roles (
  id INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(80) NOT NULL,
  descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT uq_roles_nombre UNIQUE (nombre)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS permisos (
  id INT NOT NULL AUTO_INCREMENT,
  codigo VARCHAR(80) NOT NULL,
  descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT uq_permisos_codigo UNIQUE (codigo)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS usuarios (
  id INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(100) NOT NULL,
  apellido VARCHAR(100) NOT NULL,
  nombre_usuario VARCHAR(80) NOT NULL,
  email VARCHAR(150) NOT NULL,
  rol_id INT NOT NULL,
  activo BOOLEAN NOT NULL DEFAULT TRUE,
  PRIMARY KEY (id),
  CONSTRAINT uq_usuarios_nombre_usuario UNIQUE (nombre_usuario),
  CONSTRAINT uq_usuarios_email UNIQUE (email),
  CONSTRAINT fk_usuarios_roles
    FOREIGN KEY (rol_id) REFERENCES roles (id)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS roles_permisos (
  rol_id INT NOT NULL,
  permiso_id INT NOT NULL,
  PRIMARY KEY (rol_id, permiso_id),
  CONSTRAINT fk_roles_permisos_roles
    FOREIGN KEY (rol_id) REFERENCES roles (id),
  CONSTRAINT fk_roles_permisos_permisos
    FOREIGN KEY (permiso_id) REFERENCES permisos (id)
) ENGINE = InnoDB;

INSERT INTO roles (id, nombre, descripcion) VALUES
  (1, 'Administrador', 'Gestiona usuarios, roles, permisos y configuracion general.'),
  (2, 'Supervisor', 'Consulta informacion operativa y administra usuarios del equipo.'),
  (3, 'Operador', 'Carga y consulta informacion del sistema.'),
  (4, 'Auditor', 'Revisa usuarios, roles y permisos sin modificar datos.'),
  (5, 'Invitado', 'Accede solo a opciones de lectura basicas.');

INSERT INTO permisos (id, codigo, descripcion) VALUES
  (1, 'USUARIOS_VER', 'Ver el listado y detalle de usuarios.'),
  (2, 'USUARIOS_CREAR', 'Registrar nuevos usuarios.'),
  (3, 'USUARIOS_EDITAR_ROL', 'Cambiar el rol asignado a un usuario.'),
  (4, 'USUARIOS_CAMBIAR_ESTADO', 'Activar o desactivar usuarios.'),
  (5, 'ROLES_VER', 'Ver roles del sistema.'),
  (6, 'ROLES_CREAR', 'Registrar nuevos roles.'),
  (7, 'PERMISOS_VER', 'Ver permisos disponibles.'),
  (8, 'PERMISOS_CREAR', 'Registrar nuevos permisos.'),
  (9, 'ROLES_ASIGNAR_PERMISOS', 'Asignar permisos a roles.'),
  (10, 'ROLES_QUITAR_PERMISOS', 'Quitar permisos a roles.');

INSERT INTO usuarios (id, nombre, apellido, nombre_usuario, email, rol_id, activo) VALUES
  (1, 'Ana', 'Garcia', 'agarcia', 'ana.garcia@example.com', 1, TRUE),
  (2, 'Bruno', 'Martinez', 'bmartinez', 'bruno.martinez@example.com', 2, TRUE),
  (3, 'Carla', 'Lopez', 'clopez', 'carla.lopez@example.com', 3, TRUE),
  (4, 'Diego', 'Fernandez', 'dfernandez', 'diego.fernandez@example.com', 3, TRUE),
  (5, 'Elena', 'Suarez', 'esuarez', 'elena.suarez@example.com', 4, TRUE),
  (6, 'Federico', 'Romero', 'fromero', 'federico.romero@example.com', 5, FALSE),
  (7, 'Gabriela', 'Torres', 'gtorres', 'gabriela.torres@example.com', 2, TRUE),
  (8, 'Hernan', 'Diaz', 'hdiaz', 'hernan.diaz@example.com', 3, TRUE),
  (9, 'Irene', 'Navarro', 'inavarro', 'irene.navarro@example.com', 4, TRUE),
  (10, 'Javier', 'Molina', 'jmolina', 'javier.molina@example.com', 5, TRUE);

INSERT INTO roles_permisos (rol_id, permiso_id) VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (1, 4),
  (1, 5),
  (1, 6),
  (1, 7),
  (1, 8),
  (1, 9),
  (1, 10),
  (2, 1),
  (2, 2),
  (2, 3),
  (2, 4),
  (2, 5),
  (2, 7),
  (2, 9),
  (3, 1),
  (3, 5),
  (3, 7),
  (4, 1),
  (4, 5),
  (4, 7),
  (5, 1);

COMMIT;
