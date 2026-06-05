-- migration_003: Add ContactMessages table for contact form submissions
IF OBJECT_ID('ContactMessages', 'U') IS NULL
BEGIN
    CREATE TABLE ContactMessages (
        messageId   int           IDENTITY(1,1) PRIMARY KEY NOT NULL,
        senderName  varchar(max)  NOT NULL,
        senderEmail varchar(max)  NOT NULL,
        subject     varchar(max)  NOT NULL,
        body        varchar(max)  NOT NULL,
        sentAt      datetime      NOT NULL DEFAULT GETDATE()
    );
END
