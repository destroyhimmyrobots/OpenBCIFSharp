namespace OpenBCI.chipKIT_32bit
module OpenBCIPacket =
    open System
    type OpenBCIPacket  (magic  : byte // Byte 1:      0xA0
                        ,count  : byte // Byte 2:      Sample Number
                        ,eeg10  : byte // Bytes 3-5:   Data value for EEG channel 1
                        ,eeg11  : byte // Note: values are 24-bit signed, MSB first (big-endian)
                        ,eeg12  : byte // Must marshal into 32-bit signed native-endian
                        ,eeg20  : byte // Bytes 6-8:   Data value for EEG channel 2
                        ,eeg21  : byte
                        ,eeg22  : byte
                        ,eeg30  : byte // Bytes 9-11:  Data value for EEG channel 3
                        ,eeg31  : byte
                        ,eeg32  : byte
                        ,eeg40  : byte // Bytes 12-14: Data value for EEG channel 4
                        ,eeg41  : byte
                        ,eeg42  : byte
                        ,eeg50  : byte // Bytes 15-17: Data value for EEG channel 5
                        ,eeg51  : byte
                        ,eeg52  : byte
                        ,eeg60  : byte // Bytes 18-20: Data value for EEG channel 6
                        ,eeg61  : byte
                        ,eeg62  : byte
                        ,eeg70  : byte // Bytes 21-23: Data value for EEG channel 6
                        ,eeg71  : byte
                        ,eeg72  : byte
                        ,eeg80  : byte // Bytes 24-26: Data value for EEG channel 8
                        ,eeg81  : byte
                        ,eeg82  : byte
                        ,accX0  : byte // Bytes 27-28: Data value for accelerometer channel X
                        ,accX1  : byte // Note: values are 16-bit signed, MSB first (big-endian)
                        ,accY0  : byte // Bytes 29-30: Data value for accelerometer channel Y
                        ,accY1  : byte
                        ,accZ0  : byte // Bytes 31-32: Data value for accelerometer channel Z
                        ,accZ1  : byte
                        ,footer : byte) = // Byte 33: 0xC0
            let Int24toInt32Native  (byte0 : byte) // MSB
                                    (byte1 : byte)
                                    (byte2 : byte) =
                match BitConverter.IsLittleEndian with
                    | true  -> BitConverter.ToInt32([|byte2 ; byte1; byte0; 0x00uy|], 0)
                    | false -> BitConverter.ToInt32([|0x00uy; byte0; byte1; byte2 |], 0)
            let Int16toInt32Native  (byte0 : byte) // MSB
                                    (byte1 : byte) =
                match BitConverter.IsLittleEndian with
                    | true  -> BitConverter.ToInt32([|byte1 ; byte0 ; 0x00uy; 0x00uy|], 0)
                    | false -> BitConverter.ToInt32([|0x00uy; 0x00uy; byte0 ; byte1 |], 0)
            new() = OpenBCIPacket   (0xA0uy ,   0uy
                                    ,   0uy ,   0uy ,   0uy // 1
                                    ,   0uy ,   0uy ,   0uy
                                    ,   0uy ,   0uy ,   0uy
                                    ,   0uy ,   0uy ,   0uy // 4
                                    ,   0uy ,   0uy ,   0uy
                                    ,   0uy ,   0uy ,   0uy
                                    ,   0uy ,   0uy ,   0uy
                                    ,   0uy ,   0uy ,   0uy // 8
                                    ,   0uy ,   0uy // X
                                    ,   0uy ,   0uy // Y
                                    ,   0uy ,   0uy // Z
                                    , 0xC0uy)
            member this.X = "F#"
