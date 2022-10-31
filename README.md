## Paso a paso para poner en marcha el backend

Se debe tener todas las dependencias necesarias para la correcta ejecución del proyecto.

1. Clonar el repositorio donde se encuentra alojado el proyecto

```jsx
git clone git@github.com:jwalvarezuninorte/sophos-university-backend.git
```

1. Crear la base de datos según el siguiente script:

```sql
CREATE DATABASE SophosUniversityDB;
USE SophosUniversityDB;

CREATE TABLE Departments
(
    id int primary key identity,
    department_name varchar(100),
);

CREATE TABLE Students
(
	id int primary key identity,
	first_name varchar(20) not null, --required
	last_name varchar(20) not null, --required
	department_id int not null, --required
	total_courses int not null default 0, -- default value here
	total_credits int not null default 0, -- default value here
	foreign key (department_id) references Departments (id),
);

CREATE TABLE Proffesors
(
	id int primary key identity,
	first_name varchar(20) not null, --required
	last_name varchar(20) not null, --required
	maximum_degree varchar(20) not null, --required
	years_of_experience int not null, --required
);

CREATE TABLE Courses
(
	id int primary key identity,
	title varchar(30) not null, --required
	description varchar(1024) not null, --required
	credits int not null, --required
	limit int not null, --required
	total_students int not null default 0, --default 0
	proffesor_id int not null, --required
	foreign key (proffesor_id) references Proffesors (id),
)

CREATE TABLE StudentsCourses
(
    student_id int,
    course_id  int,
		completed bit not null default 0, --default false
    foreign key (student_id) references Students (id),
    foreign key (course_id) references Courses (id),
    primary key (student_id, course_id)
)

CREATE TABLE ProffesorsCourses
(
    proffesor_id int,
    course_id  int unique, --Unique, because a course just have one proffesor
    foreign key (proffesor_id) references Proffesors (id),
    foreign key (course_id) references Courses (id),
    primary key (proffesor_id, course_id)
)
```

1. Poblar la base de datos con datos de prueba:

```sql
-- Insert into Departments
INSERT INTO Departments VALUES ('Ingenieria de Sistemas');
INSERT INTO Departments VALUES ('Idiomas');
INSERT INTO Departments VALUES ('Ciencias de la Salud');
INSERT INTO Departments VALUES ('Humanidades');
INSERT INTO Departments VALUES ('Diseño');
INSERT INTO Departments VALUES ('Ingenieria Industrial');
INSERT INTO Departments VALUES ('Arquitectura');

------------------------------------------------------------

-- Insert into Students
INSERT INTO Students VALUES ('Eddy','Cardona',1, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Michael','Valero', 1, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Helymar','Acosta', 1, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Joel','Borrero', 1, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Jose','Arteaga', 1, DEFAULT, DEFAULT);

INSERT INTO Students VALUES ('Camila','Campo', 2, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Mariana','Campo', 2, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Darlys','Cervantes', 2, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Victoria','Acuna', 2, DEFAULT, DEFAULT);

INSERT INTO Students VALUES ('Juan','Escobar', 3, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Isaac','Alvarez', 3, DEFAULT, DEFAULT);

INSERT INTO Students VALUES ('Andrea','Castrillon', 4, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Jhiangy','Arango', 4, DEFAULT, DEFAULT);
INSERT INTO Students VALUES ('Yurledys','Borrero', 4, DEFAULT, DEFAULT);

INSERT INTO Students VALUES ('William','Alvarez', 7, DEFAULT, DEFAULT);

------------------------------------------------------------

-- Insert into Proffesors
INSERT INTO Proffesors VALUES ('PEddy','PCardona', 'Pregrado 1', 1);
INSERT INTO Proffesors VALUES ('PMichael','PValero', 'Pregrado 1', 1);
INSERT INTO Proffesors VALUES ('PHelymar','PAcosta','Pregrado 1', 1);
INSERT INTO Proffesors VALUES ('PJoel','PBorrero','Pregrado 1', 1);
INSERT INTO Proffesors VALUES ('PJose','PArteaga','Pregrado 1', 1);

INSERT INTO Proffesors VALUES ('PCamila','PCampo','Pregrado 2', 2);
INSERT INTO Proffesors VALUES ('PMariana','PCampo','Pregrado 2', 2);
INSERT INTO Proffesors VALUES ('PDarlys','PCervantes','Pregrado 2', 2);
INSERT INTO Proffesors VALUES ('PVictoria','PAcuna','Pregrado 2', 2);

INSERT INTO Proffesors VALUES ('PJuan','PEscobar','Pregrado 3', 3);
INSERT INTO Proffesors VALUES ('PIsaac','PAlvarez','Pregrado 3', 3);

INSERT INTO Proffesors VALUES ('PAndrea','PCastrillon','Pregrado 4', 4);
INSERT INTO Proffesors VALUES ('PJhiangy','PArango','Pregrado 4', 4);
INSERT INTO Proffesors VALUES ('PYurledys','PBorrero','Pregrado 4', 4);

INSERT INTO Proffesors VALUES ('PWilliam','PAlvarez','Pregrado 7', 7);

------------------------------------------------------------

-- Insert into Courses
INSERT INTO Courses VALUES ('Algoritmia 1', 'Sed dignissim ultrices commodo. Curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',3,20, DEFAULT, 1);
INSERT INTO Courses VALUES ('Soluciones Computacionales', 'Class aptent taciti sociosqu ad litora torquent curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',4,30,DEFAULT, 1);
INSERT INTO Courses VALUES ('Electiva Sophos', 'Sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',3,20,DEFAULT, 1);
INSERT INTO Courses VALUES ('Filosofia', 'Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed dignissim ultrices commodo. Curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',2,10,DEFAULT, 2);
INSERT INTO Courses VALUES ('Criptografia', 'Per inceptos himenaeos. Sed dignissim ultrices commodo. Curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',2,10,DEFAULT, 3);
INSERT INTO Courses VALUES ('Quimica', 'Aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed dignissim ultrices commodo. Curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',3,60,DEFAULT, 4);
INSERT INTO Courses VALUES ('Desarrollo Movil', 'Taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed dignissim ultrices commodo. Curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',2,16,DEFAULT, 4);
INSERT INTO Courses VALUES ('POO en python', 'Sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed dignissim ultrices commodo. Curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',3,20,DEFAULT, 5);
INSERT INTO Courses VALUES ('Videojuegos con Unity', 'Per conubia nostra, per inceptos himenaeos. Sed dignissim ultrices commodo. Curabitur gravida massa eget malesuada tempor. Quisque interdum rhoncus erat, eu volutpat justo rutrum eu. Etiam hendrerit nibh sit amet arcu placerat suscipit. Mauris placerat nulla interdum egestas viverra. Praesent sem dui, efficitur vitae velit in, ultricies varius mauris. Etiam id posuere dui. Integer sagittis accumsan nisi, sed mollis erat tincidunt ut. Nam placerat tortor ut risus efficitur, id convallis ex lacinia.',5,80,DEFAULT, 6);

------------------------------------------------------------

-- Register Student into Course
INSERT INTO StudentsCourses VALUES (1, 1, DEFAULT);
INSERT INTO StudentsCourses VALUES (1, 2, DEFAULT);
INSERT INTO StudentsCourses VALUES (1, 3, DEFAULT);

INSERT INTO StudentsCourses VALUES (2, 1, DEFAULT);
INSERT INTO StudentsCourses VALUES (2, 2, DEFAULT);
INSERT INTO StudentsCourses VALUES (2, 3, DEFAULT);
INSERT INTO StudentsCourses VALUES (2, 4, DEFAULT);

INSERT INTO StudentsCourses VALUES (3, 2, DEFAULT);
INSERT INTO StudentsCourses VALUES (3, 3, DEFAULT);

INSERT INTO StudentsCourses VALUES (4, 5, DEFAULT);
INSERT INTO StudentsCourses VALUES (4, 6, DEFAULT);
INSERT INTO StudentsCourses VALUES (4, 7, DEFAULT);

-- Register Proffesor into Course
INSERT INTO ProffesorsCourses VALUES (1, 2);
INSERT INTO ProffesorsCourses VALUES (1, 3);
INSERT INTO ProffesorsCourses VALUES (1, 4);

INSERT INTO ProffesorsCourses VALUES (2, 5);
INSERT INTO ProffesorsCourses VALUES (2, 6);
INSERT INTO ProffesorsCourses VALUES (2, 7);
INSERT INTO ProffesorsCourses VALUES (2, 8);
```

1. Una vez teniendo todo esto listo, se procede a ejecutar el proyecto

```sql
dotnet run
```

1. En este punto el backend se encontrará listo para escuchar las peticiones de un cliente. Dichos endpoints se puede comprobar por medio de la aplicación web o bien, a traves de Postman con este workspace.
