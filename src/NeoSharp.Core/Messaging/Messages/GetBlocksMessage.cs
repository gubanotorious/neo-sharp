﻿using System.IO;
using NeoSharp.BinarySerialization;
using NeoSharp.Core.Types;

namespace NeoSharp.Core.Messaging.Messages
{
    public class GetBlocksMessage : Message<GetBlocksPayload>
    {
        public GetBlocksMessage()
        {
            Command = MessageCommand.getblocks;
            Payload = new GetBlocksPayload
            {
                HashStart = new UInt256[] { }
            };
        }

        public GetBlocksMessage(UInt256 hashStart)
        {
            Command = MessageCommand.getblocks;
            Payload = new GetBlocksPayload
            {
                HashStart = hashStart == null ? new UInt256[] { } : new[] { hashStart }
            };
        }
    }

    public class GetBlocksPayload : ISerializable
    {
        // TODO #372: Why is it an array if it is always initialized with a single value?
        [BinaryProperty(0)]
        public UInt256[] HashStart;

        [BinaryProperty(1)]
        public UInt256 HashStop = UInt256.Zero;

        public int Size { get; }
        public void Serialize(BinaryWriter writer)
        {
            //writer.Write(this.HashStart);
            //writer.Write(this.HashStop);
        }

        public void Deserialize(BinaryReader reader)
        {
            throw new System.NotImplementedException();
        }
    }
}