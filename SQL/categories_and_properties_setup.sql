INSERT INTO "PropertyTypes" ("Name") VALUES
('Integer'),
('Decimal'),
('Option'),
('Nominal');

INSERT INTO "Categories" ("Name") VALUES
('Телефоны');

INSERT INTO "Properties" ("Name", "TypeId", "CategoryId", "FilterInfo") VALUES
('Объем оперативной памяти', 1, 1, '{}'),
('Производитель', 3, 1, '{"options":["Huawei", "Nokia"]}');