---
drop table Users cascade;
Drop table Groups cascade;
Drop table Files cascade;
CREATE TABLE Users (
    userid UUID PRIMARY KEY,
    userroot VARCHAR NOT NULL unique
);
CREATE TABLE Groups (
    usergroup BIGINT PRIMARY KEY,
    userid UUID REFERENCES Users(userid),
    accesslevel INTEGER
);
CREATE TABLE Files (
    fileid BIGINT PRIMARY KEY,
    filepath VARCHAR NOT NULL,
    userid UUID REFERENCES Users (userid),
    usergroup BIGINT REFERENCES Groups (usergroup)
);
---
INSERT INTO Users
VALUES (
        'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11',
        '/test/test1.txt'
    );
INSERT INTO Groups
VALUES (
        (
            SELECT COUNT(*) + 1
            FROM Groups
        ),
        'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11',
        1
    );
INSERT INTO Files
VALUES (
        (
            SELECT COUNT(*) + 1
            FROM Files
        ),
        '/fjsdkv/kljvbls.erh',
        'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11',
        1
    );