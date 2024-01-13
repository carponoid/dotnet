using System.Text;
using pkb.winutils;
using Microsoft.Extensions.Primitives;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;

namespace pkb.winutils;

[ApiController]
// [EnableCors]
public class WinHostController : ControllerBase
{

    [HttpGet]
    [Route("/service/list")]
    public IEnumerable<ServiceEntry> GetAllServices()
    {
        return WindowsServiceHandler.GetAllWinService();
    }

    [HttpGet]
    [Route("/service/list/{partialServiceId}")]
    public IEnumerable<ServiceEntry> GetMathingWindowsServices(String partialServiceId)
    {
        return WindowsServiceHandler.GetMatchingWinService(partialServiceId);
    }

    [HttpPost]
    [Route("/service/search")]
    public IEnumerable<ServiceEntry> GetAllMathingWindowsServices([FromBody] ServiceId serviceId)
    {
        return WindowsServiceHandler.GetAllMatchingWinService(serviceId);
    }

    [HttpPost]
    [Route("/service/start/{ServiceID}")]
    public String StartService(String serviceId)
    {
        return WindowsServiceHandler.StartWinService(serviceId);
    }

    [HttpPost]
    [Route("/service/startAll")]
    public Result StartService([FromBody] ServiceId serviceId)
    {
        return WindowsServiceHandler.StartAllWinService(serviceId);
    }

    [HttpPost]
    [Route("/service/stop/{ServiceID}")]
    public String StopService(String serviceId)
    {
        return WindowsServiceHandler.StopWinService(serviceId);
    }

    [HttpPost]
    [Route("/service/stopAll")]
    public Result StopService([FromBody] ServiceId serviceId)
    {
        return WindowsServiceHandler.StopAllWinService(serviceId);
    }

    [HttpGet]
    [Route("/info")]
    public String GetRequestInfo()
    {
        // var context = Request.Properties["MS_HttpContext"] as HttpContext;
        ImmutableList<KeyValuePair<String, StringValues>> headersList = [.. Request.Headers];
        StringBuilder builderStr = new();
        foreach (KeyValuePair<String, StringValues> pair in headersList)
        {
            builderStr.AppendLine(pair.Key.ToString() + "," + pair.Value.ToString());
        }
        builderStr.AppendLine("====================================");
        builderStr.AppendLine(Request.Path.ToString());
        return builderStr.ToString();
    }

    [HttpPost]
    [Route("/process/list")]
    public IEnumerable<ProcessEntry> getAllProcesses()
    {
        return WinProcessHandler.GetAllProcesses();
    }

    [HttpPost]
    [Route("/process/search")]
    public IEnumerable<ProcessEntry> SearchForProcesses([FromBody] ProcessNames Names)
    {
        List<ProcessEntry> results = [];
        foreach (String name in Names.Names)
        {
            results.AddRange(WinProcessHandler.SearchProcessByName(name));
        }
        return results;
    }

    [HttpPost]
    [Route("/process/retrieve")]
    public ProcessEntry GetProcessById([FromBody] int ID)
    {
        return WinProcessHandler.GetProcessById(ID);
    }

    [HttpPost]
    [Route("/process/kill")]
    public String KillProcess([FromBody] int ID)
    {
        return WinProcessHandler.KillProcess(ID);
    }

    [HttpPost]
    [Route("/process/killall")]
    public IEnumerable<String> KillAllProcess([FromBody] ProcessNames Names)
    {
        List<String> results = [];
        foreach (String name in Names.Names)
        {
            results.AddRange(WinProcessHandler.KillAllProcess(name));
        }
        return results;
    }

    [HttpPost]
    [Route("/process/start")]
    public ProcessEntry StartProcess([FromBody] String pathToExe)
    {
        return WinProcessHandler.StartProcess(pathToExe);
    }

    [HttpGet]
    [Route("/refreshplex")]
    public ProcessEntry RefreshPlex()
    {
        WinProcessHandler.KillAllProcess("Plex Media Server");
        WinProcessHandler.KillAllProcess("Plex Tuner Service");
        WinProcessHandler.KillAllProcess("PlexScriptHost");
        return WinProcessHandler.StartProcess("C:/Program Files/Plex/Plex Media Server/Plex Media Server.exe");
    }

    [HttpGet]
    [Route("/refreshArr")]
    public Result RefreshArr()
    {
        ServiceId arrServices = new(["ProtonVPN Service",
                                    "ProtonVPN WireGuard",
                                    "Prowlarr",
                                    "Qbit"]);
        Result res = WindowsServiceHandler.StopAllWinService(arrServices);
        res = WindowsServiceHandler.StartAllWinService(arrServices);
        return res;
    }

}