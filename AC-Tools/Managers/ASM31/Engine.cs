using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASM31
{
    public class Engine 
    {
        ServerType type { get; set; }                // type of server this is
        ServerView ui { get; set; }                 // view to communicate with
        public bool shutdown { get; set; }
        Thread monThread, inThread, outThread, errorThread, waitThread;
        private DateTime serverStartTime;
        // parameters for use by the process:
        Process proc;                               // the reason tis class exists

        public string args { get; set; }                  // the arguments to the process
        public string startPath { get; set; }            // shere the process will run
        int port;
        string host;
        public Engine(ServerType type, ServerView ui)
        {
            this.type = type;
            this.ui = ui;
            if (type == ServerType.Chat)
            {
                port = Globals.instance.getChatPort();
                host = Globals.instance.getChatHost();
            }
            else if (type == ServerType.Login)
            {
                port = Globals.instance.getLoginPort();
                host = Globals.instance.getLoginHost();
            }
            else // type must be ServerType.Game
            {
                port = Globals.instance.getGamePort();
                host = Globals.instance.getGameHost();
            }

            shutdown = false;
            monThread = new Thread(new ThreadStart(serverMonitor));
            monThread.Start();
        }
        
        // shutdown the server process and all its threads
        public void shutDown()
        {
            shutdown = true;
            try
            {
                if (!proc.HasExited)
                    proc.Kill();
            }
            catch (InvalidOperationException)
            {

            }
            if (inThread != null && inThread.IsAlive)
                inThread.Abort();
            if (outThread != null && inThread.IsAlive)
                outThread.Abort();
            if (errorThread != null && errorThread.IsAlive)
                errorThread.Abort();
        }

        public void start()
        {
            proc = new Process();
            // set up the information needed to start up this process. 
            // first the program to run and its arguments
            proc.StartInfo.FileName = Properties.Settings.Default.javaHome+ "\\bin\\javaw.exe";
            proc.StartInfo.Arguments = args;
            // let the system know we want to redirect the I/O streams
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardInput = true;
            // we are not using the cmd.exe shell to run this.
            proc.StartInfo.UseShellExecute = false;
            // we want the process to b e started in this directory
            proc.StartInfo.WorkingDirectory = startPath;

            // start the process
            proc.Start();

            // once started we can redirect the process I/O as we need to.
            // start threads for dealing with the various process I/O streams 
            inThread = new Thread(() => inputMonitor(proc.StandardInput));
            inThread.Start();
            // normal output appears as standard black text
            outThread = new Thread(() => outputMonitor(proc.StandardOutput));
            outThread.Start();
            // normal output appears as RED text
            errorThread = new Thread(() => errorMonitor(proc.StandardError));
            errorThread.Start();

            // finally a thread to monitor the server process state and restart if conditions direct it to. 
            waitThread = new Thread(() => processWait(proc));
            waitThread.Start();
        }
            
        /*
         *background thread to monitor the state of the associated process. if it detects that 6the parent view exits
         *it will attempt to kill the associated process (if there is one) and will prompotly exit.
         */
        void serverMonitor()
        {
            while (true)
            {
                if (ui.IsDisposed)
                {
                    // try to kill the associated process if there is one and it is running.
                    if (proc != null)
                    {
                        try
                        {
                            if (!proc.HasExited)
                                proc.Kill();
                        }
                        catch (Exception)
                        {
                            // main thing is we tried. if it failed we won't cry
                        }
                    }
                    return;
                }
                if (proc == null)
                {
                    // if the process is null, we haven't attenpted to start it
                    // keep watching until it starts
                    ui.updateState(State.Stopped);
                }
                else
                {
                    try
                    {
                        int rc = proc.ExitCode;
                        // process died. we don't want to stay in this thread
                        ui.updateState(State.Stopped);
                        return;
                    }
                    catch (InvalidOperationException)
                    {
                        // process is running
                        if (ui.state != State.Running)
                        {
                            // check the TCP port to see if it is alive
                            // if port unavailable, set status to running
                            if (NetUtils.isConnected(host, port))
                            {
                                ui.updateState(State.Running);
                                serverStartTime = DateTime.Now;
                                
                            }
                            else
                                ui.updateState(State.Starting);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private void processWait(Process proc)
        {
            proc.WaitForExit();
            int rc = proc.ExitCode;
            shutDown();

            if (rc == 2 && type == ServerType.Game)
                start();   // restart server engine
        }


        public void inputMonitor( StreamWriter writer)
        {
            // need to read this from the console somehow

        }

        public void outputMonitor(StreamReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                ui.updateConsole(line);
            }    
            // need to send this to the console somehow
        }
        public void errorMonitor(StreamReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                ui.updateConsoleErr(line );
            }    
                            // need to send this to the console somehow
        }

        public TimeSpan timeRunning()
        {
            DateTime now = DateTime.Now;
            TimeSpan diff;
            if (ui.state == State.Running)
                diff = now - serverStartTime;
            else
                diff = now - now;
            return diff;
        }
    }
}
