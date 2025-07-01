CREATE DATABASE locadora_db;
USE locadora_db;

CREATE TABLE filmes (
	id INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(255) NOT NULL,
    genero VARCHAR(255) NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE estudios (
	id INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(255) NOT NULL,
    PRIMARY KEY(id)
);

select * from filmes;
select * from estudios;

UPDATE filmes set estudio_id = null where id > 0;

insert into filmes values (null, 'Velozes e Furiosos', 'Acao'), (null, 'Como eu era antes de voce', 'Romance');

insert into estudios values (null, 'Walt Disney Pictures'), (null, 'Warner Bros. Pictures'), (null, 'Universal Pictures'); #, Paramount Pictures e Columbia Pictures.

alter table filmes add column ano_lancamento date null;

alter table filmes add column estudio_id int null;
alter table filmes add foreign key(estudio_id) references estudios(id);