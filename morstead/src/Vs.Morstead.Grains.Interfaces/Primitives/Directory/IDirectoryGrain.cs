﻿using Orleans;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public interface IDirectoryGrain : IGrainWithStringKey
    {
        Task<DirectoryItem> CreateDirectory(string path);
        Task<DirectoryItem> GetDirectory(string path);
    }
}
