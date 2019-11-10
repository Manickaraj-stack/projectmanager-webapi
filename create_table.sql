CREATE TABLE Users (
    ID int IDENTITY (1, 1) NOT NULL,
    FirstName varchar(255) NOT NULL,
	LastName varchar(255) NOT NULL,
    PRIMARY KEY (ID)
);

CREATE TABLE Project (
    ProjectId int IDENTITY (1, 1) NOT NULL,
    ProjectName varchar(255) NOT NULL,
    IsSetdate bit,
	ID int NOT NULL,
	StartDate nvarchar(255) Null,
	EndDate nvarchar(255) null,
	ProjectPriority int NOT NULL,
    PRIMARY KEY (ProjectId),
    FOREIGN KEY (ID) REFERENCES Users(ID)
);

CREATE TABLE ParentTask (
    ParentId int IDENTITY (1, 1) NOT NULL,
	ParentTask nvarchar(max) NOT NULL, 
    PRIMARY KEY (ParentId)
);

CREATE TABLE Tasks (
    TaskId INT IDENTITY (1, 1) NOT NULL,
	ProjectId INT NOT NULL,
    ParentId INT NOT NULL,
	IsParentTask bit,
    TaskName NVARCHAR (MAX) NULL,
    StartDate NVARCHAR (MAX) NULL,
    EndDate NVARCHAR (MAX) NULL,
	UserId INT NOT NULL,
    TaskPriority INT NOT NULL,
	PRIMARY KEY (TaskId),
	FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId),
	FOREIGN KEY (ParentId) REFERENCES ParentTask(ParentId),
    FOREIGN KEY (UserId) REFERENCES Users(ID)
);