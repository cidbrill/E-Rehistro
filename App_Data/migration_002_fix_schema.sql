-- migration_002: Fix userId IDENTITY bug and add status column
-- Run this once against the target database before deploying Task 2+.

-- Step 1: Drop FK constraints that reference the broken columns
-- (Query actual FK names first: SELECT name FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('UserData');)
-- Then replace the constraint names below with the real ones, or omit these lines if tables are empty.

-- Step 2: Recreate UserData without IDENTITY on userId
EXEC sp_rename 'UserData', 'UserData_old';

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

INSERT INTO UserData (userId, userLast, userFirst, userSuffix, userMiddle,
    userGender, userBirthday, userBirthCity, userBirthProvince, userProvince,
    userCity, userBarangay, userBlknlot, userCitizenship, userDateofNat,
    userCertNo, fatherName, motherName, oath, registered)
SELECT userId, userLast, userFirst, userSuffix, userMiddle,
    userGender, userBirthday, userBirthCity, userBirthProvince, userProvince,
    userCity, userBarangay, userBlknlot, userCitizenship, userDateofNat,
    userCertNo, fatherName, motherName, oath, registered
FROM UserData_old;

DROP TABLE UserData_old;

-- Step 3: Recreate userInfoPic without IDENTITY on userId
EXEC sp_rename 'userInfoPic', 'userInfoPic_old';

CREATE TABLE userInfoPic (
    userId    int            NOT NULL,
    fileBytes varbinary(max) NOT NULL,
    fileName  varchar(max)   NOT NULL,
    CONSTRAINT PK_userInfoPic PRIMARY KEY (userId),
    CONSTRAINT FK_userInfoPic_Register FOREIGN KEY (userId) REFERENCES Register(userId)
);

INSERT INTO userInfoPic (userId, fileBytes, fileName)
SELECT userId, fileBytes, fileName FROM userInfoPic_old;

DROP TABLE userInfoPic_old;
