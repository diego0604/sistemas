CREATE DATABASE IF NOT EXISTS AuthDb CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE AuthDb;

CREATE TABLE IF NOT EXISTS Users (
    Id           INT            NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name         VARCHAR(100)   NOT NULL,
    Email        VARCHAR(200)   NOT NULL UNIQUE,
    PasswordHash VARCHAR(200)   NOT NULL,
    Role         VARCHAR(50)    NOT NULL,
    IsActive     TINYINT(1)     NOT NULL DEFAULT 1,
    CreatedAt    DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT IGNORE INTO Users (Id, Name, Email, PasswordHash, Role, IsActive, CreatedAt)
VALUES
    (1, 'diego', 'diego@farmacia.com', SHA2('123', 256), 'admin', 1, NOW()),
    (2, 'juan',  'juan@farmacia.com',  SHA2('123', 256), 'worker', 1, NOW());
