import psycopg2
from psycopg2 import Error


# def database_connect():
try:
    connection = psycopg2.connect(dbname='postgres', user='postgres', password='', host='localhost')
    cursor = connection.cursor()
except(Exception, Error) as error:
    print('Can`t connect to database, error ', error)
    
    def insert_into_users(userid, userroot):
        cursor.execute(
        'INSERT INTO public.Users \
        ("userid", "userroot") \
        VALUES (%s, %s);',
        (userid, str(userroot)),
        )
        connection.commit()
    
    def insert_into_groups(usergroup, userid, accesslevel):
        cursor.execute(
        'INSERT INTO public.Gpoups \
        ("usergroup", "userid", "accesslevel") \
        VALUES (%s, %s, %s);',
        (usergroup, userid, accesslevel),
        )
        connection.commit()

    def insert_into_files(fileid, filepath, userid, usergroup):
        cursor.execute(
        'INSERT INTO public.Files \
        ("fileid", "filepath", "userid", "usergroup") \
        VALUES (%s, %s, %s, %s);',
        (fileid, str(filepath), userid, usergroup),
        )
        connection.commit()

    def select_from_files(userid, usergroup):
        result = ""
        cursor.execute(
        f"SELECT filepath FROM public.Files \
            WHERE userid = '{userid}' AND usergroup = '{usergroup}';"
        )
        result = cursor.fetchall()
        return result[0][0]
    
    def select_from_files():
        result = ""
        cursor.execute(
        f"SELECT * FROM public.Files;"
        )
        result = cursor.fetchall()
        return result

def database_close_connect(connection, cursor):
    if connection:
        cursor.close()
        connection.close()

