using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoControl.DataUtils.Tests
{
    [TestClass]
    public class BytePtrTest
    {
        public static IEnumerable<object[]> GetPtrAndSizeTestData {
            get {
                var data1 = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                yield return new object[] { new BytesPtr(data1), data1 };
            }
        }
        [TestMethod]
        [DynamicData(nameof(GetPtrAndSizeTestData))]
        public void GetPtrAndSizeTest(BytesPtr BytePtr, byte[] data)
        {
            using (BytePtr.GetPtrAndSize(out var IntPtr, out var Size))
            {
                Assert.AreEqual((uint)data.Length, Size);
                byte[] _data = new byte[Size];
                Marshal.Copy(IntPtr, _data, 0, (int)Size);
                CollectionAssert.AreEqual(_data, data);
            }
        }
    }
}
