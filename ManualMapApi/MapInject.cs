// Decompiled with JetBrains decompiler
// Type: ManualMapApi.MapInject
// Assembly: CeleryApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C2BCA464-2E77-4DEE-B9BF-40F89C268B00
// Assembly location: C:\Users\brady\Downloads\Celery\CeleryApp.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ManualMapApi
{
  public class MapInject
  {
    private const int IMAGE_NUMBEROF_DIRECTORY_ENTRIES = 16;
    private const ushort IMAGE_FILE_MACHINE_I386 = 332;
    private const ushort CURRENT_ARCH = 332;
    private const uint STATUS_PENDING = 259;
    private const uint STILL_ACTIVE = 259;
    private static int previousHandle;
    private static int previousModuleBase;
    private static int previousModuleSize;

    private static MapInject.IMAGE_OPTIONAL_HEADER toImageOptionalHeader(
      byte[] byteArray,
      int offset)
    {
      MapInject.IMAGE_OPTIONAL_HEADER imageOptionalHeader;
      imageOptionalHeader.Magic = BitConverter.ToUInt16(byteArray, offset);
      imageOptionalHeader.MajorLinkerVersion = (byte) BitConverter.ToChar(byteArray, offset + 2);
      imageOptionalHeader.MinorLinkerVersion = (byte) BitConverter.ToChar(byteArray, offset + 3);
      imageOptionalHeader.SizeOfCode = BitConverter.ToUInt32(byteArray, offset + 4);
      imageOptionalHeader.SizeOfInitializedData = BitConverter.ToUInt32(byteArray, offset + 8);
      imageOptionalHeader.SizeOfUninitializedData = BitConverter.ToUInt32(byteArray, offset + 12);
      imageOptionalHeader.AddressOfEntryPoint = BitConverter.ToUInt32(byteArray, offset + 16);
      imageOptionalHeader.BaseOfCode = BitConverter.ToUInt32(byteArray, offset + 20);
      imageOptionalHeader.BaseOfData = BitConverter.ToUInt32(byteArray, offset + 24);
      imageOptionalHeader.ImageBase = BitConverter.ToUInt32(byteArray, offset + 28);
      imageOptionalHeader.SectionAlignment = BitConverter.ToUInt32(byteArray, offset + 32);
      imageOptionalHeader.FileAlignment = BitConverter.ToUInt32(byteArray, offset + 36);
      imageOptionalHeader.MajorOperatingSystemVersion = BitConverter.ToUInt16(byteArray, offset + 40);
      imageOptionalHeader.MinorOperatingSystemVersion = BitConverter.ToUInt16(byteArray, offset + 42);
      imageOptionalHeader.MajorImageVersion = BitConverter.ToUInt16(byteArray, offset + 44);
      imageOptionalHeader.MinorImageVersion = BitConverter.ToUInt16(byteArray, offset + 46);
      imageOptionalHeader.MajorSubsystemVersion = BitConverter.ToUInt16(byteArray, offset + 48);
      imageOptionalHeader.MinorSubsystemVersion = BitConverter.ToUInt16(byteArray, offset + 50);
      imageOptionalHeader.Win32VersionValue = BitConverter.ToUInt32(byteArray, offset + 52);
      imageOptionalHeader.SizeOfImage = BitConverter.ToUInt32(byteArray, offset + 56);
      imageOptionalHeader.SizeOfHeaders = BitConverter.ToUInt32(byteArray, offset + 60);
      imageOptionalHeader.CheckSum = BitConverter.ToUInt32(byteArray, offset + 64);
      imageOptionalHeader.Subsystem = BitConverter.ToUInt16(byteArray, offset + 68);
      imageOptionalHeader.DllCharacteristics = BitConverter.ToUInt16(byteArray, offset + 70);
      imageOptionalHeader.SizeOfStackReserve = BitConverter.ToUInt32(byteArray, offset + 72);
      imageOptionalHeader.SizeOfStackCommit = BitConverter.ToUInt32(byteArray, offset + 76);
      imageOptionalHeader.SizeOfHeapReserve = BitConverter.ToUInt32(byteArray, offset + 80);
      imageOptionalHeader.SizeOfHeapCommit = BitConverter.ToUInt32(byteArray, offset + 84);
      imageOptionalHeader.LoaderFlags = BitConverter.ToUInt32(byteArray, offset + 88);
      imageOptionalHeader.NumberOfRvaAndSizes = BitConverter.ToUInt32(byteArray, offset + 92);
      imageOptionalHeader.DataDirectory = new MapInject.IMAGE_DATA_DIRECTORY[16];
      for (int index = 0; index < 10; ++index)
      {
        MapInject.IMAGE_DATA_DIRECTORY imageDataDirectory;
        imageDataDirectory.VirtualAddress = BitConverter.ToUInt32(byteArray, offset + (96 + index * 8));
        imageDataDirectory.Size = BitConverter.ToUInt32(byteArray, offset + (96 + index * 8) + 4);
        imageOptionalHeader.DataDirectory[index] = imageDataDirectory;
      }
      return imageOptionalHeader;
    }

    private static MapInject.IMAGE_FILE_HEADER toImageFileHeader(byte[] byteArray, int offset)
    {
      MapInject.IMAGE_FILE_HEADER imageFileHeader;
      imageFileHeader.Machine = BitConverter.ToUInt16(byteArray, offset);
      imageFileHeader.NumberOfSections = BitConverter.ToUInt16(byteArray, offset + 2);
      imageFileHeader.TimeDateStamp = BitConverter.ToUInt32(byteArray, offset + 4);
      imageFileHeader.PointerToSymbolTable = BitConverter.ToUInt32(byteArray, offset + 8);
      imageFileHeader.NumberOfSymbols = BitConverter.ToUInt32(byteArray, offset + 12);
      imageFileHeader.SizeOfOptionalHeader = BitConverter.ToUInt16(byteArray, offset + 16);
      imageFileHeader.Characteristics = BitConverter.ToUInt16(byteArray, offset + 18);
      return imageFileHeader;
    }

    private static MapInject.IMAGE_DOS_HEADER toImageDosHeader(byte[] byteArray, int offset)
    {
      MapInject.IMAGE_DOS_HEADER imageDosHeader;
      imageDosHeader.e_magic = BitConverter.ToUInt16(byteArray, offset);
      imageDosHeader.e_cblp = BitConverter.ToUInt16(byteArray, offset + 2);
      imageDosHeader.e_cp = BitConverter.ToUInt16(byteArray, offset + 4);
      imageDosHeader.e_crlc = BitConverter.ToUInt16(byteArray, offset + 6);
      imageDosHeader.e_cparhdr = BitConverter.ToUInt16(byteArray, offset + 8);
      imageDosHeader.e_minalloc = BitConverter.ToUInt16(byteArray, offset + 10);
      imageDosHeader.e_maxalloc = BitConverter.ToUInt16(byteArray, offset + 12);
      imageDosHeader.e_ss = BitConverter.ToUInt16(byteArray, offset + 14);
      imageDosHeader.e_sp = BitConverter.ToUInt16(byteArray, offset + 16);
      imageDosHeader.e_csum = BitConverter.ToUInt16(byteArray, offset + 18);
      imageDosHeader.e_ip = BitConverter.ToUInt16(byteArray, offset + 20);
      imageDosHeader.e_cs = BitConverter.ToUInt16(byteArray, offset + 22);
      imageDosHeader.e_lfarlc = BitConverter.ToUInt16(byteArray, offset + 24);
      imageDosHeader.e_ovno = BitConverter.ToUInt16(byteArray, offset + 26);
      imageDosHeader.e_res = new ushort[4];
      imageDosHeader.e_res[0] = BitConverter.ToUInt16(byteArray, offset + 28);
      imageDosHeader.e_res[1] = BitConverter.ToUInt16(byteArray, offset + 30);
      imageDosHeader.e_res[2] = BitConverter.ToUInt16(byteArray, offset + 32);
      imageDosHeader.e_res[3] = BitConverter.ToUInt16(byteArray, offset + 34);
      imageDosHeader.e_oemid = BitConverter.ToUInt16(byteArray, offset + 36);
      imageDosHeader.e_oeminfo = BitConverter.ToUInt16(byteArray, offset + 38);
      imageDosHeader.e_res2 = new ushort[10];
      imageDosHeader.e_res2[0] = BitConverter.ToUInt16(byteArray, offset + 40);
      imageDosHeader.e_res2[1] = BitConverter.ToUInt16(byteArray, offset + 42);
      imageDosHeader.e_res2[2] = BitConverter.ToUInt16(byteArray, offset + 44);
      imageDosHeader.e_res2[3] = BitConverter.ToUInt16(byteArray, offset + 46);
      imageDosHeader.e_res2[4] = BitConverter.ToUInt16(byteArray, offset + 48);
      imageDosHeader.e_res2[5] = BitConverter.ToUInt16(byteArray, offset + 50);
      imageDosHeader.e_res2[6] = BitConverter.ToUInt16(byteArray, offset + 52);
      imageDosHeader.e_res2[7] = BitConverter.ToUInt16(byteArray, offset + 54);
      imageDosHeader.e_res2[8] = BitConverter.ToUInt16(byteArray, offset + 56);
      imageDosHeader.e_res2[9] = BitConverter.ToUInt16(byteArray, offset + 58);
      imageDosHeader.e_lfanew = BitConverter.ToUInt32(byteArray, offset + 60);
      return imageDosHeader;
    }

    private static MapInject.IMAGE_NT_HEADERS toImageNtHeaders(byte[] byteArray, int offset)
    {
      MapInject.IMAGE_NT_HEADERS imageNtHeaders;
      imageNtHeaders.Signature = BitConverter.ToUInt32(byteArray, offset);
      imageNtHeaders.FileHeader = MapInject.toImageFileHeader(byteArray, offset + 4);
      imageNtHeaders.OptionalHeader = MapInject.toImageOptionalHeader(byteArray, offset + 24);
      return imageNtHeaders;
    }

    private static MapInject.IMAGE_SECTION_HEADER toImageSectionHeader(byte[] byteArray, int offset)
    {
      MapInject.IMAGE_SECTION_HEADER imageSectionHeader;
      imageSectionHeader.Name = new byte[8];
      for (int index = 0; index < 8; ++index)
        imageSectionHeader.Name[index] = (byte) BitConverter.ToChar(byteArray, offset + index);
      imageSectionHeader.PhysicalAddressOrVirtualSize = BitConverter.ToUInt32(byteArray, offset + 8);
      imageSectionHeader.VirtualAddress = BitConverter.ToUInt32(byteArray, offset + 12);
      imageSectionHeader.SizeOfRawData = BitConverter.ToUInt32(byteArray, offset + 16);
      imageSectionHeader.PointerToRawData = BitConverter.ToUInt32(byteArray, offset + 20);
      imageSectionHeader.PointerToRelocations = BitConverter.ToUInt32(byteArray, offset + 24);
      imageSectionHeader.PointerToLinenumbers = BitConverter.ToUInt32(byteArray, offset + 28);
      imageSectionHeader.NumberOfRelocations = BitConverter.ToUInt16(byteArray, offset + 32);
      imageSectionHeader.NumberOfLinenumbers = BitConverter.ToUInt16(byteArray, offset + 36);
      imageSectionHeader.Characteristics = BitConverter.ToUInt32(byteArray, offset + 40);
      return imageSectionHeader;
    }

    private static byte[] makePayload() => new byte[662]
    {
      (byte) 85,
      (byte) 139,
      (byte) 236,
      (byte) 131,
      (byte) 236,
      (byte) 96,
      (byte) 131,
      (byte) 125,
      (byte) 8,
      (byte) 0,
      (byte) 117,
      (byte) 15,
      (byte) 139,
      (byte) 69,
      (byte) 8,
      (byte) 199,
      (byte) 64,
      (byte) 12,
      (byte) 64,
      (byte) 64,
      (byte) 64,
      (byte) 0,
      (byte) 233,
      (byte) 117,
      (byte) 2,
      (byte) 0,
      (byte) 0,
      (byte) 139,
      (byte) 77,
      (byte) 8,
      (byte) 139,
      (byte) 81,
      (byte) 8,
      (byte) 137,
      (byte) 85,
      (byte) 252,
      (byte) 139,
      (byte) 69,
      (byte) 252,
      (byte) 139,
      (byte) 72,
      (byte) 60,
      (byte) 139,
      (byte) 85,
      (byte) 252,
      (byte) 141,
      (byte) 68,
      (byte) 10,
      (byte) 24,
      (byte) 137,
      (byte) 69,
      (byte) 244,
      (byte) 139,
      (byte) 77,
      (byte) 8,
      (byte) 139,
      (byte) 17,
      (byte) 137,
      (byte) 85,
      (byte) 196,
      (byte) 139,
      (byte) 69,
      (byte) 8,
      (byte) 139,
      (byte) 72,
      (byte) 4,
      (byte) 137,
      (byte) 77,
      (byte) 208,
      (byte) 139,
      (byte) 85,
      (byte) 244,
      (byte) 139,
      (byte) 69,
      (byte) 252,
      (byte) 3,
      (byte) 66,
      (byte) 16,
      (byte) 137,
      (byte) 69,
      (byte) 164,
      (byte) 139,
      (byte) 77,
      (byte) 244,
      (byte) 139,
      (byte) 85,
      (byte) 252,
      (byte) 43,
      (byte) 81,
      (byte) 28,
      (byte) 137,
      (byte) 85,
      (byte) 216,
      (byte) 15,
      (byte) 132,
      (byte) 195,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 184,
      (byte) 8,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 107,
      (byte) 200,
      (byte) 5,
      (byte) 139,
      (byte) 85,
      (byte) 244,
      (byte) 131,
      (byte) 124,
      (byte) 10,
      (byte) 100,
      (byte) 0,
      (byte) 117,
      (byte) 15,
      (byte) 139,
      (byte) 69,
      (byte) 8,
      (byte) 199,
      (byte) 64,
      (byte) 12,
      (byte) 96,
      (byte) 96,
      (byte) 96,
      (byte) 0,
      (byte) 233,
      (byte) 12,
      (byte) 2,
      (byte) 0,
      (byte) 0,
      (byte) 185,
      (byte) 8,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 107,
      (byte) 209,
      (byte) 5,
      (byte) 139,
      (byte) 69,
      (byte) 244,
      (byte) 139,
      (byte) 77,
      (byte) 252,
      (byte) 3,
      (byte) 76,
      (byte) 16,
      (byte) 96,
      (byte) 137,
      (byte) 77,
      (byte) 240,
      (byte) 139,
      (byte) 85,
      (byte) 240,
      (byte) 131,
      (byte) 58,
      (byte) 0,
      (byte) 15,
      (byte) 132,
      (byte) 129,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 139,
      (byte) 69,
      (byte) 240,
      (byte) 139,
      (byte) 72,
      (byte) 4,
      (byte) 131,
      (byte) 233,
      (byte) 8,
      (byte) 209,
      (byte) 233,
      (byte) 137,
      (byte) 77,
      (byte) 200,
      (byte) 139,
      (byte) 85,
      (byte) 240,
      (byte) 131,
      (byte) 194,
      (byte) 8,
      (byte) 137,
      (byte) 85,
      (byte) 224,
      (byte) 199,
      (byte) 69,
      (byte) 220,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 235,
      (byte) 18,
      (byte) 139,
      (byte) 69,
      (byte) 220,
      (byte) 131,
      (byte) 192,
      (byte) 1,
      (byte) 137,
      (byte) 69,
      (byte) 220,
      (byte) 139,
      (byte) 77,
      (byte) 224,
      (byte) 131,
      (byte) 193,
      (byte) 2,
      (byte) 137,
      (byte) 77,
      (byte) 224,
      (byte) 139,
      (byte) 85,
      (byte) 220,
      (byte) 59,
      (byte) 85,
      (byte) 200,
      (byte) 116,
      (byte) 54,
      (byte) 139,
      (byte) 69,
      (byte) 224,
      (byte) 15,
      (byte) 183,
      (byte) 8,
      (byte) 193,
      (byte) 249,
      (byte) 12,
      (byte) 131,
      (byte) 249,
      (byte) 3,
      (byte) 117,
      (byte) 38,
      (byte) 139,
      (byte) 85,
      (byte) 240,
      (byte) 139,
      (byte) 69,
      (byte) 252,
      (byte) 3,
      (byte) 2,
      (byte) 139,
      (byte) 77,
      (byte) 224,
      (byte) 15,
      (byte) 183,
      (byte) 17,
      (byte) 129,
      (byte) 226,
      byte.MaxValue,
      (byte) 15,
      (byte) 0,
      (byte) 0,
      (byte) 3,
      (byte) 194,
      (byte) 137,
      (byte) 69,
      (byte) 212,
      (byte) 139,
      (byte) 69,
      (byte) 212,
      (byte) 139,
      (byte) 8,
      (byte) 3,
      (byte) 77,
      (byte) 216,
      (byte) 139,
      (byte) 85,
      (byte) 212,
      (byte) 137,
      (byte) 10,
      (byte) 235,
      (byte) 176,
      (byte) 139,
      (byte) 69,
      (byte) 240,
      (byte) 139,
      (byte) 77,
      (byte) 240,
      (byte) 3,
      (byte) 72,
      (byte) 4,
      (byte) 137,
      (byte) 77,
      (byte) 240,
      (byte) 233,
      (byte) 115,
      byte.MaxValue,
      byte.MaxValue,
      byte.MaxValue,
      (byte) 186,
      (byte) 8,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 193,
      (byte) 226,
      (byte) 0,
      (byte) 139,
      (byte) 69,
      (byte) 244,
      (byte) 131,
      (byte) 124,
      (byte) 16,
      (byte) 100,
      (byte) 0,
      (byte) 15,
      (byte) 132,
      (byte) 220,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 185,
      (byte) 8,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 193,
      (byte) 225,
      (byte) 0,
      (byte) 139,
      (byte) 85,
      (byte) 244,
      (byte) 139,
      (byte) 69,
      (byte) 252,
      (byte) 3,
      (byte) 68,
      (byte) 10,
      (byte) 96,
      (byte) 137,
      (byte) 69,
      (byte) 236,
      (byte) 139,
      (byte) 77,
      (byte) 236,
      (byte) 131,
      (byte) 121,
      (byte) 12,
      (byte) 0,
      (byte) 15,
      (byte) 132,
      (byte) 186,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 139,
      (byte) 85,
      (byte) 236,
      (byte) 139,
      (byte) 69,
      (byte) 252,
      (byte) 3,
      (byte) 66,
      (byte) 12,
      (byte) 137,
      (byte) 69,
      (byte) 192,
      (byte) 139,
      (byte) 77,
      (byte) 196,
      (byte) 137,
      (byte) 77,
      (byte) 188,
      (byte) 139,
      (byte) 85,
      (byte) 192,
      (byte) 82,
      byte.MaxValue,
      (byte) 85,
      (byte) 188,
      (byte) 137,
      (byte) 69,
      (byte) 204,
      (byte) 139,
      (byte) 69,
      (byte) 236,
      (byte) 139,
      (byte) 77,
      (byte) 252,
      (byte) 3,
      (byte) 8,
      (byte) 137,
      (byte) 77,
      (byte) 248,
      (byte) 139,
      (byte) 85,
      (byte) 236,
      (byte) 139,
      (byte) 69,
      (byte) 252,
      (byte) 3,
      (byte) 66,
      (byte) 16,
      (byte) 137,
      (byte) 69,
      (byte) 232,
      (byte) 131,
      (byte) 125,
      (byte) 248,
      (byte) 0,
      (byte) 117,
      (byte) 6,
      (byte) 139,
      (byte) 77,
      (byte) 232,
      (byte) 137,
      (byte) 77,
      (byte) 248,
      (byte) 235,
      (byte) 18,
      (byte) 139,
      (byte) 85,
      (byte) 248,
      (byte) 131,
      (byte) 194,
      (byte) 4,
      (byte) 137,
      (byte) 85,
      (byte) 248,
      (byte) 139,
      (byte) 69,
      (byte) 232,
      (byte) 131,
      (byte) 192,
      (byte) 4,
      (byte) 137,
      (byte) 69,
      (byte) 232,
      (byte) 139,
      (byte) 77,
      (byte) 248,
      (byte) 131,
      (byte) 57,
      (byte) 0,
      (byte) 116,
      (byte) 81,
      (byte) 139,
      (byte) 85,
      (byte) 248,
      (byte) 139,
      (byte) 2,
      (byte) 37,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 128,
      (byte) 116,
      (byte) 31,
      (byte) 139,
      (byte) 77,
      (byte) 208,
      (byte) 137,
      (byte) 77,
      (byte) 184,
      (byte) 139,
      (byte) 85,
      (byte) 248,
      (byte) 139,
      (byte) 2,
      (byte) 37,
      byte.MaxValue,
      byte.MaxValue,
      (byte) 0,
      (byte) 0,
      (byte) 80,
      (byte) 139,
      (byte) 77,
      (byte) 204,
      (byte) 81,
      byte.MaxValue,
      (byte) 85,
      (byte) 184,
      (byte) 139,
      (byte) 85,
      (byte) 232,
      (byte) 137,
      (byte) 2,
      (byte) 235,
      (byte) 36,
      (byte) 139,
      (byte) 69,
      (byte) 248,
      (byte) 139,
      (byte) 77,
      (byte) 252,
      (byte) 3,
      (byte) 8,
      (byte) 137,
      (byte) 77,
      (byte) 180,
      (byte) 139,
      (byte) 85,
      (byte) 208,
      (byte) 137,
      (byte) 85,
      (byte) 176,
      (byte) 139,
      (byte) 69,
      (byte) 180,
      (byte) 131,
      (byte) 192,
      (byte) 2,
      (byte) 80,
      (byte) 139,
      (byte) 77,
      (byte) 204,
      (byte) 81,
      byte.MaxValue,
      (byte) 85,
      (byte) 176,
      (byte) 139,
      (byte) 85,
      (byte) 232,
      (byte) 137,
      (byte) 2,
      (byte) 235,
      (byte) 149,
      (byte) 139,
      (byte) 69,
      (byte) 236,
      (byte) 131,
      (byte) 192,
      (byte) 20,
      (byte) 137,
      (byte) 69,
      (byte) 236,
      (byte) 233,
      (byte) 57,
      byte.MaxValue,
      byte.MaxValue,
      byte.MaxValue,
      (byte) 185,
      (byte) 8,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 107,
      (byte) 209,
      (byte) 9,
      (byte) 139,
      (byte) 69,
      (byte) 244,
      (byte) 131,
      (byte) 124,
      (byte) 16,
      (byte) 100,
      (byte) 0,
      (byte) 116,
      (byte) 76,
      (byte) 185,
      (byte) 8,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 107,
      (byte) 209,
      (byte) 9,
      (byte) 139,
      (byte) 69,
      (byte) 244,
      (byte) 139,
      (byte) 77,
      (byte) 252,
      (byte) 3,
      (byte) 76,
      (byte) 16,
      (byte) 96,
      (byte) 137,
      (byte) 77,
      (byte) 172,
      (byte) 139,
      (byte) 85,
      (byte) 172,
      (byte) 139,
      (byte) 66,
      (byte) 12,
      (byte) 137,
      (byte) 69,
      (byte) 228,
      (byte) 235,
      (byte) 9,
      (byte) 139,
      (byte) 77,
      (byte) 228,
      (byte) 131,
      (byte) 193,
      (byte) 4,
      (byte) 137,
      (byte) 77,
      (byte) 228,
      (byte) 131,
      (byte) 125,
      (byte) 228,
      (byte) 0,
      (byte) 116,
      (byte) 29,
      (byte) 139,
      (byte) 85,
      (byte) 228,
      (byte) 131,
      (byte) 58,
      (byte) 0,
      (byte) 116,
      (byte) 21,
      (byte) 139,
      (byte) 69,
      (byte) 228,
      (byte) 139,
      (byte) 8,
      (byte) 137,
      (byte) 77,
      (byte) 168,
      (byte) 106,
      (byte) 0,
      (byte) 106,
      (byte) 1,
      (byte) 139,
      (byte) 85,
      (byte) 252,
      (byte) 82,
      byte.MaxValue,
      (byte) 85,
      (byte) 168,
      (byte) 235,
      (byte) 212,
      (byte) 139,
      (byte) 69,
      (byte) 164,
      (byte) 137,
      (byte) 69,
      (byte) 160,
      (byte) 106,
      (byte) 0,
      (byte) 106,
      (byte) 1,
      (byte) 139,
      (byte) 77,
      (byte) 252,
      (byte) 81,
      byte.MaxValue,
      (byte) 85,
      (byte) 160,
      (byte) 139,
      (byte) 85,
      (byte) 8,
      (byte) 139,
      (byte) 69,
      (byte) 252,
      (byte) 137,
      (byte) 66,
      (byte) 12,
      (byte) 139,
      (byte) 229,
      (byte) 93,
      (byte) 194,
      (byte) 4,
      (byte) 0
    };

    public static bool ManualMap(Process proc, string filepath)
    {
      int hProcess = MapInject.Imports.OpenProcess(2035711U, false, proc.Id);
      if (hProcess == 0)
        throw new Exception("Could not open process");
      int num1 = 0;
      if (MapInject.previousHandle == hProcess)
      {
        byte[] lpBuffer = new byte[MapInject.previousModuleSize];
        Array.Clear((Array) lpBuffer, 0, MapInject.previousModuleSize);
        MapInject.Imports.WriteProcessMemory(hProcess, MapInject.previousModuleBase, lpBuffer, MapInject.previousModuleSize, ref num1);
        MapInject.Imports.VirtualFreeEx(hProcess, MapInject.previousModuleBase, 0, 32768U);
        MapInject.previousModuleBase = 0;
        MapInject.previousModuleSize = 0;
      }
      MapInject.previousHandle = hProcess;
      byte[] numArray = File.ReadAllBytes(filepath);
      MapInject.IMAGE_DOS_HEADER imageDosHeader = MapInject.toImageDosHeader(numArray, 0);
      if (imageDosHeader.e_magic != (ushort) 23117)
        throw new Exception("Invalid file type");
      MapInject.IMAGE_NT_HEADERS imageNtHeaders = MapInject.toImageNtHeaders(numArray, (int) imageDosHeader.e_lfanew);
      MapInject.IMAGE_OPTIONAL_HEADER optionalHeader = imageNtHeaders.OptionalHeader;
      MapInject.IMAGE_FILE_HEADER fileHeader = imageNtHeaders.FileHeader;
      if (fileHeader.Machine != (ushort) 332)
        throw new Exception("Invalid platform");
      int num2 = MapInject.Imports.VirtualAllocEx(hProcess, 0, 4096, 12288U, 64U);
      int lpBaseAddress = MapInject.Imports.VirtualAllocEx(hProcess, 0, (int) optionalHeader.SizeOfImage, 12288U, 64U);
      MapInject.previousModuleBase = lpBaseAddress != 0 && num2 != 0 ? lpBaseAddress : throw new Exception("Target process memory allocation failed (ex) [Error Code: " + MapInject.Imports.GetLastError().ToString() + "]");
      MapInject.previousModuleSize = (int) optionalHeader.SizeOfImage;
      byte[] lpBuffer1 = new byte[4096];
      Array.Clear((Array) lpBuffer1, 0, lpBuffer1.Length);
      MapInject.Imports.WriteProcessMemory(hProcess, num2, lpBuffer1, lpBuffer1.Length, ref num1);
      byte[] lpBuffer2 = new byte[(int) optionalHeader.SizeOfImage];
      Array.Clear((Array) lpBuffer2, 0, lpBuffer2.Length);
      MapInject.Imports.WriteProcessMemory(hProcess, lpBaseAddress, lpBuffer2, lpBuffer2.Length, ref num1);
      MapInject.MANUAL_MAPPING_DATA manualMappingData1;
      manualMappingData1.pLoadLibraryA = MapInject.Imports.GetProcAddress(MapInject.Imports.GetModuleHandle("KERNEL32.dll"), "LoadLibraryA");
      manualMappingData1.pGetProcAddress = MapInject.Imports.GetProcAddress(MapInject.Imports.GetModuleHandle("KERNEL32.dll"), "GetProcAddress");
      manualMappingData1.pbase = lpBaseAddress;
      manualMappingData1.hMod = 0;
      if (MapInject.Imports.WriteProcessMemory(hProcess, lpBaseAddress, numArray, 4096, ref num1) == 0)
        throw new Exception("Can't write file header [Error Code: " + MapInject.Imports.GetLastError().ToString() + "]");
      int eLfanew = (int) imageDosHeader.e_lfanew;
      int offset = eLfanew + 24 + (int) imageNtHeaders.FileHeader.SizeOfOptionalHeader;
      uint num3 = 0;
      while ((int) num3 != (int) fileHeader.NumberOfSections)
      {
        MapInject.IMAGE_SECTION_HEADER imageSectionHeader = MapInject.toImageSectionHeader(numArray, offset);
        if (imageSectionHeader.SizeOfRawData != 0U)
        {
          byte[] lpBuffer3 = new byte[(int) imageSectionHeader.SizeOfRawData];
          for (int index = 0; index < (int) imageSectionHeader.SizeOfRawData; ++index)
            lpBuffer3[index] = numArray[(long) imageSectionHeader.PointerToRawData + (long) index];
          if (MapInject.Imports.WriteProcessMemory(hProcess, lpBaseAddress + (int) imageSectionHeader.VirtualAddress, lpBuffer3, lpBuffer3.Length, ref num1) == 0)
            throw new Exception("Can't map sections [Error Code: " + MapInject.Imports.GetLastError().ToString() + "]");
        }
        ++num3;
        offset += 40;
      }
      int num4 = MapInject.Imports.VirtualAllocEx(hProcess, 0, 16, 4096U, 4U);
      if (num4 == 0)
        throw new Exception("Target process mapping allocation failed (ex) [Error Code: " + MapInject.Imports.GetLastError().ToString() + "]");
      MapInject.Imports.WriteProcessMemory(hProcess, num4, BitConverter.GetBytes(manualMappingData1.pLoadLibraryA), 4, ref num1);
      MapInject.Imports.WriteProcessMemory(hProcess, num4 + 4, BitConverter.GetBytes(manualMappingData1.pGetProcAddress), 4, ref num1);
      MapInject.Imports.WriteProcessMemory(hProcess, num4 + 8, BitConverter.GetBytes(manualMappingData1.pbase), 4, ref num1);
      MapInject.Imports.WriteProcessMemory(hProcess, num4 + 12, BitConverter.GetBytes(manualMappingData1.hMod), 4, ref num1);
      if (num2 == 0)
        throw new Exception("Memory shellcode allocation failed (ex) [Error Code: " + MapInject.Imports.GetLastError().ToString() + "]");
      byte[] lpBuffer4 = MapInject.makePayload();
      if (MapInject.Imports.WriteProcessMemory(hProcess, num2, lpBuffer4, lpBuffer4.Length, ref num1) == 0)
        throw new Exception("Can't write shellcode [Error Code: " + MapInject.Imports.GetLastError().ToString() + "]");
      int lpThreadId = 0;
      int remoteThread = MapInject.Imports.CreateRemoteThread(hProcess, 0, 0U, num2, num4, 0U, out lpThreadId);
      if (remoteThread == 0 || lpThreadId == 0)
        throw new Exception("Thread creation failed [Error Code: " + MapInject.Imports.GetLastError().ToString() + "]");
      MapInject.Imports.WaitForSingleObjectEx(remoteThread, 1000, true);
      MapInject.Imports.CloseHandle(remoteThread);
      int num5 = 0;
      while (num5 == 0)
      {
        uint lpExitCode = 0;
        MapInject.Imports.GetExitCodeProcess(hProcess, out lpExitCode);
        if (lpExitCode != 259U)
          throw new Exception("Process crashed, exit code: " + lpExitCode.ToString());
        byte[] lpBuffer5 = new byte[16];
        if (MapInject.Imports.ReadProcessMemory(hProcess, num4, lpBuffer5, 16, ref num1) == 0)
          throw new Exception("Failed to read process memory");
        MapInject.MANUAL_MAPPING_DATA manualMappingData2;
        manualMappingData2.pLoadLibraryA = BitConverter.ToInt32(lpBuffer5, 0);
        manualMappingData2.pGetProcAddress = BitConverter.ToInt32(lpBuffer5, 4);
        manualMappingData2.pbase = BitConverter.ToInt32(lpBuffer5, 8);
        manualMappingData2.hMod = BitConverter.ToInt32(lpBuffer5, 12);
        num5 = manualMappingData2.hMod;
        switch (num5)
        {
          case 4210752:
            throw new Exception("Wrong mapping ptr");
          case 6316128:
            throw new Exception("Wrong directory base relocation");
          default:
            Thread.Sleep(10);
            continue;
        }
      }
      byte[] lpBuffer6 = new byte[4096];
      Array.Clear((Array) lpBuffer6, 0, lpBuffer6.Length);
      if (MapInject.Imports.WriteProcessMemory(hProcess, lpBaseAddress, lpBuffer6, 4096, ref num1) == 0)
        throw new Exception("Failed to erase file header(s)");
      byte[] lpBuffer7 = new byte[1048576];
      Array.Clear((Array) lpBuffer7, 0, lpBuffer7.Length);
      int num6 = eLfanew + 24 + (int) imageNtHeaders.FileHeader.SizeOfOptionalHeader;
      uint num7 = 0;
      while ((int) num7 != (int) fileHeader.NumberOfSections)
      {
        MapInject.IMAGE_SECTION_HEADER imageSectionHeader = MapInject.toImageSectionHeader(numArray, num6);
        if (imageSectionHeader.SizeOfRawData != 0U)
        {
          string str = "";
          byte[] lpBuffer8 = new byte[16];
          MapInject.Imports.ReadProcessMemory(hProcess, num6, lpBuffer8, 16, ref num1);
          for (int index = 0; index < 16 && lpBuffer8[index] >= (byte) 32 && lpBuffer8[index] < (byte) 127; ++index)
            str += ((char) lpBuffer8[index]).ToString();
          if ((str == ".pdata" || str == ".rsrc" || str == ".reloc") && MapInject.Imports.WriteProcessMemory(hProcess, lpBaseAddress + (int) imageSectionHeader.VirtualAddress, lpBuffer7, (int) imageSectionHeader.SizeOfRawData, ref num1) == 0)
            throw new Exception("Can't clear section " + str + " [Error code: " + MapInject.Imports.GetLastError().ToString() + "]");
        }
        ++num7;
        num6 += 40;
      }
      return true;
    }

    private class Imports
    {
      public const uint PAGE_NOACCESS = 1;
      public const uint PAGE_READONLY = 2;
      public const uint PAGE_READWRITE = 4;
      public const uint PAGE_WRITECOPY = 8;
      public const uint PAGE_EXECUTE = 16;
      public const uint PAGE_EXECUTE_READ = 32;
      public const uint PAGE_EXECUTE_READWRITE = 64;
      public const uint PAGE_EXECUTE_WRITECOPY = 128;
      public const uint PAGE_GUARD = 256;
      public const uint PAGE_NOCACHE = 512;
      public const uint PAGE_WRITECOMBINE = 1024;
      public const uint MEM_COMMIT = 4096;
      public const uint MEM_RESERVE = 8192;
      public const uint MEM_DECOMMIT = 16384;
      public const uint MEM_RELEASE = 32768;
      public const uint PROCESS_WM_READ = 16;
      public const uint PROCESS_ALL_ACCESS = 2035711;
      public const int EXCEPTION_CONTINUE_EXECUTION = -1;
      public const int EXCEPTION_CONTINUE_SEARCH = 0;

      [DllImport("kernel32.dll")]
      public static extern int OpenProcess(
        uint dwDesiredAccess,
        bool bInheritHandle,
        int dwProcessId);

      [DllImport("kernel32.dll")]
      public static extern int ReadProcessMemory(
        int hProcess,
        int lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        ref int lpNumberOfBytesRead);

      [DllImport("kernel32.dll")]
      public static extern int WriteProcessMemory(
        int hProcess,
        int lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        ref int lpNumberOfBytesWritten);

      [DllImport("kernel32.dll")]
      public static extern int VirtualProtectEx(
        int hProcess,
        int lpBaseAddress,
        int dwSize,
        uint new_protect,
        ref uint lpOldProtect);

      [DllImport("kernel32.dll")]
      public static extern int VirtualQueryEx(
        int hProcess,
        int lpAddress,
        out MapInject.Imports.MEMORY_BASIC_INFORMATION lpBuffer,
        uint dwLength);

      [DllImport("kernel32.dll")]
      public static extern int VirtualAllocEx(
        int hProcess,
        int lpAddress,
        int size,
        uint allocation_type,
        uint protect);

      [DllImport("kernel32.dll")]
      public static extern int VirtualFreeEx(
        int hProcess,
        int lpAddress,
        int size,
        uint allocation_type);

      [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
      public static extern int GetModuleHandle(string lpModuleName);

      [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
      public static extern int GetProcAddress(int hModule, string procName);

      [DllImport("kernel32.dll")]
      public static extern uint GetLastError();

      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern bool CloseHandle(int hObject);

      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.U4)]
      public static extern int WaitForSingleObjectEx(
        int hHandle,
        [MarshalAs(UnmanagedType.U4)] int dwMilliseconds,
        bool bAlertable);

      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetExitCodeProcess(int hProcess, out uint lpExitCode);

      [DllImport("kernel32.dll")]
      public static extern int CreateRemoteThread(
        int hProcess,
        int lpThreadAttributes,
        uint dwStackSize,
        int lpStartAddress,
        int lpParameter,
        uint dwCreationFlags,
        out int lpThreadId);

      public struct MEMORY_BASIC_INFORMATION
      {
        public int BaseAddress;
        public int AllocationBase;
        public uint AllocationProtect;
        public int RegionSize;
        public uint State;
        public uint Protect;
        public uint Type;
      }
    }

    private struct IMAGE_DATA_DIRECTORY
    {
      public uint VirtualAddress;
      public uint Size;
    }

    private struct IMAGE_OPTIONAL_HEADER
    {
      public ushort Magic;
      public byte MajorLinkerVersion;
      public byte MinorLinkerVersion;
      public uint SizeOfCode;
      public uint SizeOfInitializedData;
      public uint SizeOfUninitializedData;
      public uint AddressOfEntryPoint;
      public uint BaseOfCode;
      public uint BaseOfData;
      public uint ImageBase;
      public uint SectionAlignment;
      public uint FileAlignment;
      public ushort MajorOperatingSystemVersion;
      public ushort MinorOperatingSystemVersion;
      public ushort MajorImageVersion;
      public ushort MinorImageVersion;
      public ushort MajorSubsystemVersion;
      public ushort MinorSubsystemVersion;
      public uint Win32VersionValue;
      public uint SizeOfImage;
      public uint SizeOfHeaders;
      public uint CheckSum;
      public ushort Subsystem;
      public ushort DllCharacteristics;
      public uint SizeOfStackReserve;
      public uint SizeOfStackCommit;
      public uint SizeOfHeapReserve;
      public uint SizeOfHeapCommit;
      public uint LoaderFlags;
      public uint NumberOfRvaAndSizes;
      public MapInject.IMAGE_DATA_DIRECTORY[] DataDirectory;
    }

    private struct IMAGE_FILE_HEADER
    {
      public ushort Machine;
      public ushort NumberOfSections;
      public uint TimeDateStamp;
      public uint PointerToSymbolTable;
      public uint NumberOfSymbols;
      public ushort SizeOfOptionalHeader;
      public ushort Characteristics;
    }

    private struct IMAGE_DOS_HEADER
    {
      public ushort e_magic;
      public ushort e_cblp;
      public ushort e_cp;
      public ushort e_crlc;
      public ushort e_cparhdr;
      public ushort e_minalloc;
      public ushort e_maxalloc;
      public ushort e_ss;
      public ushort e_sp;
      public ushort e_csum;
      public ushort e_ip;
      public ushort e_cs;
      public ushort e_lfarlc;
      public ushort e_ovno;
      public ushort[] e_res;
      public ushort e_oemid;
      public ushort e_oeminfo;
      public ushort[] e_res2;
      public uint e_lfanew;
    }

    private struct IMAGE_NT_HEADERS
    {
      public uint Signature;
      public MapInject.IMAGE_FILE_HEADER FileHeader;
      public MapInject.IMAGE_OPTIONAL_HEADER OptionalHeader;
    }

    private struct MANUAL_MAPPING_DATA
    {
      public int pLoadLibraryA;
      public int pGetProcAddress;
      public int pbase;
      public int hMod;
    }

    private struct IMAGE_SECTION_HEADER
    {
      public byte[] Name;
      public uint PhysicalAddressOrVirtualSize;
      public uint VirtualAddress;
      public uint SizeOfRawData;
      public uint PointerToRawData;
      public uint PointerToRelocations;
      public uint PointerToLinenumbers;
      public ushort NumberOfRelocations;
      public ushort NumberOfLinenumbers;
      public uint Characteristics;
    }
  }
}
