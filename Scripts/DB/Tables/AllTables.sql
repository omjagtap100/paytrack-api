CREATE TABLE "Employees"(
    "id" INT NOT NULL,
    "firstName" VARCHAR(255) NOT NULL,
    "lastName" VARCHAR(255) NOT NULL,
    "userName" VARCHAR(255) NOT NULL,
    "password" VARCHAR(255) NOT NULL,
    "email" VARCHAR(255) NOT NULL,
    "organizationId" INT NOT NULL,
    "isAccount" TINYINT NOT NULL,
    "isHR" TINYINT NOT NULL,
    "accountNo" VARCHAR(255) NULL,
    "ifscCode" VARCHAR(255) NULL,
    "HRId" INT NOT NULL,
    "isManager" TINYINT NOT NULL
);
ALTER TABLE
    "Employees" ADD CONSTRAINT "employees_id_primary" PRIMARY KEY("id");
CREATE TABLE "organization"(
    "id" INT NOT NULL,
    "fullName" VARCHAR(255) NOT NULL,
    "password" VARCHAR(255) NOT NULL,
    "ownerName" VARCHAR(255) NOT NULL,
    "ownerEmail" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "organization" ADD CONSTRAINT "organization_id_primary" PRIMARY KEY("id");
CREATE TABLE "salaries"(
    "id" INT NOT NULL,
    "employeeId" INT NOT NULL,
    "organizationId" INT NOT NULL,
    "salaryAmount" BIGINT NOT NULL,
    "basicSalary" BIGINT NOT NULL,
    "HRA" BIGINT NOT NULL,
    "DA" BIGINT NOT NULL,
    "PF" BIGINT NOT NULL
);
ALTER TABLE
    "salaries" ADD CONSTRAINT "salaries_id_primary" PRIMARY KEY("id");
CREATE TABLE "Configs"(
    "keyName" VARCHAR(255) NOT NULL,
    "value" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "Configs" ADD CONSTRAINT "configs_keyname_primary" PRIMARY KEY("keyName");
CREATE TABLE "salariesRequest"(
    "id" INT NOT NULL,
    "organizationId" INT NOT NULL,
    "monthYear" BIGINT NOT NULL,
    "employeeId" INT NOT NULL,
    "submittedAt" DATETIME NOT NULL,
    "approvedAt" DATETIME NOT NULL,
    "status" NVARCHAR(255) CHECK
        (
            "status" IN(
                N'draft',
                N'submitted',
                N'approved',
                N'rejected',
                N'processed'
            )
        ) NOT NULL DEFAULT 'draft',
        "orgAccountNumber" VARCHAR(255) NOT NULL,
        "orgIfscCode" VARCHAR(255) NOT NULL,
        "remarks" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "salariesRequest" ADD CONSTRAINT "salariesrequest_id_primary" PRIMARY KEY("id");
CREATE TABLE "payroll"(
    "id" INT NOT NULL,
    "payrollMonth" BIGINT NOT NULL,
    "basicSalary" BIGINT NOT NULL,
    "HRA" BIGINT NOT NULL,
    "DA" BIGINT NOT NULL,
    "PF" BIGINT NOT NULL,
    "Deductions" BIGINT NOT NULL,
    "netSalary" BIGINT NOT NULL,
    "status" NVARCHAR(255) CHECK
        (
            "status" IN(N'pending', N'processed')
        ) NOT NULL DEFAULT 'pending',
        "employeeId" INT NOT NULL,
        "organizationId" INT NOT NULL
);
ALTER TABLE
    "payroll" ADD CONSTRAINT "payroll_id_primary" PRIMARY KEY("id");
CREATE TABLE "clients"(
    "id" INT NOT NULL,
    "clientName" VARCHAR(255) NOT NULL,
    "organizationId" INT NOT NULL,
    "employeeId" INT NOT NULL
);
ALTER TABLE
    "clients" ADD CONSTRAINT "clients_id_primary" PRIMARY KEY("id");
CREATE TABLE "organizationTransactions"(
    "id" INT NOT NULL,
    "clientId" INT NULL,
    "description" VARCHAR(255) NOT NULL,
    "status" NVARCHAR(255) CHECK
        (
            "status" IN(N'pending', N'paid', N'overdue')
        ) NOT NULL DEFAULT 'pending',
        "organizationId" INT NOT NULL,
        "amount" DECIMAL(8, 2) NOT NULL,
        "due_date" DATETIME NOT NULL,
        "typeOfPayment" NVARCHAR(255)
    CHECK
        (
            "typeOfPayment" IN(N'receivable', N'payable')
        ) NOT NULL DEFAULT 'receivable'
);
ALTER TABLE
    "organizationTransactions" ADD CONSTRAINT "organizationtransactions_id_primary" PRIMARY KEY("id");
CREATE TABLE "handleQueriesEmployees"(
    "id" INT NOT NULL,
    "description" VARCHAR(255) NOT NULL,
    "employeeId" INT NOT NULL,
    "status" NVARCHAR(255) CHECK
        (
            "status" IN(N'pending', N'resolved')
        ) NOT NULL DEFAULT 'pending'
);
ALTER TABLE
    "handleQueriesEmployees" ADD CONSTRAINT "handlequeriesemployees_id_primary" PRIMARY KEY("id");
CREATE TABLE "handleQueriesClient"(
    "id" INT NOT NULL,
    "description" VARCHAR(255) NOT NULL,
    "clientId" INT NOT NULL,
    "status" NVARCHAR(255) CHECK
        (
            "status" IN(N'pending', N'resolved')
        ) NOT NULL DEFAULT 'pending'
);
ALTER TABLE
    "handleQueriesClient" ADD CONSTRAINT "handlequeriesclient_id_primary" PRIMARY KEY("id");
ALTER TABLE
    "salaries" ADD CONSTRAINT "salaries_organizationid_foreign" FOREIGN KEY("organizationId") REFERENCES "organization"("id");
ALTER TABLE
    "organizationTransactions" ADD CONSTRAINT "organizationtransactions_clientid_foreign" FOREIGN KEY("clientId") REFERENCES "clients"("id");
ALTER TABLE
    "payroll" ADD CONSTRAINT "payroll_employeeid_foreign" FOREIGN KEY("employeeId") REFERENCES "Employees"("id");
ALTER TABLE
    "clients" ADD CONSTRAINT "clients_organizationid_foreign" FOREIGN KEY("organizationId") REFERENCES "organization"("id");
ALTER TABLE
    "salaries" ADD CONSTRAINT "salaries_employeeid_foreign" FOREIGN KEY("employeeId") REFERENCES "Employees"("id");
ALTER TABLE
    "clients" ADD CONSTRAINT "clients_employeeid_foreign" FOREIGN KEY("employeeId") REFERENCES "Employees"("id");
ALTER TABLE
    "organizationTransactions" ADD CONSTRAINT "organizationtransactions_organizationid_foreign" FOREIGN KEY("organizationId") REFERENCES "organization"("id");
ALTER TABLE
    "payroll" ADD CONSTRAINT "payroll_organizationid_foreign" FOREIGN KEY("organizationId") REFERENCES "organization"("id");
ALTER TABLE
    "handleQueriesEmployees" ADD CONSTRAINT "handlequeriesemployees_employeeid_foreign" FOREIGN KEY("employeeId") REFERENCES "Employees"("id");
ALTER TABLE
    "salariesRequest" ADD CONSTRAINT "salariesrequest_employeeid_foreign" FOREIGN KEY("employeeId") REFERENCES "Employees"("id");
ALTER TABLE
    "Employees" ADD CONSTRAINT "employees_organizationid_foreign" FOREIGN KEY("organizationId") REFERENCES "organization"("id");
ALTER TABLE
    "handleQueriesClient" ADD CONSTRAINT "handlequeriesclient_clientid_foreign" FOREIGN KEY("clientId") REFERENCES "clients"("id");