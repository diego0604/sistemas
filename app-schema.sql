
CREATE DATABASE IF NOT EXISTS FarmaciaDb CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE FarmaciaDb;

CREATE TABLE IF NOT EXISTS Products (
    Id                   INT             NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name                 VARCHAR(200)    NOT NULL,
    Description          TEXT            NULL,
    Category             VARCHAR(100)    NOT NULL,
    Price                DECIMAL(18,2)   NOT NULL,
    Stock                INT             NOT NULL DEFAULT 0,
    ExpirationDate       DATE            NULL,
    RequiresPrescription TINYINT(1)      NOT NULL DEFAULT 0,
    CreatedAt            DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Sales (
    Id           INT             NOT NULL AUTO_INCREMENT PRIMARY KEY,
    SaleDate     DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TotalAmount  DECIMAL(18,2)   NOT NULL
);

CREATE TABLE IF NOT EXISTS SaleItems (
    Id         INT             NOT NULL AUTO_INCREMENT PRIMARY KEY,
    SaleId     INT             NOT NULL,
    ProductId  INT             NOT NULL,
    Quantity   INT             NOT NULL,
    UnitPrice  DECIMAL(18,2)   NOT NULL,
    FOREIGN KEY (SaleId)    REFERENCES Sales(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE RESTRICT
);
