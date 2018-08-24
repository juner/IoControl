using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoControl.DataUtils.Tests
{
    [TestClass]
    public class BytePtrTest
    {
        public static IEnumerable<object[]> GetPtrAndSizeTestData {
            get {
                var data1 = Enumerable.Range(byte.MinValue, byte.MaxValue).Select(v => (byte)v).ToArray();
                yield return new object[] { new BytesPtr(data1), data1 };
            }
        }
        [TestMethod]
        [DynamicData(nameof(GetPtrAndSizeTestData))]
        public void GetPtrAndSizeTest(BytesPtr BytePtr, byte[] data)
        {
            using (BytePtr.CreatePtr(out var IntPtr, out var Size))
            {
                Assert.AreEqual((uint)data.Length, Size);
                byte[] _data = new byte[Size];
                Marshal.Copy(IntPtr, _data, 0, (int)Size);
                CollectionAssert.AreEqual(_data, data);
            }
        }
        [TestMethod]
        [DynamicData(nameof(GetPtrAndSizeTestData))]
        public void SetPtrTest(BytesPtr BytesPtr, byte[] data)
        {
            using (BytesPtr.CreatePtr(out var Ptr, out var Size))
            {
                BytesPtr.SetPtr(Ptr, Size);
                CollectionAssert.AreEqual(data, BytesPtr.Get());
            }
        }
    }
}
