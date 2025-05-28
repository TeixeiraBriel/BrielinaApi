CREATE DATABASE brielinaBanco;
Use brielinaBanco;
CREATE TABLE narrativas (
    IdNarrativas INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Titulo VARCHAR(1000) DEFAULT '',
    Descricao VARCHAR(2000) DEFAULT '',
    Texto TEXT,
    Ramificacoes TEXT,
    Tipo INT DEFAULT 0
);
CREATE TABLE usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Usuario VARCHAR(255) NOT NULL DEFAULT '',
    SenhaHash VARCHAR(255) NOT NULL DEFAULT ''
);

ALTER TABLE narrativas
add Autor VARCHAR(1000) DEFAULT '';