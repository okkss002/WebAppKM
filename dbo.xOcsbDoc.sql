CREATE TABLE [dbo].[xOcsbDoc] (
    [DocID]    INT         IDENTITY (1, 1) NOT NULL,
    [DocType]  VARCHAR (4) NULL,
	[DocGroup] VARCHAR (4) NULL,
    [DocName]  VARCHAR (400)        NULL,
    [Details]  TEXT        NULL,
    [Keyword]  TEXT        NULL,
    [FileName] VARCHAR (400)        NULL,
    [Links]    TEXT        NULL,
    [Status]   VARCHAR (2) NULL,
    PRIMARY KEY CLUSTERED ([DocID] ASC)
);

