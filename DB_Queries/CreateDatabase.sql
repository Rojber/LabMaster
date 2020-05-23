Create table Studenci(
	indeks int NOT NULL PRIMARY KEY,
	imie varchar(255),
	nazwisko varchar(255),
	mail varchar(255)
	);
Create table Przedmioty(
	id int NOT NULL PRIMARY KEY,
	nazwa varchar(255)
);
Create table Sale(
	id int NOT NULL PRIMARY KEY,
	nazwa varchar(255)
);
Create table Aplikacje(
	id int NOT NULL PRIMARY KEY,
	id_sali int foreign key REFERENCES Sale(id),
	nazwa_komputera varchar(255),
	passwd varchar(255)
);
Create table Prowadzacy(
	id int NOT NULL PRIMARY KEY,
	imie varchar(255),
	nazwisko varchar(255),
	mail varchar(255)
	);
Create table Grupy(
	id int NOT NULL PRIMARY KEY,
	nazwa varchar(255),
	id_przedmiotu int foreign key REFERENCES Przedmioty(id),
	id_prowadzacego int foreign key REFERENCES Prowadzacy(id)
	);
Create table Zajecia(
	id int NOT NULL PRIMARY KEY,
	id_grupy int foreign key REFERENCES Grupy(id),
	rozpoczecie DATETIME,
	zakonczenie DATETIME,
	id_sali int foreign key REFERENCES Sale(id)
);
Create table StudenciGrupy(
	id_studenta int foreign key REFERENCES Studenci(indeks),
	id_grupy int foreign key REFERENCES Grupy(id)
);

Create table Obecnosci(
	id_studenta int foreign key REFERENCES Studenci(indeks),
	id_zajec int foreign key REFERENCES Zajecia(id),
	obecnosc bit,
	usprawiedliwienie bit
);