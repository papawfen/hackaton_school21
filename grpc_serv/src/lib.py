import os
from concurrent import futures
import grpc
import time
import chunk_pb2
import chunk_pb2_grpc
import aiofiles
import aiohttp
import PIL
from PIL import Image
import asyncio

CHUNK_SIZE = 1024 * 1024
cached_resources_path = '../cached_resources/'
root_path = '../resources/'

async def get_file_chunks_async(filename):
    async with aiofiles.open(filename, 'rb') as f:
        while True:
            piece = await f.read(CHUNK_SIZE)
            if len(piece) == 0:
                return
            yield chunk_pb2.Chunk(buffer=piece)

async def save_chunks_to_file_async(chunks, filename):
    async with aiofiles.open(filename, 'wb') as f:
        for chunk in chunks:
            await f.write(chunk.buffer)

class FileClient:
    def __init__(self, address):
        channel = grpc.aio.insecure_channel(address)
        self.stub = chunk_pb2_grpc.FileServerStub(channel)

    async def upload(self, in_file_name):
        chunks_generator = get_file_chunks_async(in_file_name)
        response = await self.stub.upload(chunks_generator)
        assert response.length == os.path.getsize(in_file_name)

    async def download(self, target_name, out_file_name):
        response = await self.stub.download(chunk_pb2.Request(name=target_name))
        await save_chunks_to_file_async(response, 'maxat.jpeg')

    async def getUserMinis(self, uid):
        path = os.path.join(root_path, uid)
        with os.scandir(path) as it:
            for entry in it:
                if not entry.name.startswith('.') and entry.is_file():
                    im = Image.open(os.path.join(path, entry.name))
                    resizedImage = im.resize((int(312), int(312)), PIL.Image.LANCZOS)
                    resizedImage.save(os.path.join(os.path.join(cached_resources_path, uid), entry.name))
        return os.listdir(os.path.join(cached_resources_path, uid))

    async def getUserList(self, request, context):
        uid = request
        if os.path.isdir(os.path.join(root_path, uid)):
            return os.listdir(os.path.join(root_path, uid))
        else:
            os.mkdir(os.path.join(root_path, uid))
            os.mkdir(os.path.join(cached_resources_path, uid))
            return {0}

class FileServer(chunk_pb2_grpc.FileServerServicer):
    def __init__(self):
        self.server = grpc.aio.server(futures.ThreadPoolExecutor(max_workers=1))
        chunk_pb2_grpc.add_FileServerServicer_to_server(self, self.server)

    async def upload(self, request_iterator):
        await save_chunks_to_file_async(request_iterator, tmp_file)
        return chunk_pb2.Reply(length=os.path.getsize(tmp_file))

    async def download(self, request):
        if request.name:
            async for chunk in get_file_chunks_async(tmp_file):
                yield chunk

    async def getUserMinis(self, uid):
        with os.scandir(os.path.join(root_path, uid)) as it:
            for entry in it:
                if not entry.name.startswith('.') and entry.is_file():
                    im = Image.open(os.path.join(root_path, entry.name))
                    resizedImage = im.resize((int(312), int(312)), PIL.Image.LANCZOS)
                    resizedImage.save(os.path.join(os.path.join(cached_resources_path, uid), entry.name))
        return os.listdir(os.path.join(cached_resources, uid))

    async def getUserList(self, request, context):
        uid = request
        if os.path.isdir(os.path.join(root_path, uid)):
            return os.listdir(os.path.join(root_path, uid))
        else:
            os.mkdir(os.path.join(root_path, uid))
            os.mkdir(os.path.join(cached_resources_path, uid))
            return {0}

    async def start(self, port):  # Make start() an async method
        self.server.add_insecure_port(f'10.54.201.22:{port}')
        await self.server.start()  # Await the start() method

        try:
            while True:
                await asyncio.sleep(60 * 60 * 24)
        except KeyboardInterrupt:
            await self.server.stop(0)
