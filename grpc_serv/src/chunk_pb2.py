# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: chunk.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\x0b\x63hunk.proto\"\x18\n\x03Uid\x12\x11\n\tuser_uuid\x18\x01 \x01(\t\"\x1b\n\x05\x46iles\x12\x12\n\nuser_files\x18\x01 \x01(\t\"\x17\n\x05\x43hunk\x12\x0e\n\x06\x62uffer\x18\x01 \x01(\x0c\"\x16\n\x07Request\x12\x0b\n\x03uid\x18\x01 \x01(\t\"\x17\n\x05Reply\x12\x0e\n\x06length\x18\x01 \x01(\x05\x32\x91\x01\n\nFileServer\x12\x1c\n\x06upload\x12\x06.Chunk\x1a\x06.Reply\"\x00(\x01\x12 \n\x08\x64ownload\x12\x08.Request\x1a\x06.Chunk\"\x00\x30\x01\x12#\n\x0bgetUserList\x12\x08.Request\x1a\x06.Files\"\x00\x30\x01\x12\x1e\n\x0cgetUserMinis\x12\x04.Uid\x1a\x06.Files\"\x00\x62\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'chunk_pb2', _globals)
if _descriptor._USE_C_DESCRIPTORS == False:

  DESCRIPTOR._options = None
  _globals['_UID']._serialized_start=15
  _globals['_UID']._serialized_end=39
  _globals['_FILES']._serialized_start=41
  _globals['_FILES']._serialized_end=68
  _globals['_CHUNK']._serialized_start=70
  _globals['_CHUNK']._serialized_end=93
  _globals['_REQUEST']._serialized_start=95
  _globals['_REQUEST']._serialized_end=117
  _globals['_REPLY']._serialized_start=119
  _globals['_REPLY']._serialized_end=142
  _globals['_FILESERVER']._serialized_start=145
  _globals['_FILESERVER']._serialized_end=290
# @@protoc_insertion_point(module_scope)