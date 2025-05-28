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