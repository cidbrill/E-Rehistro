-- E-Rehistro database schema (final state after migration_002)
-- Run this to create a fresh database from scratch.
-- For existing databases, run the migration files in App_Data/ instead.

CREATE TABLE Register (
    userId   int           IDENTITY(1,1) PRIMARY KEY NOT NULL,
    email    varchar(max)  NOT NULL,
    password varchar(max)  NOT NULL,
    role     varchar(20)   NOT NULL DEFAULT 'user'
);

CREATE TABLE UserData (
    userId            int           NOT NULL,
    userLast          varchar(max)  NOT NULL,
    userFirst         varchar(max)  NOT NULL,
    userSuffix        varchar(8)    NULL,
    userMiddle        varchar(max)  NULL,
    userGender        varchar(8)    NOT NULL,
    userBirthday      date          NOT NULL,
    userBirthCity     varchar(max)  NOT NULL,
    userBirthProvince varchar(max)  NOT NULL,
    userProvince      varchar(max)  NOT NULL,
    userCity          varchar(max)  NOT NULL,
    userBarangay      varchar(max)  NOT NULL,
    userBlknlot       varchar(max)  NOT NULL,
    userCitizenship   varchar(20)   NOT NULL,
    userDateofNat     date          NULL,
    userCertNo        varchar(max)  NULL,
    fatherName        varchar(max)  NOT NULL,
    motherName        varchar(max)  NOT NULL,
    oath              varchar(15)   NOT NULL,
    registered        varchar(100)  NOT NULL,
    status            varchar(20)   NOT NULL DEFAULT 'pending',
    CONSTRAINT PK_UserData PRIMARY KEY (userId),
    CONSTRAINT FK_UserData_Register FOREIGN KEY (userId) REFERENCES Register(userId)
);

CREATE TABLE userInfoPic (
    userId    int            NOT NULL,
    fileBytes varbinary(max) NOT NULL,
    fileName  varchar(max)   NOT NULL,
    CONSTRAINT PK_userInfoPic PRIMARY KEY (userId),
    CONSTRAINT FK_userInfoPic_Register FOREIGN KEY (userId) REFERENCES Register(userId)
);

CREATE TABLE ContactMessages (
    messageId   int           IDENTITY(1,1) PRIMARY KEY NOT NULL,
    senderName  varchar(max)  NOT NULL,
    senderEmail varchar(max)  NOT NULL,
    subject     varchar(max)  NOT NULL,
    body        varchar(max)  NOT NULL,
    sentAt      datetime      NOT NULL DEFAULT GETDATE()
);
