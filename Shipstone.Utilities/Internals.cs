using System;

namespace Shipstone.Utilities
{
    internal static class Internals
    {
        internal static readonly Random _Random;

        static Internals() => Internals._Random = new Random();
    }
}
