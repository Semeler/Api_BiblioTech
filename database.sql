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
('Drama', 'Livros com histórias emocionais e conflitos humanos'),
('Aventura', 'Livros com histórias de exploração e desafios'),
('Biografia', 'Relatos da vida de pessoas reais'),
('História', 'Livros sobre eventos históricos'),
('Humor', 'Livros com histórias engraçadas e divertidas'),
('Mistério', 'Livros com investigação e enigmas'),
('Poesia', 'Coletâneas de poemas e versos'),
('Autoajuda', 'Livros para desenvolvimento pessoal'),
('Infantil', 'Livros para crianças com histórias educativas');

INSERT INTO Livro (Titulo, Autor, Isbn, Editora, Sinopse, AnoPublicacao, GeneroId) VALUES
('O Senhor dos Anéis', 'J.R.R. Tolkien', '978-85-333-0227-3', 'Martins Fontes', 'Uma jornada épica em um mundo de fantasia', '1954-07-29', 3),
('1984', 'George Orwell', '978-85-359-0277-1', 'Companhia das Letras', 'Um futuro distópico onde a sociedade é controlada', '1949-06-08', 2),
('O Iluminado', 'Stephen King', '978-85-359-0277-2', 'Suma', 'Uma família isolada em um hotel assombrado', '1977-01-28', 4),
('Orgulho e Preconceito', 'Jane Austen', '978-85-359-0001-5', 'Penguin', 'Romance clássico sobre costumes e relacionamentos', '1813-01-28', 1),
('Como Eu Era Antes de Você', 'Jojo Moyes', '978-85-503-0001-2', 'Intrínseca', 'Uma história de amor e superação', '2012-01-05', 1),
('Duna', 'Frank Herbert', '978-85-359-0002-3', 'Aleph', 'Uma disputa política em um planeta desértico', '1965-08-01', 2),
('Neuromancer', 'William Gibson', '978-85-503-0002-9', 'Aleph', 'Um marco do cyberpunk com hackers e IA', '1984-07-01', 2),
('Harry Potter e a Pedra Filosofal', 'J.K. Rowling', '978-85-333-0003-7', 'Rocco', 'Um garoto descobre que é um bruxo', '1997-06-26', 3),
('A Guerra dos Tronos', 'George R.R. Martin', '978-85-333-0004-4', 'Leya', 'Intrigas políticas em um mundo medieval', '1996-08-06', 3),
('It - A Coisa', 'Stephen King', '978-85-503-0003-6', 'Suma', 'Um grupo enfrenta uma entidade maligna', '1986-09-15', 4),
('A Menina que Roubava Livros', 'Markus Zusak', '978-85-359-0005-0', 'Intrínseca', 'A história de uma garota durante a Segunda Guerra', '2005-03-14', 5),
('As Aventuras de Tom Sawyer', 'Mark Twain', '978-85-503-0004-3', 'Penguin', 'Um garoto vive travessuras às margens do Mississippi', '1876-06-17', 6),
('Steve Jobs', 'Walter Isaacson', '978-85-503-0005-0', 'Companhia das Letras', 'A vida do fundador da Apple', '2011-10-24', 7),
('Sapiens - Uma Breve História da Humanidade', 'Yuval Noah Harari', '978-85-503-0006-7', 'L&PM', 'A evolução da humanidade ao longo dos séculos', '2011-01-01', 8),
('O Assassinato no Expresso do Oriente', 'Agatha Christie', '978-85-503-0007-4', 'Globo', 'Um crime misterioso em um trem de luxo', '1934-01-01', 10);


