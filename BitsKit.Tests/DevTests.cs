
#pragma warning disable

#if DEBUG

using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BitsKit.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitsKit.Tests;

[TestClass]
public class DevTests
{
    private readonly byte[] Data = new byte[]
    {
        239, 68, 162, 245, 92, 32, 202, 103, 218,
        251, 248, 150, 234, 92, 161, 192, 27, 146,
        52, 99, 200, 1
    };

    [TestMethod]
    public void DebugTest()
    {
       
    }
}

#endif
