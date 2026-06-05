-- migration_002: Fix userId IDENTITY bug and add status column
-- Run ONCE manually against the target database before deploying the updated application.
-- Safe to execute against an empty database or one with existing data.

-- =============================================================================
-- PRE-FLIGHT: Duplicate userId check (IDENTITY bug may have caused duplicates)
-- Run these SELECT statements FIRST. If they return rows, fix duplicates before proceeding.
-- =============================================================================
-- SELECT userId, COUNT(*) as cnt FROM UserData   GROUP BY userId HAVING COUNT(*) > 1;
-- SELECT userId, COUNT(*) as cnt FROM userInfoPic GROUP BY userId HAVING COUNT(*) > 1;

-- =============================================================================
-- IDEMPOTENCY GUARD
-- =============================================================================
IF OBJECT_ID('UserData_old', 'U') IS NOT NULL
BEGIN
    RAISERROR('Migration 002 appears to have been run already (UserData_old exists). Verify state and clean up manually.', 16, 1);
    RETURN;
END

BEGIN TRANSACTION;
BEGIN TRY

    -- =========================================================================
    -- Step 1: Drop existing FK constraints before renaming tables
    -- =========================================================================
    DECLARE @sql NVARCHAR(MAX) = '';

    SELECT @sql = @sql + 'ALTER TABLE [' + OBJECT_NAME(parent_object_id) + '] DROP CONSTRAINT [' + name + ']; '
    FROM sys.foreign_keys
    WHERE parent_object_id IN (OBJECT_ID('UserData'), OBJECT_ID('userInfoPic'));

    IF LEN(@sql) > 0
        EXEC sp_executesql @sql;

    -- =========================================================================
    -- Step 2: Recreate UserData without IDENTITY on userId, add status column
    -- =========================================================================
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

    -- =========================================================================
    -- Step 3: Recreate userInfoPic without IDENTITY on userId
    -- =========================================================================
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

    COMMIT TRANSACTION;
    PRINT 'Migration 002 completed successfully.';

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @errMsg NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('Migration 002 failed and was rolled back. Error: %s', 16, 1, @errMsg);
END CATCH;
