# GuidGen
This is a tiny tool to replace Inefficient Visual Studio GUIDGen tool


The GuidGen tool included with visual studio is very inefficient and only one of the guid that it creats is usable wiht C# (sql).
It also need at least to extra clicks to get the code required. 

This application however as soon as execution creates a guid and places it in clipboard for use.
if a shortcut is defined for ExternalCommand1 in visual studio (I define Ctrl-Shift-G) then developer will need to press Ctrl-Shift-G and then Ctrl-V  (or Shift-Insert) to get a new Guid in their code.

There are 6 modes of generation available which match all 6 signatures of `new Guid()` these modes can be defined in command line parameters as `mode={mode}`
```console
mode=1: String
    Example: CFB78D94-052F-46F5-87C3-0133A7B280BA
mode=2: Array of 16 hex bytes
    Example: new byte[] { 0x94, 0x8D, 0xB7, 0xCF, 0x2F, 0x05, 0xF5, 0x46, 0x87, 0xC3, 0x01, 0x33, 0xA7, 0xB2, 0x80, 0xBA }

mode=3: 16 bytes array is base 10 numbers
    Example: new byte[] { 148, 141, 183, 207, 47, 5, 245, 70, 135, 195, 1, 51, 167, 178, 128, 186 }

mode=4: int, short , short, byte[8]
    Example: -810054252, 1327, 18165, new byte[] { 135, 195, 1, 51, 167, 178, 128, 186 }

mode=5: int, short, short, byte, byte, byte, byte, byte, byte, byte, byte
    Example: -810054252, 1327, 18165, 135, 195, 1, 51, 167, 178, 128, 186

mode=6: uint, ushort, ushort, byte, byte, byte, byte, byte, byte, byte, byte
    Example: 0xCFB78D94, 0x052F, 0x46F5, 0x87, 0xC3, 0x01, 0x33, 0xA7, 0xB2, 0x80, 0xBA
```

### Notes:
`new Guid()` is not included in clipboard assuming developer has already wrote that part and only needs content of paranthesis.

## Configuring ExternalCommand1 for Visual Studio
1. From menu, click on Tools -> External Tools.
2. Change path to Create &GUID to this application.
3. In Arguments text box include mode=1, mode=2 ..., default is mode=1 which creates string guid that is usable in both SQL and C#
4. Click ok.


### configuring shortcut for ExternalCommand1
1. From menu, click on Tools -> Customize
2. Click on Keyboard button
3. in `Show commands containing` box type ExternalCommand1
4. Select Tools.ExternalCommand1 from list
5. in `Press shortcut keys` box press your desired shortcut (I use Ctrl-Shift-G)
6. Click Assign then Ok then Close.



