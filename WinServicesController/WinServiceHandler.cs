using System.ComponentModel;
using System.ServiceProcess;

namespace pkb.winutils;

public class WindowsServiceHandler : Component
{

#pragma warning disable CA1416 // Validate platform compatibility

    public static ServiceEntry[] GetMatchingWinService(String ID)
    {
        List<ServiceEntry> services = [];
        ServiceController[] scServices = ServiceController.GetServices();
        foreach (ServiceController scTemp in scServices)
        {
            if (scTemp.ServiceName.StartsWith(ID))
            {
                services.Add(new ServiceEntry(scTemp.ServiceName, scTemp.Status.ToString(), scTemp.DisplayName));
            }
        }
        return [.. services];
    }

    public static ServiceEntry[] GetAllMatchingWinService(ServiceId ID)
    {
        List<ServiceEntry> services = [];
        foreach (String serviceId in ID.Id)
        {
            services.AddRange(GetMatchingWinService(serviceId));
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

    public static Result StopAllWinService(ServiceId Id)
    {
        List<String> success = [];
        List<String> failiures = [];
        foreach (String serviceId in Id.Id)
        {
            String res = StopWinService(serviceId);
            if (res.Contains("successfully"))
            {
                success.Add(serviceId);
            }
            else
            {
                failiures.Add(serviceId);
            }
        }
        return new Result(success, failiures);
    }

    public static Result StartAllWinService(ServiceId Id)
    {
        List<String> success = [];
        List<String> failiures = [];
        foreach (String serviceId in Id.Id)
        {
            String res = StartWinService(serviceId);
            if (res.Contains("successfully"))
            {
                success.Add(serviceId);
            }
            else
            {
                failiures.Add(serviceId);
            }
        }

        return new Result(success, failiures);
    }


}
#pragma warning restore CA1416 // Validate platform compatibility

