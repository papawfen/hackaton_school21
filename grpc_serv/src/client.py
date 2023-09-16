import os
import lib
import asyncio
import chunk_pb2, chunk_pb2_grpc
# if __name__ == '__main__':
#     client = lib.FileClient('10.54.201.22:8765')

#     # in_file_name = 'amogus.png'
#     # client.upload(in_file_name)

#     # out_file_name = 'aboba.png'
#     # if os.path.exists(out_file_name):
#     #     os.remove(out_file_name)
#     # client.download('whatever_name', out_file_name)

#     a = await client.getUserList('user1')
#     for x in a:
#         print(x, '\n'); 


async def main():
    client = lib.FileClient('10.54.201.22:8765')

    # Assuming 'user1' is a valid UID, you should await the coroutine.
    # a = await client.getUserList('user1') 
    a = await client.getUserList('user1', context=None)

    for x in a:
        print(x, '\n')

if __name__ == '__main__':
    asyncio.run(main())