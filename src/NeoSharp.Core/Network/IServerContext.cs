﻿using System.Collections.Concurrent;
using NeoSharp.Core.Messaging.Messages;

namespace NeoSharp.Core.Network
{
    public interface IServerContext
    {
        /// <summary>
        /// Version
        /// </summary>
        VersionPayload Version { get; }

        /// <summary>
        /// Connected peers
        /// </summary>
        ConcurrentBag<IPeer> ConnectedPeers { get; }
    }
}