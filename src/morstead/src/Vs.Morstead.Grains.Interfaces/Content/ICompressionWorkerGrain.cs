﻿using Orleans;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Content
{
    public interface ICompressionWorkerGrain : IGrainWithIntegerKey
    {
        //[AlwaysInterleave]
        Task<byte[]> Compress(CompressionType type, byte[] uncompressed);
        Task<byte[]> Decompress(CompressionType type, byte[] compressed);
    }
}
