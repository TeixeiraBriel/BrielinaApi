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

ALTER TABLE usuarios
    MODIFY COLUMN Usuario   VARCHAR(80)  NOT NULL,
    MODIFY COLUMN SenhaHash VARCHAR(255) NOT NULL;

ALTER TABLE usuarios
    ADD COLUMN Nome         VARCHAR(150) NULL AFTER Usuario,
    ADD COLUMN Email        VARCHAR(150) NULL AFTER Nome,
    ADD COLUMN Perfil       VARCHAR(30)  NOT NULL DEFAULT 'aluno' AFTER SenhaHash,
    ADD COLUMN Ativo        TINYINT(1)   NOT NULL DEFAULT 1 AFTER Perfil,
    ADD COLUMN CriadoEm     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP AFTER Ativo,
    ADD COLUMN AtualizadoEm DATETIME     NULL ON UPDATE CURRENT_TIMESTAMP AFTER CriadoEm;

ALTER TABLE usuarios
    ADD UNIQUE KEY ux_usuarios_usuario (Usuario),
    ADD UNIQUE KEY ux_usuarios_email   (Email);

CREATE TABLE tema (
    id              INT PRIMARY KEY AUTO_INCREMENT,
    livro           VARCHAR(150) NOT NULL,
    responsavel_id  INT NULL,              -- FK para usuário (opcional)
    data_apresentacao DATE NULL,

    -- controle
    criado_por_id   INT NOT NULL,          -- usuário que criou o tema
    criado_em       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em   DATETIME NULL ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE tema_comentario (
    id           INT PRIMARY KEY AUTO_INCREMENT,
    tema_id      INT NOT NULL,
    usuario_id   INT NOT NULL,          -- autor do comentário
    texto        TEXT NOT NULL,
    criado_em    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_tema_comentario_tema
        FOREIGN KEY (tema_id) REFERENCES tema (id),

    -- opcional: FK para usuário
    CONSTRAINT fk_tema_comentario_usuario
        FOREIGN KEY (usuario_id) REFERENCES usuarios (id)
);

CREATE TABLE movie (
  id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(250) NOT NULL,
  genre VARCHAR(100) NOT NULL,
  release_year INT NOT NULL,
  rating DOUBLE NOT NULL,
  poster_url VARCHAR(500),
  directed_by VARCHAR(200),
  sinopse TEXT
);