CREATE TABLE "Configs"(
    "keyName" VARCHAR(255) NOT NULL,
    "value" VARCHAR(255) NOT NULL,
    CONSTRAINT "configs_keyname_primary" PRIMARY KEY("keyName")
);
SELECT name FROM sys.databases WHERE name = 'paytrack_db';

CREATE TABLE "Employees"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "firstName" VARCHAR(255) NULL,
    "lastName" VARCHAR(255) NULL,
    "email" VARCHAR(255) NULL,
    "organizationId" INT NOT NULL,
    "isAccount" TINYINT NULL,
    "isHR" TINYINT NULL,
    "accountNo" VARCHAR(255) NULL,
    "ifscCode" VARCHAR(255) NULL,
    "HRId" INT NULL,
    "isManager" TINYINT NULL,
    "keycloak_user_id" VARCHAR(255) NULL,
    CONSTRAINT "employees_id_primary" PRIMARY KEY("id")
);

CREATE TABLE "organization"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "fullName" VARCHAR(255) NOT NULL,
    "ownerName" VARCHAR(255) NOT NULL,
    "ownerEmail" VARCHAR(255) NOT NULL,
    "keycloak_owner_id" VARCHAR(255) NULL,
    CONSTRAINT "organization_id_primary" PRIMARY KEY("id")
);

CREATE TABLE "salaries"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "employeeId" INT NOT NULL,
    "organizationId" INT NOT NULL,
    "salaryAmount" BIGINT NOT NULL,
    "basicSalary" BIGINT NOT NULL,
    "HRA" BIGINT NOT NULL,
    "DA" BIGINT NOT NULL,
    "PF" BIGINT NOT NULL,
    CONSTRAINT "salaries_id_primary" PRIMARY KEY("id")
);

CREATE TABLE "salariesRequest"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "organizationId" INT NOT NULL,
    "monthYear" BIGINT NOT NULL,
    "employeeId" INT NOT NULL,
    "submittedAt" DATETIME NOT NULL,
    "approvedAt" DATETIME NOT NULL,
    "status" NVARCHAR(255) CHECK (
        "status" IN(N'draft', N'submitted', N'approved', N'rejected', N'processed')
    ) NOT NULL DEFAULT 'draft',
    "orgAccountNumber" VARCHAR(255) NOT NULL,
    "orgIfscCode" VARCHAR(255) NOT NULL,
    "remarks" VARCHAR(255) NOT NULL,
    CONSTRAINT "salariesrequest_id_primary" PRIMARY KEY("id")
);

CREATE TABLE "payroll"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "payrollMonth" BIGINT NOT NULL,
    "basicSalary" BIGINT NOT NULL,
    "HRA" BIGINT NOT NULL,
    "DA" BIGINT NOT NULL,
    "PF" BIGINT NOT NULL,
    "Deductions" BIGINT NOT NULL,
    "netSalary" BIGINT NOT NULL,
    "status" NVARCHAR(255) CHECK ("status" IN(N'pending', N'processed')) NOT NULL DEFAULT 'pending',
    "employeeId" INT NOT NULL,
    "organizationId" INT NOT NULL,
    CONSTRAINT "payroll_id_primary" PRIMARY KEY("id")
);

CREATE TABLE "clients"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "clientName" VARCHAR(255) NOT NULL,
    "organizationId" INT NOT NULL,
    "employeeId" INT NOT NULL,
    CONSTRAINT "clients_id_primary" PRIMARY KEY("id")
);

CREATE TABLE "organizationTransactions"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "clientId" INT NULL,
    "description" VARCHAR(255) NOT NULL,
    "status" NVARCHAR(255) CHECK ("status" IN(N'pending', N'paid', N'overdue')) NOT NULL DEFAULT 'pending',
    "organizationId" INT NOT NULL,
    "amount" DECIMAL(8, 2) NOT NULL,
    "due_date" DATETIME NOT NULL,
    "typeOfPayment" NVARCHAR(255) CHECK ("typeOfPayment" IN(N'receivable', N'payable')) NOT NULL DEFAULT 'receivable',
    CONSTRAINT "organizationtransactions_id_primary" PRIMARY KEY("id")
);

CREATE TABLE "handleQueriesEmployees"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "description" VARCHAR(255) NOT NULL,
    "employeeId" INT NOT NULL,
    "status" NVARCHAR(255) CHECK ("status" IN(N'pending', N'resolved')) NOT NULL DEFAULT 'pending',
    CONSTRAINT "handlequeriesemployees_id_primary" PRIMARY KEY("id")
);
CREATE TABLE "handleQueriesEmployees" (
    "id" INT IDENTITY(1,1) NOT NULL,
    "description" VARCHAR(255) NOT NULL, 
    "employeeId" INT NOT NULL,   
        "organizationId" INT NOT NULL, 
    "assignedTo" INT NULL,               
    "status" NVARCHAR(255) CHECK ("status" IN(N'pending', N'resolved')) 
        NOT NULL DEFAULT 'pending',
    "createdAt" DATETIME NOT NULL DEFAULT GETDATE(), 
    "resolvedAt" DATETIME NULL,                
    CONSTRAINT "handlequeriesemployees_id_primary" PRIMARY KEY("id"),
    CONSTRAINT "fk_handlequeries_employee" 
        FOREIGN KEY ("employeeId") REFERENCES "Employees"("id"),
    CONSTRAINT "fk_handlequeries_assignedto" 
        FOREIGN KEY ("assignedTo") REFERENCES "Employees"("id"),
            CONSTRAINT "fk_handlequeries_organization" 
        FOREIGN KEY ("organizationId") REFERENCES "Organizations"("id")
);


CREATE TABLE "handleQueriesClient"(
    "id" INT IDENTITY(1,1) NOT NULL,
    "description" VARCHAR(255) NOT NULL,
    "clientId" INT NOT NULL,
    "status" NVARCHAR(255) CHECK ("status" IN(N'pending', N'resolved')) NOT NULL DEFAULT 'pending',
    CONSTRAINT "handlequeriesclient_id_primary" PRIMARY KEY("id")
);
