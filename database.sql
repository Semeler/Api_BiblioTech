create database Bibliotech;

use Bibliotech;

create table Cliente (
    Id int primary key auto_increment,
    Nome varchar(50),
    CPF varchar(20),
    Telefone varchar(500),
    Email varchar(50),
    Cep varchar(50),
    Rua varchar(50),
    Bairro varchar(50),
    Numero varchar(50),
    Estado varchar(50),
    Cidade varchar(50),
    DataNascimento date                     
);

create table Funcionario (
    Id int primary key auto_increment,
    Nome varchar(50),
    CPF varchar(20),
    Cargo varchar(50),
    Telefone varchar(500),
    Email varchar(50),
    Cep varchar(50),
    Rua varchar(50),
    Bairro varchar(50),
    Numero varchar(50),
    Estado varchar(50),
    Cidade varchar(50),
    DataAdmissao date                                                  
);

create table Fornecedor(
    Id int primary key auto_increment,
    Nome varchar(50),
    Cnpj varchar(20),
    Telefone varchar(500),
    Email varchar(50),
    Cep varchar(50),
    Rua varchar(50),
    Bairro varchar(50),
    Numero varchar(50),
    Estado varchar(50),
    Cidade varchar(50),
    AnoLancamento date                       
);

create table Genero (
    Id int primary key auto_increment,
    Nome varchar(50),
    Descricao varchar(500)                    
);

create table Livro (
    Id int primary key auto_increment,
    Titulo varchar(100),
    Autor varchar(50),
    Isbn varchar(50),
    Editora varchar(50),
    Sinopse varchar(2000),
    AnoPublicacao date,
    GeneroId int,
    foreign key (GeneroId) references Genero(Id)                   
);


create table Estoque(
    Id int primary key auto_increment,
    Quantidade int,
    CodigoDeBarras varchar(30) not null,
    LivroId int,
    foreign key (LivroId) references Livro(Id)                    
);

create table Emprestimo (
    Id int primary key auto_increment,
    DataInicio date,
    DataPrevista date,
    DataDevolucao date,
    Status varchar(100),
    ClienteId int,
    FuncionarioId int,
    foreign key (FuncionarioId) references Funcionario(Id),
    foreign key (ClienteId) references Cliente(Id)                        
);

create table EmprestimoLivro(
    Id int primary key auto_increment,
    LivroId int,
    EmprestimoId int,
    foreign key (EmprestimoId) references Emprestimo(Id)                            
);

create table FornecedorLivro(
    Id int primary key auto_increment,
    LivroId int ,
    FornecedorId int ,
    foreign key (LivroId) references Livro(Id),
    foreign key (FornecedorId) references Fornecedor(Id)                            
);

INSERT INTO Cliente (Nome, CPF, Telefone, Email, Cep, Rua, Bairro, Numero, Estado, Cidade, DataNascimento) VALUES
('Maria Silva', '123.456.789-01', '(11) 98765-4321', 'maria@email.com', '01234-567', 'Rua das Flores', 'Centro', '123', 'SP', 'São Paulo', '1990-05-15'),
('João Santos', '987.654.321-00', '(11) 91234-5678', 'joao@email.com', '04567-890', 'Av. Principal', 'Vila Nova', '456', 'SP', 'São Paulo', '1985-08-22'),
('Ana Oliveira', '456.789.123-02', '(11) 97890-1234', 'ana@email.com', '02345-678', 'Rua do Sol', 'Jardim Europa', '789', 'SP', 'São Paulo', '1995-03-30');

INSERT INTO Funcionario (Nome, CPF, Cargo, Telefone, Email, Cep, Rua, Bairro, Numero, Estado, Cidade, DataAdmissao) VALUES
('Carlos Souza', '234.567.890-03', 'Bibliotecário', '(11) 96543-2109', 'carlos@biblioteca.com', '03456-789', 'Rua das Acácias', 'Centro', '100', 'SP', 'São Paulo', '2020-01-15'),
('Patricia Lima', '345.678.901-04', 'Atendente', '(11) 95432-1098', 'patricia@biblioteca.com', '05678-901', 'Av. dos Ipês', 'Jardim', '200', 'SP', 'São Paulo', '2021-03-20'),
('Roberto Alves', '456.789.012-05', 'Gerente', '(11) 94321-0987', 'roberto@biblioteca.com', '06789-012', 'Rua dos Pinheiros', 'Pinheiros', '300', 'SP', 'São Paulo', '2019-06-10');

INSERT INTO Fornecedor (Nome, Cnpj, Telefone, Email, Cep, Rua, Bairro, Numero, Estado, Cidade, AnoLancamento) VALUES
('Editora ABC', '12.345.678/0001-90', '(11) 3333-4444', 'contato@editoraabc.com', '07890-123', 'Av. Comercial', 'Centro', '1000', 'SP', 'São Paulo', '2000-01-01'),
('Distribuidora XYZ', '23.456.789/0001-01', '(11) 4444-5555', 'vendas@xyz.com', '08901-234', 'Rua do Comércio', 'Centro', '2000', 'SP', 'São Paulo', '2005-01-01'),
('Livros & Cia', '34.567.890/0001-12', '(11) 5555-6666', 'contato@livrosecia.com', '09012-345', 'Av. dos Livros', 'Centro', '3000', 'SP', 'São Paulo', '2010-01-01');

INSERT INTO Genero (Nome, Descricao) VALUES
('Romance', 'Livros com histórias de amor e relacionamentos'),
('Ficção Científica', 'Livros que exploram conceitos científicos e tecnológicos'),
('Fantasia', 'Livros com elementos mágicos e mundos imaginários'),
('Terror', 'Livros com histórias de suspense e horror'),
('Drama', 'Livros com histórias emocionais e conflitos humanos');

INSERT INTO Livro (Titulo, Autor, Isbn, Editora, Sinopse, AnoPublicacao, GeneroId) VALUES
('O Senhor dos Anéis', 'J.R.R. Tolkien', '978-85-333-0227-3', 'Martins Fontes', 'Uma jornada épica em um mundo de fantasia', '1954-07-29', 3),
('1984', 'George Orwell', '978-85-359-0277-1', 'Companhia das Letras', 'Um futuro distópico onde a sociedade é controlada', '1949-06-08', 2),
('O Iluminado', 'Stephen King', '978-85-359-0277-2', 'Suma', 'Uma família isolada em um hotel assombrado', '1977-01-28', 4);

INSERT INTO Livro (Titulo, Autor, Isbn, Editora, Sinopse, AnoPublicacao, GeneroId) VALUES
('O Senhor dos Anéis', 'J.R.R. Tolkien', '978-85-333-0227-3', 'Martins Fontes', 'Uma jornada épica em um mundo de fantasia', '1954-07-29', 3),
('1984', 'George Orwell', '978-85-359-0277-1', 'Companhia das Letras', 'Um futuro distópico onde a sociedade é controlada', '1949-06-08', 2),
('O Iluminado', 'Stephen King', '978-85-359-0277-2', 'Suma', 'Uma família isolada em um hotel assombrado', '1977-01-28', 4);


INSERT INTO Emprestimo (DataInicio, DataPrevista, DataDevolucao, Status, ClienteId, FuncionarioId) VALUES
('2024-01-01', '2024-01-15', NULL, 'Em andamento', 1, 1),
('2024-01-05', '2024-01-20', NULL, 'Em andamento', 2, 2),
('2024-01-10', '2024-01-25', '2024-01-23', 'Finalizado', 3, 3);



