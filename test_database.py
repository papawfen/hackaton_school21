from database import Database
import uuid
import random
import string

db = Database('postgres', 'postgres', '', 'localhost', '5432')
db.connect()
db.execute_select('SELECT * FROM public.Users;')
names = ran = ''.join(random.choices(
    string.ascii_uppercase + string.digits, k=5))
query = "INSERT INTO public.Users VALUES ('" + str(
    uuid.uuid4())+"', '/"+names + ".png');"
db.execute_insert(
    query)
db.execute_select('SELECT * FROM public.Users;')
