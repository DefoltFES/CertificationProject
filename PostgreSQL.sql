create type user_role as enum ('user', 'admin');

create table Users (
	id integer PRIMARY KEY,
	name varchar NOT NULL,
	role user_role NOT NULL,
	password varchar NOT NULL
);

create table Questions (
	id integer PRIMARY KEY,
	question varchar, 
	answer varchar,
	topic varchar
);

create table Tests (
	id integer PRIMARY KEY,
	name varchar NOT NULL,
	description varchar NULL,
	count int NOT NULL CHECK (count > 0),
	topic varchar NOT NULL
);

create table UsersAndTests (
    id integer PRIMARY KEY,
	user_id integer REFERENCES Users (id) on delete cascade on update cascade,
	test_id integer REFERENCES Tests (id) on delete cascade on update cascade
);

create table Results (
	session_id integer PRIMARY KEY,
	user_id integer REFERENCES Users (id) on update cascade,
	mark integer NULL,
	date_start date NOT NULL,
	date_end date NULL CHECK (Date_end > Date_start)
);

create table Answers (
    id integer PRIMARY KEY,
	session_id integer NOT NULL REFERENCES Results(session_id) on update cascade,
	question_id integer NOT NULL REFERENCES questions (id) on update cascade,
	user_answer varchar NOT NULL
);
