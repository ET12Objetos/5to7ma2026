CREATE DATABASE IF NOT EXISTS usuarios_app
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE usuarios_app;

CREATE TABLE IF NOT EXISTS Usuarios (
    Id CHAR(36) CHARACTER SET ascii COLLATE ascii_bin NOT NULL,
    Nombre VARCHAR(200) NOT NULL,
    Email VARCHAR(254) NOT NULL,
    PRIMARY KEY (Id)
) ENGINE=InnoDB;
