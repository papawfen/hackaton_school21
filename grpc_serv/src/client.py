import os
import lib

if __name__ == '__main__':
    client = lib.FileClient('localhost:8888')

    # # demo for file uploading
    # in_file_name = 'amogus.png'
    # client.upload(in_file_name)

    # demo for file downloading:
    # out_file_name = 'aboba.png'
    # if os.path.exists(out_file_name):
    #     os.remove(out_file_name)
    # client.download('whatever_name', out_file_name)

    a = client.getUserList('aboba')
    for x in a:
        print(x, '\n'); 
