using System.ComponentModel;
using System.ServiceProcess;
using System.Security.Principal;

namespace pkb.winutils;


public class WinServiceController : Component
{

#pragma warning disable CA1416 // Validate platform compatibility

    public static ServiceEntry[] GetMatchingWinService(String ID)
    {
        ServiceController[] scServices = ServiceController.GetServices();
        List<ServiceEntry> services = [];
        foreach (ServiceController scTemp in scServices)
        {
            if (scTemp.ServiceName.StartsWith(ID))
            {
                services.Add(new ServiceEntry(scTemp.ServiceName, scTemp.Status.ToString(), scTemp.DisplayName));
            }
        }
        return [.. services];
    }

    public static ServiceEntry[] GetAllWinService()
    {
        ServiceController[] scServices = ServiceController.GetServices();
        List<ServiceEntry> services = [];
        foreach (ServiceController scTemp in scServices)
        {
            services.Add(new ServiceEntry(scTemp.ServiceName, scTemp.Status.ToString(), scTemp.DisplayName));
        }
        return [.. services];
    }

    public static String StopWinService(String ID)
    {
        ServiceController[] scServices = ServiceController.GetServices();
        foreach (ServiceController scTemp in scServices)
        {
            if (!scTemp.ServiceName.StartsWith(ID)) continue;
            if (scTemp.Status == ServiceControllerStatus.Stopped || scTemp.Status == ServiceControllerStatus.StopPending)
            {
                return "Service already stopped";
            }
            else if (scTemp.CanStop)
            {
                scTemp.Stop();
                return "Service stopped successfully";
            }
            else
            {
                return "Service cannot be stopped";
            }
        }
        return "Service not found";

    }


    public static String StartWinService(String ID)
    {
        ServiceController[] scServices = ServiceController.GetServices();
        foreach (ServiceController scTemp in scServices)
        {
            if (!scTemp.ServiceName.StartsWith(ID)) continue;
            if (scTemp.Status == ServiceControllerStatus.Running || scTemp.Status == ServiceControllerStatus.StartPending)
            {
                return "Service aleady started";
            }
            scTemp.Start();
            return "Service started successfully";
        }
        return "Service not found";
    }
}
#pragma warning restore CA1416 // Validate platform compatibility

public record ServiceEntry(String ServiceName, String ServiceStatus, String ServiceDisplayName);