namespace pkb.winutils;

public record ServiceId(String[] Id);

public record ServiceEntry(String ServiceName, String ServiceStatus, String ServiceDisplayName);

public record Result(List<string> Success, List<String> Failiures);

public record ProcessEntry(String ProcessName, int PorcessId);

public record ProcessNames(String[] Names);
