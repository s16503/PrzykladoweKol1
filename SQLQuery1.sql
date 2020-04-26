use s16503

SELECT * FROM Animal;


SELECT Animal.name as name, Animal.Type, Animal.AdmissionDate as date , Owner.LastName as owner
FROM Animal 
JOIN Owner ON Owner.IdOwner = Animal.IdOwner
ORDER BY name ASC;


INSERT INTO Animal values('Alex','Kaczka','2020-02-05',1);


SELECT * FROM Procedure_Animal

INSERT INTO "Procedure" VALUES('Mycie','Mycie zwierzaka wodą z mydłem');

SELECT * FROM "Procedure";

DELETE FROM "Procedure" WHERE IdProcedure = 2