---
create schema public;
Drop table Groups cascade;
Drop table Files cascade;
drop table Users cascade;
Drop function fnc_trg();
CREATE TABLE Users (
    userid UUID PRIMARY KEY,
    userroot VARCHAR NOT NULL unique
);
CREATE TABLE Files (
    groupid BIGINT PRIMARY KEY,
    userid UUID REFERENCES Users (userid),
    filepath VARCHAR NOT NULL,
    unique (userid, filepath)
);
CREATE TABLE Groups (
    id BIGINT PRIMARY KEY,
    groupid BIGINT REFERENCES Files (groupid),
    userid UUID REFERENCES Users(userid),
    accesslevel INTEGER
);
---
CREATE OR REPLACE FUNCTION fnc_trg() RETURNS trigger AS $$ BEGIN
INSERT INTO Groups
VALUES (
        (
            SELECT coalesce(MAX(id), 0) + 1
            FROM Groups
        ),
        NEW.groupid,
        NEW.userid,
        0
    );
RETURN NEW;
END;
$$ language plpgsql;
---
CREATE TRIGGER tr_file
AFTER
INSERT ON Files FOR EACH ROW EXECUTE FUNCTION fnc_trg();
---
INSERT INTO Users
VALUES (
        'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11',
        '/test1/'
    );
INSERT INTO Users
VALUES (
        '203caf80-1ee7-4246-8187-a148c3ecc61b',
        '/test2/'
    );
INSERT INTO Files
VALUES (
        (
            SELECT coalesce(MAX(groupid), 0) + 1
            FROM Files
        ),
        'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11',
        '/test2/test.txt'
    );
INSERT INTO Groups
VALUES (
        (
            SELECT coalesce(MAX(id), 0) + 1
            FROM Groups
        ),
        (
            SELECT groupid
            FROM Files
            WHERE filepath = '/test2/test.txt'
                AND userid = 'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11'
        ),
        '203caf80-1ee7-4246-8187-a148c3ecc61b',
        1
    );
---
SELECT userid
FROM users
WHERE userid = 'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11';
--
SELECT filepath FROM files WHERE userid = 'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11';
--
SELECT userid, accesslevel FROM groups WHERE groupid =
(
    SELECT groupid
    FROM files
    WHERE filepath = '/test2/test.txt'
        AND userid = 'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11'
);