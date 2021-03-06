﻿using IoControl;
using IoControl.Disk;
using IoControl.Controller;
using IoControl.MassStorage;
using IoControl.FileSystem;
using static IoControl.Utils.DeviceUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using IoControl.Volume;

namespace IoControlPhysicalExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.WriteLine($"[{string.Join(", ", QueryDocDevice())}]");
            foreach (var IoControl in FindVolumes()
                        .Select(v => v.Replace(@"\\?\", @"\\.\").TrimEnd('\\'))
                    .Concat(
                        QueryDocDevice()
                        .Where(v => v.StartsWith("HarddiskVolume"))
                        .Select(v => $@"\\.\{v}")
                    )
                    .Select(v => new IoControl.IoControl(v, 
                        FileAccess: FileAccess.ReadWrite,
                        FileShare: FileShare.ReadWrite,
                        CreationDisposition: FileMode.Open,
                        FlagAndAttributes: FileFlagAndAttributesExtensions.Create(FileAttributes.Normal)))
                    .Using())
            {
                try
                {
                    Trace.WriteLine(IoControl);
                    Trace.WriteLine(nameof(FileSystemExtensions.FsctlIsVolumeMounted));
                    Trace.WriteLine(IoControl.FsctlIsVolumeMounted());

                } catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine($"{nameof(MassStorageExtensions.StorageGetDeviceNumber)}");
                    Trace.WriteLine($"{IoControl.StorageGetDeviceNumber()}");
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(DiskExtensions.DiskGetPartitionInfoEx));
                    Trace.WriteLine(IoControl.DiskGetPartitionInfoEx());
                }catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine($"{nameof(VolumeExtensions.VolumeGetVolumeDiskExtents)}");
                    Trace.WriteLine(IoControl.VolumeGetVolumeDiskExtents());
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }

            }
            foreach (var IoControl in GetPhysicalDrives(
                FileAccess: FileAccess.ReadWrite,
                    FileShare: FileShare.ReadWrite,
                    CreationDisposition: FileMode.Open,
                    FlagsAndAttributes: FileFlagAndAttributesExtensions.Create(FileAttributes.Normal)))
            {
                try
                {
                    Trace.WriteLine(nameof(MassStorageExtensions.StorageGetDeviceNumber));
                    IoControl.StorageGetDeviceNumber(out var number, out _);
                    Trace.WriteLine(number);
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(DiskExtensions.DiskGetDriveGeometryEx2));
                    IoControl.DiskGetDriveGeometryEx(out var geometry, out var disksize, out _);
                    Trace.WriteLine($"Geometry: {geometry}");
                    Trace.WriteLine($"DiskSize: {disksize}");
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough));
                    Trace.WriteLine(IoControl.AtaPassThroughCheckPowerMode());
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough));
                    Trace.WriteLine(IoControl.AtaPassThroughSmartData());
                }catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough));;
                    Trace.WriteLine(IoControl.AtaPassThroughIdentifyDevice());

                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetLengthInfo));
                    IoControl.DiskGetLengthInfo(out var disksize);
                    Trace.WriteLine(disksize);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetDriveLayoutEx));
                    IoControl.DiskGetDriveLayoutEx(out var layout, out _);
                    Trace.WriteLine(layout);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(DiskExtensions.SmartRcvDriveDataIdentifyDevice) + " 0xA0");
                    var result = IoControl.SmartRcvDriveDataIdentifyDevice(0xA0, out var AtaIdentifyDevice);
                    Trace.WriteLine($"{nameof(result)}:{result}");
                    Trace.WriteLine(AtaIdentifyDevice);

                    Trace.WriteLine(nameof(DiskExtensions.SmartRcvDriveDataIdentifyDevice) + " 0xB0");
                    result = IoControl.SmartRcvDriveDataIdentifyDevice(0xB0, out AtaIdentifyDevice);
                    Trace.WriteLine($"{nameof(result)}:{result}");
                    Trace.WriteLine(AtaIdentifyDevice);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
            foreach (var IoControl in GetScsiDrives(
                FileAccess: FileAccess.ReadWrite,
                    FileShare: FileShare.ReadWrite,
                    CreationDisposition: FileMode.Open,
                    FlagsAndAttributes: FileFlagAndAttributesExtensions.Create(FileAttributes.Normal)))
            {
                try
                {
                    Trace.WriteLine(nameof(ControllerExtentions.ScsiMiniportIdentify));
                    var Address = IoControl.ScsiGetAddress();
                    Trace.WriteLine(Address);
                    Trace.WriteLine(IoControl.ScsiMiniportIdentify(Address.TargetId));
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
            Console.ReadLine();
        }
        public static IEnumerable<IoControl.IoControl> GetPhysicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileFlagAndAttributes FlagsAndAttributes = default)
        {
            bool hasDrive = false;
            foreach (var DeviceName in QueryDocDevice().Where(v => v.IndexOf("PhysicalDrive") == 0))
            {
                var Path = $@"\\.\{DeviceName}";
                using (var file = new IoControl.IoControl(Path, FileAccess, FileShare, CreationDisposition, FlagsAndAttributes))
                {
                    Trace.WriteLine($"Open {file} ... {(file.IsInvalid ? "NG" : "OK")}.");
                    if (file.IsInvalid)
                        continue;
                    hasDrive = true;
                    yield return file;
                }
            }
            if (!hasDrive)
                throw new Exception("対象となるドライブがありません。");
        }

        public static IEnumerable<IoControl.IoControl> GetScsiDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileFlagAndAttributes FlagsAndAttributes = default)
        {
            bool hasDrive = false;
            foreach (var DeviceName in QueryDocDevice().Where(v => v.IndexOf("Scsi") == 0))
            {
                var Path = $@"\\.\{DeviceName}";
                using (var file = new IoControl.IoControl(Path, FileAccess, FileShare, CreationDisposition, FlagsAndAttributes))
                {
                    Trace.WriteLine($"Open {file} ... {(file.IsInvalid ? "NG" : "OK")}.");
                    if (file.IsInvalid)
                        continue;
                    hasDrive = true;
                    yield return file;
                }
            }
            if (!hasDrive)
                throw new Exception("対象となるドライブがありません。");
        }
    }
}
