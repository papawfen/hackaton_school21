import lib
import asyncio

if __name__ == '__main__':
    tmp_file = 'temp_file'
    server = lib.FileServer()
    loop = asyncio.get_event_loop()
    loop.run_until_complete(server.start(8765))

