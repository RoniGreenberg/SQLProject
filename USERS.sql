-- Script Date: 06/06/2024 19:14  - ErikEJ.SqlCeScripting version 3.5.2.95
CREATE TABLE [USERS] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT,
  [Email] NVARCHAR(50) NOT NULL,
  [FirstName] NVARCHAR(50) NOT NULL,
  [LastName] NVARCHAR(50) NOT NULL,
  [Password] NVARCHAR(50) NOT NULL
);

INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('john@example.com', 'John', 'Doe', '123456');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('jane@example.com', 'Jane', 'Smith', 'password');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('michael@example.com', 'Michael', 'Johnson', 'qwerty');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('emily@example.com', 'Emily', 'Davis', 'letmein');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('daniel@example.com', 'Daniel', 'Brown', 'football');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('sarah@example.com', 'Sarah', 'Wilson', 'baseball');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('matthew@example.com', 'Matthew', 'Taylor', 'soccer');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('jessica@example.com', 'Jessica', 'Anderson', 'basketball');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('david@example.com', 'David', 'Martinez', 'tennis');
INSERT INTO USERS (Email, FirstName, LastName, Password) VALUES ('jennifer@example.com', 'Jennifer', 'Hernandez', 'golf');

