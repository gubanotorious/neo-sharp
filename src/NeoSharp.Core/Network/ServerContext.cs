using System;
using System.Collections.Concurrent;
using System.Reflection;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Extensions;
using NeoSharp.Core.Messaging.Messages;

namespace NeoSharp.Core.Network
{
    public class ServerContext : IServerContext
    {
        #region Private fields 
        private const uint ProtocolVersion = 0;
        private const ulong NodeNetwork = 1;

        private readonly IBlockchainContext _blockchainContext;
        private readonly VersionPayload _version;
        #endregion

        #region Public properties
        /// <inheritdoc />
        public VersionPayload Version
        {
            get
            {
                this._version.Timestamp = DateTime.UtcNow.ToTimestamp();
                this._version.CurrentBlockIndex = this._blockchainContext.CurrentBlock?.Index ?? 0;

                return _version;
            }
        }

        /// <inheritdoc />
        public ConcurrentBag<IPeer> ConnectedPeers { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Server context
        /// </summary>
        /// <param name="config">Config</param>
        /// <param name="blockchainContext">Context information updated by blockchain.</param>
        public ServerContext(NetworkConfig config, IBlockchainContext blockchainContext)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            this._blockchainContext = blockchainContext ?? throw new ArgumentNullException(nameof(blockchainContext));

            this._version = new VersionPayload
            {
                Version = ProtocolVersion,
                Services = NodeNetwork,
                Timestamp = DateTime.UtcNow.ToTimestamp(),
                Port = config.Port,
                Nonce = BitConverter.ToUInt32(Crypto.Default.GenerateRandomBytes(4), 0),
                UserAgent = $"/NEO-Sharp:{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}/",
                CurrentBlockIndex = this._blockchainContext.CurrentBlock?.Index ?? 0,
                Relay = true
            };

            this.ConnectedPeers = new ConcurrentBag<IPeer>();
        }
        #endregion
    }
}
