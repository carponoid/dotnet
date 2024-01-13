using System;
using System.Diagnostics;
using System.ComponentModel;

namespace pkb.winutils;

[TestClass]
public class WinProcessHandlerTest
{
    [TestMethod]
    public void TestMethod1()
    {
      Debug.WriteLine("Process Start >>>>>>>>>>>>>><<<<<<<<<<<<< ");
      foreach (ProcessEntry process in  WinProcessHandler.SearchProcessByName("Device Association Framework Provider Host") ) {
        Debug.WriteLine("Process " + process.ProcessName+" id " + process.PorcessId);
      }
      Debug.WriteLine("Process End <<<<<<<<<<<<<<<>>>>>>>>>>>>>>> ");
       
    }
}