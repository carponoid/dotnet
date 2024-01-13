using System;
using System.Diagnostics;
using System.ComponentModel;

namespace pkb.winutils;
public class WinProcessHandler
{

    public static ProcessEntry[] SearchProcessByName(String ProcessName) {
        List<ProcessEntry> result = [];
        foreach (Process process  in Process.GetProcessesByName(ProcessName) ) {
            result.Add(new ProcessEntry(process.ProcessName, process.Id));
        }

        return [.. result];
    }

    public static ProcessEntry[] GetAllProcesses() {
        List<ProcessEntry> result = [];
        foreach (Process process  in Process.GetProcesses() ) {
            result.Add(new ProcessEntry(process.ProcessName, process.Id));
        }

        return [.. result];
    }

    public static ProcessEntry GetProcessById(int ProcessId) {
        Process process = Process.GetProcessById(ProcessId);

        return new ProcessEntry(process.ProcessName, process.Id);
    }

    public static String KillProcess(int ProcessId) {
        Process.GetProcessById(ProcessId).Kill();
        return "Process "+ProcessId+" was killed";
    }

    public static List<String> KillAllProcess(String ProcessName) {
        List<String> result = [];
        foreach (Process process  in Process.GetProcessesByName(ProcessName) ) {
            process.Kill();
            result.Add("Process "+process.Id+" was killed");
        }
        return result;
    }

    public static ProcessEntry StartProcess(String PathToExe) {
        Process process = Process.Start(@PathToExe);
        return new ProcessEntry(process.ProcessName, process.Id);
    }

}
