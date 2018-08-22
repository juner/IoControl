using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoControl.DataUtils.Tests
{
    [TestClass]
    public class StructPtrTest
    {
        public static IEnumerable<object[]> GetPtrAndSizeTestData {
            get {
                var Size = (uint)Marshal.SizeOf<TestData>();
                yield return new object[] { new StructPtr<TestData>(), (TestData)default, Size };
                var Data1 = new TestData(100, 255);
                yield return new object[] { new StructPtr<TestData>(Data1), Data1, Size };
            }
        }
        [TestMethod]
        [DynamicData(nameof(GetPtrAndSizeTestData))]
        public void GetPtrAndSizeTest(StructPtr<TestData> StructPtr, TestData data, uint Size)
        {
            using (StructPtr.GetPtrAndSize(out var Ptr, out var _Size))
            {
                Assert.AreEqual(Size, _Size);
                Assert.AreEqual(data, (TestData)Marshal.PtrToStructure(Ptr, typeof(TestData)));
            }
        }
        public readonly struct TestData : IEquatable<TestData>
        {
            public readonly uint A;
            public readonly byte B;
            public TestData(uint A, byte B) => (this.A, this.B) = (A, B);

            public override bool Equals(object obj) => obj is TestData && Equals((TestData)obj);

            public bool Equals(TestData other) => A == other.A && B == other.B;

            public override int GetHashCode()
            {
                var hashCode = -1817952719;
                hashCode = hashCode * -1521134295 + A.GetHashCode();
                hashCode = hashCode * -1521134295 + B.GetHashCode();
                return hashCode;
            }

            public override string ToString()
                => $"{nameof(TestData)}{{{nameof(A)}:{A}, {nameof(B)}:{B}}}";

            public static bool operator ==(TestData data1, TestData data2) => data1.Equals(data2);

            public static bool operator !=(TestData data1, TestData data2) => !(data1 == data2);
        }
    }
}
