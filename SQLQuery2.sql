
CREATE TABLE Book(
	--Id int primary key identity,
	 Id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Title nvarchar(100) not null,
	Author nvarchar(100) not null,
	Year int Null
)

insert into Book(Title,Author,Year) values ('Pakistan','Saqib',25)

select * from Book