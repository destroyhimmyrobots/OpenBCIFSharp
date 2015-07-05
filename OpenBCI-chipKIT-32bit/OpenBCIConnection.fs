namespace OpenBCI.chipKIT_32bit
module OpenBCIConnection =
    open System.IO.Ports
    open System.Collections.Generic
    open System.Timers
    type OpenBCIReader() = class end
    type OpenBCIConnection(comPort : string) =
        let serialPort = new SerialPort(comPort, 1152000)
        let writeQueue = Queue<char>()
        let writeTimer = new Timer(50.0)
        let writeAction _ _ = 
            match serialPort.IsOpen with
                | true  -> writeQueue.Dequeue |> string |> serialPort.WriteLine
                | false -> ()
        let writeEvent = ElapsedEventHandler(writeAction)
        let MSG_CHAN_ON  = seq {1..8} |> Seq.map string |> String.concat ""
        let MSG_CHAN_OFF = "!@#$%^&*"
        let reader = OpenBCIReader()
        let send = writeQueue.Enqueue
        let channelEnable (n : int) = 
            match 0 < n && n < 8 with 
                | true  -> (>>) char send n
                | false -> ()
        let channelSettings (channel : int)
                            (powerdown : int)
                            (gain : int)
                            (input : int)
                            (bias : int)
                            (srb2 : int)
                            (srb1 : int) =
            List.map send <| 'x' :: (List.map char [channel; powerdown; gain; input; bias; srb2; srb1]) @ ['X']
        do 
            serialPort.Open()
            serialPort.NewLine <- "\n"
            writeTimer.Elapsed.AddHandler(writeEvent)
            writeTimer.Start()
        member this.Interval with set interval = writeTimer.Interval <- interval
        override this.Finalize() = do
            writeTimer.Stop()
            writeTimer.Elapsed.RemoveHandler(writeEvent)
            serialPort.Close()