using System;
using System.Text;
using System.Windows.Forms;

internal static class Program {

    [STAThread]
    internal static int Main(string[] args) {

        int mode = 1;
        if (args.Length > 0) {
            try {
                mode = Convert.ToInt32(args[0].ToLower().Replace("mode=", ""));
            } catch {
                Console.WriteLine("Creates a new GUID and copies it to clipboard");
                Console.WriteLine("Usage:");
                Console.WriteLine("    GuidGen [mode=1~6]");
                Console.WriteLine();
                Console.WriteLine("    mode is optional number between 1 to 6 to create guid in different formats as following");
                Console.WriteLine("      1: String");
                Console.WriteLine("             Example: {CFB78D94-052F-46F5-87C3-0133A7B280BA}");
                Console.WriteLine();
                Console.WriteLine("      2: Array of 16 hex bytes");
                Console.WriteLine("             Example: new byte[] { 0x94, 0x8D, 0xB7, 0xCF, 0x2F, 0x05, 0xF5, 0x46, 0x87, 0xC3, 0x01, 0x33, 0xA7, 0xB2, 0x80, 0xBA }");
                Console.WriteLine();
                Console.WriteLine("      3: 16 bytes array is base 10 numbers");
                Console.WriteLine("             Example: new byte[] { 148, 141, 183, 207, 47, 5, 245, 70, 135, 195, 1, 51, 167, 178, 128, 186 }");
                Console.WriteLine();
                Console.WriteLine("      4: int, short , short, byte[8]");
                Console.WriteLine("             Example: -810054252, 1327, 18165, new byte[] { 135, 195, 1, 51, 167, 178, 128, 186 }");
                Console.WriteLine();
                Console.WriteLine("      5: int, short, short, byte, byte, byte, byte, byte, byte, byte, byte");
                Console.WriteLine("             Example: -810054252, 1327, 18165, 135, 195, 1, 51, 167, 178, 128, 186");
                Console.WriteLine();
                Console.WriteLine("      6: uint, ushort, ushort, byte, byte, byte, byte, byte, byte, byte, byte");
                Console.WriteLine("             Example: 0xCFB78D94, 0x052F, 0x46F5, 0x87, 0xC3, 0x01, 0x33, 0xA7, 0xB2, 0x80, 0xBA");
            }
        }
        Clipboard.SetText(GenerateGuidText(Guid.NewGuid(), mode));
        return 0;
    }

    internal static string GenerateGuidText(Guid guid, int mode) {
        StringBuilder sb = new StringBuilder();
        byte[] bytearr = guid.ToByteArray();

        switch (mode) {
            default:
            case 1:
                sb.Append(guid.ToString().ToLower());
                break;

            case 2:
                sb.Append("new byte[]{ ");
                foreach (byte b in bytearr) {
                    sb.Append("0x" + b.ToString("X2") + ", ");
                }
                sb.Append("}");
                return sb.ToString().Replace(", }", " }");

            case 3:
                //Mode 3
                sb.Append("new byte[]{ ");
                foreach (byte b in bytearr) {
                    sb.Append(b + ", ");
                }
                sb.Append("}");
                return sb.ToString().Replace(", }", " }");

            case 4:
                sb.Append(BitConverter.ToInt32(bytearr, 0) + ", ");
                sb.Append(BitConverter.ToInt16(bytearr, 4) + ", ");
                sb.Append(BitConverter.ToInt16(bytearr, 6) + ", ");
                sb.Append("new byte[]{ ");
                for (int i = 8;i < bytearr.Length;i++) {
                    sb.Append(bytearr[i] + ", ");
                }
                sb.Append("}");
                return sb.ToString().Replace(", }", " }");

            case 5:
                sb.Append(BitConverter.ToInt32(bytearr, 0) + ", ");
                sb.Append(BitConverter.ToInt16(bytearr, 4) + ", ");
                sb.Append(BitConverter.ToInt16(bytearr, 6) + ", ");
                for (int i = 8;i < bytearr.Length;i++) {
                    sb.Append(bytearr[i] + ", ");
                }
                return sb.ToString().TrimEnd(' ', ',');

            case 6:
                sb.Append("0x" + BitConverter.ToUInt32(bytearr, 0).ToString("X8") + ", ");
                sb.Append("0x" + BitConverter.ToUInt16(bytearr, 4).ToString("X4") + ", ");
                sb.Append("0x" + BitConverter.ToUInt16(bytearr, 6).ToString("X4") + ", ");
                for (int i = 8;i < bytearr.Length;i++) {
                    sb.Append("0x" + bytearr[i].ToString("X2") + ", ");
                }
                return sb.ToString().TrimEnd(' ', ',');
        }

        Guid guid1 = new Guid("{CFB78D94-052F-46F5-87C3-0133A7B280BA}");
        Guid guid2 = new Guid(new byte[] { 0x94, 0x8D, 0xB7, 0xCF, 0x2F, 0x05, 0xF5, 0x46, 0x87, 0xC3, 0x01, 0x33, 0xA7, 0xB2, 0x80, 0xBA });
        Guid guid3 = new Guid(new byte[] { 148, 141, 183, 207, 47, 5, 245, 70, 135, 195, 1, 51, 167, 178, 128, 186 });
        Guid guid4 = new Guid(-810054252, 1327, 18165, new byte[] { 135, 195, 1, 51, 167, 178, 128, 186 });
        Guid guid5 = new Guid(-810054252, 1327, 18165, 135, 195, 1, 51, 167, 178, 128, 186);
        Guid guid6 = new Guid(0xCFB78D94, 0x052F, 0x46F5, 0x87, 0xC3, 0x01, 0x33, 0xA7, 0xB2, 0x80, 0xBA);

        if (guid1 != guid2)
            throw new Exception("");
        if (guid1 != guid3)
            throw new Exception("");
        if (guid1 != guid4)
            throw new Exception("");
        if (guid1 != guid5)
            throw new Exception("");
        if (guid1 != guid6)
            throw new Exception("");

        return sb.ToString().Replace(", }", "}").TrimEnd(' ', ',');
    }
}
